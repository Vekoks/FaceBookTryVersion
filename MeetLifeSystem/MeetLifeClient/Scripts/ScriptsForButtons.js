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

$('#NotificationsOnUserButton').click(function () {
    var $allNotifications = $('#AllNotifications');

    if ($allNotifications.hasClass("hidden")) {
        $allNotifications.removeClass("hidden");
    }
    else {
        $allNotifications.addClass("hidden");
    }
});

$('#EditInfoUser').click(function () {
    var $editInfo = $('#FormInfo');

    if ($editInfo.hasClass("hidden")) {
        $editInfo.removeClass("hidden");
    }
    else {
        $editInfo.addClass("hidden");
    }
});
