$(function () {

    // Proxy created on the fly
    var job = $.connection.jobHub;

    // Declare a function on the job hub so the server can invoke it
    job.client.displayStatus = function () {
        getData();
    };

    // Start the connection
    $.connection.hub.start();
    getData();
});


function getData() {
    var $tbl = $('#UsersList');
    $.ajax({
        url: 'ResultInfoFOrUsers',
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
                console.log(rows);

                $tbl.append(rows.join(''));
            }
        }
    });
}

$("#UsersList").on("click", "#specifik", function (e) {
    var userName = $(this).find("#User").text();
    console.log("here");
    var divForChatWithFRiend = $("#Chat").find("#ChatWithUser");

    divForChatWithFRiend.text("");

    divForChatWithFRiend.append('<a href="/DetaialUser/Details/' + userName + '">' + userName + '</a>');
});