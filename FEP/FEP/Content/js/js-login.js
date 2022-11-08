

function checkEmail(mail) {
    if(!(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(mail.value))) {return false;}
    return true;
}
function checkPass(pass) {
    if(pass.value.length < 10) {return false;}
    return true;
}
function checkAgreeTerm(agr) {
    if(agr.checked == false) {return false;}
    return true;
}

function Login() {
    var emailLogin = document.getElementById('email-account');
    var passLogin = document.getElementById('password');
    var agreeTerm = document.getElementById('agree');
    var btnLogin = document.getElementById('login').querySelector('input');
    btnLogin.onclick = function() {
        var ktEmail = checkEmail(emailLogin);
        var ktPass = checkPass(passLogin);
        var ktAgree = checkAgreeTerm(agreeTerm);
        var errEmailLogin = document.querySelector('#input-account span');
        var errPassLogin = document.querySelector('#input-password span');
        var errAgreeTerm = document.querySelector('.cover-clauses span');
        if(ktEmail == false) {
            errEmailLogin.classList.remove('hide');
            errEmailLogin.classList.add('show');
        }else {
            errEmailLogin.classList.add('hide');
            errEmailLogin.classList.remove('show');
        }
        if(ktPass == false) {
            errPassLogin.classList.remove('hide');
            errPassLogin.classList.add('show');
        }else {
            errPassLogin.classList.add('hide');
            errPassLogin.classList.remove('show');
        }
        if(ktAgree == false) {
            errAgreeTerm.classList.remove('hide');
            errAgreeTerm.classList.add('show');
        }else {
            errAgreeTerm.classList.add('hide');
            errAgreeTerm.classList.remove('show');
        }
        if(ktEmail == true && ktPass == true && ktAgree == true) {
            var fSuccessLongin = document.getElementById('form-success-login');
            fSuccessLongin.classList.remove('hide');
            fSuccessLongin.classList.add('show');
            setTimeout(() => {
                fSuccessLongin.classList.remove("show");
                fSuccessLongin.classList.add("hide");
                window.open("./index.html","_self");
            }, 2000);
        }
    }
}

function Register() {
    var emailRegister = document.getElementById('email-register');
    var passRegister = document.getElementById('password-register');
    var rePassRegister = document.getElementById('password-register-re');
    var agreeTermRegister = document.getElementById('agree-register');
    var btnRegister = document.getElementById('register').querySelector('input');
    btnRegister.onclick = function() {
        var ktEmailRegister = checkEmail(emailRegister);
        var ktPassRegister = checkPass(passRegister);
        var ktPassRe = checkPass(rePassRegister);
        var ktAgreeRegister = checkAgreeTerm(agreeTermRegister);
        var errEmailRegister = document.querySelector('#input-account-register span');
        var errPassRegister = document.querySelector('#input-password-register span');
        var errPassReLength = document.querySelector('#length');
        var errPassRE = document.querySelector('#re-enter');
        var errAgreeTerm = document.querySelector('#body-register .cover-clauses span');
        if(ktEmailRegister == false) {
            errEmailRegister.classList.remove('hide');
            errEmailRegister.classList.add('show');
        }else {
            errEmailRegister.classList.add('hide');
            errEmailRegister.classList.remove('show');
        }
        if(ktPassRegister == false) {
            errPassRegister.classList.remove('hide');
            errPassRegister.classList.add('show');
        }else {
            errPassRegister.classList.add('hide');
            errPassRegister.classList.remove('show');
        }
        if(ktPassRe == false) {
            errPassReLength.classList.remove('hide');
            errPassReLength.classList.add('show');
        }else {
            errPassReLength.classList.add('hide');
            errPassReLength.classList.remove('show');
        }
        if(passRegister.value != rePassRegister.value) {
            errPassRE.classList.remove('hide');
            errPassRE.classList.add('show');
        } else {
            errPassRE.classList.add('hide');
            errPassRE.classList.remove('show');
        }
        if(ktAgreeRegister == false) {
            errAgreeTerm.classList.remove('hide');
            errAgreeTerm.classList.add('show');
        }else {
            errAgreeTerm.classList.add('hide');
            errAgreeTerm.classList.remove('show');
        }

        if(ktEmailRegister == true && ktPassRegister == true && ktPassRe == true && passRegister.value == rePassRegister.value && ktAgreeRegister == true) {
            var fSuccessRegister = document.getElementById('form-success-register');
            fSuccessRegister.classList.remove('hide');
            fSuccessRegister.classList.add('show');
            setTimeout(() => {
                fSuccessRegister.classList.remove("show");
                fSuccessRegister.classList.add("hide");
                window.open("./login.html","_self");
            }, 2000);
        }

    }
}




Login();
var loginP = document.getElementById('itemLogin');
var bdLogin = document.getElementById('body-login');
var registerP = document.getElementById('itemRegister');
var bdRegister = document.getElementById('body-register');

loginP.onclick = function() {
    loginP.classList.add('activeStateUser');
    bdLogin.classList.add('show');
    bdLogin.classList.remove('hide');
    registerP.classList.remove('activeStateUser');
    bdRegister.classList.remove('show');
    bdRegister.classList.add('hide');
    Login();
}


registerP.onclick = function() {
    registerP.classList.add('activeStateUser');
    bdRegister.classList.add('show');
    bdRegister.classList.remove('hide');
    loginP.classList.remove('activeStateUser');
    bdLogin.classList.add('hide');
    bdLogin.classList.remove('show');
    Register();
}