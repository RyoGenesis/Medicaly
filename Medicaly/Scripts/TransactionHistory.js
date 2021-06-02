$(document).ready(function () {
    $('#transaksi-baru').DataTable();
    $('#transaksi-belumDikirim').DataTable();
    $('#transaksi-dikirim').DataTable();
    $('#transaksi-selesai').DataTable();
});

$("#formUpdateStatus").submit(function (event) {

    // Stop form from submitting normally
    event.preventDefault();

    // Get some values from elements on the page:
    var id = $('#Id').val();
    var isShipped = $('#IsShipped').val();


    if (confirm("Are you sure ?")) {
        $.ajax({
            type: "POST",
            url: "/Transaction/EditStatus",
            data: {
                id: id,
                isShipped: isShipped,
            },
            success: function (result) {
                if (result.success) {
                    if (result.message == "Success update status!") {
                        alert(result.message);
                        location.reload();
                    } else {
                        alert(result.message);
                    }
                } else {
                    console.log(result.message);
                    alert("Failed update status");
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
});

