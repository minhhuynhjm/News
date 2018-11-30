function AddCart(productID) {
    $.ajax({
        type: "GET",
        url: "/Comment/AddCartServlet?proID=" + productID,
        success: function (data) {
            hotsnackbar('hsdone', data);
        }
    });
}