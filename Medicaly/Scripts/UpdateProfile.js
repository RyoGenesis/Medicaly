function updateCustomer(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Profile/EditCustomer",
        data: new FormData(formData),
        success: function (result) {
            console.log(result.message);
            if (result.success) {
                alert(result.message);
                if (result.message != "Email already registered!") {
                    $('#profile').load('/Profile/ #profile');
                }
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