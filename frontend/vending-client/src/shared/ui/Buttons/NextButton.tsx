import React from 'react';
import Button from '@mui/material/Button';

interface NextButtonProps {
    onClick: () => void;
    children?: React.ReactNode;
    disabled?: boolean;
}

const NextButton: React.FC<NextButtonProps> = ({ onClick, children, disabled }) => (
    <Button
        variant="contained"
        onClick={onClick}
        disabled={disabled}
        sx={{
            textTransform: 'none',
            fontSize: '1.2rem',
            py: 1,
            backgroundColor: '#6aa84f',
            width: { xs: '100%', sm: '220px' },
        }}
    >
        {children || 'Оплата'}
    </Button>
);

export default NextButton;