$(function () {
    //Adiciona o DATAPICKER de acordo com a classe selecionada.
    $(".calendario").datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        changeMonth: true,
        changeYear: true,
        onSelect: function () {
            $(this).next().addClass("active");
        }


    });

    $(document).on('click', "#btnConsultar", function () {
        if (validaCampo()) {
            $('#dvResultadoConsulta').css('display', 'block');
        }
    });

    $('#btnLimpar').click(function () {
        $('#txtCPFCNPJ').val('');
        $('#txtNomeSeg').val('');
        $('#cmbStatus').options[0];
    });


    // Esconde o td
    $("td[colspan=12]").hide();

    $("#tdUsuario").click(function (e) {
        e.stopPropagation();
        var $target = $(e.target);
        // Seleciona o td mais proximo do atual e o expande
        if ($target.closest("td").attr("colspan") > 1) {
            $target.slideUp();
        } else {
            $target.closest("tr").next().find("td").slideToggle();
        }
    });
});


function validaCampo() {
    var validaPassou = true;

    var nome = document.getElementById("txtNome");
    var empresa = document.getElementById("txtNomeEmp");
    var solicitacao = document.getElementById("txtDtSolic");

    if (nome.value == "" && empresa.value == "" && solicitacao.value == "") {
        $('#SPRetornoErro').show();
        validaPassou = false;
    } else {
        $('#SPRetornoErro').hide();
    }

    return validaPassou;
}