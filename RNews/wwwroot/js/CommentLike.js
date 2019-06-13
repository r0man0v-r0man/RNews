"use strict";
var connectionLike = new signalR.HubConnectionBuilder().withUrl("/CommentLikeHub").build();

connectionLike.on("CommentLikes", function (likesCount, currentUserLikeValue, commentId) {
    document.getElementById("comment-heart-" + commentId + "-counter").innerText = likesCount;
    //document.getElementById("comment-" + commentId + "-is-like-value").value = currentUserLikeValue;
    
    //if (document.getElementById("comment-" + commentId + "-is-like-value").value == "true") {
    //    document.getElementById("comment-" + commentId + "-is-like-value").closest("li").getElementsByTagName("i")[0].classList.add("fas");
    //    };
    //if (document.getElementById("comment-" + commentId + "-is-like-value").value == "false") {
    //    document.getElementById("comment-" + commentId + "-is-like-value").closest("li").getElementsByTagName("i")[0].classList.remove("fas");
    //    document.getElementById("comment-" + commentId + "-is-like-value").closest("li").getElementsByTagName("i")[0].classList.add("far");

    //    };

    
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
    if (item.closest("li").getElementsByTagName("input")[2].value == "true") {
        item.classList.add("fas");
        //add solid class when hover
    };
    if (item.closest("li").getElementsByTagName("input")[2].value == "false") {
        item.classList.add("far");
    };
    
    item.addEventListener("click", function (event) {
        var userId = document.getElementById("user-id").value;
        var commentId = item.closest("li").getElementsByTagName("input")[1].value;
        var isLike = item.closest("li").getElementsByTagName("input")[2].value;
        connectionLike.invoke("Comment", commentId, userId, isLike)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    });
});

function heart(commentId) {
    var newCommentId = document.getElementById("comment-heart-" + commentId).closest("li").getElementsByTagName("input")[0].value;
    var userId = document.getElementById("user-id").value;
    var isLike = document.getElementById("comment-heart-" + commentId).closest("li").getElementsByTagName("input")[2].value;
    connectionLike.invoke("Comment", newCommentId, userId, isLike)
        .catch(function (err) {
            return console.error(err.toString());
        });
};