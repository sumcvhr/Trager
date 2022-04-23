

$(function () {
    $("#ilad").on("change", function () {

        var SeciliIlId = $("#ilad").val();
        $.ajax({
            type: "GET",
            url: "/courier/ilcegetir",
            data: { Id: SeciliIlId },
            success: function (sonuc) {
                $("#ilce_div").html(sonuc);
            }
        });
    })
})


