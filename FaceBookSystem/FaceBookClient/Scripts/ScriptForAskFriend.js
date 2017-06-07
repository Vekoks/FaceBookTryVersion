
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
                    rows.push('  <a href="ConferFriend/' + data[i].Name + '/accept" class = "btn btn-info" >Accept invitation</a>');
                    rows.push('  <a href="ConferFriend/' + data[i].Name + '/delete" class = "btn btn-info" >Detele invitation</a>');

                    rows.push(' </div>');
                }

                $tbl.append(rows.join(''));
            }
        }
    });
}