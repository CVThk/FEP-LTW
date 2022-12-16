
function changeImage(id) {
    let imagePath = document.getElementById(id).getAttribute('src');
    document.getElementById('img-main').setAttribute('src', imagePath);
}

//TĂNG GIẢM SỐ LƯỢNG SẢN PHẨM (JQUERY)

$('.is-form').on('click', function () {
    var inputThis = $('#input-amount')
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
    $('#Amount').val(d)
    $('#AmountN').val(d)
})

var listInventory = sessionStorage.inventory.split(',');
var inputAmount = $('#input-amount')
const buttons = document.querySelectorAll(".button_size");
buttons.forEach(function(btn, index){
    btn.addEventListener("click", function () {
        if (document.querySelector(".button_size.active") != null) {
            document.querySelector(".button_size.active").classList.remove("active");
        }
        this.classList.add("active");
        $(inputAmount).attr('max', listInventory[index])
        $(inputAmount).attr('value', '1')
        max = Number(listInventory[index])
        $('#Amount-inventory').text(max)
        $('#Size').val($('.button_size.active').attr('id'))
        $('#SizeN').val($('.button_size.active').attr('id'))
  })
})

$(listInventory).on('input', function () {
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
        val = max
    }
    else if (val < min) {
        Swal.fire({
            icon: 'error',
            html: '<b style="color:red;">Vui lòng chọn số lượng cho phù hợp!</b>',
            timer: 3000
        })
        $(this).val(min)
        val = min
    }
})




$('#log-out').on('click', function () {
    sessionStorage.clear();
})






