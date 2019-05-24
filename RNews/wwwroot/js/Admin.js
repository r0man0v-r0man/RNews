$(document).ready(function () {
    $("#what-search").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#where-search tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});