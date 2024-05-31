
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
            url: '/admin/Blog/GetBlogCategory',
            type: 'POST',
            data: function (d) {
                d['deleted'] = deleted;
            },
            complete: function () {
            },
        },
        columns: [
            { data: 'id' },
            { data: 'title' },
            { data: 'createdAt' },
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
                //return `<p class="text-justify mt-2">` + data + `</p>`
                let html = `<p class="text-justify mt-3">`;
                if (data.length <= 40)
                    html += data;
                else
                    html += data.substr(0, 40) + "...";
                html += `</p>`;
                return html;
            },
            "targets": 1
        }, {
            "render": function (data, type, row, meta) {
                return `<p class="text-justify mt-3">` + data + `</p>`;
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
                html += `<a href="javascript:;" onclick=(modalEdit(` + meta.row + `))   class="btn text-white btn-warning"><i class="fa fa-edit "></i></a></div>`;


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
                url: '/admin/Blog/DisableBlogCategory?Id=' + id,
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
                url: '/admin/Blog/ActiveBlogCategory?Id=' + id,
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


$('#create-category-submit').on('click', function () {
    let form = document.getElementById('create-category');
    let formdata = new FormData(form);
    formdata.append('Id', typeId);
    $.ajax({
        url: '/admin/Blog/UpsertBlogCategory',
        data: formdata,
        method: 'POST',
        processData: false,
        contentType: false,
        success: function (data) {
            form.reset();
            $('#CreateCategory-Modal').modal('toggle');
            data.status == "0" ? Swal.fire('', data.message, 'error') : Swal.fire('', data.message, 'success');
            table.draw('page');
        }
    })
});

$('#update-category-submit').on('click', function () {
    let form = document.getElementById('update-category');
    let formdata = new FormData(form);
    formdata.set("Id", typeId);
    var checkbox = document.getElementById('showdelted').checked;
    console.log(checkbox);
    formdata.append('IsActive', checkbox);
    $.ajax({
        url: '/admin/Blog/UpsertBlogCategory',
        data: formdata,
        method: 'POST',
        processData: false,
        contentType: false,
        success: function (data) {
            //form.reset();
            $('#UpdateCategory-Modal').modal('toggle');
            table.draw('page');
            data.status == "0" ? Swal.fire('', data.message, 'error') : Swal.fire('', data.message, 'success');
        }
    })
});

function modalEdit(rowId) {
    document.getElementById('Title').value = table.row(rowId).data().title;
    typeId = table.row(rowId).data().id;
    $('#UpdateCategory-Modal').modal('show');
}

function CreateCategory() {
    $('#create-category').trigger('reset');
    $('#CreateCategory-Modal').modal('show');
}

