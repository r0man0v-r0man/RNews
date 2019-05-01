"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/GeneratePasswordHub").build();


connection.on("sendPassword", function (randomPassword) {
    document.getElementById("auto-password").innerHTML = "Generated password:&nbsp;&nbsp;&nbsp;" + "<kbd>" + randomPassword + "</kbd>";
});

connection.start()
    .then(function () {
        console.log("connection started");
        document.getElementById("generate-password").onclick = function () {
            connection.invoke("send");
        }
        
    })
    .catch(error => {
        console.error(error.message);
    });
