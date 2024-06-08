function ShowPreview(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgPrev').attr('src', e.target.result);    // .onload yani referenci ke bayad avaz beshe.
        };
        reader.readAsDataURL(input.files[0]);      // filei(image) ke daryaft mishe. #ref1
    }
}

function changeLikeState(newsId, state, code) {
    $.ajax({
        url: "/News/changeLikeState",
        type: "GET",
        data: { newsId: newsId, state: state, code : code },
        success: function (res) {
            $(`#showLike_${newsId}${code}`).html(res);
        },
        error: () => {
            alert("Error");
        }
    });
}
