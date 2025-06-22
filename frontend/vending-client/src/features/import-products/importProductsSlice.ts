import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { importProductsApi } from './importProductsApi';

interface ImportProductsState {
    loading: boolean;
    error: string | null;
    success: boolean;
}

const initialState: ImportProductsState = {
    loading: false,
    error: null,
    success: false,
};

export const importProducts = createAsyncThunk<void, File, { rejectValue: string }>(
    'importProducts/import',
    async (file, { rejectWithValue }) => {
        try {
            await importProductsApi(file);
            alert('Импорт товаров успешно завершён');
            return;
        } catch (err) {
            if (err instanceof Error) {
                return rejectWithValue(err.message);
            }
            return rejectWithValue('Неизвестная ошибка');
        }
    }
);


const importProductsSlice = createSlice({
    name: 'importProducts',
    initialState,
    reducers: {
        resetImportState(state) {
            state.loading = false;
            state.error = null;
            state.success = false;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(importProducts.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.success = false;
            })
            .addCase(importProducts.fulfilled, (state) => {
                state.loading = false;
                state.success = true;
            })
            .addCase(importProducts.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload || 'Ошибка при импорте';
            });
    },
});

export const { resetImportState } = importProductsSlice.actions;
export default importProductsSlice.reducer;
