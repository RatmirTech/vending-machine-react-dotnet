import { TextField, Button } from '@mui/material';

interface Props {
    value: number;
    onIncrement: () => void;
    onDecrement: () => void;
    onChange: (value: number) => void;
}

export const CoinControls = ({ value, onIncrement, onDecrement, onChange }: Props) => (
    <>
        <Button
            size="small"
            onClick={onDecrement}
            disabled={value <= 0}
            sx={{ minWidth: '30px', height: '30px', backgroundColor: '#333', color: 'white' }}
        >
            -
        </Button>
        <TextField
            value={value === 0 ? '' : value}
            onChange={(e) => {
                const val = e.target.value;
                if (/^\d*$/.test(val)) {
                    const num = parseInt(val, 10);
                    onChange(val === '' ? 0 : num);
                }
            }}
            onBlur={(e) => {
                if (e.target.value === '') {
                    onChange(0);
                }
            }}
            type="number"
            size="small"    
            slotProps={{
                input: {
                    min: 0,
                    inputMode: 'numeric',
                    pattern: '[0-9]*',
                    style: {
                        textAlign: 'center',
                        appearance: 'textfield',
                        MozAppearance: 'textfield',
                        WebkitAppearance: 'none',
                    },
                },
            }}
            sx={{
                mx: 1,
                width: '120px',
                '& input[type=number]::-webkit-outer-spin-button, & input[type=number]::-webkit-inner-spin-button': {
                    WebkitAppearance: 'none',
                    margin: 0,
                },
            }}
        />
        <Button
            size="small"
            onClick={onIncrement}
            sx={{ minWidth: '30px', height: '30px', backgroundColor: '#333', color: 'white' }}
        >
            +
        </Button>
    </>
);
