


//THAY ĐỔI HÌNH ẢNH SẢN PHẨM
function changeImage(id){
    let imagePath = document.getElementById(id).getAttribute('src');
    document.getElementById('img-main').setAttribute('src', imagePath);
}

//TĂNG GIẢM SỐ LƯỢNG SẢN PHẨM (JQUERY)

$('input.product__describe-custom-input').each(function() {
    var $this = $(this),
      qty = $this.parent().find('.is-form'),
      min = Number($this.attr('min')),
      max = Number($this.attr('max'))
    if (min == 0) {
      var d = 0
    }
      else d = min
    $(qty).on('click', function() {
      if ($(this).hasClass('product__describe-custom-reduced')) {
        if (d > min) d += -1
      } else if ($(this).hasClass('product__describe-custom-increase')) {
        var x = Number($this.val()) + 1
        if (x <= 11) d += 1
      }
      if(d >= 11){
        d -= 1;
        alert("Đã quá giới hạn mua hàng, không thể thêm sản phẩm!");
      }
      $this.attr('value', d).val(d)
    })
  })


 //CHỌN SIZE
const buttons = document.querySelectorAll(".button_size");
buttons.forEach(function(btn, index){
    btn.addEventListener("click", function(){
      document.querySelector(".button_size.active").classList.remove("active");
      this.classList.add("active");    
  })
})










