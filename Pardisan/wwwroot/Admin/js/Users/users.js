
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
            url: '/admin/Users/GetUsers',
            type: 'POST',
            data: function (d) {
                d['deleted'] = deleted;
            },
        },
        columns: [
            { data: 'id' },
            { data: 'firstName' },
            { data: 'userName' },
            { data: 'email' },
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
        },
        {
            "render": function (data, type, row, meta) {
                return `<p class="text-justify mt-4">` + data + " " + row.lastName + `</p>`
            },
            "targets": 1
        },
        {
            "render": function (data, type, row, meta) {
                return `<p class="text-justify mt-4">` + data + `</p>`
            },
            "targets": 2
        },
        {
            "render": function (data, type, row, meta) {
                return `<p class="text-justify mt-4">` + data + `</p>`
            },
            "targets": 3
        },
        {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group mt-1" role="group">`;
                html += `<a href="/admin/Users/Upsert?id=` + data + `"  class="btn text-white btn-warning"><i class="fa fa-edit "></i></a></div>`;
                return html;
            },
            "targets": -1
        }]
    });

});


