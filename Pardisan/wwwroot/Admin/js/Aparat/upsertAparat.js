
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
    $('#Image').change(function () {
        var input = this;
        var url = $(this).val();
        var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
        if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#thumbimage').attr('src', e.target.result);
            }   
            reader.readAsDataURL(input.files[0]);
        }
        else {
            $('#Image').attr('src', '/Uploads/tempimage.jpg');
        }
    });


});

$('#submit-formCreate').on('click', function () {
    let form = document.getElementById('formCreate');
    let formdata = new FormData(form);
    $.ajax({
        url: '/admin/Aparat/Upsert',
        data: formdata,
        method: 'POST',
        processData: false,
        contentType: false,
        success: function (data) {
            form.reset();
            !data.succeeded ? Swal.fire('', data.errors[i], 'error') : Swal.fire('', data.message, 'success');
            window.location = "/admin/Aparat/Index"
        }
    })
});

$('#submit-formEdit').on('click', function () {
    let form = document.getElementById('formCreate');
    let formdata = new FormData(form);
    $.ajax({
        url: '/admin/Aparat/Upsert',
        data: formdata,
        method: 'POST',
        processData: false,
        contentType: false,
        success: function (data) {
            form.reset();
            !data.succeeded ? Swal.fire('', data.errors[i], 'error') : Swal.fire('', data.message, 'success');
            window.location = "/admin/Aparat/Index"
        }
    })
});
