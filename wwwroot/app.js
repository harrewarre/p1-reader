import onDataReceived from "./app/on-data-received.js";

const connection = new signalR.HubConnectionBuilder().withUrl("/powerconsumption").build();
onDataReceived(connection);

connection.onClosed = e => {
    console.warn('Connection closed');
};

connection.start().catch(function (err) {
    return console.error(err.toString());
});
