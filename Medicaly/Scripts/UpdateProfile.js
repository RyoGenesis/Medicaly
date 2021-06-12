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
                alert("Failed Update Customer");
            }
        },
        error: function (err) {
            alert("Error Update Customer");
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

function updatePharmacy(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Profile/EditPharmacy",
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
                alert("Failed Update Pharmacy");
            }
        },
        error: function (err) {
            alert("Error Update Pharmacy");
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

function updateDoctor(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Profile/EditDoctor",
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
                alert("Failed Update Doctor");
            }
        },
        error: function (err) {
            alert("Error Update Doctor");
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



function formPictureCustomer(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Profile/EditPicture",
        data: new FormData(formData),
        success: function (result) {
            console.log(result.message);
            if (result.message == "Success update profile picture!") {
                alert("Success update profile picture!");
                $('#profile').load('/Profile/ #profile');
                $("#updatePicture .btn-close").click();
                $("#formupdate").trigger("reset");
            } else {
                console.log(result.message);
                alert("Failed Edit Picture!");
            }
        },
        error: function (err) {
            alert("Error Edit Picture!");
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

function formPicturePharmacy(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Profile/EditPicturePharmacy",
        data: new FormData(formData),
        success: function (result) {
            console.log(result.message);
            if (result.message == "Success update profile picture!") {
                alert("Success update profile picture!");
                $('#profile').load('/Profile/ #profile');
                $("#updatePicture .btn-close").click();
                $("#formupdate").trigger("reset");
            } else {
                console.log(result.message);
                alert("Failed Edit Picture!");
            }
        },
        error: function (err) {
            alert("Error Edit Picture!");
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

function formPictureDoctor(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Profile/EditPictureDoctor",
        data: new FormData(formData),
        success: function (result) {
            console.log(result.message);
            if (result.message == "Success update profile picture!") {
                alert("Success update profile picture!");
                $('#profile').load('/Profile/ #profile');
                $("#updatePicture .btn-close").click();
                $("#formupdate").trigger("reset");
            } else {
                console.log(result.message);
                alert("Failed Edit Picture!");
            }
        },
        error: function (err) {
            alert("Error Edit Picture!");
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