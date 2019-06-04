$(function () {
    $("#tagcloud a").tagcloud();
});
$(function () {
    $("#tagcloud a").tagcloud({
        size: {
            start: 20,
            end: 30,
            unit: "px"
        },
    });
});
$(function () {
    $("#tagcloud a").tagcloud({
        color: {
            start: "#1c5866",
            end: "#661c49"
        }
    });
});