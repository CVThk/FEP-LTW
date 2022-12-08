

var inputPhone = $('#iPhone')
console.log(inputPhone)

if (/^([0-9]{10,11})$/gm.test(inputPhone.val)) {
    .setCustomValidity("Nhập không đúng định dạng.")
}
