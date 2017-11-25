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

$('#EditInfoUser').click(function () {
    var $editInfo = $('#FormInfo');

    if ($editInfo.hasClass("hidden")) {
        $editInfo.removeClass("hidden");
    }
    else {
        $editInfo.addClass("hidden");
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

$('#PostList').on('click', '#CountLike', function () {

    var idOnPost = $(this).attr('name');

    var $editInfo = $('#ListWithLikes' + idOnPost);

    if ($editInfo.hasClass("hidden")) {
        $editInfo.removeClass("hidden");
    }
    else {
        $editInfo.addClass("hidden");
    }
})

$('#SearchUserId').keyup(function () {
    var searchUserName = $(this).val();

    var $UserList = $('#ListUsers');

    if (searchUserName == "") {
        $UserList.empty();
        return;
    }

    $.ajax({
        url: '/DetaialUser/SearchUsers',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $UserList.empty();

                var usersFind = [];
                var rows = [];

                for (var i = 0; i < data.length; i++) {
                    if (data[i].UserName.includes(searchUserName)) {
                        usersFind.push(data[i]);
                    }
                }

                for (var i = 0; i < usersFind.length; i++) {
                    rows.push(' <img src="' + usersFind[i].ProfilePicture + '" alt="Alternate Text" style="width:50px;height:50px"/>');
                    rows.push(' <a href=href="/DetaialUser/Details/' + usersFind[i].UserName + '">' + usersFind[i].UserName + '</a>');
                }

                $UserList.append(rows.join(''));
            }
        }
    });

});