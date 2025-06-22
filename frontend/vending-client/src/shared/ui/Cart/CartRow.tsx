import React from 'react';
import Grid from '@mui/material/Grid';
import { Box, CardMedia, Typography, Button } from '@mui/material';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import QuantitySelector from '../QuantitySelector/QuantitySelector';
import Skeleton from '@mui/material/Skeleton';

export interface CartItem {
    id: string;
    name: string;
    price: number;
    quantity: number;
    images?: { imageUrl: string }[];
}

interface CartRowProps {
    item: CartItem & { quantityInStock: number };
    handleQuantityChange: (item: CartItem, quantity: number) => void;
    handleRemoveItem: (id: string) => void;
}

const CartRow: React.FC<CartRowProps> = ({ item, handleQuantityChange, handleRemoveItem }) => (
    <Grid container spacing={2} alignItems="center" sx={{ mb: 2 }}>
        <Grid size={4}>
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                {item.images?.[0]?.imageUrl ? (
                    <CardMedia
                        component="img"
                        image={item.images[0].imageUrl}
                        alt={item.name}
                        sx={{ width: 60, height: 100, objectFit: 'cover', background: '#fafafa' }}
                        onError={(e: React.SyntheticEvent<HTMLImageElement>) => {
                            (e.target as HTMLImageElement).style.display = 'none';
                            const skeleton = document.createElement('div');
                            skeleton.style.width = '60px';
                            skeleton.style.height = '100px';
                            skeleton.style.background = '#eee';
                            (e.target as HTMLImageElement).parentElement?.appendChild(skeleton);
                        }}
                    />
                ) : (
                    <Skeleton variant="rectangular" width={60} height={100} />
                )}
                <Typography variant="subtitle1" sx={{ ml: 2, textAlign: 'left' }}>
                    {item.name}
                </Typography>
            </Box>
        </Grid>

        <Grid size={4}>
            <QuantitySelector
                item={{ id: item.id, quantity: item.quantity, quantityInStock: item.quantityInStock }}
                handleQuantityChange={(_, qty) => handleQuantityChange(item, qty)}
            />
        </Grid>

        <Grid size={3}>
            <Typography variant="h5" sx={{ textAlign: 'center' }}>
                {item.price * item.quantity} руб.
            </Typography>
        </Grid>

        <Grid size={1} sx={{ textAlign: 'center' }}>
            <Button onClick={() => handleRemoveItem(item.id)} sx={{ color: 'black' }}>
                <DeleteOutlineIcon />
            </Button>
        </Grid>
    </Grid>
);

export default CartRow;
