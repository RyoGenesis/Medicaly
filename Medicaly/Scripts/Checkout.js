// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict'

    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation')

        // Loop over them and prevent submission
        Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault()
                    event.stopPropagation()
                }
                form.classList.add('was-validated')
            }, false)
        })
    }, false)
}())

function AddTransaction(formData) {
    if (confirm('Are you sure ?')) {
        var ajaxConfig = {
            type: "post",
            url: "/Checkout/Add",
            data: new FormData(formData),
            success: function (result) {
                console.log(result.message);
                if (result.success) {
                    alert(result.message);
                    if (result.message == "Berhasil checkout!") {
                        window.location.href = "/Home/";
                    }
                } else {
                    alert("Failed checkout!");
                }
            },
            error: function (err) {
                alert("Error checkout!");
                console.log(err);
            }
        }
        if ($(formData).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);
    }
    return false;
}