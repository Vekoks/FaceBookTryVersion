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
                    rows.push(' <a href="/DetaialUser/Details/' + usersFind[i].UserName + '">' + usersFind[i].UserName + '</a>');
                }

                $UserList.append(rows.join(''));
            }
        }
    });

});

//create commnet on target post event
$('#PostList').on('click', '#CommentCreateButtonId', function () {

    var nameOnButtonArr = $(this).attr('name').split(" ");
    var idOnPost = nameOnButtonArr[1];

    var descriptionOnPost = $('#CommentDisctriptionId' + idOnPost).val();

    $('#CommentDisctriptionId' + idOnPost).val("");

    $.ajax({
        url: "/Post/CreateComment",
        type: "POST",
        data: JSON.stringify({ model: descriptionOnPost, postId: idOnPost }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

        },
        error: function (data) {
            alert("An error has occured!!!");
        }
    });
})

//put likes on posts
$('#PostList').on('click', '#PutLikeButtonId', function () {

    var nameOnButtonArr = $(this).attr('name').split(" ");
    var idOnPost = nameOnButtonArr[1];

    $.ajax({
        url: "/Post/PutLikeOnThePOst",
        type: "POST",
        data: JSON.stringify({ model: idOnPost }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

        },
        error: function (data) {
            alert("An error has occured!!!");
        }
    });
})


$("#TablePictures").on("click", "#CommentPictures", function (e) {

    var pictureId = $(this).attr('name');

    var description = $('#' + pictureId).val();

    $.ajax({
        url: "/Post/CreateCommentOnPostWithPicture",
        type: "POST",
        data: JSON.stringify({ Description: description, PictureId: pictureId }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            location.reload();
        }
    });

});

$("#TablePictures").on("click", "#LikePictures", function (e) {

    var pictureId = $(this).attr('name');

    $.ajax({
        url: "/Post/PutLikeOnThePostWithPicture",
        type: "POST",
        data: JSON.stringify({ PictureId: pictureId }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            location.reload();
        }
    });

});

$("#TablePictures").on("click", "#MekaProfilePicture", function (e) {

    var pictureId = $(this).attr('name');

    $.ajax({
        url: "/DetaialUser/ChangeProfilePicture",
        type: "POST",
        data: JSON.stringify({ PictureId: pictureId.toString() }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.status == "Success") {
                location.reload();
            }
        }
    });

});