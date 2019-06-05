$(document).ready(function () {
    $(".rating").rating();
    $("#star-post").click(rateMyPost)
});
"use strict";
var connectionRating = new signalR.HubConnectionBuilder().withUrl("/RatingPostHub").build();
var userId = document.getElementById("user-id").value;
var postId = document.getElementById("post-id").value;

connectionRating.on("RecieveRating", function (userRating, allRating) {
    
    console.log(userRating);
    console.log(allRating);
});

connectionRating.start()
    .then(function () {
        console.log("connection rating started");
    })
    .catch(error => {
        console.error(error.message);
    });
function rateMyPost() {
    var ratingPost = document.getElementById("post-rating").value;
    connectionRating.invoke("Rating", ratingPost, postId, userId)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
}
