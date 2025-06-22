import { useEffect } from "react";
import { useSignalR } from "./providers/SignalRProvider";

export const BusyMessage: React.FC<{ setIsLocked: (locked: boolean) => void }> = ({ setIsLocked }) => {
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
