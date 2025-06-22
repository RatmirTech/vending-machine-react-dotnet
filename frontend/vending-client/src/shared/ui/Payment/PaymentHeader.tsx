import React from 'react';
import Grid from '@mui/material/Grid';
import { Typography, Divider } from '@mui/material';

const PaymentHeader: React.FC = () => (
    <Grid container spacing={2} alignItems="center">
        <Grid size={4}>
            <Typography variant="subtitle1" sx={{ textAlign: 'left' }}>
                Номинал
            </Typography>
        </Grid>

        <Grid size={4}>
            <Typography variant="subtitle1" sx={{ textAlign: 'center' }}>
                Количество
            </Typography>
        </Grid>

        <Grid size={4}>
            <Typography variant="subtitle1" sx={{ textAlign: 'center' }}>
                Сумма
            </Typography>
        </Grid>

        <Grid size={12}>
            <Divider />
        </Grid>
    </Grid>
);

export default PaymentHeader;
