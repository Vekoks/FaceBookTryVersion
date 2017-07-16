
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
                    rows.push('  <a id="UserName" href="/DetaialUser/Details/' + data[i].UserName + '">' + data[i].UserName + '</a>');
                    rows.push('  <p>Time:' + data[i].DateOnPost + ' min</p>');
                    rows.push('  <p>Post:' + data[i].DiscriptionPost + '</p>');
                    rows.push(' </div>');
                }
                $tbl.append(rows.join(''));
            }
        }
    });
}