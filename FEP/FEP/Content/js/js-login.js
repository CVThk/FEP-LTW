
var eye = document.querySelector('.eye-pass');
var input = document.querySelector('#iPassword');
eye.onclick = function () {
	var iconeye = document.querySelector('#iPassword ~ .eye-pass > i');
	if (input.type == "password") {
		input.type = "text";
		iconeye.classList.remove('mdi-eye');
		iconeye.classList.add('mdi-eye-off');
	}
	else {
		input.type = "password";
		iconeye.classList.remove('mdi-eye-off');
		iconeye.classList.add('mdi-eye');
    }
}

function checkPasscode() {
	var passcode_input = document.querySelector(".pass");
	if (/^([\x00-\x7F]+[^.])+$/gm.test(passcode_input.value)) {
		passcode_input.setCustomValidity("Không nhập ký tự unicode và '.'");
	} else {
		passcode_input.setCustomValidity("");
	}
}