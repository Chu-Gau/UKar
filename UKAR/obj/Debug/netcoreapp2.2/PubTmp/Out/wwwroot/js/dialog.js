
var Dialog = {
    create: function (data, dialogId, dialogName, callback) {
        if (!Dialog["dialog" + dialogName]) {
            Dialog["dialog" + dialogName] = {};
        }
        var dialog = Dialog["dialog" + dialogName];
        if (!dialog.template) {
            $.ajax({
                url: '/shared/' + dialogName,
                success: function (result) {
                    dialog.template = result;
                    Dialog.create(data, dialogId, dialogName, callback);
                }
            });
        } else {
            var newDialog = $(dialog.template);
            newDialog.find(".dialog").attr("dataid", dialogId);
            $("body").append(newDialog);
            Dialog.bind(data, newDialog.find(".dialog"));
            Dialog.bindEvents(newDialog);
            if (typeof callback === "function") {
                callback();
            }
        }
    },
    bind: function (data, dialog) {
        dialog = $(dialog);
        if (data) {
            //dialog.find("input[type=text]").attr("readonly", "true");
            //dialog.find("input[type=file]").attr("disabled", "true");

            dialog.find("input[type=text], input[type=number]").each(function (index, elem) {
                $(elem).val(data[$(elem).attr("dataindex")]);
            });

            dialog.find("input[type=date]").each(function (index, elem) {
                var datestring = data[$(elem).attr("dataindex")];
                if (datestring) {
                    var date = new Date(datestring);
                    $(elem).val(formatDate(date));
                }
            });

            dialog.find("input[type=file]").each(function (index, elem) {
                $(elem).css("background-image", 'url(' + data[$(elem).attr("dataindex")] + ')');
            });
        } else {

        }
    },
    bindEvents: function (dialog) {
        dialog = $(dialog);
        dialog.find(".btn-cancel").click(function (event) {
            event.preventDefault();
            Dialog.remove(this);
        })   
    },
    remove: function (dialog) {
        $(dialog).parents(".dialog-cover").remove();
    },
    checkEmpty: function(dialog, dataindex) {
        var input = $(dialog).find("input[dataindex=" + dataindex + "]");
        if (!input.val()) {
            var label = $(dialog).find("label[for=" + input.attr("id") + "]");
            Tooltip.showTooltipOnInput(input, label.text() + " không được bỏ trống!")
            return false;
        } else {
            Tooltip.hideTooltipOnInput();
            return true;
        }
    }
}




