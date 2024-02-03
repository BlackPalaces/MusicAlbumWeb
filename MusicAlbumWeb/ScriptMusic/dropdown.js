// myscripts.js
function toggleDropdown(icon) {
    var dropdownContent = icon.nextElementSibling;
    dropdownContent.classList.toggle("show");
}

document.addEventListener("click", function (event) {
    var dropdowns = document.querySelectorAll('.dropdown-content');
    dropdowns.forEach(function (dropdownContent) {
        if (!event.target.matches('.dots-icon') && !event.target.matches('.dropdown-content a')) {
            dropdownContent.classList.remove('show');
        }
    });
});

async function deleteComment(commentId) {
    if (confirm("คุณแน่ใจหรือไม่ที่ต้องการลบคอมเมนต์นี้?")) {
        try {
            const response = await $.ajax({
                url: '/Comments/DeleteComment',  // Make sure the URL is correct
                type: 'POST',
                data: { commentId: commentId },
            });

            console.log(response);

            // Reload the page after successful deletion
            if (response.success) {
                location.reload();
            }
        } catch (error) {
            console.log(error);
        }
    }
}

function editCommentInline(commentId) {
    // Hide the comment text
    document.getElementById('comment-' + commentId).style.display = 'none';

    // Show the input field for editing
    document.getElementById('edit-comment-' + commentId).style.display = 'block';
}

async function updateCommentInline(commentId) {
    var editedComment = document.getElementById('commentInput-' + commentId).value;

    try {
        const response = await $.ajax({
            url: '/Comments/UpdateComment',
            type: 'POST',
            data: { commentId: commentId, editedComment: editedComment },
        });

        console.log(response);

        // Update the comment text with the edited comment
        var commentTextElement = document.getElementById('comment-' + commentId);
        commentTextElement.style.display = 'block';
        commentTextElement.innerHTML = '<p class="g-color-gray-dark-v1">' + editedComment + '<br /></p>';

        // Hide the input field after updating
        document.getElementById('edit-comment-' + commentId).style.display = 'none';
    } catch (error) {
        console.log(error);
        // Handle error, if needed
    }
}
