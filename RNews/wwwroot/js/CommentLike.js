"use strict";
var connectionLike = new signalR.HubConnectionBuilder().withUrl("/CommentLikeHub").build();

connectionLike.on("CommentLikes", function (likesCount) {
    console.log(likesCount);
});

connectionLike.start()
    .then(function () {
        console.log("connection like started");
    })
    .catch(error => {
        console.error(error.message);
    });

var hearts = document.querySelectorAll(".comment-heart");

[].forEach.call(hearts, function (item) {
    item.addEventListener("mouseover", function () {
        item.classList.remove("far");
        item.classList.add("fas");
    });
    item.addEventListener("mouseout", function () {
        item.classList.remove("fas");
        item.classList.add("far");
    });
    item.addEventListener("click", function (event) {
        var userId = item.closest("li").getElementsByTagName("input")[0].value;
        var commentId = item.closest("li").getElementsByTagName("input")[1].value;
        var isLike = item.closest("li").getElementsByTagName("input")[2].value;
        connectionLike.invoke("Comment", commentId, userId, isLike)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    });
});