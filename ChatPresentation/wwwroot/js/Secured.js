////$(document).ready(() => {
////    const connection = new signalR.HubConnectionBuilder()
////        .withUrl("https://localhost:5001/chatHub", {
////            skipNegotiation: true,
////            transport: signalR.HttpTrasnsportType.WebSockets
////        })
////        .configureLogging(signalR.LogLevel.Information)
////        .build();
////    connection.start();
////});

$(document).ready(() => {

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:5001/chatHub", {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();
    
});