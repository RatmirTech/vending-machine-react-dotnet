import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

interface CartActionsProps {
    selectedCount: number;
}

export const CartActions = ({ selectedCount }: CartActionsProps) => {
    const navigate = useNavigate();

    const handleGoToCart = () => {
        if (selectedCount > 0) {
            navigate('/cart');
        }
    };

    return (
        <Button
            variant="contained"
            disabled={selectedCount === 0}
            onClick={handleGoToCart}
            fullWidth
            sx={{
            height: 56,
            fontWeight: 700,
            backgroundColor: '#6aa84f',
            '&:hover': {
                backgroundColor: '#4c7a36',
            },
            }}
            color="success"
        >
            Выбрано: {selectedCount}
        </Button>
    );
};
