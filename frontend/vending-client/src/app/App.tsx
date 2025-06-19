import { StoreProvider } from './providers/StoreProvider';
import { SignalRProvider, useSignalR } from './providers/SignalRProvider';
import { DrinksCatalogPage } from '../pages/DrinksCatalogPage/DrinksCatalogPage';
import React from 'react';

const BusyMessage: React.FC = () => {
  const { message } = useSignalR();
  if (message === 'Извините, в данный момент автомат занят') {
    return (
      <div style={{
        position: 'fixed',
        top: 0,
        left: 0,
        width: '100vw',
        height: '100vh',
        background: 'rgba(0,0,0,0.7)',
        color: 'white',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        fontSize: 32,
        zIndex: 1000
      }}>
        {message}
      </div>
    );
  }
  return null;
};

function App() {
  return (
    <StoreProvider>
      <SignalRProvider>
        <BusyMessage />
        <DrinksCatalogPage />
      </SignalRProvider>
    </StoreProvider>
  );
}

export default App;
