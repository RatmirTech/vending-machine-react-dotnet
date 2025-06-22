import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { PayloadAction } from '@reduxjs/toolkit';
import type { RootState } from '../../app/store';
import type { OrderCreateRequest, OrderResponse } from '../../entities/Order/types';
import axios from 'axios';

interface OrderState {
    insertedCoins: Record<number, number>;
    totalInserted: number;
    orderResponse: OrderResponse | null;
    loading: boolean;
    error: string | null;
}

const initialState: OrderState = {
    insertedCoins: { 1: 0, 2: 0, 5: 0, 10: 0 },
    totalInserted: 0,
    orderResponse: null,
    loading: false,
    error: null,
};

const API_URL = import.meta.env.VITE_API_URL;

export const createOrder = createAsyncThunk(
    'order/createOrder',
    async (orderData: OrderCreateRequest) => {
        try {
            const response = await axios.post<OrderResponse>(
                `${API_URL}/Orders`,
                orderData
            );
            return response.data;
        } catch (error) {
            console.error('Error creating order:', error);
            throw error;
        }
    }
);

const orderSlice = createSlice({
    name: 'order',
    initialState,
    reducers: {
        setCoinCount: (state, action: PayloadAction<{ denomination: number; count: number }>) => {
            const { denomination, count } = action.payload;
            state.insertedCoins[denomination] = Math.max(0, count);
            state.totalInserted = Object.entries(state.insertedCoins)
                .reduce((total, [denom, coinCount]) => total + (Number(denom) * coinCount), 0);
        },
        incrementCoin: (state, action: PayloadAction<number>) => {
            const denomination = action.payload;
            state.insertedCoins[denomination] = (state.insertedCoins[denomination] || 0) + 1;
            state.totalInserted = Object.entries(state.insertedCoins)
                .reduce((total, [denom, count]) => total + (Number(denom) * count), 0);
        },
        decrementCoin: (state, action: PayloadAction<number>) => {
            const denomination = action.payload;
            if (state.insertedCoins[denomination] > 0) {
                state.insertedCoins[denomination] -= 1;
                state.totalInserted = Object.entries(state.insertedCoins)
                    .reduce((total, [denom, count]) => total + (Number(denom) * count), 0);
            }
        },
        resetOrder: (state) => {
            state.insertedCoins = { 1: 0, 2: 0, 5: 0, 10: 0 };
            state.totalInserted = 0;
            state.orderResponse = null;
            state.error = null;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(createOrder.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(createOrder.fulfilled, (state, action) => {
                state.loading = false;
                state.orderResponse = action.payload;
            })
            .addCase(createOrder.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'Ошибка создания заказа';
            });
    },
});

export const { setCoinCount, incrementCoin, decrementCoin, resetOrder } = orderSlice.actions;

export const selectInsertedCoins = (state: RootState) => state.order.insertedCoins;
export const selectTotalInserted = (state: RootState) => state.order.totalInserted;
export const selectOrderResponse = (state: RootState) => state.order.orderResponse;
export const selectOrderLoading = (state: RootState) => state.order.loading;
export const selectOrderError = (state: RootState) => state.order.error;

export default orderSlice.reducer;
