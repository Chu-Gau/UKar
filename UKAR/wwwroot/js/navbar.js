$(document).ready(function () {
    $("nav li").click(function () {
        Navbar.select(this, Navbar.globalCallback);
    });
});

var Navbar = {
    globalCallback: function () { },
    select: function (item, callback) {
        $("nav").find(".selected").removeClass("selected");
        $(item).addClass("selected");
        $(item).parent().children("span").addClass("selected");
        if (typeof callback === "function") {
            callback($(item).attr("id"));
        }
    }
}