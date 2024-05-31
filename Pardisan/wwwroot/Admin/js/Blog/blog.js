
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
            url: '/admin/Blog/GetBlog',
            type: 'POST',
            data: function (d) {
                d['deleted'] = deleted;
            },
            complete: function () {
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
                centerilizeTd()
            },
        },
        columns: [
            { data: 'id' },
            { data: 'title' },
            { data: 'image' },
            { data: 'describtion' },
            { data: 'id', responsivePriority: -1 },
        ],
        createdRow: function (row, data, index) {
            if (data['isActive'] == false) {
                $(row).addClass("table-danger");
            }
        },
        columnDefs: [{
            "render": function (data, type, row, meta) {
                return meta.row + 1;
            },
            "targets": 0
        }, {
            "render": function (data, type, row, meta) {
                return `<a class="avatar-group image-popup" href="` + data + `">
                            <figure class="avatar mt-4 overflow-hidden" style="width:5rem;height:4rem;">
                                <img src="`+ data + `" class="rounded-circle w-100 h-100">
                            </figure>
                        </a>`;
            },
            "targets": 2
        },
        {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group" role="group">`;
                if (row['isActive'] == false) {
                    html += `<button onclick="RestoreItem(` + data + `)" class="btn btn-success "><i class="fa fa-retweet"></i></button>`;
                } else {
                    html += `<button onclick="DeleteItem(` + data + `)" class="btn btn-youtube"><i class="fa fa-trash"></i></button>`;
                }
                html += `<a href="/admin/Blog/Upsert?id=` + data + `"  class="btn text-white btn-warning"><i class="fa fa-edit "></i></a></div>`;


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
                url: '/admin/Blog/DisableBlog?Id=' + id,
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
        text: "آیا مطمئن به بازگردانی هستید؟",
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
                url: '/admin/Blog/ActiveBlog?Id=' + id,
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
