$(document).ready(function () {

    var quantitiy = 0;
    $('.quantity-right-plus').click(function (e) {

        // Stop acting like a button
        e.preventDefault();
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
        if (quantity > 0) {
            $('#quantity').val(quantity - 1);
        }
    });

});

function deleteCart(val) {
    var id = val;
    console.log("masuk");
    console.log(id);

    $.ajax({
        type: "POST",
        url: "/Shopping/DeleteCart",
        data: {
            id: id
        },
        success: function (result) {
            console.log(result.message)
            if (result.success) {
                alert("Success Remove Product");
                $('#Cart').load('/Shopping/Cart #Cart');
            }
        },
        error: function (err) {
            console.log(err);
        }
    })
}