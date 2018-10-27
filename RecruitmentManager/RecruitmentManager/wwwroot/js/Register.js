document.addEventListener("DOMContentLoaded", function (event) {

    var login = document.getElementById("input_login");
    var pass = document.getElementById("input_password");
    var pass2 = document.getElementById("input_repeat_password");
    var message = document.getElementById("warning_message");
    var btn = document.getElementById("sub_btn");
    var warningColor = "red";

    btn.addEventListener("click", function (event) {
        message.innerText = "";

        if (login.value == null || login.value == "" || login.value.length < 3) {
            event.preventDefault();
            login.style.borderColor = warningColor;
            message.innerText = "Wypełnij wszystkie wymagane pola (Login - minimum 3 znaki)";
        }

        if (pass.value == null || pass.value == "") {
            event.preventDefault();
            pass.style.borderColor = warningColor;
            message.innerText = "Wypełnij wszystkie wymagane pola";
        }

        if (pass2.value == null || pass2.value == "") {
            event.preventDefault();
            pass2.style.borderColor = warningColor;
            message.innerText = "Wypełnij wszystkie wymagane pola";
        }

        if (pass.value !== pass2.value) {
            event.preventDefault();
            pass.style.borderColor = warningColor;
            pass2.style.borderColor = warningColor;
            message.innerText = "Hasła muszą być identyczne";
        }

    });


});