$(function () {
    var job = $.connection.notificationsHub;

    // Declare a function on the job hub so the server can invoke it
    job.client.displayNotifications = function () {
        getDataForNotifications();
    };

    // Start the connection
    $.connection.hub.start();
    getDataForNotifications();
});

function getDataForNotifications() {
    var $tbl = $('#AllNotifications');
    $.ajax({
        url: 'GetNotificationsOnUser',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $tbl.empty();
                var rows = [];

                for (var i = 0; i < data.length; i++) {
                    rows.push(' <div>');
                    rows.push('  <p>' + data[i].Description + ' <a href="/Post/DetailsPost/?id=' + data[i].PostId + '">view</a> ' + '</p>');
                    rows.push(' </div>');
                }

                $tbl.append(rows.join(''));

            }
        }

    })
}