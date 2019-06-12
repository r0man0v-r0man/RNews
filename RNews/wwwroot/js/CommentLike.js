"use strict";
var connectionLike = new signalR.HubConnectionBuilder().withUrl("/CommentLikeHub").build();

connectionLike.on("CommentLikes", function (likesCount, currentUserLikeValue, commentId) {
    document.getElementById("comment-heart-" + commentId + "-counter").innerText = likesCount;
    document.getElementById("comment-" + commentId + "-is-like-value").value = currentUserLikeValue;
    
    if (currentUserLikeValue == true) {
        document.getElementById("comment-" + commentId + "-is-like-value").closest("li").getElementsByTagName("i")[0].classList.add("fas");
        };
    if (currentUserLikeValue == false) {
        document.getElementById("comment-" + commentId + "-is-like-value").closest("li").getElementsByTagName("i")[0].classList.remove("fas");
        document.getElementById("comment-" + commentId + "-is-like-value").closest("li").getElementsByTagName("i")[0].classList.add("far");

        };

    
    console.log(likesCount);
    console.log(currentUserLikeValue);
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
        item.style.color = "#dc3545";
    });
    item.addEventListener("mouseout", function () {
        item.style.color = "#212529";
    });
    if (item.closest("li").getElementsByTagName("input")[2].value == "true") {
        item.classList.add("fas");
    };
    if (item.closest("li").getElementsByTagName("input")[2].value == "false") {
        item.classList.add("far");
    };
    
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