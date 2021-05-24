function jawabKonsultasi(formData) {
    console.log(formData.data);
    var ajaxConfig = {
        type: "post",
        url: "/Doctor/EditKonsultasi",
        data: new FormData(formData),
        success: function (result) {
            console.log(result)
            if (result.success) {

                alert("Success send konsultasi");
                window.location.href = "/Doctor";
            } else {
                alert("Failed to send");

            }
        },
        error: function (err) {
            alert("Error send konsultasi");
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