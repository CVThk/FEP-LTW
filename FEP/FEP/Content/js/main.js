


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

// Cart
var cartSrcImgs;
var cartNameProducts;
var cartSizeProducts;
var cartPriceProducts;
var cartQuantitys;
var cartSL;

newCart();
function newCart() {
  // console.log(sessionStorage.CartSL)
  if(sessionStorage.CartSL == undefined) {
    cartSL = 0;
    cartSrcImgs = [];
    cartNameProducts = [];
    cartSizeProducts = [];
    cartPriceProducts = [];
    cartQuantitys = [];
  } else {
    // console.log('gan gia tri')
    cartSrcImgs = sessionStorage.CartSrcImgs.split(',');
    cartNameProducts = sessionStorage.CartNameProducts.split(',');
    cartSizeProducts = sessionStorage.CartSizeProducts.split(',');
    cartPriceProducts = sessionStorage.CartPriceProducts.split(',');
    cartQuantitys = sessionStorage.CartQuantitys.split(',');
    cartSL = sessionStorage.CartSL;
    
  }
}


showCart();

function showCart() {
    // console.log('show')
    // console.log(cartSL)
    // console.log(cartSrcImgs)
    // console.log(cartNameProducts)
    // console.log(cartSizeProducts)
    // console.log(cartPriceProducts)
    // console.log(cartQuantitys)
    var cartListProduct = document.querySelector('.cart-list-product');
    console.log(cartListProduct)
    var contentTam = cartListProduct.innerHTML;
    var content = '';
    for(var i = 0; i < cartSL; i++) {
      content += '<div class="cart__box"><div class="cart__img"><img src="'+cartSrcImgs[i]+'" alt=""></div><div class="cart__info"><div class="cart__title"><h6>'+cartNameProducts[i]+'</h6></div><div class="cart__int text-muted"><div class="cart-size"><h6>'+cartSizeProducts[i]+'</h6></div><div class="cart__number"><h6>'+cartQuantitys[i]+'</h6></div><h6>x</h6><div class="cart__price"><h6>'+cartPriceProducts[i]+'</h6></div></div></div></div>';
    }
    cartListProduct.innerHTML = content + contentTam;
    sessionStorage.CartSrcImgs = cartSrcImgs;
    sessionStorage.CartNameProducts = cartNameProducts;
    sessionStorage.CartSizeProducts = cartSizeProducts;
    sessionStorage.CartPriceProducts = cartPriceProducts;
    sessionStorage.CartQuantitys = cartQuantitys;
    sessionStorage.CartSL = cartSL;
}

function updateCart() {
  // newCart();
  cartSrcImgs[cartSL] = sessionStorage.imgPro;
  cartNameProducts[cartSL] = sessionStorage.titlePro;
  cartSizeProducts[cartSL] = sessionStorage.sizePro;
  cartPriceProducts[cartSL] = sessionStorage.pricePro;
  cartQuantitys[cartSL] = sessionStorage.quantityPro;
  
  // console.log(cartSL)
  // console.log(cartSrcImgs[cartSL])
  // console.log(cartNameProducts[cartSL])
  // console.log(cartSizeProducts[cartSL])
  // console.log(cartPriceProducts[cartSL])
  // console.log(cartQuantitys[cartSL])
  cartSL = parseInt(cartSL) + 1;
  var cartListProduct = document.querySelector('.cart-list-product');
  var contentTam = cartListProduct.innerHTML;
  var content = '<div class="cart__box"><div class="cart__img"><img src="'+cartSrcImgs[cartSL - 1]+'" alt=""></div><div class="cart__info"><div class="cart__title"><h6>'+cartNameProducts[cartSL - 1]+'</h6></div><div class="cart__int text-muted"><div class="cart-size"><h6>'+cartSizeProducts[cartSL - 1]+'</h6></div><div class="cart__number"><h6>'+cartQuantitys[cartSL - 1]+'</h6></div><h6>x</h6><div class="cart__price"><h6>'+cartPriceProducts[cartSL - 1]+'</h6></div></div></div></div>';
    
  cartListProduct.innerHTML = content + contentTam;
  
  sessionStorage.CartSrcImgs = cartSrcImgs;
  sessionStorage.CartNameProducts = cartNameProducts;
  sessionStorage.CartSizeProducts = cartSizeProducts;
  sessionStorage.CartPriceProducts = cartPriceProducts;
  sessionStorage.CartQuantitys = cartQuantitys;
  sessionStorage.CartSL = cartSL;
  // console.log(sessionStorage.CartSL)
  // console.log(sessionStorage.CartSrcImgs)
  // console.log(sessionStorage.CartNameProducts)
  // console.log(sessionStorage.CartSizeProducts)
  // console.log(sessionStorage.CartPriceProducts)
  // console.log(sessionStorage.CartQuantitys)
}

