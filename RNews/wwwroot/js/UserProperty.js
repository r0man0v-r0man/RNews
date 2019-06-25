"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/UserPropertyHub").build();
var connectionAvatar = new signalR.HubConnectionBuilder().withUrl("/UserAvatarHub").build();

var userId = document.getElementById("PropertyViewModelId");


var nameArea = document.getElementById("name");
var nameButtons = document.getElementById("name-buttons");
var nameEditButton = document.getElementById("name-edit-button");
var nameSubmitButton = document.getElementById("name-submit-button");
var nameField = document.getElementById("user-property-name");
var nameError = document.getElementById("name-error");
var existName = nameField.value;

var descriptionArea = document.getElementById("description");
var descriptionButtons = document.getElementById("description-buttons");
var descriptionEditButton = document.getElementById("description-edit-button");
var descriptionSubmitButton = document.getElementById("description-submit-button");
var descriptionField = document.getElementById("user-property-description");
var existDescription = descriptionField.value;


nameArea.onmouseenter = function () {
    nameButtons.style.display = "inline-block";
};
nameArea.onmouseleave = function () {
    nameButtons.style.display = "none";
};
nameField.onblur = function () {
    if (!nameField.value) {
        nameField.value = existName;
    };
};
nameEditButton.onclick = function () {
    nameField.focus();
};
nameSubmitButton.onclick = function () {
    nameButtons.style.display = "none";
    existName = nameField.value;
    connection.invoke("NameChange", nameField.value, userId.value)
        .catch(function (err) {
            return console.error(err.toString());
        });
};
connection.on("NameChange", function (changedName, status) {
    alertify.success(status);
    nameField.value = changedName;
    nameError.innerText = status;
});

connection.start()
    .then(function () {
        console.log("connection started");
    })
    .catch(error => {
        console.error(error.message);
    });


descriptionArea.onmouseenter = function () {
    descriptionButtons.style.display = "inline-block";
};
descriptionArea.onmouseleave = function () {
    descriptionButtons.style.display = "none";
};
descriptionField.onblur = function () {
    if (!descriptionField.value) {
        descriptionField.value = existDescription;
    };
};
descriptionEditButton.onclick = function () {
    descriptionField.focus();
};
descriptionSubmitButton.onclick = function () {
    descriptionButtons.style.display = "none";
    existDescription = descriptionField.value;
    connection.invoke("DescriptionChange", descriptionField.value, userId.value)
        .catch(function (err) {
            return console.error(err.toString());
        });
};
connection.on("DescriptionChange", function (changedDescription, status) {
    alertify.success(status);
    descriptionField.value = changedDescription;
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


