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
                    rows.push(' <div id="AskFriend' + data[i].Name + '">');
                    rows.push('  <h4>' + data[i].Name + '</h4>');
                    rows.push('  <input id="Confirm" type="button" class = "btn btn-info" name="Accept ' + data[i].Name + '" value="Accept invitation" />');
                    rows.push('  <input id="Confirm" type="button" class = "btn btn-info" name="Delete ' + data[i].Name + '" value="Delete invitation" />');

                    rows.push(' </div>');
                }

                $tbl.append(rows.join(''));
            }
        }
    });

    $("#AskFriendList").on("click", "#Confirm", function (e) {
        var nameOnButtonArr = $(this).attr('name').split(" ");

        var confirmNameButton = nameOnButtonArr[0];
        var userName = nameOnButtonArr[1];

        var indexAskFriend = "#AskFriend" + userName.toString();
        var nameOnButtonArr = $('#AskFriendList').find(indexAskFriend).remove();


        $.ajax({
            url: "/Home/ConferFriend",
            type: "POST",
            data: JSON.stringify({ UserName: userName.toString(), confirm: confirmNameButton }),
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