
-$(function () {
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
                var element = $('[name=' + data[i].IdOnCurrentPost + ']');
                var value = "Like: " + data[i].Likes.length
                element.text(value);

                var $listLikes = $('#ListWithLikes' + data[i].IdOnCurrentPost);
                $listLikes.empty();

                var rows = [];
                for (var j = 0; j < data[i].Likes.length; j++) {
                    rows.push('  <p>' + data[i].Likes[j].UserName + '</p>');
                }
                $listLikes.append(rows.join(''));

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