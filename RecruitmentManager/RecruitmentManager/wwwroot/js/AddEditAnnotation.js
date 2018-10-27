document.addEventListener("DOMContentLoaded",
    function(event) {

        var submit = document.getElementById("submit_button");
        var inputTitle = document.getElementById("input_title");
        var inputContent = document.getElementById("input_content");
        var message = document.getElementById("general_message");
        var warningColor = "red";
        var defaultColor = inputTitle.style.borderColor;

        submit.addEventListener("click", function (event) {
            message.innerText = "";
            inputTitle.style.borderColor = defaultColor;
            inputContent.style.borderColor = defaultColor;


            if (inputTitle.value == null || inputTitle.value == "" || inputTitle.value.length < 3) {
                event.preventDefault();
                inputTitle.style.borderColor = warningColor;
                message.innerText = "Wypełnij wszystkie wymagane pola (min 3 znaki)";
            }

            if (inputContent.value == null || inputContent.value == "" || inputContent.value.length < 3) {
                event.preventDefault();
                inputContent.style.borderColor = warningColor;
                message.innerText = "Wypełnij wszystkie wymagane pola (min 3 znaki)";
            }

        });

    });