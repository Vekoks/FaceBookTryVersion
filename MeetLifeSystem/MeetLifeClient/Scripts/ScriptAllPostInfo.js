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

            var $tbl = $('#CommentPostWithId' + data.IdOnCurrentPost);
            $tbl.empty();

            var rows = [];
            for (var j = 0; j < data.Comments.length; j++) {
                rows.push('  <p>' + data.Comments[j].Username + ':' + data.Comments[j].Description + ' min</p>');
            }
            $tbl.append(rows.join(''));
        },
        error: function (data) {
            alert("An error has occured!!!");
        }
    });
}


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
        url: 'GetAllPostFromUsers',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $tbl.empty();
                var rows = [];
                rows.push(' <p>Post</p>');
                for (var i = 0; i < data.length; i++) {
                    rows.push(' <div>');
                    rows.push('  <p id="PostId" class="hidden">' + data[i].PostId + '</p>');
                    rows.push('  <a 4id="UserName" href="/DetaialUser/Details/' + data[i].UserName + '">' + data[i].UserName + '</a>');
                    rows.push('  <p>Time:' + data[i].DateOnPost + ' min</p>');
                    rows.push('  <p>Post:' + data[i].DiscriptionPost + '</p>');
                    rows.push('  <div><input id="CommentDisctriptionId' + data[i].PostId + '" type="text" name="discriptin" /></div>');
                    rows.push('  <input id="ComId" type="button" name="' + data[i].PostId + '" value="Save comment" />');
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

    //create commnet on target post event
    $('#PostList').on('click', '#ComId', function () {

        var idOnPost = $(this).attr('name');

        var descriptionOnPost = $('#CommentDisctriptionId' + idOnPost).val();

        var subResult = idOnPost + " " + descriptionOnPost;

        var $tbl = $('#CommentPostWithId' + idOnPost);

        $.ajax({
            url: "/Post/CreateComment",
            type: "POST",
            data: JSON.stringify({ model: subResult }),
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $tbl.empty();
                var rows = [];
                for (var i = 0; i < data.length; i++) {
                    rows.push('  <p>' + data[i].Username + ':' + data[i].Description + ' min</p>');
                }
                $tbl.append(rows.join(''));
            },
            error: function (data) {
                alert("An error has occured!!!");
            }
        });
    })

}
