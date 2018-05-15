$(function () {

    $('#chatBody').hide();
    $('#loginBlock').show();

    // Reference to hub
    var chat = $.connection.chatHub;

    //Function will be raised when new message received
    chat.client.addMessage = function (name, message) {
        $('#chatroom').append('<p><i>' + htmlEncode(name)
            + '</i>: ' + htmlEncode(message) + '</p>');
    };

    // Function will be called on new user connection
    chat.client.onConnected = function (id, userName, allUsers) {

        $('#loginBlock').hide();
        $('#chatBody').show();

        $('#hdId').val(id);
        $('#username').val(userName);
        $('#greeting').html('<h4>Welcome, ' + userName + '</h4>');

        // Adding all users
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    }

    // Adding new user
    chat.client.onNewUserConnected = function (id, name) {

        AddUser(id, name);
    }

    // Removing user
    chat.client.onUserDisconnected = function (id, userName) {

        $('#' + id).remove();
    }

    // Openning connection
    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {
            //Calling method send from HUB
            chat.server.send($('#username').val(), $('#message').val());
            $('#message').val('');
        });

        $("#btnLogin").click(function () {

            var name = $("#txtUserName").val();
            if (name.length > 0) {
                chat.server.connect(name);
            }
            else {
                alert("Enter your name");
            }
        });
    });
});

// Encoding given text
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

//Client side handler for adding new user
function AddUser(id, name) {

    var userId = $('#hdId').val();

    if (userId != id) {

        $("#chatusers").append('<p id="' + id + '"><i>' + name + '</i></p>');
    }
}