function UpdateProductPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Pharmacy/UpdateProductPost",
        data: new FormData(formData),
        success: function (result) {
            console.log(result.message);
            if (result.success) {
                alert("Success Update Product");
                window.location.href = "/Pharmacy/Products/Manages";
            } else {
                console.log(result.message);
                alert("Failed Update Product");
            }
        },
        error: function (err) {
            alert("Error Update Product");
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