import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL;

export async function importProductsApi(file: File): Promise<void> {
    const formData = new FormData();
    formData.append('file', file);

    try {
        await axios.post(`${API_URL}/product/import`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
    } catch (error: unknown) {
        let message = '';
        if (axios.isAxiosError(error)) {
            message = error.response?.data || error.message;
        } else if (error instanceof Error) {
            message = error.message;
        } else {
            message = String(error);
        }
        throw new Error(`Ошибка при импорте: ${message}`);
    }
}