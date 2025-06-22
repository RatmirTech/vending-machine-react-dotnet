import React, { createContext, useContext, useEffect, useRef, useState } from 'react';
import { HubConnectionBuilder, HubConnection, HubConnectionState } from '@microsoft/signalr';

interface SignalRContextType {
    connected: boolean;
    message: string | null;
}

const SignalRContext = createContext<SignalRContextType>({
    connected: false,
    message: null,
});

export const SignalRProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [connected, setConnected] = useState(false);
    const [message, setMessage] = useState<string | null>(null);
    const connectionRef = useRef<HubConnection | null>(null);

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(import.meta.env.VITE_SIGNALR_HUB_URL, { withCredentials: true })
            .withAutomaticReconnect()
            .build();

        connectionRef.current = connection;

        connection.on('Notify', (msg: string) => {
            setMessage(msg);
        });

        connection.onclose(() => {
            setConnected(false);
        });

        const start = async () => {
            try {
                if (connection.state === HubConnectionState.Disconnected) {
                    await connection.start();
                    setConnected(true);
                }
            } catch (err) {
                console.error('Ошибка подключения:', err);
            }
        };

        start();

        return () => {
            connection.off('Notify');
            if (connection.state === HubConnectionState.Connected || connection.state === HubConnectionState.Connecting) {
                connection.stop();
            }
        };
    }, []);

    return (
        <SignalRContext.Provider value={{ connected, message }}>
            {children}
        </SignalRContext.Provider>
    );
};

export const useSignalR = () => useContext(SignalRContext);