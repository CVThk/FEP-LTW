// xử lý ẩn hiện menu trên tablet + mobile khi click vào icon menu
var iconMenu = document.querySelector('.header__web-menu-tablet');
var overPlayMenu = document.querySelector('#overplay-menu');

var menu = document.querySelector('#menu-tab-mobi');
iconMenu.onclick = function() {
    menu.classList.toggle('showMenuTabMobi');
    overPlayMenu.classList.toggle('show');
    if(iconMenu.style.display == 'none') {
        menu.classList.remove('showMenuTabMobi');
        overPlayMenu.classList.remove('show');
    }
}
overPlayMenu.onclick = function() {
    overPlayMenu.classList.toggle('show');
    menu.classList.toggle('showMenuTabMobi');
    console.log(overPlayMenu.classList)
}


// Xử lý menu sổ xuống, ẩn hiện khi click icon down và close
var iconDownList = document.querySelectorAll('.cover-icon');
var subMenuDropdownList = document.querySelectorAll('.sub-menu-dropdown');

for(var i = 0; i < iconDownList.length; i++) {
    iconDownList[i].onclick = function() {
        var iconClose = this.querySelector('.ti-close');
        var iconDown = this.querySelector('.fa-angle-down');
        if(iconClose.style.display != 'flex') {
            iconClose.style.display = 'flex';
            iconDown.style.display = 'none';
        } else {
            iconClose.style.display = 'none';
            iconDown.style.display = 'flex';
        }
        var j = indexArrayIcon(this);
        if(j != -1) {subMenuDropdownList[j].classList.toggle('show')}
    }
}
function indexArrayIcon(icon) {
    for(var i = 0; i < iconDownList.length; i++) {
        if(iconDownList[i] == icon) {return i;}
    }
    return -1;
}