$(document).ready(function () {
    $(".rating").rating();
    $("#star-post").click(rateMyPost)
});
"use strict";
var connectionRating = new signalR.HubConnectionBuilder().withUrl("/RatingPostHub").build();

connectionRating.on("RecieveRating", function (averageRating, ratingCount) {
    
    console.log(averageRating);
    console.log(ratingCount);
});

connectionRating.start()
    .then(function () {
        console.log("connection rating started");
    })
    .catch(error => {
        console.error(error.message);
    });
function rateMyPost() {
    var postId = document.getElementById("post-id").value;
    var ratingPost = document.getElementById("post-rating").value;
    connectionRating.invoke("Rating", ratingPost, postId)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
}
