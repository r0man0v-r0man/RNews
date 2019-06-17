"use strict";
//document.addEventListener("DOMContentLoaded", function () {
//    var currentTheme = getCookie("theme");
//    if (currentTheme == "") {
//        setCookie("theme", document.getElementById("css-link").getAttribute("href"), 7);
//        currentTheme = getCookie("theme");
//    };
//    var link = document.getElementById("css-link");
//    link.setAttribute("href", currentTheme);
//});

  
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}