var DataHelper = {
    ajax: function (url, data, method, callback) {
        $.ajax({
            method: method ? method : 'GET',
            url: url,
            data: data,
            success: callback
        });
    },
    getAllUsers: function(callback) {
        $.ajax({
            method: 'GET',
            url: '/user',
            dataType: 'json',
            success: callback
        });
    }
}