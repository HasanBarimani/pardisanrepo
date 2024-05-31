
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
var KTDocsAdd = function () {
    var map, marker = null;
    const locationChanged = function (e) {
        fetch(`https://api.neshan.org/v4/reverse?lat=${e.lat}&lng=${e.lng}`, {
            headers: {
                'Content-Type': 'application/json',
                'Api-Key': 'service.6JJqS1dMExBteluzRuL4zlIDzVXb7cieW2FO3SBI'
            },
        }).then(response => response.json())
            .then(result => {
                document.getElementById('Lat').value = e.lat;
                document.getElementById('Long').value = e.lng;
                let addressElement = document.getElementById('Province');
                addressElement.value = result.formatted_address;
                //KTUtil.scrollTo(addressElement, 150);
                //addressElement.focus();
            }).catch(error => console.log(error));
    };


    return {
        init: function () {
            const defaultLat = document.getElementById('Lat').value == '' ? 36.467240 : document.getElementById('Lat').value;
            const defaultLng = document.getElementById('Long').value == '' ? 52.844797 : document.getElementById('Long').value;
            map = new L.Map('map', {
                key: 'web.DvgRmwDJDGt5lyqNPnYQ8FOkyIaofLrShHKDennS',
                maptype: 'standard-day',
                poi: true,
                traffic: false,
                center: [defaultLat, defaultLng],
                zoom: 14
            });
            if (document.getElementById('Lat').value != '' && document.getElementById('Long').value != '') {
                let latlng = new L.LatLng(defaultLat, defaultLng);
                marker = L.marker(latlng)
                    .bindPopup(`<span class="font-sans-serif"><b>مختصات جغرافیایی:</b><br>${latlng.toString()}</span>`)
                    .addTo(map);
                map.flyTo(latlng, 13);
            }
            map.on('click', (e) => {
                if (marker == null) {
                    marker = L.marker(e.latlng)
                        .bindPopup(`<span class="font-sans-serif"><b>مختصات جغرافیایی:</b><br>${e.latlng.toString()}</span>`)
                        .addTo(map);
                } else {
                    marker.setLatLng(e.latlng);
                }
                map.flyTo(e.latlng, 13);
                locationChanged(e.latlng);
            });
        }
    }
}();
KTUtil.onDOMContentLoaded((function () {
    KTDocsAdd.init();
}));


$('#submit-formCreate').on('click', function () {
    let form = document.getElementById('formCreate');
    let formdata = new FormData(form);
    formdata.append("History", theEditor.getData());

    $.ajax({
        url: '/admin/Holding/Upsert',
        data: formdata,
        method: 'POST',
        processData: false,
        contentType: false,
        success: function (data) {
            form.reset();
            data.status == "0" ? Swal.fire('', data.message, 'error') : Swal.fire('', data.message, 'success');
            window.location = "/admin/Holding/Index"
        }
    })
});

$('#submit-formEdit').on('click', function () {
    let form = document.getElementById('formCreate');
    let formdata = new FormData(form);
    formdata.append("History", theEditor.getData());

    $.ajax({
        url: '/admin/Holding/Upsert',
        data: formdata,
        method: 'POST',
        processData: false,
        contentType: false,
        success: function (data) {
            form.reset();
            data.status == "0" ? Swal.fire('', data.message, 'error') : Swal.fire('', data.message, 'success');
            window.location = "/admin/Holding/Index"
        }
    })
});
