

$(document).ready(function () {
    var date_input = $('input[id="inputBirth"]');
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    var options = {
        format: 'yyyy-mm-dd',
        container: container,
        todayHighlight: true,
        autoclose: true,
    };
    date_input.datepicker(options);

});
$("#inputbank").val(name_bank).change();
$("#usergender").val(user_gender).change();
var tmp = new Date(user_birthdate);
var birthdate_simply = tmp.getFullYear() + '-' + ((tmp.getMonth() > 8) ? (tmp.getMonth() + 1) : ('0' + (tmp.getMonth() + 1))) + '-' + ((tmp.getDate() > 9) ? tmp.getDate() : ('0' + tmp.getDate()));
$("#inputBirth").val(birthdate_simply).change();


(function () {
    'use strict';
    window.addEventListener('load', function () {
        
        var forms = document.getElementsByClassName('needs-validation-user');
        var validation = Array.prototype.filter.call(forms, function (form) {
            $("#btnupdateuser").click(function () {
                if ($("#usergender").val() === "") {
                    Swal.fire(
                        'Opps!',
                        'Gender is empty',
                        'warning'
                    )
                } else {
                    if (form.checkValidity() === false) {
                        event.preventDefault();
                        event.stopPropagation();
                    } else {
                        UpdateProfile();

                    }
                }
                

                form.classList.add('was-validated');

            })

        });
    }, false);
})();

function UpdateProfile() {
    var gender = $("#usergender").val();
    var genderid;
    if (gender == "Male") {
        genderid = 0;
    } else if(gender == "Female") {
        genderid = 1;
    }
    var obj = new Object();
    obj.id = $("#inputid").val();
    obj.email = $("#inputemail").val();
    obj.name = $("#inputname").val();
    obj.phone = $("#inputphone").val();
    obj.gender = genderid;
    obj.birthDate = $("#inputBirth").val();
    console.log(obj);
    $.ajax({
        url: "/Users/put/"+obj.id,
        type: "PUT",
        data: obj,
        success: function (response) {
            console.log(response)
            if (response == 200) {
                Swal.fire(
                    'Yeay!',
                    'Your account has been successfully updated',
                    'success'
                    )
            } else if (response == 400) {
                console.log(response);
                Swal.fire(
                    'Opps!',
                    'Looks like something went wrong, check again',
                    'error'
                )
            } else if (response == 409) {
                console.log(response);
                Swal.fire(
                    'Opps!',
                    'Looks like something went wrong, check again',
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

//update Account bank
(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation-bank');
        var validation = Array.prototype.filter.call(forms, function (form) {
            $("#btnsavebank").click(function () {
                if ($("#inputbank").val() === "") {
                    Swal.fire(
                        'Opps!',
                        'Select Name Bank',
                        'warning'
                    )
                } else {
                    if (form.checkValidity() === false) {
                        event.preventDefault();
                        event.stopPropagation();
                    } else {
                        //UpdateProfile();
                        updateBank();
                    }
                }


                form.classList.add('was-validated');

            })

        });
    }, false);
})();


function updateBank() {
    var obj = new Object();
    obj.id = $("#idrek").val();
    obj.no = $("#inputnorek").val();
    obj.holder_name = $("#inputholdername").val();
    obj.bank_name = $("#inputbank").val();
    obj.id_user = $("#inputid").val();
    console.log(obj);
    //check nomor account
    $.ajax({
        url: "/check/" + obj.no,
        type: "GET",
        success: function (response) {
            console.log(response)
            if (response == 200) {
                //update bank account
                $.ajax({
                    url: "/bank/put/"+ obj.id,
                    type: "PUT",
                    data: obj,
                    success: function (response) {
                        console.log(response);
                        Swal.fire(
                            'Yeay!',
                            'Bank information has been successfully changed',
                            'success'
                        )
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

//update password
(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation-change');
        var validation = Array.prototype.filter.call(forms, function (form) {
            $("#btnsavepass").click(function () {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    changepassword()
                }

                form.classList.add('was-validated');

            })

        });
    }, false);
})();

function changepassword() {
    var obj = new Object();
    var newpass = $("#newpass").val();
    var repass = $("#repass").val();

    if (newpass != repass) {
        Swal.fire(
            'Opps!',
            'Password must be the same',
            'warning'
        )
    } else {
        obj.Email = $("#inputemail").val();
        obj.NewPassword = newpass;
        console.log(obj);
        $.ajax({
            url: "/Accounts/Change",
            type: "PUT",
            data :obj,
            success: function (response) {
                console.log(response)
                if (response == 200) {
                    console.log(response);
                    Swal.fire({
                        title: 'Yeay!!',
                        html:
                            'Password changed successfully, please login again </br> ' +
                            'Mohon Tunggu <br>' +
                            '<strong></strong> detik <br>' +
                            'Automation Logout Session',
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
                            window.location.href = '/Login/logout';
                        }
                    })

                } else if (response == 400) {
                    console.log(response);
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

   
}