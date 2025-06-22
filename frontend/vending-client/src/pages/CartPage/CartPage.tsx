import { useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../shared/lib/hooks';
import { selectCartItems, selectCartTotal, updateQuantity, removeFromCart } from '../../features/cart/cartSlice';
import { Container, Typography, Box, Divider, Grid } from '@mui/material';
import SideBarMenu from '../../widgets/SideBar/SideBarCart';
import CartRow from '../../shared/ui/Cart/CartRow';
import CartHeader from '../../shared/ui/Cart/CartHeader';
import BackButton from '../../shared/ui/Buttons/BackButton';

export const CartPage = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const cartItems = useAppSelector(selectCartItems);
    const cartTotal = useAppSelector(selectCartTotal);

    const handleQuantityChange = (id: string | number, quantity: number) => {
        dispatch(updateQuantity({ productId: String(id), quantity }));
    };

    const handleRemoveItem = (productId: string) => {
        dispatch(removeFromCart(productId));
    };

    const handleGoToPayment = () => {
        navigate('/payment');
    };

    const handleGoBack = () => {
        navigate('/');
    };

    if (cartItems.length === 0) {
        return (
        <Container maxWidth="lg" sx={{ py: 4 }}>
            <Typography variant="h4" sx={{ mb: 4 }}>
                Оформление заказа
            </Typography>
                <Typography variant="h6" sx={{ mb: 4 }}>
                    У вас нет ни одного товара, вернитесь на страницу каталога
                </Typography>
                <BackButton
                    onClick={handleGoBack}
                    children="Вернуться"
                    />
            </Container>
        );
    }

    return (
        <Container maxWidth="lg" sx={{ py: 4 }}>
            <Typography variant="h4" sx={{ mb: 4 }}>
                Оформление заказа
            </Typography>

            <Grid maxWidth="lg" sx={{ mb: 2 }}>
            <CartHeader/>
                {cartItems.map((item) => (
                    <CartRow
                        key={item.id}
                        item={item}
                        handleQuantityChange={(item, quantity) => handleQuantityChange(item.id, quantity)}
                        handleRemoveItem={handleRemoveItem}
                    />
                ))}
                <Divider sx={{ mt: 1 }} />
            </Grid>

            <Box sx={{ mb: 4 }}>
                <Typography variant="h5" sx={{ textAlign: 'right', fontSize: '1.2rem' }}>
                    Итоговая сумма
                    <Typography component="span" sx={{ fontWeight: 'bold', ml: 5, fontSize: '1.7rem' }}>
                        {cartTotal} руб.
                    </Typography>
                </Typography>
            </Box>
            <SideBarMenu
                onNext={handleGoToPayment}
                onBack={handleGoBack}
                nextLabel="Оплата"
                backLabel="Вернуться"
            />
        </Container>
    );
};
