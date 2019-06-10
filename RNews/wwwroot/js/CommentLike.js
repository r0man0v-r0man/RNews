"use strict";
var connectionLike = new signalR.HubConnectionBuilder().withUrl("/CommentLikeHub").build();

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
});

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
document.getElementById("comment-heart").addEventListener("click", function () {
    document.getElementById("comment-form-like").submit(function (event) {
        var userId = document.getElementById("user-id").value;
        var commentId = document.getElementById("comment-id").value;
        var isLike = document.getElementById("comment-is-like-value").value;
    });
});
//document.getElementById("").addEventListener("click", function (event) {
//    var userId = document.getElementById("user-id").value;
//    var postId = document.getElementById("post-id").value;
//    var content = document.getElementById("comment-content").value;
//    connectionLike.invoke("Comments", content, postId, userId)
//        .catch(function (err) {
//            return console.error(err.toString());
//        });
//    event.preventDefault();
//    document.getElementById("messagesList").lastElementChild.scrollIntoView();
//    button.disabled = true;
//    textarea.value = "";
//});