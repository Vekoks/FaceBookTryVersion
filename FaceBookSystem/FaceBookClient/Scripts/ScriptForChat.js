
var rooms = [];

$("#UserName").on("click", "#specifik", function (e) {
    var userName = $(this).find("#User").text();

    var divForChatWithFRiend = $("#Chat").find("#ChatWithUser");

    divForChatWithFRiend.text("");

    divForChatWithFRiend.append('<a href="/DetaialUser/Details/' + userName + '">' + userName + '</a>');
});

$(document).ready(function () {

    $.connection.hub.start();

    var chat = $.connection.chat;

    $("#UserNameDrobDown").change(function () {
        $('#ChatWithUser').text("");

        var userName = $('#UserNameDrobDown option:selected').text();

        $('#ChatWithUser').append('<h1>' + userName + '</h1>');
    });

    $('#message').keyup(function (e) {
        if (e.keyCode == 13) {
            var username = $('#UserNameDrobDown option:selected').text();

            var msg = $('#message').val();

            chat.server.sendMessage(username, msg);

            $('#message').val("");
        }
    });

    $('#send-message').click(function () {
        var username = $('#UserNameDrobDown option:selected').text();

        var msg = $('#message').val();

        chat.server.sendMessage(username, msg);

    });

    $("#join-room").click(function () {

        var room = $('#room').val();

        chat.server.joinRoom(room)
    });

    $('#send-message-to-room').click(function () {

        var msg = $('#room-message').val();

        chat.server.sendMessageToRoom(msg, rooms);
    });

    chat.client.addMessage = addMessage;
    chat.client.joinRoom = joinRoom;
});

function addMessage(message) {
    debugger;
    $('#messages').append('<div>' + message + '</div>');
}

function joinRoom(room) {
    rooms.push(room);
    $('#currentRooms').append('<div>' + room + '</div>');
}
