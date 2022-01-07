console.log("TEST AUTH " + is_Authentication);
console.log(title.substring(0, 50));
$(function () {
    $("#video-preview").hide();
    $("#pause-preview").hide();
    $("#play-preview").click(function () {
        $("#video-preview").show();
        $("#thumbsnails").attr({
            "style": ""
        })
        $("#videoarea").attr({
            "src": display("/client/preview/" + courseID + ".mp4"),
            "poster": "",
            "autoplay": ""
        })
        $("#pause-preview").show();
    });

    $("#pause-preview").click(function () {
        $("#videoarea").trigger("pause");
        $("#video-preview").hide();
        $("#thumbsnails").attr({
            "style": "background-image:url('/client/img/course/" + thumbnail + "')"
        })
        $("#pause-preview").hide();

    })


})




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


var urlnow = window.location.href;
if (is_Authentication == 1) {
    if (is_have == 1) {
        $(".cart-btn").html(
            ' <a id="pay-button" class="theme_btn text-white" href="/mycourse" ><span class="fas fa-book-reader fa-1x fa-fw">&nbsp;</span>My Course</a>'
        )
    } else {
        $(".cart-btn").html(
            ' <a id="pay-button" class="theme_btn text-white" onclick="PaymentGateway()" type="button"><span class="fas fa-cash-register fa-1x fa-fw">&nbsp;</span>Buy Course</a>'
        )
    }
    
} else {
    $(".cart-btn").html(
        ' <a id="pay-button" class="theme_btn text-white" href="/login" data-id="' + urlnow +'"  type="button" )"><span class="fas fa-cash-register fa-1x fa-fw">&nbsp;</span>Buy Course </a>'
    )
}

function PaymentGateway() {
    var obj = new Object();
    obj.Orderid = Orderid;
    obj.gross_amount = price;
    obj.Courseid = courseID;
    obj.Price = price;
    obj.Titlecourse = title.substring(0,50);
    obj.Namecustomer = name;
    obj.Emailcustomer = email;
    obj.Teleponcustomer = phone;
    console.log(obj);
    $.ajax({
        url: "/payment/payment",
        type: "POST",
        data: obj,
        success: function (response) {
            console.log(response)
            var token = response.result.token;
            console.log(token);
            snap.pay(token, {
                onSuccess: function (result) {
                    var obj = new Object();
                    obj.Orderid = result.order_id;
                    obj.Method = result.payment_type;
                    obj.Price = parseInt(result.gross_amount);
                    obj.VANumber = "";
                    obj.Courseid = courseID;
                    obj.Userid = id_user;
                    obj.status = result.transaction_status;
                    console.log("SUKSES");
                    console.log(obj);
                    $.ajax({
                        url: "/payment/Addpayment",
                        type: "POST",
                        data: obj,
                        success: function (response) {
                            console.log(response);
                            if (response == 202) {
                                Swal.fire({
                                    title: 'Payment Pending',
                                    html:
                                        "don't worry, if the payment is successful then the course can be seen in the 'Course' menu<br>" +
                                        '<strong></strong> second <br>',
                                    icon: 'success',
                                    timer: 5000,
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
                                        window.location.href = '/Course';
                                    }
                                });
                            } else if (response == 200) {
                                Swal.fire({
                                    title: 'Successful Payment',
                                    html:
                                        "Please wait, we will redirect you to the 'MyCourse' menu<br>" +
                                        '<strong></strong> second <br>',
                                    icon: 'success',
                                    timer: 5000,
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
                                        window.location.href = '/Mycourse';
                                    }
                                });
                            }
                        }, error: function (response) {
                            console.log(response);
                            Swal.fire(
                                'Opps!',
                                'Looks like something went wrong, check again',
                                'error'
                            )
                        }
                    });
                },
                onPending: function (result) {
                    var obj = new Object();
                    obj.Orderid = result.order_id;
                    obj.Method = result.payment_type;
                    obj.Price = parseInt(result.gross_amount);
                    obj.VANumber = "";
                    obj.Courseid = courseID;
                    obj.Userid = id_user;
                    obj.status = result.transaction_status;
                    console.log("PENDING");
                    console.log(obj);
                    $.ajax({
                        url: "/payment/Addpayment",
                        type: "POST",
                        data: obj,
                        success: function (response) {
                            console.log(response);
                            if (response == 202) {
                                Swal.fire({
                                    title: 'Payment Pending',
                                    html:
                                        "don't worry, if the payment is successful then the course can be seen in the 'Course' menu<br>" +
                                        '<strong></strong> second <br>',
                                    icon: 'success',
                                    timer: 5000,
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
                                        window.location.href = '/Course';
                                    }
                                });
                            } else if (response == 200) {
                                Swal.fire({
                                    title: 'Successful Payment',
                                    html:
                                        "Please wait, we will redirect you to the 'MyCourse' menu<br>" +
                                        '<strong></strong> second <br>',
                                    icon: 'success',
                                    timer: 5000,
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
                                        window.location.href = '/Mycourse';
                                    }
                                });
                            }
                        }, error: function (response) {
                            Swal.fire(
                                'Opps!',
                                'Looks like something went wrong, check again',
                                'error'
                            )
                        }
                    });
                },
                onError: function (result) {
                    Swal.fire(
                        'Opps!',
                        'Looks like something went wrong, check again',
                        'error'
                    )
                },
                onClose: function () {
                    Swal.fire(
                        'Opps!',
                        'Do not close the payment window please',
                        'warning'
                    )
                }
            })
            
        },
        error: function (response) {
            console.log(response);
            Swal.fire(
                'Opps!',
                'Looks like something went wrong, check again',
                'error'
            )
        }
    });
      
}
        
function base64Encode(text) {
    var encodedString = btoa(text);
    return encodedString;
}

function base64Decode(text) {
    var decodedString = atob(text);
    return decodedString;
}

////midtranss
//// For example trigger on button clicked, or any time you need
//var payButton = document.getElementById('pay-button');
//payButton.addEventListener('click', function () {
//    // Trigger snap popup. @TODO: Replace TRANSACTION_TOKEN_HERE with your transaction token
//    window.snap.pay('aadb3715-5bc6-4c10-8021-1af8c299ef57');
//    // customer will be redirected after completing payment pop-up
//});


$('#price-detail').each(function (f) {

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