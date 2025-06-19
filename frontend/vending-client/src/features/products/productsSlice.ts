import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { Product } from '../../entities/Product/types';
import axios from 'axios';

export interface ProductsState {
    items: Product[];
    loading: boolean;
    error: string | null;
}

const initialState: ProductsState = {
    items: [],
    loading: false,
    error: null,
};

const API_URL = import.meta.env.VITE_API_URL;

export const fetchProducts = createAsyncThunk(
    'products/fetchProducts', 
    async (params: { brand?: string; minPrice?: number; maxPrice?: number }) => {
        const response = await axios.get<Product[]>(
            `${API_URL}/Product/products`,
            { params }
        );
        return response.data;
    }
);

const productsSlice = createSlice({
    name: 'products',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchProducts.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchProducts.fulfilled, (state, action) => {
                state.loading = false;
                state.items = action.payload;
            })
            .addCase(fetchProducts.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'Ошибка загрузки товаров';
            });
    },
});

export default productsSlice.reducer;
