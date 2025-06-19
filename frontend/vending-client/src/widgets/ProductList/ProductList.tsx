import { useCallback, useEffect, useMemo } from 'react';
import type { Product } from '../../entities/Product/types';
import { useAppDispatch, useAppSelector } from '../../shared/lib/hooks';
import { fetchProducts } from '../../features/products/productsSlice';
import { ProductCard } from '../../shared/ui/ProductCard/ProductCard';
import type { RootState } from '../../app/store';
import Grid from '@mui/material/Grid';

export const ProductList = () => {
    const dispatch = useAppDispatch();
    const { items, loading, error } = useAppSelector((state: RootState) => state.products);
    const { selectedBrand, priceRange } = useAppSelector((state: RootState) => state.filters);

    // Получаем корзину из localStorage
    const cart: (Product & { quantity: number })[] = useMemo(() => {
        try {
            return JSON.parse(localStorage.getItem('cart') || '[]');
        } catch {
            return [];
        }
    }, []); // убрана лишняя зависимость

    // Проверка, выбран ли товар
    const isProductSelected = useCallback(
        (productId: string) => cart.some((item) => item.id === productId),
        [cart]
    );

    // Обработчик выбора товара
    const handleSelect = useCallback((product: Product) => {
        let newCart: (Product & { quantity: number })[] = [];
        const existing = cart.find((item) => item.id === product.id);
        if (!existing) {
            newCart = [...cart, { ...product, quantity: 1 }];
        } else {
            newCart = cart.map((item) =>
                item.id === product.id ? { ...item, quantity: item.quantity + 1 } : item
            );
        }
        localStorage.setItem('cart', JSON.stringify(newCart));
        window.dispatchEvent(new Event('storage'));
    }, [cart]);

    useEffect(() => {
        dispatch(
            fetchProducts({
                brand: selectedBrand || undefined,
                minPrice: priceRange[0],
                maxPrice: priceRange[1],
            })
        );
    }, [dispatch, selectedBrand, priceRange]);

    if (loading) return <div>Загрузка...</div>;
    if (error) return <div>Ошибка: {error}</div>;

    return (
        <Grid container spacing={2}>
            {items.map((product: Product) => (
                <Grid key={product.id} size={{ xs: 12, sm: 4, md: 3 }}>
                    <ProductCard
                        product={product}
                        isSelected={isProductSelected(product.id)}
                        onSelect={handleSelect}
                    />
                </Grid>
            ))}
        </Grid>
    );
};
