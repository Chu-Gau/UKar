$(document).ready(function () {
    $("#login").on("click", Login.login);
});

var Login = {
    login: function (event) {
        event.preventDefault();
        $.ajax({
            method: 'POST',
            url: '/login',
            dataType: 'json',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify({
                "username": $('#user-name').val(),
                "password": $('#password').val()
            }),
            success: function (result) {
                if (result.data.succeeded) {
                    window.location.href = "/";
                }
            },
            error: function () {
                Tooltip.showTooltipOnInput("#user-name", "Tên đăng nhập hoặc mật khẩu không đúng!");
            }
        });
    }
}