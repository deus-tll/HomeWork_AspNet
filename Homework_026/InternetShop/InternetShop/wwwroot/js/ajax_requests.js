$(document).ready(function () {
    $(".add-to-cart").click(function () {
        let productId = $(this).data("product-id");

        $.ajax({
            url: "/Cart/AddToCart",
            method: "POST",
            data: { productId: productId },
            success: function (data) {
                if (data.success) {
                    $("#cartCount").text(data.cartCount);
                    console.log(data.message);
                } else {
                    alert(data.error);
                }
            },
            error: function () {
                console.log("Error was occured while performing AJAX-request.");
            }
        });
    });

    $.ajax({
        url: '/Product/GetCategories',
        type: 'GET',
        success: function (data) {
            var categories = data;
            var dropdown = document.querySelector('#navbar-vertical .navbar-nav');

            categories.forEach(function (category) {
                var link = document.createElement('a');
                link.classList.add('nav-item', 'nav-link');
                link.href = '/Product/Shop/' + category.id;
                link.textContent = category.name;
                dropdown.appendChild(link);
            });
        }
    });
});