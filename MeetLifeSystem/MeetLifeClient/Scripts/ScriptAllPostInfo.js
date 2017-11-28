$(function () {
    var job = $.connection.postHub;

    // Declare a function on the job hub so the server can invoke it
    job.client.displayAllPost = function () {
        getDataForAllPost();
    };

    // Start the connection
    $.connection.hub.start();
    getDataForAllPost();
});

//load all post
function getDataForAllPost() {
    var $tbl = $('#PostList');
    $.ajax({
        url: '/Post/GetAllPostFromUsers',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $tbl.empty();
                var rows = [];
                rows.push(' <p>Post</p>');
                for (var i = 0; i < data.length; i++) {
                    rows.push(' <div>');
                    rows.push('  <a id="UserName" href="/DetaialUser/Details/' + data[i].UserName + '">' + data[i].UserName + '</a>');
                    rows.push('  <p>Time:' + data[i].DateOnPost + ' min</p>');
                    rows.push('  <p>Post:' + data[i].DiscriptionPost + '</p>');
                    rows.push('  <img src="' + data[i].PicturePost + '" alt="" style="width:100px; height:100px"/>');
                    rows.push('  <p id="CountLikeId' + data[i].PostId + '">Like:' + data[i].Likes + '</p>');
                    rows.push('  <input id="PutLikeButtonId" type="button" name="ButtonLike ' + data[i].PostId + '" value="Like" />');
                    rows.push('  <div><input id="CommentDisctriptionId' + data[i].PostId + '" type="text" name="discriptin" /></div>');
                    rows.push('  <input id="CommentCreateButtonId" type="button" name="ButtonComment ' + data[i].PostId + '" value="Save comment" />');
                    rows.push(' <div id="CommentPostWithId' + data[i].PostId + '">');

                    for (var j = 0; j < data[i].Comments.length; j++) {
                        rows.push('  <p>' + data[i].Comments[j].Username + ':' + data[i].Comments[j].Description + ' min</p>');
                    }

                    rows.push(' </div>');
                    rows.push(' </div>');
                }
                $tbl.append(rows.join(''));
            }
        }
    })
}

//refresh like
$(function () {
    var job = $.connection.likeHub;

    // Declare a function on the job hub so the server can invoke it
    job.client.displayLikes = function () {
        getDataForLikes();
    };

    // Start the connection
    $.connection.hub.start();
    getDataForLikes();
});

function getDataForLikes() {
    $.ajax({
        url: '/Post/GetLikes',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var $tbl = $('#CountLikeId' + data[i].IdOnCurrentPost);
                $tbl.empty();
                $tbl.text("Like: " + data[i].Likes);
            }
        }
    })
}

//refresh comments
$(function () {
    var job = $.connection.commentsPostHub;

    // Declare a function on the job hub so the server can invoke it
    job.client.displayCommentsOnThePost = function () {
        GetCommentsOnThePost();
    };

    // Start the connection
    $.connection.hub.start();
    GetCommentsOnThePost();
});

function GetCommentsOnThePost() {
    $.ajax({
        url: "/Post/GetCommentsOnThePost",
        type: "GET",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {

                var $tbl = $('#CommentPostWithId' + data[i].IdOnCurrentPost);
                $tbl.empty();

                var rows = [];
                for (var j = 0; j < data[i].Comments.length; j++) {
                    rows.push('  <p>' + data[i].Comments[j].Username + ':' + data[i].Comments[j].Description + '</p>');
                }
                $tbl.append(rows.join(''));
            }
        }
    });
}

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

        },
        error: function (data) {
            alert("An error has occured!!!");
        }
    });
})
