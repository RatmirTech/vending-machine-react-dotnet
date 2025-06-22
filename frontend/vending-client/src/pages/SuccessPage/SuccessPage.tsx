import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../shared/lib/hooks';
import { selectOrderResponse, resetOrder } from '../../features/order/orderSlice';
import { Container, Typography, Box, Button } from '@mui/material';
import BackButton from '../../shared/ui/Buttons/BackButton';

export const SuccessPage = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const orderResponse = useAppSelector(selectOrderResponse);

    useEffect(() => {
        return () => {
            dispatch(resetOrder());
        };
    }, [dispatch]);

    const handleGoToCatalog = () => {
        navigate('/');
    };

    if (!orderResponse) {
        return (
            <Container maxWidth="md" sx={{ py: 4, textAlign: 'center' }}>
                <Typography variant="h4" sx={{ mb: 2 }}>
                    Ошибка
                </Typography>
                <Typography variant="h6" sx={{ mb: 4 }}>
                    Информация о заказе не найдена
                </Typography>
                <Button variant="contained" onClick={handleGoToCatalog}>
                    Каталог напитков
                </Button>
            </Container>
        );
    }

    if (!orderResponse.success) {
        return (
            <Container maxWidth="md" sx={{ py: 4, textAlign: 'center' }}>
                <Typography variant="h4" sx={{ mb: 2 }}>
                    Извините
                </Typography>
                <Typography variant="h6" sx={{ mb: 4 }}>
                    {orderResponse.message || 'В данный момент мы не можем продать вам товар по причине того, что автомат не может выдать вам нужную сдачу'}
                </Typography>
                <Button variant="contained" onClick={handleGoToCatalog}>
                    Каталог напитков
                </Button>
            </Container>
        );
    }

    const changeData = orderResponse.changeToGive;

    console.log('OrderResponse:', orderResponse);

    return (
        <Container maxWidth="md" sx={{ py: 4, textAlign: 'center' }}>
            <Typography variant="h4" sx={{ mb: 2 }}>
                Спасибо за покупку!
            </Typography>
            {(orderResponse.changeAmount ?? 0) > 0 && (
                <>
                    <Typography variant="h4" sx={{ mb: 4 }}>
                        Пожалуйста, возьмите вашу сдачу:&nbsp;
                        <span style={{ color: 'green' }}>
                            {orderResponse.changeAmount ?? 0} руб.
                        </span>
                    </Typography>
                    <Box sx={{ mb: 12 }}>
                        <Typography variant="h6" sx={{ mb: 2 }}>
                            Ваши монеты:
                        </Typography>
                        {changeData && Object.entries(changeData).map(([denomination, count]) => (
                            <Box key={denomination} sx={{ display: 'flex', alignItems: 'center', justifyContent: 'center', mb: 5 }}>
                                <Box sx={{
                                    display: 'flex',
                                    alignItems: 'center',
                                    justifyContent: 'center',
                                    width: 60,
                                    height: 60,
                                    borderRadius: '50%',
                                    backgroundColor: '#f5f5f5',
                                    mr: 2,
                                    border: '1px solid #ddd',
                                }}>
                                    <Typography variant="h4">{denomination}</Typography>
                                </Box>
                                <Typography variant="body1">
                                    {Number(count)} шт.
                                </Typography>
                            </Box>
                        ))}
                    </Box>

                </>
            )}
            <BackButton onClick={handleGoToCatalog}>Каталог напитков</BackButton>
        </Container>
    );
};
