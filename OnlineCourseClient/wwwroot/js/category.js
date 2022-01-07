
function Insert() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.nik = $("#employeeNik").val();
    obj.firstName = $("#employeeFirstName").val();
    obj.lastName = $("#employeeLastName").val();
    obj.phone = $("#employeePhone").val();
    obj.salary = $("#employeeSalary").val();
    obj.email = $("#employeeEmail").val();
    obj.gender = $("#employeeGender").val();
    obj.birthDate = $("#employeeBirthDate").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        /* headers: { 'Content-Type': 'application/json' },*/
        url: "/Employees/post/",
        type: "POST",
        data: obj,
        /* data: JSON.stringify(obj),*/
        dataType: 'json'
    }).done((result) => {
        console.log(obj);
        if (result == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Adding Employee to database',
            }),
                Table.ajax.reload()
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed adding Employee to Database',
            })
        }
    }).fail((error) => {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed adding Employee to Database',
        })
    })
}



function editModal(nik) {
    $.ajax({
        url: "/Employees/get/" + nik,
        success: function (result) {
            console.log(result)
            var data = result
            $("#employeeNik").attr("value", data.nik)
            $("#employeeFirstName").attr("value", data.firstName)
            $("#employeeLastName").attr("value", data.lastName)
            $("#employeeEmail").attr("value", data.email)
            $("#employeePhone").attr("value", data.phone)
            $("#employeeSalary").attr("value", data.salary)
            $("#employeeBirthDate").attr("value", data.birthDate)
            $("#employeeGender").attr("value", data.gender)
        },
        error: function (error) {
            console.log(error)
        }
    })
}


function InsertCategory() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.id = $("#categoryId").val();
    obj.name = $("#categoryName").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        /* headers: { 'Content-Type': 'application/json' },*/
        url: "/Categories/post/",
        type: "POST",
        data: obj,
        /* data: JSON.stringify(obj),*/
        dataType: 'json'
    }).done((result) => {
        console.log(obj);
        if (result == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Adding Category to database',
            }),
                Table.ajax.reload()
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed adding Category to Database',
            })
        }
    }).fail((error) => {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed adding Category to Database',
        })
    })
}

function DeleteCategory(id) {
    console.log(id)
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
            $.ajax({
                 //headers: { 'Content-Type': 'application/json' },
                url: "/Categories/delete/" + id,
                type: "DELETE",
                dataType: 'json'
            }).done((result) => {
                console.log(result)
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Delete Category from database',
                }),
                    Table.ajax.reload()
            }).fail((error) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Failed deleting Category from Database',
                })
            })
        }
    })
}

function editCategory(id) {
    $.ajax({
        url: "/Categories/get/" + id,
        success: function (result) {
          
            var data = result
            $("#editCategoryID").attr("value", data.id)
            $("#editCategoryName").attr("value", data.name)

        },
        error: function (error) {
            console.log(error)
        }
    })
}

function UpdateCategory(id) {
    console.log(id)
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.id = $("editCategoryID").val();
    obj.name = $("#editCategoryName").val();
    $.ajax({
        /*headers: { 'Content-Type': 'application/json' },*/
        url: "/Categories/put/" + id,
        type: "PUT",
        data: obj,
        dataType: 'json'
    }).done((result) => {
        if (result == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Updating Category in database',
            }),
                Table.ajax.reload()
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed updating Category to Database',
            })
        }
    }).fail((error) => {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed updating Category to Database',
        })
    })
}



function Update(nik) {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.nik = $("#employeeNik").val();
    obj.firstName = $("#employeeFirstName").val();
    obj.lastName = $("#employeeLastName").val();
    obj.phone = $("#employeePhone").val();
    obj.salary = $("#employeeSalary").val();
    obj.email = $("#employeeEmail").val();
    obj.gender = $("#employeeGender").val();
    obj.birthDate = $("#employeeBirthDate").val();
    $.ajax({
        /*headers: { 'Content-Type': 'application/json' },*/
        url: "/Employees/put/" + nik,
        type: "PUT",
        data: obj,
        dataType: 'json'
    }).done((result) => {

        Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'Adding Employee to database',
        }),
            Table.ajax.reload()

    }).fail((error) => {

        //Swal.fire({
        //    icon: 'error',
        //    title: 'Error',
        //    text: 'Failed adding Employee to Database',
        //})
    })
}

function Delete(nik) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
            $.ajax({
                /* headers: { 'Content-Type': 'application/json' },*/
                url: "/Employees/delete/" + nik,
                type: "DELETE",
                dataType: 'json'
            }).done((result) => {
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Delete Employee from database',
                }),
                    Table.ajax.reload()
            }).fail((error) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Failed deleting Employee from Database',
                })
            })
        }
    })

}

//$(document).ready(function () {
//    $("#customers").DataTable({
//        "ajax": {
//            "url": "https://pokeapi.co/api/v2/pokemon",
//            "dataSrc": "results"
//        },
//        "columns": [
//            { "data": "url" },
//            { "data": "name" },
//            {
//                "data": null,
//                "render": function (data, type, row) {
//                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" onclick="getData('${row["url"]}')" data-bs-target="#exampleModal">
//                              Detail Character
//                            </button>`;
//                }
//            }
//        ]
//    });


//GET TABEL CATEGORY
$(document).ready(function () {
    Table = $("#categories").DataTable({
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



src = "https://cdn.jsdelivr.net/npm/jquery-validation@1.19.3/dist/jquery.validate.js"
src = "https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"