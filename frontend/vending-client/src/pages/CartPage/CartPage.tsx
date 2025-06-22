import { useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../shared/lib/hooks';
import { selectCartItems, selectCartTotal, updateQuantity, removeFromCart } from '../../features/cart/cartSlice';
import { Container, Typography, Box, Button, TextField, Divider, Grid, CardMedia } from '@mui/material';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import SideBarMenu from '../../widgets/SideBar/SideBarCart';

export const CartPage = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const cartItems = useAppSelector(selectCartItems);
    const cartTotal = useAppSelector(selectCartTotal);

    const handleQuantityChange = (productId: string, quantity: number) => {
        dispatch(updateQuantity({ productId, quantity }));
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
            <Container maxWidth="md" sx={{ py: 4, textAlign: 'center' }}>
                <Typography variant="h4" sx={{ mb: 2 }}>
                    Оформление заказа
                </Typography>
                <Typography variant="h6" sx={{ mb: 4 }}>
                    У вас нет ни одного товара, вернитесь на страницу каталога
                </Typography>
                <Button variant="contained" onClick={handleGoBack}>
                    Каталог напитков
                </Button>
            </Container>
        );
    }

    return (
        <Container maxWidth="lg" sx={{ py: 4 }}>
            <Typography variant="h4" sx={{ mb: 4 }}>
                Оформление заказа
            </Typography>

            <Grid container spacing={2} sx={{ mb: 1, alignItems: 'center' }}>
                <Grid size={{ xs: 3 }}>
                    <Typography variant="subtitle1" sx={{ textAlign: 'left' }}>
                        Товар
                    </Typography>
                </Grid>
                <Grid size={{ xs: 4}}>
                    <Typography variant="subtitle1" sx={{ textAlign: 'center' }}>
                        Количество
                    </Typography>
                </Grid>
                <Grid size={{ xs: 4 }}>
                    <Typography variant="subtitle1" sx={{ textAlign: 'center' }}>
                        Цена
                    </Typography>
                </Grid>
                <Divider sx={{ width: '100%' }} />
            </Grid>
            <Grid maxWidth="lg" sx={{ mb: 2 }}>
                {cartItems.map((item) => (
                    <Grid key={item.id} container spacing={2} alignItems="center" sx={{ mb: 3 }}>
                        <Grid size={{ xs: 3}}>
                            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                                <CardMedia
                                    component="img"
                                    image={item.images?.[0]?.imageUrl || ''}
                                    alt={item.name}
                                    sx={{ width: 60, height: 100, objectFit: 'cover', background: '#fafafa' }}
                                />
                                <Typography variant="subtitle1" sx={{ textAlign: 'left' }}>
                                    {item.name}
                                </Typography>
                            </Box>
                        </Grid>
                        <Grid size={{ xs: 4 }}>
                            <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                                <Button
                                    size="small"
                                    onClick={() => handleQuantityChange(item.id, item.quantity - 1)}
                                    disabled={item.quantity <= 1}
                                    sx={{ minWidth: '30px', height: '30px', backgroundColor: '#333', color: 'white' }}
                                >
                                    -
                                </Button>
                                <TextField
                                    type="number"
                                    value={item.quantity}
                                    onChange={(e) => {
                                        const val = e.target.value;
                                        if (/^\d*$/.test(val)) {
                                            const num = parseInt(val, 10);
                                            if (val === '') {
                                                handleQuantityChange(item.id, 0);
                                            } else if (num >= 1 && num <= item.quantityInStock) {
                                                handleQuantityChange(item.id, num);
                                            }
                                        }
                                    }}
                                    size="medium"
                                    slotProps={{
                                        input: {
                                            inputProps: {
                                                inputMode: 'numeric',
                                                pattern: '[0-9]*',
                                                min: 1,
                                                max: item.quantityInStock,
                                                style: {
                                                    textAlign: 'center',
                                                    padding: '6px 8px',
                                                    appearance: 'textfield',
                                                    MozAppearance: 'textfield',
                                                    WebkitAppearance: 'none',
                                                },
                                            },
                                        },
                                    }}
                                    sx={{
                                        width: '30%',
                                        mx: 1,
                                        '& input[type=number]::-webkit-outer-spin-button, & input[type=number]::-webkit-inner-spin-button': {
                                            WebkitAppearance: 'none',
                                            margin: 0,
                                        },
                                    }}
                                />

                                <Button
                                    size="small"
                                    onClick={() => handleQuantityChange(item.id, item.quantity + 1)}
                                    disabled={item.quantity >= item.quantityInStock}
                                    sx={{ minWidth: '30px', height: '30px', backgroundColor: '#333', color: 'white' }}
                                >
                                    +
                                </Button>
                            </Box>
                        </Grid>
                        <Grid size={{ xs: 4}}>
                            <Typography variant="h5" sx={{ textAlign: 'center' }}>
                                {item.price * item.quantity} руб.
                            </Typography>
                        </Grid>
                        <Grid size={{ xs: 1 }} sx={{ textAlign: 'right' }}>
                            <Button onClick={() => handleRemoveItem(item.id)} sx={{color:'black'}}>
                                <DeleteOutlineIcon sx={{ width: 40, height: 40 }} />
                            </Button>
                        </Grid>
                    </Grid>
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
