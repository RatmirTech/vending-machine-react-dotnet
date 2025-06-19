import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../../shared/lib/hooks';
import { fetchBrands, fetchPriceRange, setSelectedBrand, setPriceRange } from '../../features/filters/filtersSlice';
import { Select } from '../../shared/ui/Select/Select';
import { SliderRange } from '../../shared/ui/Slider/Slider';
import type { RootState } from '../../app/store';
import { Grid } from '@mui/material';

export const Filters = () => {
    const { brands, selectedBrand, priceRange, minPrice, maxPrice } = useAppSelector((state: RootState) => state.filters);
    const dispatch = useAppDispatch();

    useEffect(() => {
        dispatch(fetchBrands());
    }, [dispatch]);

    useEffect(() => {
        dispatch(fetchPriceRange({ brandId: selectedBrand || undefined }));
    }, [dispatch, selectedBrand]);

    return (
        <Grid container spacing={4} sx={{ padding: 2 }}>
            <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                <Select
                    label="Выберите бренд"
                    options={brands.map((b: { id: string; name: string }) => ({ value: b.id, label: b.name }))}
                    value={selectedBrand}
                    onChange={(value: string | null) => dispatch(setSelectedBrand(value))}
                />
            </Grid>

            <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                <SliderRange
                    label="Стоимость"
                    min={minPrice}
                    max={maxPrice}
                    value={priceRange}
                    onChange={(range: [number, number]) => dispatch(setPriceRange(range))}
                />
            </Grid>
        </Grid>
    );
};
