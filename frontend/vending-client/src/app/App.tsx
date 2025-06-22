import { StoreProvider } from './providers/StoreProvider';
import { SignalRProvider } from './providers/SignalRProvider';
import { useState } from 'react';
import { AppRouter } from './router';
import { BusyMessage } from './BusyMessage';

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
