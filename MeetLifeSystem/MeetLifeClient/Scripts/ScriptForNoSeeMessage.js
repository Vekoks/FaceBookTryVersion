function chatCreate(userName) {
    var $tbl = $("#sidebar_secondary");
    $tbl.empty();

    var control = '<div id="ChatWith' + userName + '">' +
       '<div class="popup-head">' +
   '<div class="popup-head-left pull-left">' +
   '<a design and developmenta href="/DetaialUser/Details/' + userName + '">' +
   '<img class="md-user-image" alt="' + userName + '" src="http://bootsnipp.com/img/avatars/bcf1c0d13e5500875fdd5a7e8ad9752ee16e7462.jpg">' +
   '<h1>' + userName + '</h1>' +
   '</a>' +
   '</div>' +
   '<div class="popup-head-right pull-right">' +
   '<button data-widget="remove" id="removeClass" class="chat-header-button pull-right" type="button"><i id="Exit" class="glyphicon glyphicon-remove" name="ExitChatWith ' + userName + '"></i></button>' +
   '</div>' +
   '</div>' +
   '<div id="chat" class="chat_box_wrapper chat_box_small chat_box_active" style="opacity: 1; display: block; transform: translateX(0px);">' +
   '<div id="ConversationWith' + userName + '" class="chat_box touchscroll chat_box_colors_a">' +
   '</div>' +
   '</div>' +
   '<div class="chat_submit_box">' +
   '<div class="uk-input-group">' +
   '<div class="gurdeep-chat-box">' +
   '<input type="text" placeholder="Type a message" id="MessageFor' + userName + '" name="submit_message" class="md-input">' +
   '<button id="send-message" name="ButtonFor ' + userName + '">Click</button>' +
   '</div>' +
   '</div>' +
   '</div>' +
   '</div>';

    $tbl.append(control);

    //load conversation
    $.ajax({
        url: "/Home/GetConversationWithUser",
        type: "POST",
        data: JSON.stringify({ UserName: userName.toString() }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var meesagesList = "#ConversationWith" + userName;

            var $con = $(meesagesList)
            $con.empty();
            var rowsCon = [];

            for (var i = 0; i < data.length; i++) {

                if (data[i].IsMe) {
                    var control =
                     '<div class="chat_message_wrapper chat_message_right">' +
                        '<div class="chat_user_avatar">' +
                            '<img id="ChatPitureOn' + data[i].UserName + '" alt="" src="' + data[i].Picture + '" class="md-user-image">' +
                        '</div>' +
                    '<ul class="chat_message">' +
                            '<li>' +
                                '<p>' + data[i].Letter + '<span class="chat_message_time">' + data[i].Date + '</span></p>' +
                            '</li>' +
                        '</ul>' +
                        '</div>';
                }
                else {
                    var control =
                         '<div class="chat_message_wrapper">' +
                            '<div class="chat_user_avatar">' +
                                    '<img id="ChatPitureOn' + data[i].UserName + '" alt="" src="' + data[i].Picture + '" class="md-user-image">' +
                            '</div>' +
                        '<ul class="chat_message">' +
                                '<li>' +
                                    '<p>' + data[i].Letter + '<span class="chat_message_time">' + data[i].Date + '</span></p>' +
                                '</li>' +
                            '</ul>' +
                            '</div>';
                }

                $con.append(control);
            }
        }
    });

    //delete notification of no seen message
    $.ajax({
        url: "/Home/DeletellNotificationForNoSeenMessage",
        type: "POST",
        data: JSON.stringify({ UserName: userName.toString() }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.status == "Success") {

            }
        }
    });
}

$(function () {

    // Proxy created on the fly
    var job = $.connection.noSeenMessageHub;

    // Declare a function on the job hub so the server can invoke it
    job.client.displayAllNoSeenMessage = function () {
        getDataForAllNoSeenMessage();
    };

    // Start the connection
    $.connection.hub.start();
    getDataForAllNoSeenMessage();
});


function getDataForAllNoSeenMessage() {
    var $tbl = $("#NoSeenMessage").find('#CountMissMessage');

    var $allUser = $('#AllMessageFromUsers');

    $.ajax({
        url: 'GetAllNotificationForNoSeenMessage',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $tbl.empty();

                var rows = [];

                rows.push(' <p class="text-success">' + data.length + '</p>');
                $tbl.append(rows.join(''));
                rows = [];

                for (var i = 0; i < data.length; i++) {
                    
                    if (data[i].IsOnline) {

                        var chatTableId = "#ChatWith" + data[i].FormUser;

                        var $tblChat = $("#Chat").find(chatTableId);

                        if ($tblChat.length == 0) {
                            chatCreate(data[i].FormUser);
                        }
                    }

                    else {
                        rows.push(' <div class="alert alert-dismissible alert-info" style="width:25%; text-align:center">')
                        rows.push(' <div id="ListWithMissMessage"> You have miss message from')
                        rows.push(' <p id="Sender" class="text-danger">' + data[i].FormUser + '</p>');
                        rows.push(' </div>')
                        rows.push(' </div>')
                    }
                }

                $allUser.append(rows.join(''));
            }
        }
    });


    $("#AllMessageFromUsers").on("click", "#ListWithMissMessage", function () {
        var userName = $(this).find("#Sender").text();
        
        chatCreate(userName);
    });
}