// Ẩn/hiện no-product in cart
checkCart();
function checkCart() {
  var cartListProduct = document.getElementsByClassName('cart__box');
  var noProduct = document.querySelector('.no-product');
  if(cartListProduct.length == 0) {
    noProduct.style.display = 'flex';
  } else {noProduct.style.display = 'none';}
}

// Thêm product vào cart
var btnAdd = document.querySelector('.product__describe-choose--add');
var btnPay = document.querySelector('.product__describe-choose--buy');
btnAdd.onclick = function() {
  var cartListProduct = document.querySelector('.cart-list-product');
  // var content = cartListProduct.innerHTML;
  var srcImg = document.getElementById('one').src;
  var nameProduct = document.querySelector('.product__describe-infor-name').querySelector('h1').innerText;
  var price = document.querySelector('.product__describe-infor-price--current').innerText;
  var numberProduct = document.querySelector('.product__describe-custom-input').value;
  var sizeProduct = document.querySelector('.button_size.active > h2').innerText;
  // content = '<div class="cart__box"><div class="cart__img"><img src="'+srcImg+'" alt=""></div><div class="cart__info"><div class="cart__title"><h6>'+nameProduct+'</h6></div><div class="cart__int text-muted"><div class="cart-size"><h6>'+sizeProduct+'</h6></div><div class="cart__number"><h6>'+numberProduct+'</h6></div><h6>x</h6><div class="cart__price"><h6>'+price+'</h6></div></div></div></div>' + content;
  // cartListProduct.innerHTML = content;
  
  sessionStorage.imgPro = srcImg;
  sessionStorage.titlePro = nameProduct;
  sessionStorage.sizePro = sizeProduct;
  sessionStorage.pricePro = price;
  sessionStorage.quantityPro = numberProduct;

  
  // newCart();
  updateCart();
  checkCart();
}

btnPay.onclick = function() {
  btnPay.querySelector('a').href = '/pay/pay.html';
  var srcImg = document.getElementById('one').src;
  var nameProduct = document.querySelector('.product__describe-infor-name').querySelector('h1').innerText;
  var price = document.querySelector('.product__describe-infor-price--current').innerText;
  var numberProduct = document.querySelector('.product__describe-custom-input').value;
  var sizeProduct = document.querySelector('.button_size.active > h2').innerText;

  sessionStorage.imgProRA = srcImg;
  sessionStorage.titleProRA = nameProduct;
  sessionStorage.sizeProRA = sizeProduct;
  sessionStorage.priceProRA = price;
  sessionStorage.quantityProRA = numberProduct;

  sessionStorage.valuePay = 0;
  
}


var payAllProduct = document.querySelector('.cart__payP');
payAllProduct.onclick = function() {
  var srcImgs = [];
  var nameProducts = [];
  var sizeProducts = [];
  var priceProducts = [];
  var quantitys = [];
  var sl;
  var listProductCart = document.getElementsByClassName('cart__box');
  sl = listProductCart.length;
  for(var i = 0; i < sl; i++) {
    srcImgs[i] = listProductCart[i].querySelector('.cart__img img').src;
    nameProducts[i] = listProductCart[i].querySelector('.cart__title h6').textContent;
    sizeProducts[i] = listProductCart[i].querySelector('.cart-size h6').textContent;
    priceProducts[i] = listProductCart[i].querySelector('.cart__price h6').textContent;
    quantitys[i] = listProductCart[i].querySelector('.cart__number h6').textContent;
    // console.log('Product ' + i);
    // console.log(srcImgs[i]);
    // console.log(nameProducts[i]);
    // console.log(sizeProducts[i]);
    // console.log(priceProducts[i]);
    // console.log(quantitys[i]);
  }
  sessionStorage.Sl = sl;
  sessionStorage.SrcImgs = srcImgs;
  sessionStorage.NameProducts = nameProducts;
  sessionStorage.SizeProducts = sizeProducts;
  sessionStorage.PriceProducts = priceProducts;
  sessionStorage.Quantitys = quantitys;
  sessionStorage.valuePay = 1;
}









