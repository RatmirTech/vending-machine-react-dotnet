import Grid from '@mui/material/Grid';
import { Typography, Divider } from '@mui/material';

const CartHeader: React.FC = () => (
        <Grid container spacing={2} alignItems="center" sx={{ mb: 1, width: '100%' }}>
            <Grid size={4}>
                <Typography variant="subtitle1" sx={{ textAlign: 'left' }}>
                    Товар
                </Typography>
            </Grid>
            <Grid size={4}>
                <Typography variant="subtitle1" sx={{ textAlign: 'center' }}>
                    Количество
                </Typography>
            </Grid>
            <Grid size={3}>
                <Typography variant="subtitle1" sx={{ textAlign: 'center' }}>
                    Цена
                </Typography>
            </Grid>
            <Grid size={1} sx={{ textAlign: 'right' }}>
            </Grid>
            <Grid size={12}>
                <Divider />
            </Grid>
        </Grid>
);

export default CartHeader;