import { StoreProvider } from './providers/StoreProvider';
import { SignalRProvider, useSignalR } from './providers/SignalRProvider';
import React, { useEffect, useState } from 'react';
import { AppRouter } from './router';

const BusyMessage: React.FC<{ setIsLocked: (locked: boolean) => void }> = ({ setIsLocked }) => {
  const { message } = useSignalR();

  useEffect(() => {
    if (message === 'Извините, в данный момент автомат занят') {
      setIsLocked(true);
    } else {
      setIsLocked(false);
    }
  }, [message, setIsLocked]);

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
  const [isLocked, setIsLocked] = useState(false);

  return (
    <StoreProvider>
      <SignalRProvider>
        <BusyMessage setIsLocked={setIsLocked} />
        {!isLocked && <AppRouter />}
      </SignalRProvider>
    </StoreProvider>
  );
}

export default App;
