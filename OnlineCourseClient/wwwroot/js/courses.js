function DeleteCourse(id) {

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
                url: "/Course/delete/" + id,
                type: "DELETE",
                dataType: 'json'
            }).done((result) => {
                console.log(result)
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Deleting Course from database',
                }),
                    Table.ajax.reload()
            }).fail((error) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Failed deleting Courses from Database',
                })
            })
        }
    })
}


function editCourse(id) {
    $.ajax({
        url: "/Course/get/" + id,
        success: function (result) {

            var data = result
            $("#editCourseID").attr("value", data.id)
            $("#editCourseTitle").attr("value", data.title)
            $("#editCourseDescription").attr("value", data.description)
            $("#editCoursePrice").attr("value", data.price)
            $("#editCourseThumbnail").attr("value", data.thumbnail)
            $("#editCourseCategory").attr("value", data.)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function UpdateCourse(id) {
    console.log(id)
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.id = $("editCourseID").val();
    obj.name = $("#editCourseName").val();
    $.ajax({
        /*headers: { 'Content-Type': 'application/json' },*/
        url: "/Course/put/" + id,
        type: "PUT",
        data: obj,
        dataType: 'json'
    }).done((result) => {
        if (result == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Updating Course in database',
            }),
                Table.ajax.reload()
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed updating Course to Database',
            })
        }
    }).fail((error) => {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed updating Course to Database',
        })
    })
}

function InsertCourse() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.title = $("#courseTitle").val();
    obj.description = $("#courseDescription").val();
    obj.price = $("#coursePrice").val();
    obj.thumbnail = $("#courseThumbnail").val();
    obj.Categoryid = $("#courseCategoryid").val();
    obj.Userid = $("#courseUserid").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        /* headers: { 'Content-Type': 'application/json' },*/
        url: "/Course/post/",
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
                text: 'Adding Courses to database',
            }),
                Table.ajax.reload()
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed adding Courses to Database',
            })
        }
    }).fail((error) => {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed adding Courses to Database',
        })
    })
}

function getDataCourse(id) {
    $.ajax({
        url: "/Course/get/" + id ,
    }).done((result) => {
        console.log(result);

        var img = "";
       img = `
<img class="img-fluid" src="client/img/course/OnlineCourse.jpg" alt="">
`
        var text = "";
        text = `
            <table>
            <tr>
                <td>Title</td>
                <td>:</td>
                <td>${result.title}</td>
            </tr>
            <tr>
                <td>Description</td>
                <td>:</td>
                <td>${result.description}</td>
            </tr>
            <tr>
                <td>Price</td>
                <td>:</td>
                <td>${result.price}</td>
            </tr>
                <td>Category</td>
                <td>:</td>
                <td>${result.Categoryid}</td>
            </tr>
</table>
        `
        $('.modal-body').html(img + text);
    }).fail((error) => {
        console.log(error);
    })
        ;
}


function moveDetail(id) {
    window.location.href = "/AddCourses/Detail/" + id;
}
//GET COURSE
$(document).ready(function () {
    Table = $("#courses").DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf'
        ],
        "ajax": {
            "url": "/Course/getall",
            "dataSrc": ""
        },
        "columns": [
            { "data": "id" },
            { "data": "title" },
            { "data": "description" },
            { "data": "price" },
            { "data": "thumbnail" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="fas fa-edit" onclick="moveDetail('${row["id"]}')">
                        Details
                              </button>`;
                }
            },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    return `<button type="button" class="fas fa-edit" data-bs-toggle="modal" onclick="editCourse('${row["id"]}')" data-bs-target="#editCourseModal">
                              Edit
                            </button>`;
                }
            },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    return `<button type="button" class="fas fa-trash" data-bs-toggle="modal" onclick="DeleteCourse('${row["id"]}')">
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
                title: 'Courses',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ]

    });

});

src = "https://cdn.jsdelivr.net/npm/jquery-validation@1.19.3/dist/jquery.validate.js"
src = "https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"