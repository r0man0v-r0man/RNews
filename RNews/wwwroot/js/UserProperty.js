"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/UserPropertyHub").build();
var connectionAvatar = new signalR.HubConnectionBuilder().withUrl("/UserAvatarHub").build();

var userId = document.getElementById("PropertyViewModelId");


var mailArea = document.getElementById("mail");
var mailButtons = document.getElementById("email-buttons");
var mailEditButton = document.getElementById("edit-button");
var mailSubmitButton = document.getElementById("submit-button");
var emailField = document.getElementById("user-property-email");
mailArea.addEventListener("mouseenter", function () {
    mailButtons.style.display = "inline-block";
});
mailArea.addEventListener("mouseleave", function () {
    mailButtons.style.display = "none";
});
mailEditButton.addEventListener("click", function () {
    emailField.setAttribute("contenteditable", "true");
    emailField.classList.add("single-line");
    emailField.focus();
    mailButtons.style.display = "inline-block";
});
emailField.onblur = function () {
    if (!emailField.textContent) {
        document.execCommand("undo");
    };
    mailButtons.style.display = "inline-block";
    emailField.setAttribute("contenteditable", "false");
};
emailField.onfocus = function () {
    mailButtons.style.display = "inline-block";
};
mailSubmitButton.addEventListener("click", function () {
    mailButtons.style.display = "none";
    if (!emailField.textContent) {
        document.execCommand("undo");
    };
    emailField.setAttribute("contenteditable", "false");
    connection.invoke("EmailChange", emailField.textContent, userId.value)
        .catch(function (err) {
            return console.error(err.toString());
        });
});

connection.on("EmailChange", function (changedEmail) {
    emailField.innerText = changedEmail;
    alertify.success("Success!", "success", 1);
    alertify.delay(1);
});
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