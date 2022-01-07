
(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            $("#btnloginCustomer").click(function () {

                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    
                    LoginCustomer();

                }

                form.classList.add('was-validated');
               
            })

        });
    }, false);
})();

function buttonDisabled() {
    document.getElementById("btnloginCustomer").disabled = true;
    document.getElementById("btnloginCustomer").innerHTML = "Please wait ... ";
}
function buttonEnabled() {
    document.getElementById("btnloginCustomer").disabled = false;
    document.getElementById("btnloginCustomer").innerHTML = "<span class='fas fa-sign'>&nbsp;</span> Sign in ";
}

function LoginCustomer() {
    var obj = new Object();
    obj.email = $("#email").val();
    obj.password = $("#password").val();
    console.log(obj);
    buttonDisabled();
    $.ajax({
        url: "/Auth",
        type: "POST",
        data: obj,
        success: function (response) {
            if (response.result.idtoken == null && response.result.statusCode == 0) {
                console.log(response);
                buttonEnabled();
                Swal.fire(
                    'Opps!',
                    'Email or Password is not correct.',
                    'error'
                )
            } else if (response.result.statusCode == 2) {
                console.log(response);
                console.log();
                var hashEmail = btoa(response.result.email);
                buttonEnabled();
                Swal.fire(
                    'Opps!',
                    'Your Account unverif',
                    'warning'
                )
                 Swal.fire({
                     title: 'Huuh!! Your Account is not Verified',
                    html:
                        'Please wait <br>' +
                        '<strong></strong> detik <br>' +
                        'Direct to Verification Page',
                    icon: 'warning',
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
                        //alert('done');
                        window.location.href = '/verify/' + hashEmail;
                    }
                })
            }
            else {
                buttonEnabled();
                console.log(response);
                Swal.fire({
                    title: 'Yeay!! Login Successful',
                    html:
                        'Please wait <br>' +
                        '<strong></strong> detik <br>' +
                        'Direct to Main Page',
                    icon: 'success',
                    timer: 3000,
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
                        //alert('done');
                        window.location.href = '/main';
                    }
                })
            }
        },
        error: function (response) {
            console.log(response);
            buttonEnabled();
            Swal.fire(
                'Opps!',
                'Looks like something went wrong, check again',
                'error'
            )
        }
    })
}

function Forgot() {
        var obj = new Object();
        obj.email = $("#inputemail").val();
        console.log(obj);
        $.ajax({
            url: "/Accounts/Forgot",
            type: "POST",
            data: obj,
            success: function (response) {
                console.log(response)
                if (response == 200) {

                    Swal.fire({
                        title: 'Forgot Password Email Sent',
                        html:
                            'We have sent your new password to your email <br>' +
                            '<strong></strong> detik <br>',
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
                            ;
                        }
                    });
                } else if (response == 404) {
                    Swal.fire(
                        'Opps!',
                        'Email is not found',
                        'error'
                    )
                } 
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

