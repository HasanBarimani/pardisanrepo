

toastr.options = {
    timeOut: 4345,
    progressBar: true,
    showMethod: "slideDown",
    hideMethod: "slideUp",
    showDuration: 200,
    hideDuration: 200
};

var table;
var deleted = false;
let formUrl;
let typeId;
let userId;
let userName;
let rowIdCustomer;
$(function () {
    $("body").addClass("sticky-page-header");
    $('input[name="id"]').val(new URLSearchParams(window.location.search).get('id'));



});

$('#submit-formEdit').on('click', function () {
    let form = document.getElementById('formCreate');
    let formdata = new FormData(form);

    $.ajax({
        url: '/home/Message',
        data: formdata,
        method: 'POST',
        processData: false,
        contentType: false,
        success: function (data) {
            form.reset();
            //!data.succeeded ? Swal.fire('', data.errors[i], 'error') : Swal.fire('', data.message, 'success');
            alert();
            window.location = "/home/Index"
        }
    })
    document.getElementById('formCreate').reset();
});


