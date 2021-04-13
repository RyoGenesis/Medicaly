$(document).ready(function () {
    $('#formRegisterCustomer').validate({
        rules: {
            Nama: {
                required: true,
                minlength: 3
            },
            Alamat: {
                required: true,
                minlength: 10
            },
            NoHandphone: {
                required: true,
                number: true,
                minlength: 10
            },
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 6
            }
        },
        messages: {
            Nama: {
                minlength: "Name should be at least 3 characters"
            },
            Alamat: {
                minlength: "Alamat should be at least 3 characters"
            },
            NoHandphone: {
                required: "Please enter your phone",
                number: "Please enter your age as a numerical value",
                min: "it must be 10 numeric vales"
            },
            Email: {
                email: "The email should be in the format: abc@domain.tld"
            },
            Password: {
                minlength: "password should be at least 3 characters"
            }
        }
    });
});

function AjaxPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Auth/Create",
        data: new FormData(formData),
        success: function (result) {
            if (result.success) {
                alert("Success Register Customer");
                window.location.href = "/Home";
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
