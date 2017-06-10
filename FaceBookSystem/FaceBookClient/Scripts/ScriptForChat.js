
var rooms = [];


$(document).ready(function () {

    $.connection.hub.start();

    var chat = $.connection.chat;

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
    $('#messages').append('<div>' + message + '</div>');
}

function joinRoom(room) {
    rooms.push(room);
    $('#currentRooms').append('<div>' + room + '</div>');
}

