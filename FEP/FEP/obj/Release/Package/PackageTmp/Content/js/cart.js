
if (sessionStorage.DeleteCart == 'true') {
    Swal.fire({
        icon: 'success',
        html: '<b style="color:green;">Xóa thành công.</b>'
    })
    sessionStorage.DeleteCart = "";
}

if (sessionStorage.DeleteCart == 'false') {
    Swal.fire({
        icon: 'error',
        html: '<b style="color:red;">Xóa thất bại!</b>'
    })
    sessionStorage.DeleteCart = "";
}

if (sessionStorage.UpdateCart == 'true') {
    Swal.fire({
        icon: 'success',
        html: '<b style="color:green;">Cập nhật thành công.</b>'
    })
    sessionStorage.UpdateCart = "";
}

if (sessionStorage.UpdateCart == 'false') {
    Swal.fire({
        icon: 'error',
        html: '<b style="color:red;">Vui lòng kiểm tra số lượng tồn!</b>'
    })
    sessionStorage.UpdateCart = "";
}