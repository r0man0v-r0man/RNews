"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/AddCategoryHub").build();

var buttonCreateCategory = document.getElementById("btn-create-category");
var inputNameForNewCategory = document.getElementById("new-category");


connection.on("RecieveCategory", function (categoryId, name) {
    var tr = document.createElement("tr");
    var a = "/Admin/DeleteCategory/" + categoryId;
    tr.innerHTML = "<td>" + name + "</td><td><a class='btn btn-sm btn-danger' href=" + a + ">Delete</a></td>";
    document.querySelector("tbody").appendChild(tr);
    console.log(categoryId);
    console.log(name);
})

connection.start()
    .then(function () {
        console.log("connection addcategory started");
    })
    .catch(error => {
        console.error(error.message);
    });


buttonCreateCategory.addEventListener("click", function (event) {
    var inputName = inputNameForNewCategory.value;
    connection.invoke("AddCategory", inputName)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
    inputNameForNewCategory.value = "";
})