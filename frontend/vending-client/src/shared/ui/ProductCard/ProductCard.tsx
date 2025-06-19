import type { FC } from 'react';
import type { Product } from '../../../entities/Product/types';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import { yellow, green } from '@mui/material/colors';

interface ProductCardProps {
    product: Product;
    onSelect: (product: Product) => void;
    isSelected: boolean;
}

export const ProductCard: FC<ProductCardProps> = ({ product, onSelect, isSelected }) => {
    if (!product) return null;
    const isAvailable = product.quantityInStock > 0;

    return (
        <Card sx={{ marginTop: 2, display: 'flex', flexDirection: 'column', height: '100%' }}>
            <CardMedia
                component="img"
                height="120"
                image={product.images?.[0]?.imageUrl || ''}
                alt={product.name}
                sx={{ objectFit: 'contain', background: '#fafafa' }}
            />
            <CardContent sx={{ flexGrow: 1, display: 'flex', flexDirection: 'column', justifyContent: 'space-between', p: 2 }}>
                <div style={{ minHeight: 72, display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center' }}>
                    <Typography variant="subtitle1" fontWeight="bold" align="center" gutterBottom>
                        {product.name}
                    </Typography>
                    <Typography variant="body1" align="center" gutterBottom>
                        {product.price} руб.
                    </Typography>
                </div>
                <Button
                    variant="contained"
                    color={isAvailable ? (isSelected ? "success" : "warning") : 'inherit'}
                    disabled={!isAvailable}
                    fullWidth
                    sx={{
                        mt: 2,
                        fontWeight: 600,
                        backgroundColor: isAvailable
                            ? (isSelected ? green[500] : yellow[700])
                            : undefined,
                        color: isAvailable ? '#fff' : undefined,
                        '&:hover': {
                            backgroundColor: isAvailable
                                ? (isSelected ? green[700] : yellow[800])
                                : undefined,
                        },
                    }}
                    onClick={() => isAvailable && onSelect(product)}
                >
                    {isAvailable ? (isSelected ? 'Выбрано' : 'Выбрать') : 'Закончился'}
                </Button>
            </CardContent>
        </Card>
    );
};
