"use strict";
var connectionTheme = new signalR.HubConnectionBuilder().withUrl("/ThemeHub").build();

connectionTheme.start()
    .then(function () {
        console.log("connection theme started");
    })
    .catch(error => {
        console.error(error.message);
    });


connectionTheme.on("NewTheme", function (newTheme) {
    var link = document.getElementById("css-link");
    link.setAttribute("href", newTheme);
    console.log(newTheme);
});

document.getElementById("theme").addEventListener("click", function () {
    connectionTheme.invoke("SetTheme")
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

