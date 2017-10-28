function chatCreate(userName) {
    var $tbl = $("#Chat");
    $tbl.empty();

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

$('#NotificationMessageMiss').click(function () {
    var $allUser = $('#AllMessageFromUsers');
    var $countMessage = $('#CountMissMessage');

    $countMessage.empty();

    if ($allUser.hasClass("hidden")) {
        $allUser.removeClass("hidden");
    }
    else {
        $allUser.addClass("hidden");


    }
});

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
                    debugger;
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
        debugger;
        chatCreate(userName);
    });
}