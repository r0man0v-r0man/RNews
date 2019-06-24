"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/UserPropertyHub").build();
var connectionAvatar = new signalR.HubConnectionBuilder().withUrl("/UserAvatarHub").build();

var userId = document.getElementById("PropertyViewModelId");


var nameArea = document.getElementById("name");
var nameButtons = document.getElementById("name-buttons");
var nameEditButton = document.getElementById("name-edit-button");
var nameSubmitButton = document.getElementById("name-submit-button");
var nameField = document.getElementById("user-property-name");
nameArea.addEventListener("mouseenter", function () {
    nameButtons.style.display = "inline-block";
});
nameArea.addEventListener("mouseleave", function () {
    nameButtons.style.display = "none";
});
nameEditButton.addEventListener("click", function () {
    nameField.setAttribute("contenteditable", "true");
    nameField.classList.add("single-line");
    nameField.focus();
    nameButtons.style.display = "inline-block";
});
nameField.onblur = function () {
    if (!nameField.textContent) {
        document.execCommand("undo");
    };
    nameButtons.style.display = "inline-block";
    nameField.setAttribute("contenteditable", "false");
};
nameField.onfocus = function () {
    nameButtons.style.display = "inline-block";
};
nameSubmitButton.addEventListener("click", function () {
    nameButtons.style.display = "none";
    if (!nameField.textContent) {
        document.execCommand("undo");
    };
    nameField.setAttribute("contenteditable", "false");
    connection.invoke("NameChange", nameField.textContent, userId.value)
        .catch(function (err) {
            return console.error(err.toString());
        });
});

connection.on("NameChange", function (changedName, status) {
    nameField.innerText = changedName;
    alertify.success(status);
});

connection.start()
    .then(function () {
        console.log("connection started");
    })
    .catch(error => {
        console.error(error.message);
    });

var descriptionArea = document.getElementById("description");
var descriptionButtons = document.getElementById("description-buttons");
var descriptionEditButton = document.getElementById("description-edit-button");
var descriptionField = document.getElementById("user-property-description");
descriptionEditButton.onclick = function () {
    descriptionField.focus();
}

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


