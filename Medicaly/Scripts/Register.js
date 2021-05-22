function AjaxPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Auth/Create",
        data: new FormData(formData),
        success: function (result) {
            if (result.success) {
                alert("Success Register Customer");
                window.location.href = "/Auth/Login";
            } else {
                console.log(result.message);
                if (result.message == "Email Already Registered") {
                    alert("Email Already Registered");
                } else {
                    alert("Cannot Register Customer");
                }
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
