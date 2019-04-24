var Tooltip = {
    showTooltipOnInput: function (input, msg) {
        Tooltip.hideTooltipOnInput();
        var tooltip = $("<div class='tooltip'></div>");
        tooltip.text(msg);
        $(input).parent().append(tooltip);
        var inputOffset = $(input).offset();
        tooltip.offset({ left: inputOffset.left + $(input).outerWidth() - tooltip.outerWidth(), top: inputOffset.top - tooltip.outerHeight() - 5 });
        tooltip.css('opacity', 1);
    },
    hideTooltipOnInput: function () {
        $(".tooltip").remove();
    }

}