
$(function () {

    // Proxy created on the fly
    var job = $.connection.userHub;

    // Declare a function on the job hub so the server can invoke it
    job.client.displayAskFriend = function () {
        getDataForAskFriend();
    };

    // Start the connection
    $.connection.hub.start();
    getDataForAskFriend();
});


function getDataForAskFriend() {
    var $tbl = $('#AskFriendList');

    $.ajax({
        url: 'ResultInfoForAskFriend',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $tbl.empty();
                var rows = [];
                rows.push(' <p>Invitations from Friend:' + data.length + '</p>');
                for (var i = 0; i < data.length; i++) {
                    rows.push(' <div>');
                    rows.push('  <p>' + data[i].Name + '</p>');
                    rows.push('  <button id="AcceptFriend" class = "btn btn-info" >Accept invitation</button>');
                    rows.push('  <button id="DeteleFriend" class = "btn btn-info" >Detele invitation</button>');

                    rows.push(' </div>');
                }

                $tbl.append(rows.join(''));
            }
        }
    });


    $("#AcceptFriend").on("click", "#specifik", function (e) {

        $.ajax({
            url: "/Home/ConferFriend",
            type: "POST",
            data: JSON.stringify({ UserName: userName.toString(), confer: "accept" }),
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.status == "Success") {

                }
            }
        });
    });

    $("#DeteleFriend").on("click", "#specifik", function (e) {

        $.ajax({
            url: "/Home/ConferFriend",
            type: "POST",
            data: JSON.stringify({ UserName: userName.toString(), confer: "delete" }),
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