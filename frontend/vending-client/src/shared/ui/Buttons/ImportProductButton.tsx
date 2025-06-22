import { Button } from "@mui/material";
import React, { useRef } from "react";
import { useAppDispatch } from "../../lib/hooks";
import { importProducts } from "../../../features/import-products/importProductsSlice";

export const ImportProductButton = () => {
    const dispatch = useAppDispatch();
    const inputRef = useRef<HTMLInputElement>(null);

    const handleButtonClick = () => {
        inputRef.current?.click();
    };

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (file) {
            dispatch(importProducts(file));
            e.target.value = "";
        }
    };
    return (
        <>
            <input
                type="file"
                accept=".xlsx,.xls"
                ref={inputRef}
                style={{ display: "none" }}
                onChange={handleFileChange}
            />
            <Button
                variant="contained"
                fullWidth
                sx={{
                    backgroundColor: '#f5f5f5',
                    color: '#222',
                    boxShadow: 'none',
                    height: 56,
                    fontWeight: 600,
                    '&:hover': {
                        backgroundColor: '#e0e0e0',
                        boxShadow: 'none',
                    },
                }}
                onClick={handleButtonClick}
            >
                Импорт
            </Button>
        </>
    );
};