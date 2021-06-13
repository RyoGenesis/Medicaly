function updateAlamat(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Profile/EditAlamat",
        data: new FormData(formData),
        success: function (result) {
            console.log(result.message);
            if (result.success) {
                alert(result.message);
                if (result.message == "Success update alamat!") {
                    window.history.back();
                    location.reload();
                }
            } else {
                console.log(result.message);
                alert("Failed Update Alamat");
            }
        },
        error: function (err) {
            alert("Error Update Alamat");
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


function addAlamat(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Profile/AddAlamat",
        data: new FormData(formData),
        success: function (result) {
            console.log(result.message);
            if (result.success) {
                alert(result.message);
                if (result.message == "Success add alamat baru!") {
                    window.history.back();
                    location.reload();
                }
            } else {
                console.log(result.message);
                alert("Failed Add Alamat!");
            }
        },
        error: function (err) {
            alert("Error Add Alamat!");
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