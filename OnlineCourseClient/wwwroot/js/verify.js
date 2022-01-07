(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            $("#btnverify").click(function () {

                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    VerifyAccount();
                }

                form.classList.add('was-validated');
                
            })

        });
    }, false);
})();


function VerifyAccount() {
    var obj = new Object();
    obj.no = $("#inputnorek").val();
    obj.holder_name = $("#inputholdername").val();
    obj.bank_name = $("#inputbank").val();
    obj.id_user = $("#inputuserid").val();
    console.log(obj);
        //check nomor account
        $.ajax({
            url: "/check/"+ obj.no,
            type: "GET",
            success: function (response) {
                console.log(response)
                if (response == 200) {
                    //post bank account
                    $.ajax({
                        url: "/verify/post",
                        type: "POST",
                        data: obj,
                        success: function (response) {
                            console.log(response);
                            //update role
                            $.ajax({
                                url: "/roleupdate/"+obj.id_user,
                                type: "GET",
                                success: function (response) {
                                    Swal.fire({
                                        title: 'Yeay!! Your account is verified ',
                                        html:
                                            'you can login using your account <br>' +
                                            '<strong></strong> detik <br>',

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
                                            window.location.href = '/login';
                                        }
                                    });

                                },
                                error: function (response) {
                                    Swal.fire(
                                        'Opps!',
                                        'Your account failed to verify',
                                        'error'
                                    )
                                }
                            });
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
                } else if (response == 400) {
                    Swal.fire(
                        'Opps!',
                        'Looks like something went wrong, check again',
                        'error'
                    )
                } else if (response == 409) {
                    Swal.fire(
                        'Opps!',
                        'Account Number is already in use by another account',
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

