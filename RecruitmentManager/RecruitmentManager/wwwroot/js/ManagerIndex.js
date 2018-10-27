document.addEventListener("DOMContentLoaded", function (event) {

    /*  Rozwijanie/zwijanie notatek  *************************************/

    var spans = document.getElementsByClassName("glyphicon-chevron-right");

    for (var i = 0; i < spans.length; i++) {
        spans[i].addEventListener("click", function () {

            this.classList.toggle("glyphicon-chevron-right");
            this.classList.toggle("glyphicon-chevron-down");

            var tr_id = "notes_" + this.id;

            var tr_to_show = document.querySelectorAll("#" + tr_id);

            for (var j = 0; j < tr_to_show.length; j++) {

                tr_to_show[j].classList.toggle("hide");
            }

        });
    }


    /*  Dodawanie notatek  *************************************/

    var plusSpans = document.querySelectorAll("span.glyphicon-plus");

    for (var k = 0; k < plusSpans.length; k++) {
        plusSpans[k].addEventListener("click", function () {

            this.classList.toggle("glyphicon-plus");
            this.classList.toggle("glyphicon-minus");

            var add_id = "addnote_" + this.id;
            var tr_add_note = document.querySelector("#" + add_id);

            tr_add_note.classList.toggle("hide");

        });
    }


    /*  Filtrowanie po statusie  *************************************/

    var select = document.getElementById("status_select");
    //console.log("select:" + select.id);

    select.addEventListener("change", function () {

        //console.log("zmiana:" + this.value);

        //wybrać wszystkie komórki ze statusem
        var tds = document.querySelectorAll("#td_status");

        if (this.value == "Wszystko") // pokaz wszystko
        {
            for (var l = 0; l < tds.length; l++) {
                tds[l].parentElement.classList.remove("hide");
            }

        } else // filtruj po statusie
        {

            // szukamy komorek z docelowym statusem; jesli znaleziona, to odkrywamy, jesli nie - ukrywamy 
            for (var m = 0; m < tds.length; m++) {

                if (tds[m].innerText == this.value) {

                    tds[m].parentElement.classList.remove("hide");
                } else {

                    tds[m].parentElement.classList.add("hide");
                }
            }
        }

    });

    /*  preventDefault przy próbie dodania pustej notatki  *************************************/


    var addnote_buttons = document.querySelectorAll("#addnote_button");

    for (var n = 0; n < addnote_buttons.length; n++) {

        addnote_buttons[n].addEventListener("click", function (event) {

            var note_input = this.parentElement.parentElement.querySelector("input");

            if (note_input.value == "") {
                event.preventDefault();
                note_input.placeholder = "Wpisz coś!";
            }
        });

    }

    /*  Zmiana statusu  *************************************/

    var asteriskSpans = document.querySelectorAll("span.glyphicon-asterisk");

    for (var p = 0; p < asteriskSpans.length; p++) {
        asteriskSpans[p].addEventListener("click", function () {

            //this.classList.toggle("glyphicon-plus");
            //this.classList.toggle("glyphicon-minus");

            var change_id = "changestatus_" + this.id;
            var tr_change_status = document.querySelector("#" + change_id);

            tr_change_status.classList.toggle("hide");

        });
    }

});


