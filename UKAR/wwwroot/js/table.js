$(document).ready({
});

var Table = {
    create: function (tableID, parentSelector, primaryKey, callback) {
        if (!Table.tableTemplate) { // Lấy mẫu table HTML
            $.ajax({
                url: '/shared/table',
                success: function (result) {
                    Table.tableTemplate = result;
                    Table.create(tableID, parentSelector, primaryKey, callback);
                }
            });
        } else {
            var newTable = $(Table.tableTemplate);
            newTable.attr('id', tableID);
            $(parentSelector).html('');
            $(parentSelector).append(newTable);
            Table.ajustScrolling(tableID);
            if (!Table['table' + tableID]) {
                Table['table' + tableID] = {};
            }
            Table['table' + tableID].primaryKey = primaryKey;
            if (typeof callback === "function") {
                callback();
            }
        }
    },
    bindData: function (data, tableID, callback) {
        var tableData = Table['table' + tableID];
        tableData.data = data;
        if (data) {
            var tableBodyContent = $('#' + tableID + ' .body .body-content');
            tableBodyContent.html('');
            data.forEach(function (item) {
                var row = $('<div class="row"></div>');
                row.attr("dataid", item[Table['table' + tableID].primaryKey]);
                var title = $('<div></div>');
                for (var property in item) {
                    if (item.hasOwnProperty(property)) {
                        var rowItem = $('<div class="column"> <span> </span> </div>');
                        rowItem.attr('dataindex', property);
                        rowItem.find('span').text(item[property]);
                        row.append(rowItem);

                        if (title) {
                            var titleItem = rowItem.clone();
                            titleItem.find('span').text('');
                            title.append(titleItem);
                        }
                    }
                }
                if (title) {
                    $('#' + tableID).find('.title').html('');
                    $('#' + tableID).find('.title').append(title.children());
                    title = undefined;
                }
                tableBodyContent.append(row);
            });
            Table.rowClick(tableID);
            Table.applyTableTemplate(tableID, callback);
        }
    },
    applyTableTemplate: function (tableID, callback) {
        var tableData = Table['table' + tableID];
        if (!tableData.template) {
            $.ajax({
                url: '/js/table-template/' + tableID + ".json",
                success: function (result) {
                    tableData.template = result;
                    Table.applyTableTemplate(tableID);
                }
            });
        } else {
            var tableTemplate = tableData.template;
            var table = $('#' + tableID);

            table.find(".column").hide();

            var tableTitle = table.find('.title');

            for (var columnTemplate in tableTemplate) {
                var column = table.find('.column[dataindex = ' + columnTemplate + ']');
                column.attr('dataType', tableTemplate[columnTemplate].dataType?tableTemplate[columnTemplate].dataType:"string");
                column.css('width', tableTemplate[columnTemplate].width?tableTemplate[columnTemplate].width:250 + "px");
                column.css('text-align', tableTemplate[columnTemplate].align ? tableTemplate[columnTemplate].align : "left");
                var columnTitle = column.filter(".title *");
                //var columnTitle = tableTitle.find('.title .column[dataindex = ' + columnTemplate + ']');
                columnTitle.find("span").text(tableTemplate[columnTemplate].title);
                column.show();
            }
            Table.formatData(table.find(".body"));
        }

        if (typeof callback === "function") {
            callback();
        }
    },
    ajustScrolling: function(tableID) {
        var table = $('#' + tableID);
        table.find('.body').off('scroll');
        table.find('.body').on('scroll', function () {
            table.find('.header').scrollLeft(table.find('.body').scrollLeft());
        })
    },
    rowClick: function (tableID) {
        var table = $('#' + tableID);
        table.find(".body-content .row").click(function () {
            table.find(".body-content .row").removeClass("selected");
            $(this).addClass("selected");
        });

    },
    formatData: function(container) {
        container = $(container);

        //role
        container.find("[datatype=role] span").each(function (index, field) {
            field = $(field);
            if (field.text() == "Driver") field.text("Lái xe");
            else if (field.text() == "Employer") field.text("Chủ xe");
        });
        //driverteststate
        container.find("[datatype=driverteststate] span").each(function (index, field) {
            field = $(field);
            if (field.text() == "true") field.text("Đã sát hạch");
            else if (field.text() == "false") field.text("Chưa sát hạch");
        });
        //date
        container.find("[datatype=date] span").each(function (index, field) {
            field = $(field);
            if (field.text()) field.text(formatDateDisplay(field.text())); 
        });
    }

}