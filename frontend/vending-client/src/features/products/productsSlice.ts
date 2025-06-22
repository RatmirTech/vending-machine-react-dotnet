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
        try {
            const queryParams = new URLSearchParams();
            if (params.brand) queryParams.append('brandId', params.brand);
            if (params.minPrice !== undefined) queryParams.append('minPrice', params.minPrice.toString());
            if (params.maxPrice !== undefined) queryParams.append('maxPrice', params.maxPrice.toString());

            console.log('Fetching products from:', `${API_URL}/Product?${queryParams}`);
            console.log('With params:', params);
            
            const response = await axios.get<Product[]>(
                `${API_URL}/Product/products`,
                { 
                    params: {
                        brandId: params.brand,
                        minPrice: params.minPrice,
                        maxPrice: params.maxPrice
                    }
                }
            );
            console.log('Products response:', response.data);
            return response.data;
        } catch (error) {
            console.error('Error fetching products:', error);
            throw error;
        }
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
                console.error('Products slice error:', action.error);
            });
    },
});

export default productsSlice.reducer;
