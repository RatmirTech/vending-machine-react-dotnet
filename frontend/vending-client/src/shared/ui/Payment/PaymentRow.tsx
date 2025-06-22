import React from 'react';
import Grid from '@mui/material/Grid';
import { Box, Typography } from '@mui/material';
import QuantitySelector from '../QuantitySelector/QuantitySelector';
import { getRubleSuffix } from '../../../features/payment/lib/getRubleSuffix';

export interface PaymentRowProps {
    nominal: number;
    quantity: number;
    quantityInStock: number;
    onQuantityChange: (qty: number) => void;
}

const PaymentRow: React.FC<PaymentRowProps> = ({
    nominal,
    quantity,
    quantityInStock,
    onQuantityChange,
}) => (
    <Grid container spacing={2} alignItems="center" sx={{ py: 1 }}>
        <Grid size={{ xs: 12, sm: 2 }}
            sx={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'flex-start',
                width: '100%',
            }}>
            <Box
                sx={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    width: 60,
                    height: 60,
                    borderRadius: '50%',
                    backgroundColor: '#f5f5f5',
                    border: '1px solid #ddd',
                }}
            >
                <Typography variant="h5">{nominal}</Typography>
            </Box>
        </Grid>

        <Grid size={{ xs: 12, sm: 3 }}
            sx={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'flex-start',
                    width: '100%',
                }}>
            <Typography variant="subtitle1">
                {nominal} руб{getRubleSuffix(nominal)}
            </Typography>
        </Grid>

        <Grid size={{ xs: 12, sm: 4 }}>
            <QuantitySelector
                item={{
                    id: nominal,
                    quantity,
                    quantityInStock,
                }}
                handleQuantityChange={(_, qty) => onQuantityChange(qty)}
            />
        </Grid>

        <Grid size={{ xs: 12, sm: 3 }}>
            <Typography variant="h6" sx={{ textAlign: 'center', mr: 2 }}>
                {quantity * nominal} руб.
            </Typography>
        </Grid>
    </Grid>
);

export default PaymentRow;
