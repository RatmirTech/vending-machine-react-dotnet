import { Provider } from 'react-redux';
import { store } from '../store';
import type { ReactNode } from 'react';

interface StoreProviderProps {
    children: ReactNode;
}

export const StoreProvider = ({ children }: StoreProviderProps) => (
    <Provider store={store}>{children}</Provider>
);
