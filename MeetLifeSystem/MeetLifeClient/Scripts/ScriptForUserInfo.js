﻿//live list on users 
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
    var $tbl = $('#FriendList');
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
                    rows.push(' <div id="User" class="btn btn-default">' + data[i].Name + '</div>');
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
$("#FriendList").on("click", "#specifik", function (e) {
    var userName = $(this).find("#User").text();

    var $tbl = $("#Chat");
    //$tbl.empty();

    var rows = [];
    rows.push(' <table id="ChatWith' + userName + '" style="float: left; border:2px solid red">');
    rows.push(' <tr>');
    rows.push(' <td>');
    rows.push(' <div><a id="UserName" href="/DetaialUser/Details/' + userName + '">' + userName + '</a></div>');
    rows.push(' <button id="Exit" class="btn btn-danger" name="ExitChatWith ' + userName + '">X</button>');
    rows.push(' </td>');
    rows.push(' </tr>');

    rows.push(' <tr>');
    rows.push(' <td>');
    rows.push(' <div><input type="text" id="MessageFor' + userName + '" class="modal-body" value="message " /></div>');
    rows.push(' </td>');
    rows.push(' </tr>');

    rows.push(' <tr>');
    rows.push(' <td style="padding-left: 50px">');
    rows.push(' <button id="send-message" class="btn btn-info" name="ButtonFor ' + userName + '">Send</button>');
    rows.push(' </td>');
    rows.push(' </tr>');

    rows.push(' <tr>');
    rows.push(' <td>');
    rows.push(' <div id="ConversationWith' + userName + '"></div>');
    rows.push(' </td>');
    rows.push(' </tr>');
    rows.push(' </table>');

    $tbl.append(rows.join(''));

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
                rowsCon.push(' <div>' + data[i].Sender + ':' + data[i].Letter + '</div>');
            }

            $con.append(rowsCon.join(''));
        }
    });
});