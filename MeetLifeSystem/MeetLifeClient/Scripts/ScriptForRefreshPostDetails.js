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
                var $tbl = $('button[name=' + data[i].IdOnCurrentPost + ']');
                $tbl.empty();
                $tbl.text("Like: " + data[i].LikesCount);

                var likes = $('#ListWithLikes' + data[i].IdOnCurrentPost + '');
                likes.empty();

                var rows = [];

                for (var j = 0; j < data[i].Likes.length; j++) {
                    rows.push('  <div class="divInfo">');
                    rows.push(' <img src="' + data[i].Likes[j].PictureProfile + '" class="imgPostLikeComment" />');
                    rows.push(' <p>' + data[i].Likes[j].Username + '</p>');
                    rows.push('  </div>');
                }

                likes.append(rows.join(''));
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
                    rows.push('  <div class="divInfo">');
                    rows.push('  <img src="' + data[i].Comments[j].PictureProfile + '" class="imgPostLikeComment" />');
                    rows.push('  <p>' + data[i].Comments[j].Username + ':' + data[i].Comments[j].Description + '</p>');
                    rows.push('  </div>');
                }
                $tbl.append(rows.join(''));
            }
        }
    });
}