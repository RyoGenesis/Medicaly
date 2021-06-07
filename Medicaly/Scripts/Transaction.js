$(document).ready(function () {
    $('#transaksi-baru').DataTable();
    $('#transaksi-belumDikirim').DataTable();
    $('#transaksi-dikirim').DataTable();
    $('#transaksi-selesai').DataTable();
    $('#transaksi-selesai-beneran').DataTable();
});

function EditStatus(id, name) {

    console.log(id + name);

    if (confirm("Are you sure ?")) {
        $.ajax({
            type: "POST",
            url: "/Transaction/EditStatus",
            data: {
                id: id,
                isShipped: name,
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
}

$(".editShipt").click(function () {

    var id = $(this).data('id');
    console.log("masuk");
    console.log(id);
    document.getElementById("IdModal").setAttribute('value', id);
});

$("#formShipement").submit(function (event) {

    // Stop form from submitting normally
    event.preventDefault();

    // Get some values from elements on the page:
    var id = $('#IdModal').val();
    var isShipped = 2;
    var kurir = $('#Kurir').val();
    var trackingId = $('#TrackingId').val();


    if (confirm("Are you sure ?")) {
        $.ajax({
            type: "POST",
            url: "/Transaction/EditStatusShipment",
            data: {
                id: id,
                isShipped: isShipped,
                kurir: kurir,
                trackingId: trackingId
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