document.addEventListener("DOMContentLoaded",
    function(event) {

        //-------------------- Company -> Offer Source ---------------------------------------

        var chbox = document.getElementById("chbox_source");
        var inputSource = document.getElementById("input_source");
        var inputCompany = document.getElementById("input_company");

        chbox.addEventListener("change", function () {
            if (this.checked) {

                inputSource.value = inputCompany.value;
            }
            else {
                inputSource.value = '';
            }
        });

        //-------------------- Form validation ---------------------------------------

        var submit = document.getElementById("sub_button");
        var inputJob = document.getElementById("input_jobtitle");
        var inputDate = document.getElementById("input_date");
        var message = document.getElementById("general_message");
        var warningColor = "red";
        var defaultColor = inputJob.style.borderColor;


        submit.addEventListener("click", function (event) {
            message.innerText = "";
            inputJob.style.borderColor = defaultColor;
            inputDate.style.borderColor = defaultColor;
            inputCompany.style.borderColor = defaultColor;

            if (inputJob.value == null || inputJob.value == "") {
                event.preventDefault();
                inputJob.style.borderColor = warningColor;
                message.innerText = "Wypełnij wszystkie wymagane pola";
            }

            if (inputDate.value == null || inputDate.value == "") {
                event.preventDefault();
                inputDate.style.borderColor = warningColor;
                message.innerText = "Wypełnij wszystkie wymagane pola";
            }

            if (inputCompany.value == null || inputCompany.value == "") {
                event.preventDefault();
                inputCompany.style.borderColor = warningColor;
                message.innerText = "Wypełnij wszystkie wymagane pola";
            }

        });

    });