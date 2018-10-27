document.addEventListener("DOMContentLoaded", function (event) {

        var login = document.getElementById("input_login");
        var pass = document.getElementById("input_password");
        var message = document.getElementById("warning_message");
        var btn = document.getElementById("sub_btn");
        var warningColor = "red";

        btn.addEventListener("click", function (event) {
                message.innerText = "";

                if (login.value == null || login.value == "") {
                    event.preventDefault();
                    login.style.borderColor = warningColor;
                    message.innerText = "Wypełnij wszystkie wymagane pola";
                }

                if (pass.value == null || pass.value == "") {
                    event.preventDefault();
                    pass.style.borderColor = warningColor;
                    message.innerText = "Wypełnij wszystkie wymagane pola";
                }


            });


    });