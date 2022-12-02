

// var slides = document.querySelector('.slideshow').getElementsByClassName('slide');

// var pos = 1;


// function currentSlide(n) {
//     showSlides(pos = n);
// }

// function slideShow(n) {
    
//     if(n > slides.length) {pos = 1;}
//     if(n < 0) {pos = slides.length;}
//     // for(var index = 0; index < slides.length; index++) {
//     //     slides[index].style.display = 'none';
//     // }
//     // slides[pos - 1].setAttribute('margin-left', 'calc()')
// }




var dots = document.getElementsByClassName('dot');
for(var i = 0; i < dots.length; i++) {
    // console.log(dots[i])
    dots[i].onclick = function(e) {
        var dotActive = document.querySelector('.dot.active');
        dotActive.classList.remove('active');
        this.classList.add('active');
    }
}