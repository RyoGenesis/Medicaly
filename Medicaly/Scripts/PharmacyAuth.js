function RegisterPharmacyPost(formData) {
    console.log(formData.data);
    var ajaxConfig = {
        type: "post",
        url: "/Pharmacy/Create",
        data: new FormData(formData),
        success: function (result) {
            if (result.success) {
                alert("Success Register Pharmacy");
                window.location.href = "/Pharmacy";
            } else {
                console.log(result.message);
                if (result.message == "Email Already Registered") {
                    alert("Email Already Registered");
                } else {
                    alert("Cannot Register Pharmacy");
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

function LoginPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Pharmacy/Post",
        data: new FormData(formData),
        success: function (result) {
            if (result.success) {
                window.location.href = "/Pharmacy";
            } else {
                if (result.message == "Wrong email and password") {
                    alert("Wrong email and password");
                }
                alert("Login Failed");
            }
        },
        error: function (err) {
            alert("Error Login Pharmacy");
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