$(document).ready(function () {
    Navbar.globalCallback = Admin.actionByTab;
    $("#user-mng").trigger("click");
});

var Admin = {
    actionByTab: function (tabID) {
        switch (tabID) {
            case "user-mng":
                Admin.startUserMng();
                break;
            default:
        }
    },
    toolbarByTab: function (tabID) {
        switch (tabID) {
            case "user-mng":
                Admin.startUserMng();
                break;
            default:
        }
    },
    startUserMng: function () {
        Table.create("user-mgr-table", ".container .nav-container .content", "id", function () {
            DataHelper.getAllUsers(function (data) {
                Table.bindData(data.data, "user-mgr-table", Admin.eventUserMng)
            });
        })

    },
    eventUserMng: function () {
        $("#license").off("click");
        $("#test-date").off("click");
        $("#driving-test").off("click");

        $("#license").on("click", function () {
            var userId = $("#user-mgr-table").find(".selected").attr("dataid");
            if (userId) {
                $.ajax({
                    url: '/user/license/' + userId,
                    dataType: 'json',
                    success: function (data) {
                        Dialog.create(data.data, userId, "DialogLicense", function () {
                            Admin.eventUserMngOnDialog(userId);
                            //if (data.data) {
                            //    $(".dialog[dataid = " + userId + "] .btn-action").hide();
                            //} else {
                            //    $(".dialog[dataid = " + userId + "] .btn-action").show();
                            //}
                        });
                    },
                });
            }
            
        });

        $("#test-date").on("click", function () {
            var userId = $("#user-mgr-table").find(".selected").attr("dataid");
            var dat = Table["tableuser-mgr-table"].data;

            for (i = 0; i < dat.length; i++) {
                if (dat[i].id === userId) {
                    Dialog.create(dat[i], userId, "DialogTestDate", function () {
                        Admin.eventUserMngOnTestDateDialog(userId);
                        //if (data.data) {
                        //    $(".dialog[dataid = " + userId + "] .btn-action").hide();
                        //} else {
                        //    $(".dialog[dataid = " + userId + "] .btn-action").show();
                        //}
                    });
                    break;
                }
            }
        });

        $("#driving-test").on("click", function () {
            var userId = $("#user-mgr-table").find(".selected").attr("dataid");
            if (userId) {
                $.ajax({
                    url: '/user/driving-test/' + userId,
                    dataType: 'json',
                    success: function (data) {
                        Dialog.create(data.data, userId, "DialogDrivingTest", function () {
                            Admin.eventUserMngOnDrivingTestDialog(userId);
                        });
                    },
                });
            }
        });
    },
    eventUserMngOnDialog: function (userId) {
        $(".dialog[dataid = " + userId + "] .btn-action").click(function (event) {
            event.preventDefault();
            var dialog = $(this).closest(".dialog");
            if (Dialog.checkEmpty(dialog, "licenseNumber") && Dialog.checkEmpty(dialog, "image") && Dialog.checkEmpty(dialog, "imageBack"))
                getBase64(dialog.find('#image').get()[0].files[0], function (base64Image) {
                    getBase64(dialog.find('#image-back').get()[0].files[0], function (base64BackImage) {
                        $.ajax({
                            method: 'POST',
                            url: '/user/license',
                            dataType: 'json',
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json'
                            },
                            data: JSON.stringify({
                                "DriverId": userId,
                                "LicenseNumber": dialog.find('#license-number').val(),
                                "Image": base64Image,
                                "ImageBack": base64BackImage,
                            }),
                            success: function (result) {
                                Dialog.remove(event.target);
                            },
                            error: function () {
                                alert("Lỗi!");
                            }
                        });
                    });
                });

        });

        $(".dialog[dataid = " + userId + "] input[type=file]").change(function () {
            var input = $(this);
            getBase64(this.files[0], function (base64String) {
                input.css("background-image", 'url(' + base64String + ')');
            });
        });
    },
    eventUserMngOnTestDateDialog: function (userId) {
        $(".dialog[dataid = " + userId + "] .btn-action").click(function (event) {
            event.preventDefault();
            var dialog = $(this).closest(".dialog");
            if (Dialog.checkEmpty(dialog, "testTime"))
                $.ajax({
                    method: 'POST',
                    url: '/user/update-test-date',
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify({
                        id: userId,
                        testTime: dialog.find("#test-date").val()
                    }),
                    success: function (result) {
                        Dialog.remove(event.target);
                        Admin.startUserMng();
                    },
                    error: function () {
                        alert("Lỗi!");
                    }
                });
        });
    },
    eventUserMngOnDrivingTestDialog: function (userId) {
        $(".dialog[dataid = " + userId + "] .btn-action").click(function (event) {
            event.preventDefault();
            var dialog = $(this).closest(".dialog");
            if (Dialog.checkEmpty(dialog, "date") && Dialog.checkEmpty(dialog, "examScore") && Dialog.checkEmpty(dialog, "practiceScore"))
                $.ajax({
                    method: 'POST',
                    url: '/user/driving-test',
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify({
                        userId: userId,
                        date: dialog.find("#test-date").val(),
                        examScore: dialog.find("#exam-score").val(),
                        practiceScore: dialog.find("#prac-score").val()
                    }),
                    success: function (result) {
                        Dialog.remove(event.target);
                        Admin.startUserMng();
                    },
                    error: function () {
                        alert("Lỗi!");
                    }
                });
        });
    },


}



function getBase64(file, callback) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
        if (typeof callback === "function") {
            callback(reader.result);
        }
    };
    reader.onerror = function (error) {
        console.log('Error: ', error);
    };
}
