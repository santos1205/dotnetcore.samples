﻿var contAux2;
var pagAtual = 1;
var qtdeLinhasPorPagina = 10;
var listaInicialHtml = '';
var paginacaoInicial = $('.pagination').html();

$(document).on('click', ".pagination li", function () {
    var nrPagina = $(this).text();
    //exibirPaginacao(nrPagina);
    exibirPaginacaoCustom(nrPagina, 20);
});

// Página de Referência: consultaCotacao.aspx
// Orientações:
// Classe 'tbody' obrigatória;
// Classes dos atributos da tabela obrigatório. Os nomes das classes devem ser o mesmo dos campos do obj JSON.

function iniciarPaginacaoCustom(obj, qtdeRegsPorPagina) {    
    if (listaInicialHtml == '')
        listaInicialHtml = $('.tbody').html();

    $('.tbody').html(listaInicialHtml);

    var contAux = 1;
    contAux2 = qtdeRegsPorPagina;
    var listaHtml = '';

    obj.forEach(function (item, index) {
        //Controle da paginação
        if ((index + 1) > contAux2) {
            contAux++;
            contAux2 = contAux2 + qtdeRegsPorPagina;
        }
        contAux = index + 1 > contAux2 ? contAux++ : contAux;

        //Limpa pesquisa antes de popular a tabela.
        //Seta html inicial da página para poder reconsultar.        
        $('.tbody').find('tr').remove();
        $('.tbody').append(listaInicialHtml);

        $('.tbody').find('tr').removeClass();
        $('.tbody').find('tr').addClass('clsPag' + contAux);


        //loop das colunas da tabela
        for (var i = 0; i < $('.tbody').find('tr').children().length; i++) {
            var classe = $('.tbody').find('td:eq(' + i + ')').attr('class');
            var elemTemp = $('.tbody').find('.' + classe).html();
            $('.tbody').find('td:eq(' + i + ')').text(item[classe]);    //A classe tem/deverá ter o msm nome do atributo do objeto obj (parâmet)
            //if (i > 0) {
            if (classe == undefined)
                break;
                if (classe.includes('Acao')) {                //Se na paginação tiver classe Acao: ex: botão excluir / incluir / editar..... inclui propriedade do botão.
                    $('.tbody').find('td:eq(' + i + ')').html(item.htmlButton);    //o atributo htmlButton deverá ter esse msm nome no objeto js da página, e receber o html do botão..                
                    $('.tbody').find('td:eq(' + i + ')').addClass('acao' + item.IdUsuario);
                }
            //}
        }
        listaHtml += $('.tbody').html();        
    });

    if(obj.length == 0)
        $('.btns-nav').html('Nenhum registro encontrado');
    else
        $('.btns-nav').html('<div class="row"><button type="button" onclick="voltarPag()" id="btnVoltar" class="page-link">&laquo; Anterior</button><ul class="pagination pag-1"></ul><button type="button" onclick="avancarPag()" id="btnProximo" class="page-link">Próximo &raquo;</button></div>');
        
    $('.tbody').html(listaHtml);
    exibirPaginacao(pagAtual);
}


function avancarPag() {
    pagAtual++;
    //exibirPaginacao(pagAtual);
    exibirPaginacaoCustom(pagAtual, 20)
}

function voltarPag() {
    pagAtual--;
    //exibirPaginacao(pagAtual);
    exibirPaginacaoCustom(pagAtual, 20)
}

