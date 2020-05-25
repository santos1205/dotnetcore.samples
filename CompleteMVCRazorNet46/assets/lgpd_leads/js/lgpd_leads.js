$(document).ready(() => {
    $('.noUi-tooltip').bind("DOMSubtreeModified", function () {
        // alert('changed: ' + $(this).text());
        $('#hdd-qtd-col').val($(this).text());
    });

});

const salvaLead = () => {
    let data = {

        LEL: {
            nome_completo: $('#nome_completo').val(),
            email: $('#email').val(),
            telefone: $('#telefone').val(),
            nome_empresa: $('#empresa').val(),
            qnt_colaborador: $('.noUi-tooltip').text(),
            cargo: $('#cargo').val(),  
            ramo: $('#ramo').val(),
            dados_cliente: $('input[name=dados_cliente]:checked').val(),
            compartilha_dados: $('input[name=compartilha_dados]:checked').val(),
            iniciou_adequacao: $('input[name=iniciou_adequacao]:checked').val()
        }
    };

    $.ajax(
        {
            type: 'POST',
            url: `/LGPD/SalvarLeads/`,
            data,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#lista-apr-usuario').html(data);
            }
        });
}
