// MyComment.js
$(document).ready(function () {
    $('.post-comment-btn').click(function () {
        var commentText = document.getElementById("commentText").value;
        var musicId = $(this).data('music-id');

        console.log("commentText:", commentText);  
        postComment(commentText, musicId);

        document.getElementById("commentText").value = "";
    });
});

function postComment(commentText, musicId) {

    $.ajax({
        url: '/Comments/PostComment',
        type: 'POST',
        data: JSON.stringify({ comment: commentText, musicId: musicId }),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            console.log(response);
            // แสดงป๊อปอัพเมื่อคอมเมนต์เรียบร้อย
           // alert('Comment posted successfully!');

            // รีเฟรชหน้าเพื่อให้คอมเมนต์แสดง
            location.reload();
        },
        error: function (error) {
            console.log(error);
        }
    });
}
