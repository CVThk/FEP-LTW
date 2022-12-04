
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
        html: '<b style="color:red;">Lỗi không thể cập nhật!</b>'
    })
    sessionStorage.UpdateCart = "";
}

if (sessionStorage.MaxInventoryCart != null) {
    console.log(sessionStorage.MaxInventoryCart)
}