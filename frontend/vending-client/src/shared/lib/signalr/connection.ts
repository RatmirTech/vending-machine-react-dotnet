import * as signalR from '@microsoft/signalr';

const hubUrl = import.meta.env.VITE_SIGNALR_HUB_URL as string;

export const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .withAutomaticReconnect()
    .build();