import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../../shared/lib/hooks';
import { fetchBrands, fetchPriceRange, setSelectedBrand, setPriceRange } from '../../features/filters/filtersSlice';
import { selectCartCount } from '../../features/cart/cartSlice';
import { Select } from '../../shared/ui/Select/Select';
import { SliderRange } from '../../shared/ui/Slider/Slider';
import { CartActions } from '../CartActions/CartActions';
import type { RootState } from '../../app/store';
import { Grid, Typography } from '@mui/material';
import { ImportProductButton } from '../../shared/ui/Buttons/ImportProductButton';


export const Filters = () => {
    const { brands, selectedBrand, priceRange, minPrice, maxPrice } = useAppSelector((state: RootState) => state.filters);
    const cartCount = useAppSelector(selectCartCount);
    const dispatch = useAppDispatch();

    useEffect(() => {
        dispatch(fetchBrands());
    }, [dispatch]);

    useEffect(() => {
        dispatch(fetchPriceRange({ brandId: selectedBrand || undefined }));
    }, [dispatch, selectedBrand]);

    return (
        <Grid
            container
            spacing={{ xs: 2, md: 3 }}
            columns={{ xs: 4, sm: 8, md: 12 }}
            alignItems="center"
            justifyContent="space-between"
        >
            <Grid size={{ xs: 6, md: 8 }}>
                <Typography variant="h4">
                    Газированные напитки
                </Typography>
            </Grid>

            <Grid  sx={{ display: 'flex', justifyContent: 'flex-end' }} 
                size={{ xs: 12, sm: 6, md: 3 }}>
                <ImportProductButton />
            </Grid>

            <Grid size={{ xs: 12, sm: 6, md: 4 }} sx={{ display: 'flex', alignItems: 'center' }}>
                <Select
                    label="Выберите бренд"
                    options={brands.map((b: { id: string; name: string }) => ({ value: b.id, label: b.name }))}
                    value={selectedBrand}
                    onChange={(value: string | null) => dispatch(setSelectedBrand(value))}
                />
            </Grid>
            <Grid size={{ xs: 12, sm: 6, md: 4 }} sx={{ display: 'flex', alignItems: 'center' }}>
                <SliderRange
                    label="Стоимость"
                    min={minPrice}
                    max={maxPrice}
                    value={priceRange}
                    onChange={(range: [number, number]) => dispatch(setPriceRange(range))}
                />
            </Grid>
            <Grid size={{ xs: 12, sm: 6, md: 3 }} sx={{ display: 'flex', justifyContent: 'flex-end' }}>       
                <CartActions selectedCount={cartCount} />
            </Grid>
        </Grid>
    );
};
