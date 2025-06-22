import React from 'react';
import BackButton from '../../shared/ui/Buttons/BackButton';
import NextButton from '../../shared/ui/Buttons/NextButton';
import { Box } from '@mui/material';

interface SideBarMenuProps {
    onNext: () => void;
    onBack?: () => void;
    nextLabel?: React.ReactNode;
    backLabel?: React.ReactNode;
    nextDisabled?: boolean;
}

const SideBarPayment: React.FC<SideBarMenuProps> = ({
    onNext,
    onBack,
    nextLabel,
    backLabel,
    nextDisabled,
}) => (
    <Box
        sx={{
            display: 'flex',
            flexDirection: { xs: 'column', sm: 'row' },
            justifyContent: { xs: 'stretch', sm: 'space-between' },
            alignItems: 'center',
            width: '100%',
            mt: 2,
        }}
    >
        <BackButton onClick={onBack}>{backLabel}</BackButton>
        <NextButton onClick={onNext} disabled={nextDisabled}>{nextLabel}</NextButton>
    </Box>
);

export default SideBarPayment;