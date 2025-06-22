using AutoMapper;
using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Abstractions.Services;
using Intravision.Vending.Core.DTO.Product;
using Intravision.Vending.Core.Models;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace Intravision.Vending.Core.Services;

/// <summary>
/// Сервис для управления товарами и их изображениями.
/// Реализует бизнес-логику получения, фильтрации и импорта данных о товарах.
/// </summary>
public class ProductService : IProductService
{
    /// <summary>
    /// Логгер для записи действий и ошибок, связанных с товарами.
    /// </summary>
    private readonly ILogger<ProductService> _logger;

    /// <summary>
    /// Репозиторий для доступа к данным товаров.
    /// </summary>
    private readonly IProductRepository _products;

    /// <summary>
    /// Репозиторий изображений товаров.
    /// </summary>
    private readonly IProductImageRepository _images;

    /// <summary>
    /// Репозиторий брендов.
    /// </summary>
    private readonly IBrandRepository _brandRepository;

    /// <summary>
    /// AutoMapper для преобразования сущностей в DTO и обратно.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Создаёт экземпляр сервиса товаров.
    /// </summary>
    /// <param name="logger">Логгер для фиксации событий и ошибок.</param>
    /// <param name="products">Репозиторий для работы с товарами.</param>
    /// <param name="images">Репозиторий изображений товаров.</param>
    /// <param name="mapper">AutoMapper для DTO-маппинга.</param>
    /// <param name="brands">Репозиторий брендов.</param>
    public ProductService(
        ILogger<ProductService> logger,
        IProductRepository products,
        IProductImageRepository images,
        IMapper mapper,
        IBrandRepository brands)
    {
        _logger = logger;
        _products = products;
        _images = images;
        _mapper = mapper;
        _brandRepository = brands;
    }

    public async Task<PriceRangeDto> GetPriceRange(Guid? brandId, CancellationToken token = default)
    {
        return await _products.GetPriceRangeAsync(brandId, token);
    }

    public async Task<IEnumerable<ProductGetResponse>> GetProducts(
        Guid? brandId,
        int? minPrice,
        int? maxPrice,
        CancellationToken token = default)
    {
        IEnumerable<Product> products = await _products.GetFilteredAsync(brandId, minPrice, maxPrice, token);
        IEnumerable<ProductGetResponse> responses = _mapper.Map<IEnumerable<ProductGetResponse>>(products);
        foreach (ProductGetResponse response in responses)
        {
            IEnumerable<ProductImage> images = await _images.GetByProductIdAsync(response.Id, token);
            response.Images = _mapper.Map<IEnumerable<ProductImageGetResponse>>(images);
        }
        return responses;
    }

    public async Task<bool> ImportFromExcel(Stream fileStream, CancellationToken token = default)
    {
        try
        {
            var requests = ReadExcelFile(fileStream);

            var brandMap = await ReadAndPrepareBrandsAsync(requests, token);
            var (productsToUpsert, imagesToCreate) = await BuildProductsAndImagesAsync(requests, brandMap, token);
            await UpsertProductsAndImagesAsync(productsToUpsert, imagesToCreate, token);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical($"Ошибка при импорте продуктов. Подробнее: {ex.Message}");
            return false;
        }
    }

    private async Task<Dictionary<string, Brand>> ReadAndPrepareBrandsAsync(
        IEnumerable<ProductImportRequest> requests,
        CancellationToken token)
    {
        var brandNames = requests
            .Select(r => r.Brand)
            .Where(b => !string.IsNullOrWhiteSpace(b))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var existingBrands = await _brandRepository.FindAsync(b => brandNames.Contains(b.Name));
        var brandMap = existingBrands.ToDictionary(b => b.Name, StringComparer.OrdinalIgnoreCase);

        foreach (var brandName in brandNames)
        {
            if (!brandMap.ContainsKey(brandName))
            {
                var newBrand = new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = brandName
                };
                await _brandRepository.CreateAsync(newBrand, token);
                brandMap[brandName] = newBrand;
            }
        }

        await _brandRepository.SaveChangesAsync(token);
        return brandMap;
    }

    private async Task<(List<Product>, List<ProductImage>)> BuildProductsAndImagesAsync(
        IEnumerable<ProductImportRequest> requests,
        Dictionary<string, Brand> brandMap,
        CancellationToken token)
    {
        var resultProducts = new List<Product>();
        var resultImages = new List<ProductImage>();

        foreach (var request in requests)
        {
            var brand = brandMap[request.Brand];

            var existingProducts = await _products.FindAsync(p =>
                p.Name == request.Name && p.BrandId == brand.Id);
            var existingProduct = existingProducts.FirstOrDefault();

            if (existingProduct != null)
            {
                existingProduct.QuantityInStock += request.Quantity;
                existingProduct.Price = request.Price;
                resultProducts.Add(existingProduct);
            }
            else
            {
                var newProduct = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    QuantityInStock = request.Quantity,
                    BrandId = brand.Id
                };
                resultProducts.Add(newProduct);
            }

            resultImages.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                Product = existingProduct ?? resultProducts.Last(),
                ImageUrl = request.ImageUrl
            });
        }

        return (resultProducts, resultImages);
    }

    private async Task UpsertProductsAndImagesAsync(
        List<Product> products,
        List<ProductImage> images,
        CancellationToken token)
    {
        foreach (var product in products)
        {
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
                await _products.CreateAsync(product, token);
            }
            else
                await _products.UpdateAsync(product);
        }

        await _products.SaveChangesAsync(token);

        foreach (var image in images)
            await _images.CreateAsync(image, token);

        await _images.SaveChangesAsync(token);
    }


    private static IEnumerable<ProductImportRequest> ReadExcelFile(Stream fileStream)
    {
        EPPlusLicense ePPlusLicense = new EPPlusLicense();
        ePPlusLicense.SetNonCommercialPersonal("User Test");

        using var package = new ExcelPackage(fileStream);

        ExcelWorksheet? worksheet = package.Workbook.Worksheets.FirstOrDefault();

        if (worksheet is null)
            throw new ArgumentNullException(nameof(worksheet));

        int rows = worksheet.Dimension.Rows;
        List<ProductImportRequest> products = new();

        for (int i = 2; i <= rows; i++)
        {
            string? name = worksheet.Cells[i, 1].Text?.Trim();
            string? priceText = worksheet.Cells[i, 2].Text;
            string? quantityText = worksheet.Cells[i, 3].Text;
            string? brand = worksheet.Cells[i, 4].Text?.Trim();
            string? imageUrl = worksheet.Cells[i, 5].Text?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"[Строка {i}] Наименование обязательно для заполнения.", nameof(name));

            if (!int.TryParse(priceText, out int price))
                throw new ArgumentException($"[Строка {i}] Цена должна быть целым числом.", nameof(price));

            if (!int.TryParse(quantityText, out int quantity))
                throw new ArgumentException($"[Строка {i}] Количество должно быть целым числом.", nameof(quantity));

            if (quantity <= 0)
                throw new ArgumentException($"[Строка {i}] Количество должно быть больше нуля.", nameof(quantity));

            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException($"[Строка {i}] Бренд обязателен для заполнения.", nameof(brand));

            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ArgumentException($"[Строка {i}] Ссылка на изображение обязательна для заполнения.", nameof(imageUrl));



            var product = new ProductImportRequest(name, price, quantity, brand, imageUrl);
            products.Add(product);
        }

        return products;
    }
}