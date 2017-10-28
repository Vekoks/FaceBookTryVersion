$("#Chat").on("click", "#Exit", function (e) {

    var nameOnButtonArr = $(this).attr('name').split(" ");
    var userName = nameOnButtonArr[1];

    var chatTableId = "#ChatWith" + userName;

    var $tbl = $("#Chat").find(chatTableId);
    $tbl.empty();

});

$("#Chat").on("click", "#send-message", function (e) {

    var chat = $.connection.chat;

    var nameOnButtonArr = $(this).attr('name').split(" ");
    var userName = nameOnButtonArr[1];

    var meesagesList = "#MessageFor" + userName;

    var msg = $(meesagesList).val();

    //add no seen message
    $.ajax({
        url: "/Home/AddNewNoSeenMessage",
        type: "POST",
        data: JSON.stringify({ UserName: userName.toString(), Message: msg.toString() }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.status == "Success") {

            }
        }
    });

    chat.server.sendMessage(userName, msg);

});



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

function addMessage(message, username) {
    var meesagesList = "#ConversationWith" + username;

    $(meesagesList).append('<div>' + message + '</div>');
}

function joinRoom(room) {
    rooms.push(room);
    $('#currentRooms').append('<div>' + room + '</div>');
}

