


var searchInput = document.getElementById('search__input');
var resultInput = document.querySelector('.result-search');
var overPlay = document.querySelector('#overplay-search');


overPlay.onclick = function() {
    resultInput.style.display = 'none';
    overPlay.style.display = 'none';
}



searchInput.oninput = function(e) {
    var productList = document.getElementsByClassName('product');
    resultInput.style.display = 'block';
    overPlay.style.display = 'block';
    var str = "";
    for(var i = 0; i < productList.length; i++) {
        var productCaptionLink = productList[i].querySelector('.product__caption-link');
        if(productCaptionLink.textContent.includes(searchInput.value)) {
            var productLink = productList[i].querySelector('.product__link');
            var link = productLink.href;
            str = str + '<a href="'+link+'">'+productCaptionLink.textContent+'</a>'
            
        }
    }
    resultInput.innerHTML = str;
}
