// MyComment.js
$(document).ready(function () {
    $('.post-comment-btn').click(function () {
        var commentText = document.getElementById("commentText").value;
        var musicId = $(this).data('music-id');

        console.log("commentText:", commentText);  // แสดง commentText ใน Developer Console
        postComment(commentText, musicId);
    });
});

function postComment(commentText, musicId) {
    // ส่ง AJAX request ไปยังเซิร์ฟเวอร์
    $.ajax({
        url: '/Comments/PostComment',
        type: 'POST',
        data: JSON.stringify({ comment: commentText, musicId: musicId }),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            console.log(response);
        },
        error: function (error) {
            console.log(error);
        }
    });
}
