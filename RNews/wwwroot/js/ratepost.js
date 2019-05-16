$(document).on("ready", function () {
    $("#input-1-ltr-star-xs").rating().on("rating:clear", function (event) {
        alert("Your rating is reset")
    }).on("rating:change", function (event, value, caption) {
        alert("You rated: " + value + " = " + $(caption).text());
    });
});
//чето не работает