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
                    rows.push('  <button id="CountLike" name="' + data[i].PostId + '">Like: ' + data[i].Likes.length + '</button>');
                    rows.push('  <div id="ListWithLikes' + data[i].PostId + '" class="hidden myBox">');
                    for (var j = 0; j < data[i].Likes.length; j++) {
                        rows.push('  <img src="' + data[i].Likes[j].PictureProfile + '" style="max-height:30px;max-width:60px" />');
                        rows.push('  <p>' + data[i].Likes[j].Username + '</p>');
                    }
                    rows.push(' </div>');
                    rows.push('  <input id="PutLikeButtonId" type="button" name="ButtonLike ' + data[i].PostId + '" value="Like" />');
                    rows.push('  <div><input id="CommentDisctriptionId' + data[i].PostId + '" type="text" name="discriptin" /></div>');
                    rows.push('  <input id="CommentCreateButtonId" type="button" name="ButtonComment ' + data[i].PostId + '" value="Save comment" />');
                    rows.push(' <div id="CommentPostWithId' + data[i].PostId + '">');

                    for (var j = 0; j < data[i].Comments.length; j++) {
                        rows.push('  <img src="' + data[i].Comments[j].PictureProfile + '" style="max-height:30px;max-width:60px" />');
                        rows.push('  <p>' + data[i].Comments[j].Username + ':' + data[i].Comments[j].Description + '/p>');
                    }

                    rows.push(' </div>');
                    rows.push(' </div>');
                }
                $tbl.append(rows.join(''));
            }
        }
    })
}