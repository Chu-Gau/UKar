// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var action = 1
//0 - not sent, 1 - sent
var url;
var method;
var jsonRequest;

var jsonRequestCodeMirror;


var urlPattern = new RegExp('https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)'); // fragment locator

$(document).ready(function () {
    $("#json-request").focus(function () {
        action = 0;
    })

    $("#json-response").focus(function () {
        if(validate() && !action){
            sendRequest();
            action = 1;
        }
    })

});

function validate() {
    url = $("#url").val();
    method = $("input[name='method']:checked").val();
    jsonRequest = $("#json-request").val()

    return true ;
}

function sendRequest() {
    $.ajax({
        url: url,
        type: method,
        dataType: 'JSON',
        data: JSON.parse(jsonRequest)
    }).done(function (result) {
        ajaxCallback(result);
    });
}

function ajaxCallback(data) {
    $("#json-response").val(JSON.stringify(data));
}

function IsJsonString(str) {
    if (str == "") return true;
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}