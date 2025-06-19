import type { FC } from 'react';
import { MenuItem, FormControl, InputLabel, Select as MuiSelect } from '@mui/material';


export interface SelectOption {
    value: string;
    label: string;
}

interface SelectProps {
    label: string;
    options: SelectOption[];
    value: string | null;
    onChange: (value: string | null) => void;
}

export const Select: FC<SelectProps> = ({ label, options, value, onChange }) => {
    return (
        <label>
            <FormControl fullWidth>
            <InputLabel>{label}</InputLabel>
            <MuiSelect
                value={value ?? ''}
                label={label}
                onChange={e => onChange(e.target.value === '' ? null : e.target.value)}
            >
                <MenuItem value="">Все</MenuItem>
                {options.map(option => (
                <MenuItem key={option.value} value={option.value}>
                    {option.label}
                </MenuItem>
                ))}
            </MuiSelect>
            </FormControl>
        </label>
    );
};
