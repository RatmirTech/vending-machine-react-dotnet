import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { Brand } from '../../entities/Brand/types';
import axios from 'axios';
import type { PriceRangeResponse } from '../../entities/Product/types';

export interface FiltersState {
    brands: Brand[];
    selectedBrand: string | null;
    minPrice: number;
    maxPrice: number;
    priceRange: [number, number];
    loading: boolean;
    error: string | null;
}

const initialState: FiltersState = {
    brands: [],
    selectedBrand: null,
    minPrice: 0,
    maxPrice: 0,
    priceRange: [0, 0],
    loading: false,
    error: null,
};

const API_URL = import.meta.env.VITE_API_URL;

export const fetchBrands = createAsyncThunk(
    'filters/fetchBrands',
    async () => {
        const response = await axios.get<Brand[]>(`${API_URL}/Brand`);
        return response.data;
    }
);

export const fetchPriceRange = createAsyncThunk(
    'filters/fetchPriceRange',
    async (params: { brandId?: string }) => {
        const response = await axios.get<PriceRangeResponse>(`${API_URL}/Product/price-range`, {
            params,
        });
        return response.data;
    }
);

const filtersSlice = createSlice({
    name: 'filters',
    initialState,
    reducers: {
        setSelectedBrand(state, action) {
            state.selectedBrand = action.payload;
        },
        setPriceRange(state, action) {
            state.priceRange = action.payload;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchBrands.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchBrands.fulfilled, (state, action) => {
                state.loading = false;
                state.brands = action.payload;
            })
            .addCase(fetchBrands.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'Ошибка загрузки брендов';
            })
            .addCase(fetchPriceRange.fulfilled, (state, action) => {
                state.minPrice = action.payload.minPrice;
                state.maxPrice = action.payload.maxPrice;
                state.priceRange = [action.payload.minPrice, action.payload.maxPrice];
            });
    },
});

export const { setSelectedBrand, setPriceRange } = filtersSlice.actions;
export default filtersSlice.reducer;
