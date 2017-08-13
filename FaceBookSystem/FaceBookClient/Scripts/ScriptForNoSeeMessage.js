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
    var $tbl = $('#CountMissMessage');
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
                    rows.push(' <div class="alert alert-dismissible alert-info" style="width:25%; text-align:center">')
                    rows.push(' <div id="ListWithMissMessage"> You have miss message from')
                    rows.push(' <p id="Sender" class="text-danger">' + data[i] + '</p>');
                    rows.push(' </div>')
                    rows.push(' </div>')
                }

                $allUser.append(rows.join(''));
            }
        }
    });

    $("#AllMessageFromUsers").on("click", "#ListWithMissMessage", function () {
        var userName = $(this).find("#Sender").text();
        debugger;
        var $tbl = $("#Chat");

        var rows = [];
        rows.push(' <table style="float: left; border:2px solid red">');
        rows.push(' <tr>');
        rows.push(' <td>');
        rows.push(' <div><a id="UserName" href="/DetaialUser/Details/' + userName + '">' + userName + '</a></div>');
        rows.push(' <button id="Exit" class="btn btn-danger">X</button>');
        rows.push(' </td>');
        rows.push(' </tr>');

        rows.push(' <tr>');
        rows.push(' <td>');
        rows.push(' <div><input type="text" id="message" class="modal-body" value="message " /></div>');
        rows.push(' </td>');
        rows.push(' </tr>');

        rows.push(' <tr>');
        rows.push(' <td style="padding-left: 50px">');
        rows.push(' <button id="send-message" class="btn btn-info">Send</button>');
        rows.push(' </td>');
        rows.push(' </tr>');

        rows.push(' <tr>');
        rows.push(' <td>');
        rows.push(' <div id="messages"></div>');
        rows.push(' </td>');
        rows.push(' </tr>');
        rows.push(' </table>');

        $tbl.append(rows.join(''));

        //Send message
        $('#message').keyup(function (e) {
            if (e.keyCode == 13) {
                var chat = $.connection.chat;

                var userName = $("#Chat").find("#UserName").text();

                var msg = $('#message').val();

                chat.server.sendMessage(userName, msg);

                $('#message').val("");
            }
        });

        $('#send-message').click(function () {

            var chat = $.connection.chat;

            var userName = $("#Chat").find("#UserName").text();

            var msg = $('#message').val();

            chat.server.sendMessage(userName, msg);

        });

        $('#Exit').click(function () {

            var $tbl = $("#Chat");
            $tbl.empty();

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
    });
}