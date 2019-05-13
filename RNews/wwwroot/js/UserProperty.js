"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/UserPropertyHub").build();
connection.on("UserPropertySend", function (name, email) {
    document.getElementById("user-property-name").value = name;
    document.getElementById("user-name").innerText = name;
    document.getElementById("user-property-email").value = email;
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
        var email = document.getElementById("user-property-email").value;
        connection.invoke("UserProperty", name, email, userId)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    }
});
document.getElementById("user-property-email").addEventListener("keypress", function (event) {
    var key = event.which || event.keyCode;
    if (key === 13) {
        var userId = document.getElementById("PropertyViewModelId").value;
        var name = document.getElementById("user-property-name").value;
        var email = document.getElementById("user-property-email").value;
        connection.invoke("UserProperty", name, email, userId)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    }
});
document.getElementById("user-property-btn").addEventListener("click", function () {
        var userId = document.getElementById("PropertyViewModelId").value;
        var name = document.getElementById("user-property-name").value;
        var email = document.getElementById("user-property-email").value;
        connection.invoke("UserProperty", name, email, userId)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }
);
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
function doCheck() {
    var allFilled = true;
    var name = document.getElementById("user-property-name").value;
    var e = document.getElementById("user-property-email").value;
    var inputs = document.getElementsByTagName('input');
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "text" && inputs[i].value == "")  {
            allFilled = false;
            break;
        }
    }

    document.getElementById("user-property-btn").disabled = !allFilled;
}

window.onload = function () {
    var inputs = document.getElementsByTagName('input');
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "text") {
            inputs[i].onkeyup = doCheck;
            inputs[i].onblur = doCheck;
        }
    }
};