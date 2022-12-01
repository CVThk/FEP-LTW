//THAY ĐỔI HÌNH ẢNH SẢN PHẨM
function changeImage(id){
    let imagePath = document.getElementById(id).getAttribute('src');
    document.getElementById('img-main').setAttribute('src', imagePath);
}

//TĂNG GIẢM SỐ LƯỢNG SẢN PHẨM (JQUERY)

//$('input.product__describe-custom-input').each(function() {
//    var $this = $(this)
//    qty = $this.parent().find('.is-form')
//    $(qty).on('click', function () {
//        var inputThis = $('input.product__describe-custom-input.show')
//        max = Number(inputThis.attr('max'))
//        min = Number(inputThis.attr('min'))
//        d = min
//        if ($(this).hasClass('product__describe-custom-reduced')) {
//            if (d > min) d += -1
//        } else if ($(this).hasClass('product__describe-custom-increase')) {
//            d += 1
//            //var x = Number(inputThis.val()) + 1
//            //if (x <= max) d += 1
//        }
//        if (d > max) {
//            d -= 1;
//            Swal.fire({
//                icon: 'error',
//                title: 'Error!',
//                html: '<b style="color:red;">Đã vượt qua số lượng tồn kho!</b>'
//                //footer: '<a href="">Why do I have this issue?</a>'
//            })
//        }
//        inputThis.attr('value', d).val(d)      
//    })
//  })

$('.is-form').on('click', function () {
    var inputThis = $('input.product__describe-custom-input.show')
    max = Number(inputThis.attr('max'))
    min = Number(inputThis.attr('min'))
    d = Number(inputThis.val())
    if ($(this).hasClass('product__describe-custom-reduced')) {
        if (d > min) d += -1
    } else if ($(this).hasClass('product__describe-custom-increase')) {
        d = d + 1;
    }
    if (d > max) {
        d -= 1;
        Swal.fire({
            icon: 'error',
            title: 'Error!',
            html: '<b style="color:red;">Đã vượt qua số lượng tồn kho!</b>'
        })
    }
    inputThis.attr('value', d).val(d)
})


 //CHỌN SIZE
const buttons = document.querySelectorAll(".button_size");
buttons.forEach(function(btn, index){
    btn.addEventListener("click", function () {
        if (document.querySelector(".button_size.active") != null) {
            document.querySelector(".button_size.active").classList.remove("active");
        }
        this.classList.add("active");
        var inputshow = document.querySelector(".product__describe-custom-input.show");
        inputshow.classList.remove("show");
        inputshow.classList.add("hide");
        var idString = "inventory" + index.toString();
        var inputchoose = document.getElementById(idString);
        inputchoose.classList.remove("hide");
        inputchoose.classList.add("show");
  })
})


$('#add-cart').on('click', function () {
    var sizeChoose = $('.button_size.active')
    if (sizeChoose == null) {
        Swal.fire({
            icon: 'error',
            title: 'Error!',
            html: '<b style="color:red;">Vui lòng chọn size giày!</b>'
        })
    }
    var size = sizeChoose.text()
    var amountInput = $('input.product__describe-custom-input.show').val().toString()
    var idSneakerText = sessionStorage.idSneaker
    var user = sessionStorage.idclient
    if (typeof(user) == 'undefined') {
        Swal.fire({
            icon: 'error',
            title: 'Error!',
            html: '<b style="color:red;">Vui lòng đăng nhập vào để chọn giày!</b>'
        })
    }
    $('#Size').val(size)
    $('#Amount').val(amountInput)
    $('#IdSneaker').val(idSneakerText)
    $('#IdClient').val(user)
})








