import { useState } from 'react';
import type { FC } from 'react';
import type { Product } from '../../../entities/Product/types';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import { yellow } from '@mui/material/colors';
import Skeleton from '@mui/material/Skeleton';

interface ProductCardProps {
    product: Product;
    onSelect: (product: Product) => void;
    isSelected: boolean;
}

export const ProductCard: FC<ProductCardProps> = ({ product, onSelect, isSelected }) => {
    const [imageLoaded, setImageLoaded] = useState(false);
    if (!product) return null;

    const isAvailable = product.quantityInStock > 0;
    const imageUrl = product.images?.[0]?.imageUrl || '';

    return (
        <Card sx={{ marginTop: 2, display: 'flex', flexDirection: 'column', height: '100%' }}>
            <div style={{ position: 'relative', width: '100%', height: 350, background: '#fafafa' }}>
                {!imageLoaded && (
                    <Skeleton
                        variant="rectangular"
                        width="100%"
                        height="100%"
                        animation="wave"
                        sx={{ position: 'absolute', top: 0, left: 0 }}
                    />
                )}
                <CardMedia
                    component="img"
                    image={imageUrl}
                    alt={product.name}
                    onLoad={() => setImageLoaded(true)}
                    sx={{
                        objectFit: 'contain',
                        width: '100%',
                        height: '100%',
                        display: imageLoaded ? 'block' : 'none',
                        background: '#fafafa',
                    }}
                />
            </div>
            <CardContent
                sx={{
                    flexGrow: 1,
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'space-between',
                    p: 2,
                }}
            >
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
                    disabled={!isAvailable}
                    fullWidth
                    sx={{
                        mt: 2,
                        fontWeight: 600,
                        backgroundColor: isAvailable
                            ? (isSelected ? '#6aa84f' : yellow[700])
                            : undefined,
                        color: isAvailable
                            ? (isSelected ? '#fff' : yellow[700] ? '#000' : undefined)
                            : undefined,
                        '&:hover': {
                            backgroundColor: isAvailable
                                ? (isSelected ? '#6aa84f' : yellow[800])
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
