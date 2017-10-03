function CreateComment() {
    var idOnPost = $('#PostId').text();
    var descriptionOnPost = $('#CommentDisctriptionId').val();

    //var json = JSON.stringify(descriptionOnPost).toString()

    var subResult = idOnPost + " " + descriptionOnPost;

    $.ajax({
        url: "/Post/CreateComment",
        type: "POST",
        data: JSON.stringify({ model: subResult }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

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
                    rows.push('  <a id="UserName" href="/DetaialUser/Details/' + data[i].UserName + '">' + data[i].UserName + '</a>');
                    rows.push('  <p>Time:' + data[i].DateOnPost + ' min</p>');
                    rows.push('  <p>Post:' + data[i].DiscriptionPost + '</p>');
                    rows.push('  <div><input id="CommentDisctriptionId" type="text" name="discriptin" /></div>');
                    rows.push('  <button id="ComId" onclick="CreateComment()">Save comment</button>');

                    rows.push(' </div>');
                }
                $tbl.append(rows.join(''));
            }
        }

    })


}