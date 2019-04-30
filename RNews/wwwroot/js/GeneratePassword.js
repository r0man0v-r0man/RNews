"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/GeneratePasswordHub").build();

connection.on("sendPassword", function (randomPassword) {
    console.log(randomPassword);
    
});

connection.start()
    .then(function () {
        console.log("connection started");
        connection.invoke("send");
    })
    .catch(error => {
        console.error(error.message);
    });