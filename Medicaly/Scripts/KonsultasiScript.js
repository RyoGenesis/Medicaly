$(document).ready(function () {
    $('#konsultasi-answered').DataTable();
    $('#konsultasi-not-answered').DataTable();
});

function answerKonsultasi(val, element) {
    var id = val;
    console.log("masuk");
    console.log(id);

    $.ajax({
        type: "POST",
        url: "/Doctor/EditKonsultasi",
        data: {
            id: id
        },
        success: function (result) {
            console.log(result.message)
            if (result.success) {
                alert("Success Answer Konsultasi");
                $('#tableList').load('/Doctor/  #tableList');
                $('#tableListKonsultasi').load('/Doctor/ #tableListKonsultasi');
            }
        },
        error: function (err) {
            console.log(err);
        }
    })
}