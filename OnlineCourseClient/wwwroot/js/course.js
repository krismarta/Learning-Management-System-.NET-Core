
//GET COURSE CATEGORY
$(document).ready(function () {
    Table = $("#courses").DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "ajax": {
            "url": "/Categories/getall",
            "dataSrc": ""
        },
        "columns": [
            { "data": "id" },
            { "data": "name" },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    return `<button type="button" class="fas fa-edit" data-bs-toggle="modal" onclick="editCategory('${row["id"]}')" data-bs-target="#editCategoryModal">
                              Edit
                            </button>`;
                }
            },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    return `<button type="button" class="fas fa-trash" data-bs-toggle="modal" onclick="DeleteCategory('${row["id"]}')">
                              Delete
                            </button>`;
                }
            }
        ],

        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'pdf', 'print',
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"></i> Excel',
                titleAttr: 'Export to Excel',
                title: 'Employees',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ]

    });
});

//kristianto (Course)

$('.paragraf-simple').each(function (f) {

    var newstr = $(this).text().substring(0, 100);
    $(this).text(newstr + "...");

});

$('.price-simple').each(function (f) {

    var newstr = $(this).text();
    $(this).html("<sup>Rp</sup>" + formatRupiah(newstr));

});

function formatRupiah(angka) {
    var number_string = angka.replace(/[^,\d]/g, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);

    // tambahkan titik jika yang di input sudah menjadi angka ribuan
    if (ribuan) {
        separator = sisa ? '.' : '';
        rupiah += separator + ribuan.join('.');
    }

    rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
    return rupiah;
}