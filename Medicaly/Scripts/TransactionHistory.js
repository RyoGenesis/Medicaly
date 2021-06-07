$(document).ready(function () {
    $('#transaksi-baru').DataTable();
    $('#transaksi-belumDikirim').DataTable();
    $('#transaksi-dikirim').DataTable();
    $('#transaksi-selesai').DataTable();
});

function EditStatusSubmit(id, name) {

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

