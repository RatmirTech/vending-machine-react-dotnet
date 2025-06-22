import { createSlice } from '@reduxjs/toolkit';
import type { PayloadAction } from '@reduxjs/toolkit';
import type { Product } from '../../entities/Product/types';
import type { RootState } from '../../app/store';

interface CartState {
    items: (Product & { quantity: number })[];
}

const loadCartFromStorage = (): CartState['items'] => {
    try {
        return JSON.parse(localStorage.getItem('cart') || '[]');
    } catch {
        return [];
    }
};

const saveCartToStorage = (items: CartState['items']) => {
    localStorage.setItem('cart', JSON.stringify(items));
};

const initialState: CartState = {
    items: loadCartFromStorage(),
};

export const cartSlice = createSlice({
    name: 'cart',
    initialState,
    reducers: {
        toggleProduct: (state, action: PayloadAction<Product>) => {
            const existingIndex = state.items.findIndex(item => item.id === action.payload.id);
            if (existingIndex >= 0) {
                state.items.splice(existingIndex, 1);
            } else {
                state.items.push({ ...action.payload, quantity: 1 });
            }
            saveCartToStorage(state.items);
        },
        updateQuantity: (state, action: PayloadAction<{ productId: string; quantity: number }>) => {
            const { productId, quantity } = action.payload;
            const item = state.items.find(item => item.id === productId);
            if (item) {
                item.quantity = Math.max(1, Math.min(quantity, item.quantityInStock));
                saveCartToStorage(state.items);
            }
        },
        removeFromCart: (state, action: PayloadAction<string>) => {
            state.items = state.items.filter(item => item.id !== action.payload);
            saveCartToStorage(state.items);
        },
        clearCart: (state) => {
            state.items = [];
            saveCartToStorage(state.items);
        },
    },
});

export const { toggleProduct, updateQuantity, removeFromCart, clearCart } = cartSlice.actions;

export const selectCartItems = (state: RootState) => state.cart.items;
export const selectIsProductInCart = (state: RootState, productId: string) => 
    state.cart.items.some(item => item.id === productId);
export const selectCartCount = (state: RootState) => state.cart.items.length;
export const selectCartTotal = (state: RootState) => 
    state.cart.items.reduce((total, item) => total + (item.price * item.quantity), 0);

export default cartSlice.reducer;
