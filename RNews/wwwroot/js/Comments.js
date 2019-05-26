"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/CommentHub").build();
var button = document.getElementById("comment-submit");
var textarea = document.getElementById("comment-content");
if (textarea.value == "") {
    button.disabled = true;
}
connection.on("ContentComment", function (content, userName, dateOfCreated) {
    var li = document.createElement("li");
    li.classList.add("list-group-item", "list-group-item-primary");
    li.innerHTML = "<div class='row'><div class='col-12'><small class='text-muted'>" + userName + "&nbsp|&nbsp" + dateOfCreated + "</small></div><div class='col-12'>" + content + "</div></div>";
    document.getElementById("messagesList").appendChild(li);
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
//нормально не работает, и куда вставлять код, чтоб перебрасывало к новому посту автора?
textarea.addEventListener("keyup", function () {
    button.disabled = !this.value;
});

