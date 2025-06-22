import { Grid, Typography, Box } from '@mui/material';
import { CoinControls } from './CoinControls';
import { getRubleSuffix } from '../../../features/payment/lib/getRubleSuffix';

interface Props {
    denomination: number;
    count: number;
    onIncrement: () => void;
    onDecrement: () => void;
    onChange: (value: number) => void;
}

export const CoinInputRow = ({ denomination, count, onIncrement, onDecrement, onChange }: Props) => (
    <Grid container spacing={2} alignItems="center" sx={{ mb: 2 }}>
        <Grid size={{ xs: 12, sm: 2}}>
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
                    mx: 'auto'
                }}
            >
                <Typography variant="h4">{denomination}</Typography>
            </Box>
        </Grid>

        <Grid size={{ xs: 12, sm: 3 }}>
            <Typography>
                {denomination} руб{getRubleSuffix(denomination)}
            </Typography>
        </Grid>

        <Grid size={{ xs: 12, sm: 4 }}>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                <CoinControls
                    value={count}
                    onIncrement={onIncrement}
                    onDecrement={onDecrement}
                    onChange={onChange}
                />
            </Box>
        </Grid>

        <Grid size={{ xs: 12, sm: 3 }}>
            <Typography sx={{ textAlign: 'center', fontWeight: 500 }}>
                {denomination * count} руб.
            </Typography>
        </Grid>
    </Grid>
);
