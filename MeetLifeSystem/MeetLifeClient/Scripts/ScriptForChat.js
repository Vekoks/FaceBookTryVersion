$("#sidebar_secondary").on("click", "#Exit", function (e) {

    var nameOnButtonArr = $(this).attr('name').split(" ");
    var userName = nameOnButtonArr[1];

    var chatTableId = "#ChatWith" + userName;

    var $tbl = $("#sidebar_secondary").find(chatTableId).remove();
    //$tbl.empty();

});

$("#sidebar_secondary").on("click", "#send-message", function (e) {

    var chat = $.connection.chat;

    var nameOnButtonArr = $(this).attr('name').split(" ");
    var userName = nameOnButtonArr[1];

    var meesagesList = "#MessageFor" + userName;

    var msg = $(meesagesList).val();

    $(meesagesList).val("");

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

function addMessage(message, username, userNameLogged, stringDate) {
    var meesagesListOnSender = "#ConversationWith" + username;
    var meesagesListOnReceiver = "#ConversationWith" + userNameLogged;
    
    var pictureOnUserNameLogged = '#ProfilePictureLoggedUser';
    var pictureOnUsername = "#UserPicture" + userNameLogged;

    var imagesOnUserNameLogged = $(pictureOnUserNameLogged).attr('src');
    var imagesOnUsername = $(pictureOnUsername).attr('src');

    var controlUserNameLogged =
                    '<div class="chat_message_wrapper chat_message_right">' +
                       '<div class="chat_user_avatar">' +
                           '<img  alt="" src="' + imagesOnUserNameLogged + '" class="md-user-image">' +
                       '</div>' +
                   '<ul class="chat_message">' +
                           '<li>' +
                               '<p>' + message + '<span class="chat_message_time">' + stringDate + '</span></p>' +
                           '</li>' +
                       '</ul>' +
                       '</div>';

    var controlOnUsername = '<div class="chat_message_wrapper">' +
                           '<div class="chat_user_avatar">' +
                                   '<img alt="" src="' + imagesOnUsername + '" class="md-user-image">' +
                           '</div>' +
                       '<ul class="chat_message">' +
                               '<li>' +
                                   '<p>' + message + '<span class="chat_message_time">' + stringDate + '</span></p>' +
                               '</li>' +
                           '</ul>' +
                           '</div>';


    $(meesagesListOnSender).append(controlUserNameLogged);
    $(meesagesListOnReceiver).append(controlOnUsername);

    //scroll everytime down
    var chat = $('#ChatWith' + username)
    var scrollHeight = chat.find(meesagesListOnSender);
    var height = scrollHeight.height();

    chat.find('#chat').scrollTop(height);
}

function joinRoom(room) {
    rooms.push(room);
    $('#currentRooms').append('<div>' + room + '</div>');
}

