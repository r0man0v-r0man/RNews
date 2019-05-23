"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/CommentHub").build();
connection.on("ContentComment", function (content) {
    document.getElementById("comment-box").innerText = content;
    console.log(content);
});

connection.start()
    .then(function () {
        console.log("connection comment started");
    })
    .catch(error => {
        console.error(error.message);
    });
document.getElementById("comment-submit").addEventListener("click", function (event) {
    var userId = document.getElementById("user-id").value;
    var postId = document.getElementById("post-id").value;
    var content = document.getElementById("comment-content").value;
    connection.invoke("Comments", content, postId, userId)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
}
);