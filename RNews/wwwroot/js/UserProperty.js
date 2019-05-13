"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/UserPropertyHub").build();
connection.on("UserProperty", function (myData) {
    document.getElementById("user-property-name").value = myData;
    document.getElementById("user-name").innerText = myData;
    console.log("i'm working");
});

connection.start()
    .then(function () {
        console.log("connection started");
    })
    .catch(error => {
        console.error(error.message);
    });

document.getElementById("user-property-name").addEventListener("keypress", function (event) {
    var key = event.which || event.keyCode;
    if (key === 13) {
        var userId = document.getElementById("PropertyViewModelId").value;
        var name = document.getElementById("user-property-name").value;
        connection.invoke("UserPropertySend", name, userId)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    }
});


$(document).ready(function () {

    $('input[type=text]').keypress(function (e) {
        if (e.keyCode == 13) {

            if ($(this).attr('class') === "last") {
                $('input[type=text]').eq(0).focus()
            } else {

                $('input[type=text]').closest('input[type=text]').focus();
            }
        }
    });
});