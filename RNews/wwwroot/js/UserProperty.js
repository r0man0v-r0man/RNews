"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/UserPropertyHub").build();
var connectionAvatar = new signalR.HubConnectionBuilder().withUrl("/UserAvatarHub").build();
//user avatar
connectionAvatar.on("UserAvatarSend", function (data) {
    var avatar = document.getElementById("user-avatar");
    avatar.src = data;
    //document.getElementById("src-image").innerText = data;
    console.log(data);
});

connectionAvatar.start()
    .then(function () {
        console.log("connection avatar started");
    })
    .catch(error => {
        console.error(error.message);
    });

//document.getElementById("user-avatar-download-btn").addEventListener("click", function (event) {
//    var userId = document.getElementById("PropertyViewModelId").value;
//    connectionAvatar.invoke("UserAvatarRecieve",  userId)
//        .catch(function (err) {
//            return console.error(err.toString());
//        });
//    event.preventDefault();
//});
//user properties
connection.on("UserPropertySend", function (description, name) {
    document.getElementById("user-property-name").value = name;
    document.getElementById("user-property-description").value = description;
});
connection.start()
    .then(function () {
        console.log("connection started");
    })
    .catch(error => {
        console.error(error.message);
    });

document.getElementById("user-property-name").addEventListener("keypress",async function (event) {
    var key = event.which || event.keyCode;
    if (key === 13) {
        var userId = document.getElementById("PropertyViewModelId").value;
        var name = document.getElementById("user-property-name").value;
        var description = document.getElementById("user-property-description").value;
        await connection.invoke("UserProperty", description, name, userId)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    }
});

document.getElementById("user-property-description").addEventListener("keypress", function (event) {
    var key = event.which || event.keyCode;
    if (key === 13) {
        var userId = document.getElementById("PropertyViewModelId").value;
        var name = document.getElementById("user-property-name").value;
        var description = document.getElementById("user-property-description").value;
        connection.invoke("UserProperty", description, name, userId)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    }
});
document.getElementById("user-property-btn").addEventListener("click", function (event) {
    var userId = document.getElementById("PropertyViewModelId").value;
    var name = document.getElementById("user-property-name").value;
    var description = document.getElementById("user-property-description").value;
    connection.invoke("UserProperty", description, name, userId)
            .catch(function (err) {
                return console.error(err.toString());
            });
    event.preventDefault();
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
//function doCheck() {
//    var allFilled = true;
//    var inputs = document.getElementsByTagName('input');
//    for (var i = 0; i < inputs.length; i++) {
//        if (inputs[i].type == "text" && inputs[i].value == "")  {
//            allFilled = false;
//            break;
//        }
//    }

//    document.getElementById("user-property-btn").disabled = !allFilled;
//}

//window.onload = function () {
//    var inputs = document.getElementsByTagName('input');
//    for (var i = 0; i < inputs.length; i++) {
//        if (inputs[i].type == "text") {
//            inputs[i].onkeyup = doCheck;
//            inputs[i].onblur = doCheck;
//        }
//    }
//};
