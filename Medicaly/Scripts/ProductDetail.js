﻿var url_string = window.location.href;
var url = new URL(url_string);
var id = url.searchParams.get("Id");



$(document).ready(function () {

    var quantitiy = 0;
    $('.quantity-right-plus').click(function (e) {

        // Stop acting like a button
        e.preventDefault();

        if ($('#quantity').val() >= $('#Stock').val()) {
            alert("You reach the maximum quantity");
            $('.quantity-right-plus').prop('disabled', true);
            return;
        }


        // Get the field name
        var quantity = parseInt($('#quantity').val());

        // If is not undefined

        $('#quantity').val(quantity + 1);

        // Increment

    });

    $('.quantity-left-minus').click(function (e) {
        // Stop acting like a button
        e.preventDefault();
        // Get the field name
        var quantity = parseInt($('#quantity').val());

        // If is not undefined

        // Increment
        if (quantity > 1) {
            $('#quantity').val(quantity - 1);
            $('.quantity-right-plus').prop('disabled', false);
        }
    });

});

$("#formAddToCart").submit(function (event) {

    // Stop form from submitting normally
    event.preventDefault();

    // Get some values from elements on the page:
    var quantity = $('#quantity').val();
    var productId = $('#ProductId').val();
    var customerId = $('#CustomerId').val();
    var stock = $('#Stock').val();

    if (customerId == null) {
        //alert("Please log in first");
        window.location.href = "/Auth/Login";
        return;
    }


    $.ajax({
        type: "POST",
        url: "/Cart/AddToCart",
        data: {
            customerId: customerId,
            quantity: quantity,
            productId: productId
        },
        success: function (result) {
            console.log(result.message)
            if (result.success) {
                alert("Success add product to cart");
            }
        },
        error: function (err) {
            console.log(err);
        }
    })
});

$(function () {
    $('.popup-enlarge').on('click', function () {
        $('.imagepreview').attr('src', $(this).find('img').attr('src'));
        $('#imagemodal').modal('show');
    });
});