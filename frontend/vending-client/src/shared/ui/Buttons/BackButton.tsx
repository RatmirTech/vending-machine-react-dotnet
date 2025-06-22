import React from 'react';
import Button from '@mui/material/Button';
import { yellow } from '@mui/material/colors';

interface BackButtonProps {
    onClick?: () => void;
    children?: React.ReactNode;
}

const BackButton: React.FC<BackButtonProps> = ({ onClick, children = 'Вернуться' }) => (
    <Button
        variant="contained"
        onClick={onClick}
        sx={{
            backgroundColor: yellow[700],
            color: 'black',
            textTransform: 'none',
            fontSize: '1rem',
            py: 1,
            width: { xs: '100%', sm: '220px' },
            mb: { xs: 1, sm: 0 },
            '&:hover': {
                backgroundColor: yellow[800],
            },
        }}
    >
        {children}
    </Button>
);

export default BackButton;