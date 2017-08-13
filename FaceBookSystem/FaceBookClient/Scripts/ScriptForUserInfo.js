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

    //for chat
    $("#UsersList").on("click", "#specifik", function (e) {
        var userName = $(this).find("#User").text();

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
    });
}

