import { useCallback, useEffect } from 'react';
import type { Product } from '../../entities/Product/types';
import { useAppDispatch, useAppSelector } from '../../shared/lib/hooks';
import { fetchProducts } from '../../features/products/productsSlice';
import { toggleProduct } from '../../features/cart/cartSlice';
import { ProductCard } from '../../shared/ui/ProductCard/ProductCard';
import type { RootState } from '../../app/store';
import { Box, Skeleton, Card, CardContent } from '@mui/material';

export const ProductList = () => {
    const dispatch = useAppDispatch();
    const { items, loading, error } = useAppSelector((state: RootState) => state.products);
    const { selectedBrand, priceRange } = useAppSelector((state: RootState) => state.filters);
    const cartItems = useAppSelector((state: RootState) => state.cart.items);

    const isProductSelected = useCallback(
        (productId: string) => cartItems.some(item => item.id === productId),
        [cartItems]
    );

    const handleSelect = useCallback((product: Product) => {
        dispatch(toggleProduct(product));
    }, [dispatch]);

    useEffect(() => {
        dispatch(
            fetchProducts({
                brand: selectedBrand || undefined,
                minPrice: priceRange[0],
                maxPrice: priceRange[1],
            })
        );
    }, [dispatch, selectedBrand, priceRange]);

    const skeletonArray = Array.from({ length: 8 });

    const renderSkeletonCard = (_: unknown, index: number) => (
        <Card key={index} sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
            <Skeleton variant="rectangular" height={200} animation="wave" />
            <CardContent sx={{ flexGrow: 1 }}>
                <Skeleton variant="text" height={30} width="80%" />
                <Skeleton variant="text" height={24} width="40%" />
                <Skeleton variant="rectangular" height={36} sx={{ mt: 2 }} />
            </CardContent>
        </Card>
    );

    return (
        <Box sx={{
            display: 'grid',
            gap: 2,
            gridTemplateColumns: {
                xs: '1fr',
                sm: 'repeat(2, 1fr)',
                md: 'repeat(3, 1fr)',
                lg: 'repeat(4, 1fr)'
            }
        }}>
            {loading
                ? skeletonArray.map(renderSkeletonCard)
                : items.map((product: Product) => (
                    <ProductCard
                        key={product.id}
                        product={product}
                        isSelected={isProductSelected(product.id)}
                        onSelect={handleSelect}
                    />
                ))
            }
        </Box>
    );
};
