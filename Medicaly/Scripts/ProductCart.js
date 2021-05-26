 

for (let i = 1; i <= $('#cartcount').val(); i++) {

    var quantitiy = 0;
    $('.plus-' + i).click(function (e) {

        // Stop acting like a button
        e.preventDefault();

        if ($('#quantity-' + i).val() >= $('#Stock-' + i).val()) {
            alert("You reach the maximum quantity");
            $('.plus-' + i).prop('disabled', true);
            return;
        }

        // Get the field name
        var quantity = parseInt($('#quantity-' + i).val());

        // If is not undefined

        $('#quantity-' + i).val(quantity + 1);

        var data = parseInt($(this).attr('data-field'));

        var newQuantity = parseInt($('#quantity-' + i).val());

        updateQuantity(data, newQuantity)
    });

    $('.minus-' + i).click(function (e) {

        // Stop acting like a button
        e.preventDefault();
        // Get the field name
        var quantity = parseInt($('#quantity-' + i).val());

        // If is not undefined

        // Increment
        if (quantity > 1) {
            $('#quantity-' + i).val(quantity - 1);
            $('.plus-' + i).prop('disabled', false);
        }
        var data = parseInt($(this).attr('data-field'));
        var newQuantity = parseInt($('#quantity-' + i).val());

        updateQuantity(data, newQuantity)
    });
}

    



function deleteCart(val) {

    if (confirm("Are you sure ?")) {
        var id = val;
        console.log("masuk");
        console.log(id);

        $.ajax({
            type: "POST",
            url: "/Cart/DeleteCart",
            data: {
                id: id
            },
            success: function (result) {
                console.log(result.message)
                if (result.success) {
                    alert("Success remove product");
                    location.reload();
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    }

    return;
}


function updateQuantity(id, quantity) {
    $.ajax({
        type: "POST",
        url: "/Cart/Update",
        data: {
            id: id,
            quantity: quantity,
        },
        success: function (result) {
            console.log(result.message)
            if (result.message == "Success update quantity!") {
                alert("Success update quantity!");
                location.reload();
            } else {
                alert(result.message);
            }
        },
        error: function (err) {
            console.log(err);
        }
    })
}