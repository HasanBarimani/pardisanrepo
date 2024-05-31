
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
            url: '/admin/Message/getAll',
            type: 'POST',
            data: function (d) {
                d['deleted'] = deleted;
            },
            complete: function () {
                //$('.image-popup').magnificPopup({
                //    type: 'image',
                //    zoom: {
                //        enabled: true,
                //        duration: 300,
                //        easing: 'ease-in-out',
                //        opener: function (openerElement) {
                //            return openerElement.is('img') ? openerElement : openerElement.find('img');
                //        }

                //    }
                //});
                centerilizeTd()
            },
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'email' },
            { data: 'content' },
            { data: 'id', responsivePriority: -1 }
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
                return `<a class="avatar-group image-popup" href="/message/` + data + `">
                      
                               <p src="`+ data + `" class="rounded-circle w-100 h-100"> ` + data + ` </p>
                       
                       </a>`;
            },
            "targets": 2
        },
        {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group" role="group">`;
               
                html += `<a href="/admin/message/Upsert?id=` + data + `"  class="btn text-white btn-warning"><i class="fa fa-edit "></i></a></div>`;


                return html;
            },
            "targets": -1
        }]
    });

});








function centerilizeTd() {
    $('td').css('vertical-align', 'middle');
}
