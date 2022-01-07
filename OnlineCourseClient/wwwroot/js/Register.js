(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            $("#btnsaveCustomer").click(function () {

                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {

                    RegisterCustomer();

                }

                form.classList.add('was-validated');
                validateField();
            })

        });
    }, false);
})();

function validateField() {
    let cont = $('#formregister');
    if (cont.hasClass('display-none')) {
        cont.removeClass('display-none');
        $("#inputpassword").attr('data-bv-validatorname', true);
        $("#inputrepassword").attr('data-bv-validatorname', true);
        $("#inputrepassword").addClass('is-invalid"');
    } else {
        cont.addClass('display-none');
        $("#inputpassword").attr('data-bv-validatorname', false);
        $("#inputrepassword").attr('data-bv-validatorname', false);
    }
}

function buttonDisabled() {
    document.getElementById("btnsaveCustomer").disabled = true;
    document.getElementById("btnsaveCustomer").innerHTML = "Please wait ... ";
}
function buttonEnabled() {
    document.getElementById("btnsaveCustomer").disabled = false;
    document.getElementById("btnsaveCustomer").innerHTML = "<span class='fas fa-save'>&nbsp;</span> Register Account";
}

$('#formregister').bootstrapValidator({
    message: 'This value is not valid',
    live: 'enabled',
    fields: {
        inputpassword: {
            validators: {
                identical: {
                    field: 'inputpassword',
                    message: 'The passwords must match. '
                },
            }
        },
        inputrepassword: {
            validators: {
                identical: {
                    field: 'inputpassword',
                    message: 'The passwords must match. '
                },
            }
        }
    }
});

function RegisterCustomer() {
    var inputPassword = $("#inputpassword").val();
    var inputrePassword = $("#inputrepassword").val();
    if (inputPassword != inputrePassword) {
        Swal.fire(
            'Opps!',
            'The passwords must match',
            'error'
        )
    } else {
        var obj = new Object();
        obj.email = $("#inputemail").val();
        obj.password = $("#inputpassword").val();
        console.log(obj);
        buttonDisabled()
        $.ajax({
            url: "/Register/RegisterAccount",
            type: "POST",
            data: obj,
            success: function (response) {
                console.log(response)
                if (response == 200) {
                    buttonEnabled()
                    Swal.fire({
                        title: 'Check your email for verification account ',
                        html:
                            'We have sent a verification link to your email <br>'+
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
                            window.location.href = '/';
                        }
                    });
                } else if (response == 400) {
                    buttonEnabled()
                    Swal.fire(
                        'Opps!',
                        'Looks like something went wrong, check again',
                        'error'
                    )
                } else if (response == 409) {
                    buttonEnabled()
                    Swal.fire(
                        'Opps!',
                        'Email is already in use by another account',
                        'error'
                    )
                }
            },
            error: function (response) {
                buttonEnabled()
                console.log(response);
                Swal.fire(
                    'Opps!',
                    'Looks like something went wrong, check again',
                    'error'
                )
            }
        });
    }
    
}

