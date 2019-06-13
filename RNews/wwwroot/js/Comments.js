"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/CommentHub").build();


var button = document.getElementById("comment-submit");
var textarea = document.getElementById("comment-content");
if (textarea.value == "") {
    button.disabled = true;
}
connection.on("ContentComment", function (content, userName, dateOfCreated, commentId, userId, isLikeValue) {
    var li = document.createElement("li");
    var input = document.createElement("input");
    var input2 = document.createElement("input");
    var input3 = document.createElement("input");
    input.setAttribute("type", "hidden");
    input.setAttribute("name", "commentId");
    input.id = "comment-id";
    input.value = commentId;
    input2.setAttribute("type", "hidden");
    input2.setAttribute("name", "userId");
    input2.id = "user-id";
    input2.value = userId;
    input3.setAttribute("type", "hidden");
    input3.setAttribute("name", "isLikeValue");
    input3.id = "comment-" + commentId + "-is-like-value";
    input3.value = isLikeValue;
    li.classList.add("list-group-item", "list-group-item-primary");
    li.id = "comment-item-" + commentId;
    li.innerHTML = "<div class='row'><div class='col-10'><small class='text-muted'>" + userName + "&nbsp|&nbsp" + dateOfCreated + "</small></div><div class='col-2 hearts'><i id=" + "comment-heart-" + commentId + " onclick=" + "heart(" + commentId +")"+" class='fa-heart far comment-heart heart-item'></i><span id=" + "comment-heart-" + commentId + "-counter" + "> 0</span></div><div class='col-12'>" + content + "</div></div>";
    
    document.getElementById("messagesList").appendChild(li);
    document.getElementById("comment-item-" + commentId).appendChild(input);
    document.getElementById("comment-item-" + commentId).appendChild(input2);
    document.getElementById("comment-item-" + commentId).appendChild(input3);
    console.log(content);
    console.log(userName);
    console.log(commentId);
});

connection.start()
    .then(function () {
        console.log("connection comment started");
    })
    .catch(error => {
        console.error(error.message);
    });
button.addEventListener("click", function (event) {
    var userId = document.getElementById("user-id").value;
    var postId = document.getElementById("post-id").value;
    var content = document.getElementById("comment-content").value;
    connection.invoke("Comments", content, postId, userId)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
    document.getElementById("messagesList").lastElementChild.scrollIntoView();
    button.disabled = true;
    textarea.value = "";
});

textarea.addEventListener("keyup", function () {
    button.disabled = !this.value;
});

