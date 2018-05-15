$(function () {
    var pushHub = $.connection.pushHub;

    pushHub.client.displayMessage = function (message) {
        $('#notification').html(message);
    };

    $.connection.hub.start();
});