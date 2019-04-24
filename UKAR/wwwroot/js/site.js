// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(document).ready(function () {
    $(".logout").click(Site.logout);
});

var Site = {
    logout: function (event) {
        event.preventDefault();
        $.ajax({
            method: 'GET',
            url: '/logout',
            dataType: 'json',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            complete: function (result) {
                window.location.href = "/";
            }
        });
    }
}