function sendForm(formData) {
    console.log(formData.data);
    var ajaxConfig = {
        type: "post",
        url: "/Customer/SupportPost",
        data: new FormData(formData),
        success: function (result) {
            console.log(result)
            if (result.success) {
                alert("Success send form!");
                window.history.back();
            } else {
                alert("Failed to send form");
            }
        },
        error: function (err) {
            alert("Error send form");
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