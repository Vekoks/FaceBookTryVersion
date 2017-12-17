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
                    rows.push(' <div style="background:#00ff90" class="divInfo">');
                }
                else {
                    rows.push(' <div  class="divInfo">');
                }
                rows.push('  <a id="UserName" href="/DetaialUser/Details/' + data.Notification[i].Username + '">');
                rows.push('  <img src="' + data.Notification[i].ImgUser + '" class="imgPostLikeComment" />');
                rows.push('  </a>');
                rows.push('  <a href="/Post/DetailsPost/?id=' + data.Notification[i].PostId + '&secondId=' + data.Notification[i].NotificationId + '" style="color:blue">' + data.Notification[i].Description + '</a>');
                rows.push(' </div>');
            }

            $tbl.append(rows.join(''));
        }

    })
}