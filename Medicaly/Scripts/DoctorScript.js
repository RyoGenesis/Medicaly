function Login(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Doctor/Post",
        data: new FormData(formData),
        success: function (result) {
            if (result.success) {
                window.location.href = "/Doctor";
            } else {
                if (result.message == "Wrong email and password") {
                    alert("Wrong email and password");
                }
                alert("Login Failed");
            }
        },
        error: function (err) {
            alert("Error Login");
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