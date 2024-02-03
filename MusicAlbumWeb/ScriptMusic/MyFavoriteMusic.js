function toggleFavorite(musicId) {
    var heartImage = $("#heartImage_" + musicId);
    var isFavorite = heartImage.attr("src").indexOf("favoriteT.png") !== -1;

    if (isFavorite) {
        heartImage.attr("src", "/Content/Images/favoriteF.png");
    } else {
        heartImage.attr("src", "/Content/Images/favoriteT.png");
    }

    $.ajax({
        type: "POST",
        url: "/FavoriteMusics/ToggleFavorite",
        data: { musicId: musicId },
        success: function (data) {
            console.log("บันทึกข้อมูลเรียบร้อย");
        },
        error: function (error) {
            console.error("เกิดข้อผิดพลาดในการบันทึกข้อมูล");
        }
    });
}
