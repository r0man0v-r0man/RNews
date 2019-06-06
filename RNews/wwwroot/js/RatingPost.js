$(".my-rating").starRating({
    totalStars: 5,
    emptyColor: 'lightgray',
    hoverColor: 'orange',
    activeColor: 'gold',
    initialRating: 4,
    strokeWidth: 0,
    useGradient: false,
    starSize: 25,
    ratedColor: 'crimson',
    useFullStars: true,
    callback: function (currentRating, $el) {
        rateMyPost(currentRating);
        console.log('DOM element ', $el);
    }
});
"use strict";
var connectionRating = new signalR.HubConnectionBuilder().withUrl("/RatingPostHub").build();
var userId = document.getElementById("user-id").value;
var postId = document.getElementById("post-id").value;

connectionRating.on("RecieveRating", function (allRating) {
    //document.getElementById("all-rating").innerText = "Рейтинг: " + allRating;
    console.log(allRating);
});

connectionRating.start()
    .then(function () {
        console.log("connection rating started");
    })
    .catch(error => {
        console.error(error.message);
    });
function rateMyPost(currentRating) {
    //var ratingPost = document.getElementById("post-rating").value;
    connectionRating.invoke("Rating", currentRating, postId, userId)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
}
