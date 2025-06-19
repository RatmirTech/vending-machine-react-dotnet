export interface Product {
    id: string;
    name: string;
    price: number;
    quantityInStock: number;
    images?: ProductImageGetResponse[];
}

export interface ProductImageGetResponse {
    imageUrl: string;
}

export interface PriceRangeResponse {
    minPrice: number;
    maxPrice: number;
}