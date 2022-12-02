
function changeImage(id) {
    let imagePath = document.getElementById(id).getAttribute('src');
    document.getElementById('img-main').setAttribute('src', imagePath);
}

//TĂNG GIẢM SỐ LƯỢNG SẢN PHẨM (JQUERY)

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
            html: '<b style="color:red;">Đã vượt qua số lượng tồn kho!</b>',
            timer: 3000
        })
    }
    inputThis.attr('value', d).val(d)
})


 //CHỌN SIZE
var inputShowCur = $('input.product__describe-custom-input.show')
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

        var inputThis = $('input.product__describe-custom-input.show')
        max = Number(inputThis.attr('max'))
        $('#Amount-inventory').text(max)

        inputShowCur = $('input.product__describe-custom-input.show')
        console.log(inputShowCur)
  })
})



$(inputShowCur).on('input', function () {
    console.log(inputShowCur)
    max = Number($(this).attr('max'))
    min = Number($(this).attr('min'))
    val = Number($(this).val())
    if (val > max) {
        Swal.fire({
            icon: 'error',
            html: '<b style="color:red;">Đã vượt qua số lượng tồn kho!</b>',
            timer: 3000
        })
        $(this).val(max)
    }
    else if (val < min) {
        Swal.fire({
            icon: 'error',
            html: '<b style="color:red;">Vui lòng chọn số lượng cho phù hợp!</b>',
            timer: 3000
        })
        $(this).val(min)
    }
})


$('#add-cart').on('click', function () {
    var sizeChoose = $('.button_size.active')
    var size = sizeChoose.text()
    var amountInput = $('input.product__describe-custom-input.show').val().toString()
    var idSneakerText = sessionStorage.idSneaker
    var user = sessionStorage.idclient
    $('#Size').val(size)
    $('#Amount').val(amountInput)
    $('#IdSneaker').val(idSneakerText)
    $('#IdClient').val(user)
})

$('#log-out').on('click', function () {
    sessionStorage.clear();
})


if (sessionStorage.errAmount != null) {
    Swal.fire({
        icon: 'error',
        title: 'Error!',
        html: '<b style="color:red;">Đã vượt qua số lượng tồn kho!</b>',
        timer: 3000
    })
}



