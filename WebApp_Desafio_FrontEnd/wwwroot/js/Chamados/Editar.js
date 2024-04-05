$(document).ready(function () {

    $('.glyphicon-calendar').closest("div.date").datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: false,
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'pt-BR'
    });

    $('.input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: false,
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'pt-BR'
    });

    $('#form').validate({
        rules: {
            Assunto: {
                required: true,
                maxlength: 30
            },
            Solicitante: {
                required: true,
                maxlength: 30
            },

        },
        messages: {
            assunto: {
                required: "Por favor, informe o assunto.",
                maxlength: "O assunto deve ter no máximo {0} caracteres."
            },
            solicitante: {
                required: "Por favor, informe o nome do solicitante.",
                maxlength: "O campo de nome deve ter no máximo {0} caracteres."
            },

        }
    });

    $('#btnCancelar').click(function () {
        Swal.fire({
            html: "Deseja cancelar essa operação? O registro não será salvo.",
            type: "warning",
            showCancelButton: true,
        }).then(function (result) {
            if (result.value) {
                history.back();
            } else {
                console.log("Cancelou a edição.");
            }
        });
    });

    $('#btnSalvar').click(function () {
        if ($('#form').valid() != true) {
            FormularioInvalidoAlert();
            return;
        }

        let chamado = SerielizeForm($('#form'));
        let url = `/Chamados/Editar/${chamado.ID}`;

        $.ajax({
            type: "POST",
            url: url,
            data: chamado,
            success: function (result) {
                Swal.fire({
                    type: result.Type,
                    title: result.Title,
                    text: result.Message,
                }).then(function () {
                    window.location.href = config.contextPath + result.Controller + '/' + result.Action;
                });
            },
            error: function (result) {
                Swal.fire({
                    text: result,
                    confirmButtonText: 'OK',
                    icon: 'error'
                });
            },
        });
    });


});
