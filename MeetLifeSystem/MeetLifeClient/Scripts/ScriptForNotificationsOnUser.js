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
            var $count = $('#NotificationsOnUser').find('#CountNotification');
            $count.empty();
            $count.html(' <p class="text-success">' + data.CountNoSeenNotification + '</p>');

            $tbl.empty();
            var rows = [];

            for (var i = 0; i < data.Notification.length; i++) {
                if (!data.Notification[i].IsSaw) {
                    rows.push(' <div style="background:#00ff90">');
                }
                else {
                    rows.push(' <div>');
                }

                rows.push('  <p>' + data.Notification[i].Description + ' <a href="/Post/DetailsPost/?id=' + data.Notification[i].PostId + '&secondId=' + data.Notification[i].NotificationId + '">view</a> ' + '</p>');
                rows.push(' </div>');
            }

            $tbl.append(rows.join(''));
        }

    })
}