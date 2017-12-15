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
    var $tbl = $('#TablePost');
    $.ajax({
        url: '/Post/GetAllPostFromUsers',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                var rows = [];
                for (var i = 0; i < data.length; i++) {
                    rows.push(' <tr>');
                    rows.push(' <td>');
                    rows.push(' <div>');
                    rows.push('  <a id="UserName" href="/DetaialUser/Details/' + data[i].UserName + '">' + data[i].UserName + '</a>');
                    rows.push('  <p>Time:' + data[i].DateOnPost + ' min</p>');
                    rows.push('  <p>Post:' + data[i].DiscriptionPost + '</p>');
                    rows.push('  <img id="imgPost" src="' + data[i].PicturePost + '" alt="" />');
                    rows.push('  <button id="CountLike" name="' + data[i].PostId + '">Like: ' + data[i].Likes.length + '</button>');
                    rows.push('  <div id="ListWithLikes' + data[i].PostId + '" class="hidden myBox">');
                    for (var j = 0; j < data[i].Likes.length; j++) {
                        rows.push('  <img src="' + data[i].Likes[j].PictureProfile + '" class="imgPostLikeComment" />');
                        rows.push('  <p>' + data[i].Likes[j].Username + '</p>');
                    }
                    rows.push(' </div>');
                    rows.push('  <input id="PutLikeButtonId" type="button" name="ButtonLike ' + data[i].PostId + '" value="Like" />');
                    rows.push('  <div><input id="CommentDisctriptionId' + data[i].PostId + '" type="text" name="discriptin" /></div>');
                    rows.push('  <input id="CommentCreateButtonId" type="button" name="ButtonComment ' + data[i].PostId + '" value="Save comment" />');
                    rows.push(' <div id="CommentPostWithId' + data[i].PostId + '" class="myBox">');

                    for (var j = 0; j < data[i].Comments.length; j++) {
                        rows.push('  <img src="' + data[i].Comments[j].PictureProfile + '" class="imgPostLikeComment" />');
                        rows.push('  <p>' + data[i].Comments[j].Username + ': ' + data[i].Comments[j].Description + '</p>');
                    }

                    rows.push(' </div>');
                    rows.push(' </div>');
                    rows.push(' </td>');
                    rows.push(' </tr>');
                }

                var firstChildren = $('#LeftScrollableDiv').children().first();

                $(rows.join('')).prependTo($tbl);

                //$tbl.append(rows.join(''));
            }
        }
    })
}