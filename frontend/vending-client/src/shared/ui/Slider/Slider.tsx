import { useEffect, useState, type FC } from 'react';
import Slider from '@mui/material/Slider';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

interface SliderProps {
    label: string;
    min: number;
    max: number;
    value: [number, number];
    onChange: (value: [number, number]) => void;
}

export const SliderRange: FC<SliderProps> = ({ label, min, max, value, onChange }) => {
    const [internalValue, setInternalValue] = useState<[number, number]>(value);

    useEffect(() => {
        setInternalValue(value);
    }, [value]);

    const handleChange = (_: Event, newValue: number | number[]) => {
        if (Array.isArray(newValue) && newValue.length === 2) {
            setInternalValue([newValue[0], newValue[1]]);
            onChange([newValue[0], newValue[1]]);
        }
    };

    return (
        <Box sx={{ width: '100%' }}>
            <Box sx={{ mb: 1 }}>
            <Typography variant="body2">{label}</Typography>
            <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
                <Typography variant="caption">{internalValue[0]} руб.</Typography>
                <Typography variant="caption">{internalValue[1]} руб.</Typography>
            </Box>
            </Box>
            <Slider
                value={internalValue}
                min={min}
                max={max}
                onChange={handleChange}
                valueLabelDisplay="auto"
                disableSwap
                sx={{ color: 'grey.500' }}
            />
        </Box>
    );
};
