$(function () {
    var job = $.connection.likeHub;

    // Declare a function on the job hub so the server can invoke it
    job.client.displayLike = function () {
        getDataForLikes();
    };

    // Start the connection
    $.connection.hub.start();
    getDataForLikes();
});

function getDataForLikes() {
    var $tbl = $('#LikeList');
    $.ajax({
        url: 'GetAllPostFromUsers',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $tbl.empty();
                var rows = [];
                rows.push(' <p>Like'+ data+'</p>');
                
                $tbl.append(rows.join(''));
            }
        }

    })

}