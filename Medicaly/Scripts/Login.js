function login() {
    console.log($("#Username").val());
}

function LoginPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Auth/Post",
        data: new FormData(formData),
        success: function (result) {
            if (result.success) {
                window.location.href = "/Home";
            } else {
                alert("Login Failed");
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