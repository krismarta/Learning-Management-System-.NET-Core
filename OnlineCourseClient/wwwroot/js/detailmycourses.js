////$('#video').hide();

////function myFunction() {
////    document.getElementById("videosource").src = "/client/video/Video2.mp4";
////}

//function myFunction() {
//    document.getElementById("field2").value = document.getElementById("field1").value;
//}

$(function () {
    $("#video").hide();

    //$("#playlist li").on("click", function () {
    //    $("#videoarea").attr({
    //        "src": display($(this).attr("movieurl")),
    //        "poster": "",
    //        "autoplay": "autoplay"
    //    })
    //})
    //$("#videoarea").attr({
    //    "src": display($("#playlist li").eq(0).attr("movieurl")),
    //    "poster": $("#playlist li").eq(0).attr("moviesposter")
    //})


    
})

$("#downloadcerti").click(function () {
    Swal.fire(
        'Yeayy!',
        'Your certificate has been successfully downloaded',
        'success'
    );
    $("#previewcerti").modal("hide");

    var html = $(".serti").html().replace(/</g, "StrTag").replace(/>/g, "EndTag").replace(/#/g, "PAGER");
    var obj = new Object();
    obj.link = html;
    console.log(obj);
    $.ajax({
        url: "/mycourse/ActionCertificate/",
        type: "POST",
        data: obj,
        success: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });

    //window.open('../myCourse/ActionCertificate?html=' + html ,'_blank');
});

$(".playlist-video").each(function () {
    $(this).click(function () {
        var url_video = $(this).attr("movieurl");
        $("#videoarea").attr({
            "src": display(url_video),
            "poster": "",
            "autoplay": "autoplay"
        })
        $("#video").show();
        $("#thumbsnails").attr({
            "style": ""
        })
    })
})



function editModal(id) {
    $.ajax({
        url: "/Review/get/" + id,
        success: function (result) {
            console.log(result)
            var data = result
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = mm + '/' + dd + '/' + yyyy;
           
            $("#reviewId").attr("value", data.id)
            $("#reviewName").attr("value", data.review)
            $("#reviewRate").attr("value", data.rate)
            $("#reviewDate").attr("value", today)
            $("#reviewUserid").attr("value", data.userid)
            $("#reviewCourseid").attr("value", data.courseid)
        },
        error: function (error) {
            console.log(error)
        }
    })
}


$("#btnsavereview").click(function () {
    var id = $("#reviewId").val();

    if (reviewid == 0) {
        //insert
        //alert("data harus insert");
        AddReview(userid, courseid);
    } else {
        //update
        editReview(id);

    }

})


function AddReview(iduser,idcourse) {
    var obj = new Object();
    //obj.id = $("#reviewId").val();
    obj.review = $("#reviewName").val();
    obj.rate = $('input[name="rate"]:checked').val();
    obj.date_review = $("#reviewDate").val();
    obj.Courseid = idcourse;
    obj.Userid = iduser;
    console.log(obj);
    $.ajax({
        /*headers: { 'Content-Type': 'application/json' },*/
        url: "/Review/post/" ,
        type: "POST",
        data: obj,
        dataType: 'json'
    }).done((result) => {
        console.log(result)
        if (result == 200) {
            
            Swal.fire({
                title: 'Thank you',
                html:
                    'Your Review has been added! <br>' +
                    '<strong></strong> second <br>',
                icon: 'success',
                timer: 2000,
                showConfirmButton: false,
                allowOutsideClick: false,
                didOpen: () => {
                    timerInterval = setInterval(() => {
                        Swal.getHtmlContainer().querySelector('strong')
                            .textContent = (Swal.getTimerLeft() / 1000)
                                .toFixed(0)
                    }, 100)
                },
                willClose: () => {
                    clearInterval(timerInterval)
                    window.location.reload();
                }
            });


        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Adding Review Failed',
            })
        }
    }).fail((error) => {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Adding Review Failed',
        })
    })
}

function editReview(id) {
    var obj = new Object();
    obj.id = $("#reviewId").val();
    obj.review = $("#reviewName").val();
    obj.rate = $('input[name="rate"]:checked').val();
    obj.date_review = $("#reviewDate").val();
    obj.Courseid = $("#reviewCourseid").val();
    obj.Userid = $("#reviewUserid").val();
    $.ajax({
        /*headers: { 'Content-Type': 'application/json' },*/
        url: "/Review/put/" + id,
        type: "PUT",
        data: obj,
        dataType: 'json'
    }).done((result) => {
        console.log(result)
        if (result == 200) {
            Swal.fire({
                title: 'Thank you',
                html:
                    'Your Review has been updated! <br>' +
                    '<strong></strong> second <br>',
                icon: 'success',
                timer: 2000,
                showConfirmButton: false,
                allowOutsideClick: false,
                didOpen: () => {
                    timerInterval = setInterval(() => {
                        Swal.getHtmlContainer().querySelector('strong')
                            .textContent = (Swal.getTimerLeft() / 1000)
                                .toFixed(0)
                    }, 100)
                },
                willClose: () => {
                    clearInterval(timerInterval)
                    window.location.reload();
                }
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Adding Review Failed',
            })
        }
    }).fail((error) => {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Adding Review Failed',
        })
    })
}


//convert to blob
function createObjectURL(object) {
    return (window.URL) ? window.URL.createObjectURL(object) : window.webkitURL.createObjectURL(object);
}

async function display(videoStream) {
    var video = document.getElementById('videoarea');
    let blob = await fetch(videoStream).then(r => r.blob());
    var videoUrl = createObjectURL(blob);
    video.src = videoUrl;
}
