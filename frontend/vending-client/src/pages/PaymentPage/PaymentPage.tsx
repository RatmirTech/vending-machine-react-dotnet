import { useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../shared/lib/hooks';
import {
    selectCartItems,
    selectCartTotal,
    clearCart
} from '../../features/cart/cartSlice';
import {
    selectInsertedCoins,
    selectTotalInserted,
    setCoinCount,
    createOrder
} from '../../features/order/orderSlice';
import {
    Container,
    Typography,
    Box,
    Divider
} from '@mui/material';
import PaymentHeader from '../../shared/ui/Payment/PaymentHeader';
import PaymentRow from '../../shared/ui/Payment/PaymentRow';
import SideBarPayment from '../../widgets/SideBar/SideBarPayment';

const COIN_DENOMINATIONS = [1, 2, 5, 10];

export const PaymentPage = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const cartItems = useAppSelector(selectCartItems);
    const cartTotal = useAppSelector(selectCartTotal);
    const insertedCoins = useAppSelector(selectInsertedCoins);
    const totalInserted = useAppSelector(selectTotalInserted);

    const isPaymentSufficient = totalInserted >= cartTotal;

    const handleCoinChange = (denomination: number, count: number) => {
        dispatch(setCoinCount({ denomination, count: Math.max(0, count) }));
    };

    const handlePayment = async () => {
        if (!isPaymentSufficient) return;

        const orderData = {
            items: cartItems.map(item => ({
                productId: item.id,
                quantity: item.quantity
            })),
            insertedCoins
        };

        try {
            await dispatch(createOrder(orderData)).unwrap();
            dispatch(clearCart());
            navigate('/success');
        } catch (error) {
            console.error('Payment failed:', error);
        }
    };

    const handleGoBack = () => {
        navigate('/cart');
    };

    return (
        <Container maxWidth="lg" sx={{ py: 4 }}>
            <Typography variant="h4" sx={{ mb: 4 }}>
                Оплата
            </Typography>

            <PaymentHeader />

            <Box sx={{ mb: 4 }}>
                {COIN_DENOMINATIONS.map((denomination) => (
                    <PaymentRow
                        key={denomination}
                        nominal={denomination}
                        quantity={insertedCoins[denomination]}
                        quantityInStock={-1}
                        onQuantityChange={(qty) => handleCoinChange(denomination, qty)}
                    />
                ))}
            </Box>

            <Divider sx={{ my: 2 }} />
            <Box sx={{ display: 'flex', justifyContent: 'right', alignItems: 'flex-end', mb: 4 }}>
                <Typography sx={{ mr: 4, fontSize: 18 }}>
                    Итоговая сумма
                </Typography>
                <Typography sx={{ fontWeight: 'bold', fontSize: 26, mr: 6 }}>
                    {cartTotal} руб.
                </Typography>
                <Typography sx={{ mr: 4, fontSize: 18 }}>
                    Вы внесли
                </Typography>
                <Typography
                    sx={{
                        color: isPaymentSufficient ? 'green' : 'red',
                        fontWeight: 'bold',
                        fontSize: 26,
                    }}
                >
                    {totalInserted} руб.
                </Typography>
            </Box>

            <SideBarPayment
                onNext={handlePayment}
                onBack={handleGoBack}
                nextLabel="Оплатить"
                backLabel="Вернуться"
                nextDisabled={!isPaymentSufficient}
            />
        </Container>
    );
};
