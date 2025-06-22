import React from 'react';
import { Button, TextField, Box } from '@mui/material';

interface QuantitySelectorProps {
    item: {
        id: string | number;
        quantity: number;
        quantityInStock: number;
    };
    handleQuantityChange: (id: string | number, quantity: number) => void;
}

const QuantitySelector: React.FC<QuantitySelectorProps> = ({ item, handleQuantityChange }) => {
    const isInfinite = item.quantityInStock === -1;

    return (
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
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                    const val = e.target.value;
                    if (/^\d*$/.test(val)) {
                        const num = parseInt(val, 10);
                        if (val === '') {
                            handleQuantityChange(item.id, 0);
                        } else if (num >= 1 && (isInfinite || num <= item.quantityInStock)) {
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
                            ...(isInfinite ? {} : { max: item.quantityInStock }),
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
                disabled={!isInfinite && item.quantity >= item.quantityInStock}
                sx={{ minWidth: '30px', height: '30px', backgroundColor: '#333', color: 'white' }}
            >
                +
            </Button>
        </Box>
    );
};

export default QuantitySelector;
