﻿//function filterTable(event) {
//    var filter = event.target.value.toUpperCase();
//    var rows = document.querySelector("#ProductList tbody").rows;

//    for (var i = 0; i < rows.length; i++) {
//        var firstCol = rows[i].cells[1].textContent.toUpperCase();
//        var secondCol = rows[i].cells[2].textContent.toUpperCase();
//        var thirdCol = rows[i].cells[3].textContent.toUpperCase();
//        var fourthCol = rows[i].cells[4].textContent.toUpperCase();
//        var fifthCol = rows[i].cells[5].textContent.toUpperCase();
//        if (firstCol.indexOf(filter) > -1 || secondCol.indexOf(filter) > -1 || thirdCol.indexOf(filter) > -1 || fourthCol.indexOf(filter) > -1 || fifthCol.indexOf(filter) > -1) {
//            rows[i].style.display = "";
//        } else {
//            rows[i].style.display = "none";
//        }
//    }
//}

//document.querySelector('#mySearch').addEventListener('keyup', filterTable, false);

$(document).ready(function () {
    $('#products-obat').DataTable();
    $('#products-alat').DataTable();
});

function AddProductPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Product/Add",
        data: new FormData(formData),
        success: function (result) {
            console.log(result.message);
            if (result.success) {
                alert("Success Add Product");
                
                location.reload();
            } else {
                console.log(result.message);
                alert("Failed Add Product");
            }
        },
        error: function (err) {
            alert("Error Register Customer");
            console.log(err);
        }
    }
    if ($(formData).attr('enctype') == "multipart/form-data") {
        ajaxConfig["contentType"] = false;
        ajaxConfig["processData"] = false;
    }

    $.ajax(ajaxConfig);
    return false;
}

function deleteProduct(val, element) {
    var id = val;
    console.log("masuk");
    console.log(id);

    if (confirm("Are you sure?")) {
        $.ajax({
            type: "POST",
            url: "/Product/Delete",
            data: {
                id: id
            },
            success: function (result) {
                console.log(result.message)
                if (result.success) {
                    alert("Success Delete Product");
                    location.reload();
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    }

    return;
}
