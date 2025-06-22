export interface OrderItem {
    productId: string;
    quantity: number;
}

export interface OrderCreateRequest {
    items: OrderItem[];
    insertedCoins: Record<number, number>;
}

export interface OrderResponse {
    orderId: string;
    totalAmount: number;
    changeAmount?: number;
    changeCoins?: Record<number, number>;
    changeToGive?: Record<number, number>;
    success: boolean;
    message?: string;
}

export interface CoinDenomination {
    value: number;
    count: number;
}
