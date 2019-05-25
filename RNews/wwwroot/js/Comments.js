"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/CommentHub").build();
connection.on("ContentComment", function (content, userName) {
    var li = document.createElement("li");
    li.classList.add("list-group-item", "list-group-item-primary");
    li.innerHTML = "<div class='row'><div class='col-12'><small class='text-muted'>" + userName + "</small></div><div class='col-12'>" + content + "</div></div>";
    document.getElementById("messagesList").appendChild(li);
    document.getElementById("messagesList").lastElementChild.scrollIntoView();
    console.log(content);
    console.log(userName);
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
