
/*console.log(sessionStorage.login)*/
if (sessionStorage.login == 'false') {
    errLogin();
}
else if (sessionStorage.login == 'true') {
    successLogin();
}

function errLogin() {
    Swal.fire({
        icon: 'error',
        title: 'Error!',
        html: '<b style="color:red;">Tên đăng nhập hoặc mật khẩu không chính xác!</b>'
        //footer: '<a href="">Why do I have this issue?</a>'
    })
}

function successLogin() {
    Swal.fire({
        icon: 'success',
        title: 'Success!',
        html: '<b style="color:green;">Welcom back!!!</b>'
    })
    setTimeout(() => {
        var link;
        if (sessionStorage.typeLogin == 'AD') {
            link = "/Admin/Dashboard";
        }
        else {
            link = "/Home/FEP";
        }
        window.open(link, "_self");
    }, 2000);
}