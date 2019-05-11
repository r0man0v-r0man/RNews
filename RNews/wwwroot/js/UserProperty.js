"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/UserPropertyHub").build();
connection.on("UserProperty", function (myData) {
    document.getElementById("user-property-name").value = myData;
    document.getElementById("user-name").value = myData;
    console.log("i'm working");
});

connection.start()
    .then(function () {
        console.log("connection started");
    })
    .catch(error => {
        console.error(error.message);
    });

document.getElementById("user-property-btn").addEventListener("click", function (event) {
    var userId = document.getElementById("PropertyViewModelId").value;
    var name = document.getElementById("user-property-name").value;
    connection.invoke("UserPropertySend", name, userId)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

