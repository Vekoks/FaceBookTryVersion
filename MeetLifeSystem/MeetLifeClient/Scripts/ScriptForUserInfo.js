//live list on users 
$(function () {
    // Proxy created on the fly
    var job = $.connection.userHub;

    // Declare a function on the job hub so the server can invoke it
    job.client.displayUser = function () {
        getDataUserInfo();
    };

    // Start the connection
    $.connection.hub.start();
    getDataUserInfo();
});


function getDataUserInfo() {
    var $tbl = $('#UsersList');
    $.ajax({
        url: 'ResultInfoForUsers',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $tbl.empty();
                var rows = [];
                for (var i = 0; i < data.length; i++) {
                    rows.push(' <li id="specifik" class="list-group-item">');
                    rows.push(' <div id="User" class="btn btn-success">' + data[i].Name + '</div>');
                    if (data[i].IsOnline) {
                        rows.push(' <div class="label label-info">Online</div>');
                    }
                    rows.push(' </li>');
                }

                $tbl.append(rows.join(''));
            }
        }
    });
}

//for chat
$("#UsersList").on("click", "#specifik", function (e) {
    var userName = $(this).find("#User").text();

    var $tbl = $("#sidebar_secondary");
    //$tbl.empty();

    var control = '<div id="ChatWith' + userName + '" class="Test">' +
        '<div class="popup-head">' +
    '<div class="popup-head-left pull-left">'+
    '<a design and developmenta href="/DetaialUser/Details/' + userName + '">' +
    '<img class="md-user-image" alt="' + userName + '" src="http://bootsnipp.com/img/avatars/bcf1c0d13e5500875fdd5a7e8ad9752ee16e7462.jpg">' +
    '<h1>' + userName + '</h1>' +
    '</a>'+
    '</div>'+
    '<div class="popup-head-right pull-right">'+
    '<button data-widget="remove" id="removeClass" class="chat-header-button pull-right" type="button"><i id="Exit" class="glyphicon glyphicon-remove" name="ExitChatWith ' + userName + '"></i></button>' +
    '</div>'+
    '</div>'+
    '<div id="chat" class="chat_box_wrapper chat_box_small chat_box_active">'+
    '<div id="ConversationWith' + userName + '" class="chat_box touchscroll chat_box_colors_a">' +
    '</div>'+
    '</div>'+
    '<div class="chat_submit_box">'+
    '<div class="uk-input-group">'+
    '<div class="gurdeep-chat-box">'+
    '<input type="text" placeholder="Type a message" id="MessageFor' + userName + '" name="submit_message" class="md-input">' +
    '<button id="send-message" name="ButtonFor ' + userName + '">Send</button>' +
    '</div>' +
    '</div>' +
    '</div>'+
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
});