function exibirPaginacaoCustom(nrPagSelecionada, qtdLinhas)         //param: <pagina selecionada>
{
    pagAtual = nrPagSelecionada;
    var qtdePags = contAux2 / qtdLinhas;

    //Retorna para a primeira pagina em caso de reconsulta.
    if (pagAtual > qtdePags)
        nrPagSelecionada = 1;
        
    //controla visualização dos botões VOLTAR  e AVANÇAR
    if (nrPagSelecionada == 1)
        $('#btnVoltar').attr("disabled", true);
    else
        $('#btnVoltar').attr("disabled", false);

    if (nrPagSelecionada == qtdePags)
        $('#btnProximo').attr("disabled", true);
    else
        $('#btnProximo').attr("disabled", false);

    //Oculta todas as páginas.
    for (var i = 1; i <= qtdePags; i++)
        $('.clsPag' + i).hide();

    //Limpa a paginação antes de popular a tabela.
    //Seta html inicial da paginação para quando o usuário reconsultar, popular de acordo com a tabela.
    $('.pagination').find('li').remove();
    $('.pagination').append(paginacaoInicial);

    //Monta númeral da paginação na tela.
    for (var p = 1; p <= qtdePags; p++) {
        if ($('.pagination').find('li').length < qtdePags) {
            if (p > 22) {
                $('.pagination.pag-2').append('<li class="page-link">');
            } else
                $('.pagination.pag-1').append('<li class="page-link">');
            var elemento = $('.pagination').find('li');
            $(elemento).each(function () {
                if ($(this).text() == "") {
                    if (p == nrPagSelecionada) {
                        $(this).css("background-color", "#045531");
                        $(this).css("color", "#fff");
                    }
                    $(this).text(p);
                }
            });
        }
    }

    //Exibe somente página selecionada.
    $('.clsPag' + nrPagSelecionada).show();
}

function exibirPaginacao(nrPagSelecionada)         //param: <pagina selecionada>
{
    pagAtual = nrPagSelecionada;
    var qtdePags = contAux2 / qtdeLinhasPorPagina;

    //Retorna para a primeira pagina em caso de reconsulta.
    if (pagAtual > qtdePags)
        nrPagSelecionada = 1;

    //controla visualização dos botões VOLTAR  e AVANÇAR
    if (nrPagSelecionada == 1)
        $('#btnVoltar').attr("disabled", true);
    else
        $('#btnVoltar').attr("disabled", false);

    if (nrPagSelecionada == qtdePags)
        $('#btnProximo').attr("disabled", true);
    else
        $('#btnProximo').attr("disabled", false);

    //Oculta todas as páginas.
    for (var i = 1; i <= qtdePags; i++)
        $('.clsPag' + i).hide();

    //Limpa a paginação antes de popular a tabela.
    //Seta html inicial da paginação para quando o usuário reconsultar, popular de acordo com a tabela.
        $('.pagination').find('li').remove();
        $('.pagination').append(paginacaoInicial);

    //Monta númeral da paginação na tela.
    for (var p = 1; p <= qtdePags; p++) {
        if ($('.pagination').find('li').length < qtdePags) {
            if (p > 22) {
                $('.pagination.pag-2').append('<li class="page-link">');
            } else
                $('.pagination.pag-1').append('<li class="page-link">');
            var elemento = $('.pagination').find('li');
            $(elemento).each(function () {
                if ($(this).text() == "") {
                    if (p == nrPagSelecionada) {
                        $(this).css("background-color", "#045531");
                        $(this).css("color", "#fff");
                    }
                    $(this).text(p);
                }
            });
        }
    }

    //Exibe somente página selecionada.
    $('.clsPag' + nrPagSelecionada).show();    
}

function pagSelecionada(nrPagSelecionada) {

    var qtdePags = contAux2 / qtdeLinhasPorPagina;
    var elemento = $('.pagination').find('li');

    if (nrPagSelecionada == 1)
        $('#btnVoltar').attr("disabled", true);
    else
        $('#btnVoltar').attr("disabled", false);

    if (nrPagSelecionada == qtdePags)
        $('#btnProximo').attr("disabled", true);
    else
        $('#btnProximo').attr("disabled", false);

    //Oculta todas as páginas.
    for (var i = 1; i <= qtdePags; i++)
        $('.clsPag' + i).hide();

    //Exibe a página selecionada.
    $('.clsPag' + nrPagSelecionada).show();
};