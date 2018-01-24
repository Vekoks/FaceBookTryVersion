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

$('#PostList').on('click', '#ShowComments', function () {

    var idOnPost = $(this).attr('name').split(" ");;

    var $editInfo = $('#CommentPostWithId' + idOnPost[1]);

    if ($editInfo.hasClass("hidden")) {
        $editInfo.removeClass("hidden");
    }
    else {
        $editInfo.addClass("hidden");
    }
})


//create commnet on target post event
$('#PostList').on('click', '#CommentCreateButtonId', function () {

    var nameOnButtonArr = $(this).attr('name').split(" ");
    var idOnPost = nameOnButtonArr[1];

    var descriptionOnPost = $('#CommentDisctriptionId' + idOnPost).val();

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
            if (data.status == "no success") {

                var infoUser = $('#InformationForUser');
                infoUser.empty();
                
                var infoDiv =
                    '<div>' +
                       '<p>This post is like already</p>' +
                    '</div>';

                $(infoUser).append(infoDiv);

                infoUser.removeClass("hidden")

                setTimeout(function(){
                    $('#InformationForUser').addClass("hidden");
                }, 3000);
            }
        },
    });
})


$("#TablePictures").on("click", "#MekaProfilePicture", function (e) {

    var postId = $(this).attr('name');

    $.ajax({
        url: "/DetaialUser/ChangeProfilePicture",
        type: "POST",
        data: JSON.stringify({ PostId: postId.toString() }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.status == "Success") {
               //window.location.href = '@Url.Action("Pictures/"' + data.message + ', "DetaialUser")';
                window.location.reload();
            }
        }
    });

});


$('#SearchUserId').keyup(function () {
    var searchUserName = $(this).val();

    var $UserList = $('#ListUsers');

    if (searchUserName == "") {
        $UserList.empty();
        return;
    }

    $.ajax({
        url: '/Home/SearchUsers',
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
                    rows.push(' <li>');
                    rows.push(' <img src="' + usersFind[i].ProfilePicture + '" alt="Alternate Text" style="width:50px;height:50px"/>');
                    rows.push(' <a href="/DetaialUser/Details/' + usersFind[i].UserName + '">' + usersFind[i].UserName + '</a>');
                    rows.push(' </li>');
                }

                $UserList.append(rows.join(''));
            }
        }
    });

});