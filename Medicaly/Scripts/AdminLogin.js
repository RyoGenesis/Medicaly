function LoginPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Admin/Login",
        data: new FormData(formData),
        success: function (result) {
            if (result.success) {
                window.location.href = "/Transaction/Manage";
            } else {
                if (result.message == "Wrong email and password") {
                    alert("Wrong email and password!");
                }
                alert("Login Failed");
            }
        },
        error: function (err) {
            alert("Error login admin!");
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