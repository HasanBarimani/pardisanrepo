
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
let model = {
    id: $('#HoldingId').val()
    }
$(function () {
    $("body").addClass("sticky-page-header");
    $('input[name="id"]').val(new URLSearchParams(window.location.search).get('id'));

    table = $('.ajax-data-table').DataTable({
        language: {
            url: '/admin/vendors/dataTable/fa.json'
        },
        responsive: true,
        searchDelay: 500,
        processing: true,
        serverSide: true,
        ordering: false,
        ajax: {
            url: '/admin/Holding/GetGallery',
            type: 'POST',
            data: model,
            complete: function () 
            {
                $('.image-popup').magnificPopup({
                    type: 'image',
                    zoom: {
                        enabled: true,
                        duration: 300,
                        easing: 'ease-in-out',
                        opener: function (openerElement) {
                            return openerElement.is('img') ? openerElement : openerElement.find('img');
                        }

                    }
                });
                centerilizeTd();
            },
        },
        columns: [
            { data: 'id' },
            { data: 'url' },
            { data: 'id'},
        ],
        //createdRow: function (row, data, index) {
        //    if (data['isActive'] == false) {
        //        $(row).addClass("table-danger");
        //    }
        //},
        columnDefs: [{
            "render": function (data, type, row, meta) {
                return meta.row + 1;
            },
            "targets": 0
        },
        {
            "render": function (data, type, row, meta) {
                return `<a class="avatar-group image-popup" href="` + data + `">
                            <figure class="avatar mt-4 overflow-hidden" style="width:5rem;height:4rem;">
                                <img src="`+ data + `" style={{border-radius: 6px}} class="w-100 h-100">
                            </figure>
                        </a>`;
            },
            "targets": 1
            },
        {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group" role="group">`;
                html += `<button onclick="RestoreItem(` + data + `)" class="btn btn-danger btn-sm"><i class="fa fa-trash"></i></button>`;
                
                return html;
            },
            "targets": -1
        }]
    });

});

function SwitchDeleted() {
    if ($('#showdelted:checked').length > 0) {
        deleted = true;
    } else {
        deleted = false;
    }
    table.draw('page');
}

function DeleteItem(id) {
    Swal.fire({
        title: '',
        text: "آیا مطمئن به حذف هستید؟",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
        confirmButtonClass: 'btn btn-primary',
        cancelButtonClass: 'btn btn-danger ml-1',
        buttonsStyling: false,
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: '/admin/Holding/DisableEstate?Id=' + id,
                beforeSend: function () { $("button").prop("disabled", true); },
                complete: function () { $("button").prop("disabled", false); },
                success: function (data) {
                    data.status == "0" ? Swal.fire('', data.message, 'error') : Swal.fire('', data.message, 'success');
                    table.draw('page');
                }
            });
        }

    });
}

function RestoreItem(id) {
    Swal.fire({
        title: '',
        text: "آیا مطمئن به حذف هستید؟",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
        confirmButtonClass: 'btn btn-primary',
        cancelButtonClass: 'btn btn-danger ml-1',
        buttonsStyling: false,
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: '/admin/Holding/DeleteImage?Id=' + id,
                beforeSend: function () { $("button").prop("disabled", true); },
                complete: function () { $("button").prop("disabled", false); },
                success: function (data) {

                    data.status == "0" ? Swal.fire('', data.message, 'error') : Swal.fire('', data.message, 'success');
                    table.draw('page');
                }
            });
        }
    });
}

function centerilizeTd() {
    $('td').css('vertical-align', 'middle');
}