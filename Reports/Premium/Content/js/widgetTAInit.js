/*!
 * widgetTAInit
 * Copyright (c) Travel Ace AssistÃªncia ao Viajante
 * 
 * Criado por: ClÃ¡udio Rocha de Jesus at Zambo Tecnologia
 * crochadejesus@zambotecnologia.com.br
 * Criado em: sexta-feira, 3 de junho de 2016
 */
window._st_account = 2009;
var idioma = "BR";
var dicionario = {};

(function (window, document) {
    'use strict';

    var OrcamentoDados = {
        destino: String(),
        dataIda: null,
        dataVolta: null,
        qtdPax: Number(),
        ate21: Number(),
        de22a70: Number(),
        de71a100: Number(),
        nomeCompleto: String(),
        telefone: String(),
        email: String(),
        cupom: String(),
        dp: false, // dados pessoais para fazer leads
        co: false,
        in: false, // se esta chamando a url de dentro da tela de venda
        ac: false,
        usuario: null,
        senha: null,
        skin: Number(),
        idioma: "BR",
        valueCo: 0
    };

    var LimitePassageiros = 20;
    var OldQuantidadePassageiros = 1;

    var json_analitics_default = "UA-77344704-3";
    var json_analitics = [
        { label: "clubeextra", id: "UA-77344704-1" },
        { label: "paodeacucar", id: "UA-77344704-2" }
    ];

    WidgetTravelAce.fn = WidgetTravelAce.prototype;

    function WidgetTravelAce() { }
    window.wgta = new WidgetTravelAce();

    wgta.version = '32.2712';
    //WidgetTravelAce.fn.domainName = 'http://localhost:52486', // Local
    //WidgetTravelAce.fn.domainName = 'http://novow.travelace.com.br', // HomologaÃ§Ã£o
    WidgetTravelAce.fn.domainName = 'https://nwlabel.travelace.com.br', // ProduÃ§Ã£o

	WidgetTravelAce.fn.scriptLoadHandler = function () {
	    WidgetTravelAce.init(WidgetTravelAce.detectScript());
	};

    WidgetTravelAce.init = function (currentScript) {
        var parameters = WidgetTravelAce.parseQueryString(currentScript);
        if (parameters !== undefined) {
            WidgetTravelAce.main();
        }
    };

    WidgetTravelAce.detectScript = function () {
        var scripts = document.getElementsByTagName('script');
        for (var i = 0; i < scripts.length; ++i) {
            if (scripts[i].src.indexOf('/widget/widgetTAInit.js') > 0) {
                return scripts[i].src;
            }
        }
    };

    // parse querystring paramenters from url
    WidgetTravelAce.parseQueryString = function (currentScript) {
        // Split into key/value pairs
        try {
            var queryString = currentScript.split('?')[1];
            var params = {},
                queries, temp, i, l;

            queries = queryString.split('&');

            // Convert the array of strings into an object
            for (i = 0, l = queries.length; i < l; i++) {
                temp = queries[i].split('=');
                params[temp[0]] = temp[1];
            }

            return params;
        } catch (e) {
            console.log(e);
        }
    };

    WidgetTravelAce.main = function () {
        var divWidget = document.getElementById('widgetTAContainer');

        var html = [];
        html.push('<!DOCTYPE html>');
        html.push('<html lang="pt-br" style="position: relative;">');
        html.push('<head>');
        html.push('<meta charset="utf-8">');
        html.push('<meta http-equiv="X-UA-Compatible" content="IE=edge">');
        html.push('<meta name="viewport" content="width=device-width, initial-scale=1.0">');
        html.push('<link rel="stylesheet" type="text/css" media="all" href="' + wgta.domainName + '/widget/kendo/styles/bundleCssKendo.css" />');
        html.push('<link rel="stylesheet" type="text/css" media="all" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />');
        html.push('<style>');
        html.push('img, picture, video, embed { height: auto; }');
        html.push('img { border: 0 none; }');
        html.push('*, *:before, *:after { -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; }');
        html.push('.wgta_clearfix:before, .wgta_clearfix:after { content: " "; display: table; }');
        html.push('.wgta_clearfix:after { clear: both; }');
        html.push('.wgta_clearfix { *zoom: 1; }');
        html.push('.wgta_row { margin-left: -15px; margin-right: -15px; }');
        html.push('.wgta_col-md-2, .wgta_col-md-3, .wgta_col-md-4, .wgta_col-md-6, .wgta_col-md-8, .wgta_col-md-12 { min-height: 1px; padding-left: 0px; padding-right: 0px; margin: 0 0.5em 0 0; position: relative; }');
        html.push('.wgta_col-md-2 { width: 11.8%; float: left; margin: 0 0.7em 0.5em 0; }');
        html.push('.wgta_col-md-2:last-child { margin-right: 0; }');
        html.push('.wgta_col-md-3 { width: 22%; float: left; margin: 0 0.7em 0.5em 0; }');
        html.push('.wgta_col-md-3:last-child { margin-right: 0; }');
        html.push('.wgta_col-md-4 { width: 30.3%; float: left; }');
        html.push('.wgta_col-md-4:last-child { margin-right: 0; }');
        html.push('.wgta_col-md-6 { width: 46.3%; float: left; }');
        html.push('.wgta_col-md-6:last-child { margin-right: 0; }');
        html.push('.wgta_col-md-8 { width: 62.3%; float: left; }');
        html.push('.wgta_col-md-8:last-child { margin-right: 0; }');
        html.push('.wgta_col-md-12 { width: 93.8%; }');
        html.push('.wgta_col-md-12:last-child { margin-right: 0; }');
        html.push('.wgta_form-control { border-radius: 0px; display: block; width: 100%; padding: 6px 12px; line-height: 1.42857143; color: #555; background-color: #fff;');
        html.push('background-image: none; border: 1px solid #ccc; -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);');
        html.push('-webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s; -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;');
        html.push('transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; box-sizing: border-box; }');
        html.push('.wgta_form-control:focus { border-color: #66afe9; outline: 0; -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);');
        html.push('box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6); box-sizing: border-box; }');
        html.push('.wgta_form-control::-moz-placeholder { color: #999; opacity: 1; box-sizing: border-box; }');
        html.push('.wgta_form-control:-ms-input-placeholder { color: #999; box-sizing: border-box; }');
        html.push('.wgta_form-control::-webkit-input-placeholder { color: #999; box-sizing: border-box; }');
        html.push('.wgta_form-control::-ms-expand { background-color: transparent; border: 0; box-sizing: border-box; }');
        html.push('.wgta_form-control[disabled], .wgta_form-control[readonly], fieldset[disabled] .wgta_form-control { background-color: #eee; opacity: 1; }');
        html.push('.wgta_form-control[disabled], fieldset[disabled] .wgta_form-control { cursor: not-allowed; }');
        html.push('.wgta_input { margin: 0; font: inherit; color: #000000; line-height: normal; font-family: inherit; font-size: inherit; line-height: inherit; }');
        html.push('.wgta_label, .wgta_h3, .wgta_h4 { color: #ffffff; font-weight: bold; }');
        html.push('.wgta_label { display: inline-block; margin-bottom: 5px; max-width: 100%;}');
        html.push('.wgta_h3, .wgta_h4 { margin-top: 20px; font-family: inherit; margin-bottom: 10px; line-height: 1.1; }');
        html.push('.wgta_h3 { font-size: 24px; }');
        html.push('.wgta_h4 { font-size: 18px; }');
        html.push('.wgta_margin_top_06 { margin-top: 0.6em; }');
        html.push('.wgta_margin_top_15 { margin-top: 1.5em; }');
        html.push('.text_right { text-align: right; }');
        html.push('input.date { width: 150px; color: #000; }');
        html.push('input[type="image"]:focus { border: thin solid white; }');
        html.push('</style>');
        html.push('</head>');
        html.push('<body style="padding: 0; margin: 0 !important; color: #333;">');
        html.push('<div id="overlay"></div>');
        html.push('<div class="wgta_clearfix" style="background-color: #004b99; width: 504px; margin-left: auto; margin-right: auto; padding-right: 15px; padding-left: 15px; max-width: 100%; line-height: 1.42857; font-family: \'Helvetica Neue\', Helvetica, Arial, sans-serif; font-size: 13px;">');

        /*
         * Quando tiver na url o parametro skin igual a 1 oculta o logotipo
         */
        var parameters = WidgetTravelAce.parseQueryString(WidgetTravelAce.detectScript());
        if (parameters !== undefined) {
            switch (parseInt(parameters.skin, 10)) {
                case 1:
                    html.push('<div class="wgta_row wgta_clearfix">');
                    html.push('</div>');
                    break;
                default:
                    html.push('<div class="wgta_row wgta_clearfix">');
                    html.push('<img src="' + wgta.domainName + '/widget/images/orcamento_cabecalho.jpg" alt="CabeÃ§alho Widget Travel Ace"/>');
                    html.push('</div>');
                    break;
            }
        }

        html.push('<div style="padding: 0 0 1.5em 1.5em;">');
        html.push('<div class="wgta_row wgta_clearfix">');
        html.push('<div class="wgta_col-md-12">');
        html.push('<h3 class="wgta_h3" id="labelEscolhaDestino">ESCOLHA SEU DESTINO</h3>');
        html.push('<div style="margin-top: -30px; position: relative; width: 100%; float: right; margin-right: 30px;">');
        html.push('<img idioma="ES" style="display: none; float: right; margin-left: 15px; cursor: pointer;" id="imgES" src="' + wgta.domainName + '/Content/images/flagES.png" title="Espanhol" alt="Espanhol"/>');
        html.push('<img idioma="US" style="float: right; margin-left: 15px; cursor: pointer;" id="imgUS" src="' + wgta.domainName + '/Content/images/flagUS.png" title="English" alt="English"/>');
        html.push('<img idioma="BR" style="float: right; cursor: pointer;" id="imgBR" src="' + wgta.domainName + '/Content/images/flagBR.png" title="Portugues(Brasil)" alt="Portugues(Brasil)"/>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');

        html.push(WidgetTravelAce.montarBlocoDestinos());
        
        html.push('<div class="wgta_row wgta_clearfix" id="idDivBlocoDatas" style="margin-top: 1.5em;">');
        html.push('<div class="wgta_col-md-6">');        
        html.push('<label class="wgta_label" id="labelIda" for="idInputWgtaIda">START DATE</label>');
        html.push('<input id="idInputWgtaIda" name="idInputWgtaIda" type="text" maxlength="10" style="width: 100%" />');
        html.push('</div>');
        html.push('<div class="wgta_col-md-6">');
        html.push('<label class="wgta_label" id="labelVolta" for="idInputWgtaVolta">VOLTA</label>');
        html.push('<input id="idInputWgtaVolta" name="idInputWgtaVolta" type="text" maxlength="10" style="width: 100%" />');
        html.push('</div>');
        html.push('</div>');

        /*
         * Quando na url tiver o parametro dp igual a true exibe os campos de dados pessoais para fazer leads
         */
        if (parameters !== undefined) {
            switch (parameters.dp) {
                case "true":
                    html.push(WidgetTravelAce.montarBlocoDadosPessoais());
                    break;
                default:
                    break;
            }
            /*
             * Quando na url tiver o parametro co igual a true exibe o campo codigo operacao
             */
            switch (parameters.co) {
                case "true":
                    html.push(WidgetTravelAce.montarBlocoCodigoOperacao());
                    break;
                default:
                    break;
            }
            /*
            * Quando na url tiver o parametro ac igual a true exibe o campo angariador / consultor

            switch (parameters.ac) {
                case "true":
                    html.push(WidgetTravelAce.montarBlocoAngariadorConsultor());
                    break;
                default:
                    break;
            }
            */
        }

        html.push('<div class="wgta_row wgta_clearfix">');
        html.push('<div class="wgta_col-md-12">');
        html.push('<h4 id="labelPassageiros" class="wgta_h4">PASSAGEIROS</h4>');
        html.push('</div>');
        html.push('</div>');
        html.push('<div class="wgta_row wgta_clearfix">');
        html.push('<div class="wgta_col-md-12">');
        html.push('<label class="wgta_label" id="labelQuantidadePassageiros" for="idInputWgtaAte21">QUANTIDADE DE PASSAGEIROS</label>');
        html.push('<select class="wgta_input wgta_form-control" id="quantidadePassageiros" onchange="">');

        for (var index = 1; index <= LimitePassageiros; index++)
            html.push('<option class="" value="' + index + '">' + index + ' passageiros</option>');

        html.push('</select>');
        html.push('</div>');
        html.push('</div>');
        html.push('<div class="wgta_row wgta_clearfix" style="margin-top: 1.5em;" id="divIdades">');
        html.push('</div>');

        html.push('<div class="wgta_row wgta_clearfix" id="idDivBlocoCupom" style="margin-top: 1.5em; display:none;">');
        html.push('<div class="wgta_col-md-12">');
        html.push('<label class="wgta_label" for="idInputWgtaCupom">CUPOM</label>');
        html.push('<input type="text" class="wgta_input wgta_form-control" name="idInputWgtaCupom" id="idInputWgtaCupom" maxlength="80"/>');
        html.push('</div>');
        html.push('</div>');

        html.push('<div class="wgta_row wgta_clearfix">');
        html.push('<div class="wgta_col-md-12" >');
        html.push('<div class="text_right wgta_margin_top_15">');
        html.push('<button type="button" style="display: inline-block; padding: 6px 12px; margin-bottom: 0; font-size: 14px; font-weight: 400; line-height: 1.42857143; text-align: center; white-space: nowrap; vertical-align: middle; -ms-touch-action: manipulation; touch-action: manipulation; cursor: pointer; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none; background-image: none;  border: 1px solid transparent;  border-radius: 4px; color: #fff; background-color: #de880e; border-color: #eea236; font-weight: bold;" id="btnCotar" onclick="wgta.cotar();">COTAR <i class="fa fa-globe fa-4"></i></button>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');
        html.push('</body>');
        html.push('</html>');

        divWidget.innerHTML = WidgetTravelAce.montarHtmlApartirDoArray(html);
        WidgetTravelAce.montarBlocoIdades();
        WidgetTravelAce.addEvent(document.getElementById('quantidadePassageiros'), 'change', WidgetTravelAce.montarBlocoIdades);

        var parameters = WidgetTravelAce.parseQueryString(WidgetTravelAce.detectScript());
        if (parameters.id != undefined)
            WidgetTravelAce.setarValoresCampos();

    };

    WidgetTravelAce.setarValoresCampos = function () {
        var parameters = WidgetTravelAce.parseQueryString(WidgetTravelAce.detectScript());
        if (parameters !== undefined) {
            // Verifica se o campo CodigoOperacao esta sendo exibido
            switch (parameters.co) {
                case "true":
                    OrcamentoDados.valueCo = parameters.valueCo;
                    document.getElementById("idInputWgtaCodigoOperacao").value = OrcamentoDados.valueCo;
                    break;
            }

            switch (parameters.d) {

                case 250:
                case "250":
                    wgta.selecionarDestino('idDivDestino_AMS', '250');
                    break;

                case 249:
                case "249":
                    wgta.selecionarDestino('idDivDestino_AMN', '249');
                    break;

                case 252:
                case "252":
                    wgta.selecionarDestino('idDivDestino_EU', '252');
                    break;

                case 248:
                case "248":
                    wgta.selecionarDestino('idDivDestino_AMC', '248');
                    break;

                case 251:
                case "251":
                    wgta.selecionarDestino('idDivDestino_AS', '251');
                    break;

                case 253:
                case "253":
                    wgta.selecionarDestino('idDivDestino_OC', '253');
                    break;

                case 247:
                case "247":
                    wgta.selecionarDestino('idDivDestino_AF', '247');
                    break;

                case 2:
                case "2":
                    wgta.selecionarDestino('idDivDestino_BR', '2');
                    break;

            }

            var quantidade_pax = parameters.id.split(",").length;
            document.getElementById("quantidadePassageiros").value = quantidade_pax;

            WidgetTravelAce.montarBlocoIdades();

            for (var index = 0; index < parameters.id.split(",").length; index++)
                document.getElementsByName("idIdade")[index].value = parameters.id.split(",")[index];

        }
    }

    WidgetTravelAce.addEvent = function (elem, event, fn) {
        if (elem.addEventListener) {
            elem.addEventListener(event, fn, false);
        } else {
            elem.attachEvent("on" + event, function () {
                return (fn.call(elem, window.event));
            });
        }
    };

    WidgetTravelAce.montarBlocoIdades = function () {        
        var html_idades = "";
        var quantidadePassageirosSelecionados = document.getElementById("quantidadePassageiros").value;

        var elementos_idades = document.getElementsByName("idIdade");
        var idades = new Array();

        for (var index = 0; index < document.getElementsByName("idIdade").length; index++) {
            if (elementos_idades[index] != "" && elementos_idades != null)
                idades[idades.length] = elementos_idades[index].value;
        }

        var labelIdade = traducaoLabel("idade");

        for (var index = 1; index <= quantidadePassageirosSelecionados; index++) {
            var idade = (idades[index - 1] != null && idades[index - 1] != undefined) ? idades[index - 1] : "";
            html_idades += '<div class="wgta_col-md-2"><label class="wgta_label labelIdade" for="idInputIdade">' + labelIdade + '</label><input type="text" class="wgta_input wgta_form-control" onkeypress="wgta.somenteNumeros(event)" name="idIdade" id="idIdade" maxlength="2" value="' + idade + '" /></div>';
        }

        document.getElementById("divIdades").innerHTML = html_idades;
    };

    WidgetTravelAce.montarBlocoDestinos = function () {

        var labelAmericaDoSul = traducaoLabel("americaDoSul");
        var labelAmericaDoNorte = traducaoLabel("americaDoNorte");
        var labelAmericaCentral = traducaoLabel("americaCentral");
        var labelAfrica = traducaoLabel("africa");
        var labelAsia = traducaoLabel("asia");
        var labelOceania = traducaoLabel("oceania");
        var labelEuropa = traducaoLabel("europa");
        var labelBrasil = traducaoLabel("brasil");

        var html = [];
        html.push('<div id="idDivBlocoDestinos">');
        html.push('<div class="wgta_row wgta_clearfix">');
        html.push('<div id="idDivDestino_AMS" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino(\'idDivDestino_AMS\', \'250\')">');
        html.push('<input type="image" src="' + wgta.domainName + '/widget/images/botao_america_do_sul.png" alt="botao_america_do_sul.png" />');
        html.push('<div id="destinoAMS" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino(\'idDivDestino_AMS\', \'250\')"><strong>' + labelAmericaDoSul + '</strong></div>');
        html.push('</div>');
        html.push('<div id="idDivDestino_AMN" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino(\'idDivDestino_AMN\', \'249\')">');
        html.push('<input type="image" src="' + wgta.domainName + '/widget/images/botao_america_do_norte.png" alt="botao_america_do_norte.png" />');
        html.push('<div id="destinoAMN" style="position: absolute; top: 17px; left: 12px; width: 79px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino(\'idDivDestino_AMN\', \'249\')">' + labelAmericaDoNorte + '</div>');
        html.push('</div>');
        html.push('<div id="idDivDestino_EU" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino(\'idDivDestino_EU\', \'252\')">');
        html.push('<input type="image" src="' + wgta.domainName + '/widget/images/botao_europa.png" alt="botao_europa.png" />');
        html.push('<div id="destinoEU" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino(\'idDivDestino_EU\', \'252\')">' + labelEuropa + '</div>');
        html.push('</div>');
        html.push('<div id="idDivDestino_AMC" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino(\'idDivDestino_AMC\', \'248\')">');
        html.push('<input type="image" src="' + wgta.domainName + '/widget/images/botao_america_central.png" alt="botao_america_central.png"/>');
        html.push('<div id="destinoAMC" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino(\'idDivDestino_AMC\', \'248\')">' + labelAmericaCentral + '</div>');
        html.push('</div>');
        html.push('</div>');
        html.push('<div class="wgta_row wgta_clearfix">');
        html.push('<div id="idDivDestino_AS" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino(\'idDivDestino_AS\', \'251\')">');
        html.push('<input type="image" src="' + wgta.domainName + '/widget/images/botao_asia.png" alt="botao_asia.png" />');
        html.push('<div id="destinoAS" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino(\'idDivDestino_AS\', \'251\')">' + labelAsia + '</div>');
        html.push('</div>');
        html.push('<div id="idDivDestino_OC" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino(\'idDivDestino_OC\', \'253\')">');
        html.push('<input type="image" src="' + wgta.domainName + '/widget/images/botao_oceania.png" alt="botao_oceania.png" />');
        html.push('<div id="destinoOC" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino(\'idDivDestino_OC\', \'253\')">' + labelOceania + '</div>');
        html.push('</div>');
        html.push('<div id="idDivDestino_AF" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino(\'idDivDestino_AF\', \'247\')">');
        html.push('<input type="image" src="' + wgta.domainName + '/widget/images/botao_africa.png" alt="botao_africa.png" />');
        html.push('<div id="destinoAF" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino(\'idDivDestino_AF\', \'247\')">' + labelAfrica + '</div>');
        html.push('</div>');
        html.push('<div id="idDivDestino_BR" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino(\'idDivDestino_BR\', \'2\')">');
        html.push('<input type="image" src="' + wgta.domainName + '/widget/images/botao_brasil.png" alt="botao_brasil.png" />');
        html.push('<div id="destinoBR" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino(\'idDivDestino_BR\', \'2\')">' + labelBrasil + '</div>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');

        return WidgetTravelAce.montarHtmlApartirDoArray(html);
    };

    WidgetTravelAce.montarBlocoDadosPessoais = function () {
        var html = [];
        html.push('<div id="idDivDadosPessoais">');
        html.push('<div class="wgta_row wgta_clearfix" style="margin-top: 1.5em;">');
        html.push('<div class="wgta_col-md-12">');
        html.push('<label class="wgta_label" id="labelNomeCompleto" for="idInputWgtaNomeCompleto">NOME COMPLETO</label>');
        html.push('<input type="text" class="wgta_input wgta_form-control" name="idInputWgtaNomeCompleto" id="idInputWgtaNomeCompleto" maxlength="80"/>');
        html.push('</div>');
        html.push('</div>');
        html.push('<div class="wgta_row wgta_clearfix" style="margin-top: 1.5em;">');
        html.push('<div class="wgta_col-md-4">');
        html.push('<label class="wgta_label" id="labelTelefone" for="idInputWgtaTelefone">TELEFONE</label>');
        html.push('<input type="tel" class="wgta_input wgta_form-control" name="idInputWgtaTelefone" id="idInputWgtaTelefone" maxlength="15" onkeypress="wgta.somenteNumeros(event); wgta.mascaraTelefone(this, event)"/>');
        html.push('</div>');
        html.push('<div class="wgta_col-md-8">');
        html.push('<label class="wgta_label" id="labelEmail for="idInputWgtaEmail">EMAIL</label>');
        html.push('<input type="email" class="wgta_input wgta_form-control" name="idInputWgtaEmail" id="idInputWgtaEmail" maxlength="50""/>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');

        return WidgetTravelAce.montarHtmlApartirDoArray(html);
    };

    WidgetTravelAce.montarBlocoCodigoOperacao = function () {
        var html = [];
        html.push('<div id="idDivCodigoOperacao">');
        html.push('<div class="wgta_row wgta_clearfix" style="margin-top: 1.5em;">');
        html.push('<div class="wgta_col-md-12">');
        html.push('<label class="wgta_label" for="idInputWgtaCodigoOperacao">CÃ“DIGO OPERAÃ‡ÃƒO</label>');
        html.push('<select class="wgta_form-control" name="idInputWgtaCodigoOperacao" id="idInputWgtaCodigoOperacao"><option value="0"></option><option value="1">15-A</option><option value="2">20-A</option><option value="3">25-A</option><option value="4">30-A</option><option value="5">35-A</option><option value="6">40-A</option></select>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');

        return WidgetTravelAce.montarHtmlApartirDoArray(html);
    };

    WidgetTravelAce.montarBlocoAngariadorConsultor = function () {

        var html = [];
        html.push('<div id="idDivAngariadorConsultor">');
        html.push('<div class="wgta_row wgta_clearfix" style="margin-top: 1.5em;">');
        html.push('<div class="wgta_col-md-12">');
        html.push('<label class="wgta_label" for="idInputWgtaAngariadorConsultor">ANGARIADOR / CONSULTOR</label>');
        html.push('<input type="text" class="wgta_input wgta_form-control" name="idInputWgtaAngariadorConsultor" id="idInputWgtaAngariadorConsultor" />');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');

        return WidgetTravelAce.montarHtmlApartirDoArray(html);

    };

    WidgetTravelAce.fn.exibeCamposIdade = function () {
        WidgetTravelAce.montarBlocoIdades();
    }


    WidgetTravelAce.fn.selecionarDestino = function (idDivDestino, destino) {
        OrcamentoDados.destino = parseInt(destino, 10);
        document.getElementById('idDivBlocoDestinos').innerHTML = WidgetTravelAce.montarBlocoDestinos();

        var labelAmericaDoSul = traducaoLabel("americaDoSul");
        var labelAmericaDoNorte = traducaoLabel("americaDoNorte");
        var labelAmericaCentral = traducaoLabel("americaCentral");
        var labelAfrica = traducaoLabel("africa");
        var labelAsia = traducaoLabel("asia");
        var labelOceania = traducaoLabel("oceania");
        var labelEuropa = traducaoLabel("europa");
        var labelBrasil = traducaoLabel("brasil");

        switch (idDivDestino) {
            case 'idDivDestino_AF':
                document.getElementById(idDivDestino).innerHTML = '<input type="image" src="' + wgta.domainName + '/widget/images/botao_africa_cinza.png" alt="botao_africa_cinza.png"/><div id="destinoAF" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;">' + labelAfrica + '</div>';
                break;
            case 'idDivDestino_AMC':
                document.getElementById(idDivDestino).innerHTML = '<input type="image" src="' + wgta.domainName + '/widget/images/botao_america_central_cinza.png" alt="botao_america_central_cinza.png"/><div id="destinoAMC" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;">' + labelAmericaCentral + '</div>';
                break;
            case 'idDivDestino_AMN':
                document.getElementById(idDivDestino).innerHTML = '<input type="image" src="' + wgta.domainName + '/widget/images/botao_america_do_norte_cinza.png" alt="botao_america_do_norte_cinza.png"/><div id="destinoAMN" style="position: absolute; top: 17px; left: 12px; width: 65px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;">' + labelAmericaDoNorte + '</div>';
                break;
            case 'idDivDestino_AMS':
                document.getElementById(idDivDestino).innerHTML = '<input type="image" src="' + wgta.domainName + '/widget/images/botao_america_do_sul_cinza.png" alt="botao_america_do_sul_cinza.png"/><div id="destinoAMS" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;"><strong>' + labelAmericaDoSul + '</strong></div>';
                break;
            case 'idDivDestino_AS':
                document.getElementById(idDivDestino).innerHTML = '<input type="image" src="' + wgta.domainName + '/widget/images/botao_asia_cinza.png" alt="botao_asia_cinza.png"/><div id="destinoAS" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;">' + labelAsia + '</div>';
                break;
            case 'idDivDestino_BR':
                document.getElementById(idDivDestino).innerHTML = '<input type="image" src="' + wgta.domainName + '/widget/images/botao_brasil_cinza.png" alt="botao_brasil_cinza.png"/><div id="destinoBR" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;">' + labelBrasil + '</div>';
                break;
            case 'idDivDestino_EU':
                document.getElementById(idDivDestino).innerHTML = '<input type="image" src="' + wgta.domainName + '/widget/images/botao_europa_cinza.png" alt="botao_europa_cinza.png"/><div id="destinoEU" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;">' + labelEuropa + '</div>';
                break;
            case 'idDivDestino_OC':
                document.getElementById(idDivDestino).innerHTML = '<input type="image" src="' + wgta.domainName + '/widget/images/botao_oceania_cinza.png" alt="botao_oceania_cinza.png"/><div id="destinoOC" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;">' + labelOceania + '</div>';
                break;
        }
    };

    WidgetTravelAce.obterIdades = function () {

        var idades = new Array();
        var elemento_idade = document.getElementsByName("idIdade");
        for (var index = 0; index < elemento_idade.length; index++) {
            if (elemento_idade[index].value != "" && elemento_idade[index].value != null && elemento_idade[index].value != undefined)
                idades[idades.length] = elemento_idade[index].value;
        }

        if (idades.length == 0)
            return null;
        else
            return idades.join(",");

    };

    WidgetTravelAce.capturarDados = function () {

        OrcamentoDados.idades = WidgetTravelAce.obterIdades();
        OrcamentoDados.dataIda = (document.getElementById('idInputWgtaIda').value).trim();
        OrcamentoDados.dataVolta = (document.getElementById('idInputWgtaVolta').value).trim();

        OrcamentoDados.qtdPax = OrcamentoDados.idades == null ? 0 : OrcamentoDados.idades.split(',').length;
        OrcamentoDados.cupom = (document.getElementById('idInputWgtaCupom').value).trim();

        var parameters = WidgetTravelAce.parseQueryString(WidgetTravelAce.detectScript());
        if (parameters !== undefined) {
            OrcamentoDados.usuario = parameters.un;
            OrcamentoDados.senha = parameters.pw;
            OrcamentoDados.in = parameters.in;

            if (parameters.dp == undefined)
                OrcamentoDados.dp = false;
            else
                OrcamentoDados.dp = parameters.dp;

            OrcamentoDados.skin = parameters.skin;
            OrcamentoDados.co = parameters.co;
            OrcamentoDados.ac = parameters.ac;

            // Verifica se passa os dados pessoais para fazer leads
            switch (parameters.dp) {
                case "true":
                    OrcamentoDados.nomeCompleto = (document.getElementById('idInputWgtaNomeCompleto').value).trim();
                    OrcamentoDados.telefone = (document.getElementById('idInputWgtaTelefone').value).trim();
                    OrcamentoDados.email = (document.getElementById('idInputWgtaEmail').value).trim();
                    break;
            }

            // Verifica se o campo CodigoOperacao esta sendo exibido
            switch (parameters.co) {
                case "true":
                    var valueCo = document.getElementById("idInputWgtaCodigoOperacao").options[document.getElementById("idInputWgtaCodigoOperacao").selectedIndex].value;
                    if (valueCo !== undefined) {
                        OrcamentoDados.valueCo = valueCo;
                    }
                    break;
            }
        } else {
            //OrcamentoDados.usuario = null;
            //OrcamentoDados.senha = null;
            OrcamentoDados.usuario = "4CA3A80D224992BEE18439B47EA3E3D4BF9EAA6CEC01FDFF";
            OrcamentoDados.senha = "E02DDAABEFAE52816732E997BE42E4EE";
        }
    };

    /*
    * FunÃ§Ã£o para evitar digitaÃ§Ã£o de caracteres em campos NumÃ©ricos
    * Uso <input type="text" maxlength="15" onkeypress="wgta.somenteNumeros(event)">
    */
    WidgetTravelAce.fn.somenteNumeros = function (event) {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            event.preventDefault();
        }
    };

    /*
    * FunÃ§Ã£o para adicionar mÃ¡scara ao telefone
    * Uso <input type="text" maxlength="15" onkeyup="wgta.mascaraTelefone(this, event)">
    */
    WidgetTravelAce.fn.mascaraTelefone = function (campo, event) {
        return WidgetTravelAce.formataCampo(campo, '(00) 00000-0000', event);
    };

    /*
    * @function
    * @name montarHtmlApartirDoArray
    * @description Recebe um array contendo trechos de html em cada registro, monta e retorna uma string html
    */
    WidgetTravelAce.montarHtmlApartirDoArray = function (template) {
        var html = '';

        for (var i = 0; i < template.length; i++) {
            html += template[i];
        }

        return html;
    };

    // formata de forma generica os campos
    WidgetTravelAce.formataCampo = function (campo, mascara, evento) {
        var boleanoMascara;
        var exp = /\-|\.|\/|\(|\)| /g;
        var campoSoNumeros = campo.value.toString().replace(exp, "");
        var posicaoCampo = 0;
        var novoValorCampo = "";
        var tamanhoMascara = campoSoNumeros.length;

        if (evento.keyCode !== 8) { // backspace 
            for (var i = 0; i <= tamanhoMascara; i++) {
                boleanoMascara = ((mascara.charAt(i) === "-") || (mascara.charAt(i) === ".")
                    || (mascara.charAt(i) === "/"));
                boleanoMascara = boleanoMascara || ((mascara.charAt(i) === "(")
                    || (mascara.charAt(i) === ")") || (mascara.charAt(i) === " "));
                if (boleanoMascara) {
                    novoValorCampo += mascara.charAt(i);
                    tamanhoMascara++;
                } else {
                    novoValorCampo += campoSoNumeros.charAt(posicaoCampo);
                    posicaoCampo++;
                }
            }
            campo.value = novoValorCampo;
            return true;
        } else {
            return true;
        }
    };

    /*
    * http://www.devmedia.com.br/validando-e-mail-em-inputs-html-com-javascript/26427
    */
    WidgetTravelAce.isEmailValido = function (campo) {
        var usuario = campo.substring(0, campo.indexOf("@"));
        var dominio = campo.substring(campo.indexOf("@") + 1, campo.length);

        if ((usuario.length >= 1) &&
            (dominio.length >= 3) &&
            (usuario.search("@") === -1) &&
            (dominio.search("@") === -1) &&
            (usuario.search(" ") === -1) &&
            (dominio.search(" ") === -1) &&
            (dominio.search(".") !== -1) &&
            (dominio.indexOf(".") >= 1) &&
            (dominio.lastIndexOf(".") < dominio.length - 1)) {
            return true;
        } else {
            return false;
        }
    };

    WidgetTravelAce.asDatasSaoValidas = function (dataIda, dataVolta) {
        var ida = dataIda.split('/'),
            volta = dataVolta.split('/'),
            // Ano, Mes, Dia - Subtrai 1 do mes porque ela vai de 0 ate 11
            nDataIda = new Date(parseInt(ida[2], 10), parseInt(ida[1], 10) - 1, parseInt(ida[0], 10)),
            nDataVolta = new Date(parseInt(volta[2], 10), parseInt(volta[1], 10) - 1, parseInt(volta[0], 10)),
            one_day = 1000 * 60 * 60 * 24, // Dia em milisegundos
            hoje = new Date(),
            inconsistencias = [];
        // Verifica se Ida Ã© maior que Volta
        var seIdaForMaior = Math.ceil((nDataIda.getTime() - nDataVolta.getTime()) / (one_day));
        if (seIdaForMaior > 0) {
            inconsistencias.push('- A data de Ida nÃ£o pode ser maior que a data da Volta');
        }
        // Verificar se Ida Ã© menor que hoje
        var seIdaMenorQueHoje = Math.ceil((nDataIda.getTime() - hoje.getTime()) / (one_day));
        if (seIdaMenorQueHoje < 0) {
            inconsistencias.push('- A data de Ida nÃ£o pode ser menor que Hoje.');
        }
        // Verificar se Volta Ã© menor que hoje
        var seVoltaMenorQueHoje = Math.ceil((nDataVolta.getTime() - hoje.getTime()) / (one_day));
        if (seVoltaMenorQueHoje < 0) {
            inconsistencias.push('- A data de Volta nÃ£o pode ser menor que Hoje.');
        }

        return inconsistencias;
    };

    WidgetTravelAce.fn.validarNome = function () {
        var nomeCompleto = (document.getElementById('idInputWgtaNomeCompleto').value).trim();
        if (nomeCompleto.length === 0) {
            alert('- O Nome Completo Ã© obrigatÃ³rio!');
        }
    };

    WidgetTravelAce.fn.validarTelefone = function () {
        var telefone = (document.getElementById('idInputWgtaTelefone').value).trim();
        if (telefone.length === 0) {
            alert('- O NÃºmero do Telefone Ã© obrigatÃ³rio!');
        }
        if (telefone.length < 14 || telefone.length > 15) {
            alert('- O NÃºmero de Telefone Ã© invÃ¡lido!');
        }
    };

    WidgetTravelAce.fn.validarEmail = function () {
        var email = (document.getElementById('idInputWgtaEmail').value).trim();
        if (email.length === 0) {
            alert('- O EndereÃ§o de E-mail Ã© obrigatÃ³rio!');
        }
    };

    WidgetTravelAce.fn.validarPassageiros = function () {

        var quantidade_idade_errada = 0;
        var elemento_idade = document.getElementsByName("idIdade");
        for (var index = 0; index < elemento_idade.length; index++) {
            if (elemento_idade[index].value == "" || elemento_idade[index].value == null || elemento_idade[index].value == undefined)
                quantidade_idade_errada++;
        }

        if (quantidade_idade_errada == 1)
            return "- Preencher o campo de idade.";
        else if (quantidade_idade_errada > 1)
            return "- Preencher os campos de idade.";

        return null;
    };

    WidgetTravelAce.isFormValido = function () {

        var pendencias = [],
            retorno = [],
            msg = traducaoLabel("mensagemErro");

        if (OrcamentoDados.destino.length === 0) {
            pendencias.push('- ' + traducaoLabel("destino"));
        }
        if (OrcamentoDados.dataIda.length === 0) {
            pendencias.push('- ' + traducaoLabel("ida"));
        }
        if (OrcamentoDados.dataVolta.length === 0) {
            pendencias.push('- ' + traducaoLabel("volta"));
        }

        var validarPassageiros = WidgetTravelAce.fn.validarPassageiros();
        if (validarPassageiros != null) {
            pendencias.push(validarPassageiros);
        }
        if (OrcamentoDados.qtdPax > LimitePassageiros) {
            pendencias.push('- O nÃºmero de passageiros nÃ£o pode ser maior que ' + LimitePassageiros);
        }
        if (OrcamentoDados.usuario === null) {
            pendencias.push('- Usuario');
        }
        if (OrcamentoDados.senha === null) {
            pendencias.push('- Senha');
        }
        if (OrcamentoDados.dataIda.length > 0 &&
            OrcamentoDados.dataVolta.length > 0) {
            var inconsistencias = WidgetTravelAce.asDatasSaoValidas(OrcamentoDados.dataIda, OrcamentoDados.dataVolta);
            for (var i = 0; i < inconsistencias.length; i++) {
                pendencias.push(inconsistencias[i]);
            }
        }

        switch (OrcamentoDados.dp) {
            case "true":
                if (OrcamentoDados.nomeCompleto.length === 0) {
                    pendencias.push('- ' + traducaoLabel("nomeCompleto"));
                }
                if (OrcamentoDados.telefone.length === 0) {
                    pendencias.push('- ' + traducaoLabel("telefone"));
                }
                if (OrcamentoDados.telefone.length <= 12) {
                    pendencias.push('- ' + traducaoLabel("ddTelefone"));
                }
                if (OrcamentoDados.telefone.length > 16 || OrcamentoDados.telefone.length < 14) {
                    pendencias.push('- ' + traducaoLabel("telefoneInvalido"));
                }
                if (OrcamentoDados.email.length === 0) {
                    pendencias.push('- ' + traducaoLabel("email"));
                } else {
                    var isValido = WidgetTravelAce.isEmailValido(OrcamentoDados.email);
                    if (!isValido) {
                        pendencias.push('- ' + traducaoLabel("emailInvalido"));
                    }
                }
                break;
        }

        for (var i = 0; i < pendencias.length; i++) {
            msg = msg + pendencias[i] + '\n';
        }

        if (pendencias.length > 0) {
            retorno.push(false);
        } else {
            retorno.push(true);
        }

        retorno.push(msg);
        return retorno;
    };

    Array.prototype.remove = function (start, end) {
        this.splice(start, end);
        return this;
    }

    WidgetTravelAce.fn.cotar = function () {
        WidgetTravelAce.capturarDados();

        var retorno = WidgetTravelAce.isFormValido();

        if (retorno[0]) {
            if (OrcamentoDados.in === 'true') {
                compra.cotar(OrcamentoDados);
            } else {
                var idAnalitics = WidgetTravelAce.findAnaliticId();

                if (OrcamentoDados.co == undefined || OrcamentoDados.co == null || OrcamentoDados.co == "")
                    OrcamentoDados.co = false;

                if (OrcamentoDados.ac == undefined || OrcamentoDados.ac == null || OrcamentoDados.ac == "")
                    OrcamentoDados.ac = false;
                //window.open(wgta.domainName + '/?d=' + OrcamentoDados.destino + '&di=' + WidgetTravelAce.getUtcDateString(OrcamentoDados.dataIda) + '&dv=' + WidgetTravelAce.getUtcDateString(OrcamentoDados.dataVolta) + '&qp=' + OrcamentoDados.qtdPax + '&dp=' + OrcamentoDados.dp + '&nc=' + OrcamentoDados.nomeCompleto + '&fone=' + OrcamentoDados.telefone + '&ai=' + idAnalitics + '&mail=' + OrcamentoDados.email + '&u=' + OrcamentoDados.usuario + '&s=' + OrcamentoDados.senha + '&c=' + OrcamentoDados.cupom + '&co=' + OrcamentoDados.co + '&valueCo=' + OrcamentoDados.valueCo + '&ac=' + OrcamentoDados.ac + '&idades=' + OrcamentoDados.idades + '&idioma=' + idioma, '_blank');
                window.location.href = wgta.domainName + '/?d=' + OrcamentoDados.destino + '&di=' + WidgetTravelAce.getUtcDateString(OrcamentoDados.dataIda) + '&dv=' + WidgetTravelAce.getUtcDateString(OrcamentoDados.dataVolta) + '&qp=' + OrcamentoDados.qtdPax + '&dp=' + OrcamentoDados.dp + '&nc=' + OrcamentoDados.nomeCompleto + '&fone=' + OrcamentoDados.telefone + '&ai=' + idAnalitics + '&mail=' + OrcamentoDados.email + '&u=' + OrcamentoDados.usuario + '&s=' + OrcamentoDados.senha + '&c=' + OrcamentoDados.cupom + '&co=' + OrcamentoDados.co + '&valueCo=' + OrcamentoDados.valueCo + '&ac=' + OrcamentoDados.ac + '&idades=' + OrcamentoDados.idades + '&idioma=' + idioma, '_blank';
            }
        } else {
            alert(retorno[1]);
        }
    };

    WidgetTravelAce.findAnaliticId = function () {
        var index = 0;
        var url = window.location.href;
        for (index = 0; index < json_analitics.length; index++) {
            if (url.search(json_analitics[index].label) !== -1)
                return json_analitics[index].id;
        }

        return json_analitics_default;
    },

        WidgetTravelAce.getUtcDateString = function (ptDate) {
            var diaMesAno = ptDate.split('/'),
                newDate = new Date(diaMesAno[2], diaMesAno[1] - 1, diaMesAno[0]);
            return newDate.toUTCString();
        };

    // Inicializa o jQuery para o Kendo.Datepicker
    var jQuery;

    /******** Load jQuery if not present *********/
    /* http://alexmarandon.com/articles/web_widget_jquery/ */
    (function () {
        var counter = 0;

        doDetect();
        function doDetect() {
            if (typeof window.jQuery !== "undefined") {
                // ...jQuery has been loaded
                jQuery = window.jQuery;//.noConflict(true);
                main();
            }
            else if (++counter < 5) { // 5 or whatever
                setTimeout(doDetect, 10);
            }
            else {
                // Time out (and either load it or don't)
                var script_tag = document.createElement('script');
                script_tag.setAttribute('type', 'text/javascript');
                script_tag.setAttribute('src', wgta.domainName + '/widget/kendo/scripts/jquery.min.js');
                if (script_tag.readyState) {
                    script_tag.onreadystatechange = function () { // For old versions of IE
                        if (this.readyState === 'complete' || this.readyState === 'loaded') {
                            scriptLoadHandler();
                        }
                    };
                } else { // Other browsers
                    script_tag.onload = scriptLoadHandler;
                }
                // Try to find the head, otherwise default to the documentElement
                (document.getElementsByTagName('head')[0] || document.documentElement).appendChild(script_tag);
            }
        }
    })();

    //window._st_account = 2009;
    //(function () {
    //    var ss = document.createElement('script');
    //    ss.type = 'text/javascript';
    //    ss.async = true;
    //    ss.src = '//app.shoptarget.com.br/js/tracking.js';
    //    var sc = document.getElementsByTagName('script')[0];
    //    sc.parentNode.insertBefore(ss, sc);
    //})();

    /******** Called once jQuery has loaded ******/
    function scriptLoadHandler() {
        // Restore $ and window.jQuery to their previous values and store the
        // new jQuery in our local jQuery variable
        jQuery = window.jQuery.noConflict(true);;
        // Call our main function
        main();
    }

    return wgta;
})(window, document);


function detectScript() {
    var scripts = document.getElementsByTagName('script');
    for (var i = 0; i < scripts.length; ++i) {
        if (scripts[i].src.indexOf('/widget/widgetTAInit.js') > 0) {
            return scripts[i].src;
        }
    }
};

function parseQueryString(currentScript) {
    // Split into key/value pairs
    try {
        var queryString = currentScript.split('?')[1];
        var params = {},
            queries, temp, i, l;

        queries = queryString.split('&');

        // Convert the array of strings into an object
        for (i = 0, l = queries.length; i < l; i++) {
            temp = queries[i].split('=');
            params[temp[0]] = temp[1];
        }

        return params;
    } catch (e) {
        console.log(e);
    }
};

function obterTraducaoFormulario() {
    if (idioma == undefined) {
        idioma = "BR";
    }

    switch (idioma) {
        case "BR":
            dicionario = TraducaoBR;
            break;
        case "US":
            dicionario = TraducaoUS;
            break;
    }

    //var url = wgta.domainName + "/widget/traducao/" + idioma + "/formulario.json";
    //dicionario = readJsonTraducao(url);
    executarTraducoesCampos();

};

function traducaoLabel(label) {
    return dicionario[label];
};

function executarTraducoesCampos() {
    $.i18n.unload();
    $.i18n.load(dicionario);

    $("#labelEscolhaDestino")._t("escolhaDestino");
    $('#labelIda')._t("ida");
    $('#labelVolta')._t("volta");
    $('#labelNomeCompleto')._t("nomeCompleto");
    $('#labelTelefone')._t("telefone");
    $('#labelEmail')._t("email");
    $('#labelPassageiros')._t("passageiros");
    $('#labelQuantidadePassageiros')._t("quantidadePassageiros");
    $('.labelIdade')._t("idade");
    $('#btnCotar')._t('cotar', "<i class='fa fa-globe fa-4'></i>");
    $('#destinoAMS')._t('americaDoSul');
    $('#destinoAMN')._t('americaDoNorte');
    $('#destinoEU')._t('europa');
    $('#destinoAMC')._t('americaCentral');
    $('#destinoAS')._t('asia');
    $('#destinoOC')._t('oceania');
    $('#destinoAF')._t('africa');
    $('#destinoBR')._t('brasil');

    $('#quantidadePassageiros option').each(function (index, item) {
        var quantidadePassageiros = index + 1;
        var labelPassageiros = traducaoLabel("passageiros").toLowerCase();
        $(this).text(quantidadePassageiros + " " + labelPassageiros);
    });
};

function readJsonTraducao(url) {
    var traducao;
    $.ajaxSetup({
        async: false
    });
    $.getJSON(url, function (data) {
        traducao = data;
    });

    return traducao;
};

/******** Our main function ********/
function main() {
    // Script Kendo contÃ©m Jquery, Kendo.Core, Kendo.Calendar, Kendo.Popup, Kendo.Datepicker

    if (!window.jQuery)
        loadScripts([wgta.domainName + '/widget/kendo/scripts/jquery.min.js'], null);

    loadScripts([wgta.domainName + '/Scripts/plugins/select2.full.min.js'], null);

    loadScripts([wgta.domainName + '/Scripts/globalization/jquery.i18n.min.js'], function () {

        loadScripts([wgta.domainName + '/widget/traducao/BR/formulario.js'], function () {

            loadScripts([wgta.domainName + '/widget/traducao/US/formulario.js'], function () {

                var parameters = parseQueryString(detectScript());
                if (parameters !== undefined) {
                    idioma = parameters.idioma;
                    obterTraducaoFormulario();
                    if (parameters.un == "17FBD5ECE9AC10C0E4D49C2C5FF13956") {
                        $("body").append('<script>!function(f,b,e,v,n,t,s){if(f.fbq)return;n=f.fbq=function(){n.callMethod ? n.callMethod.apply(n, arguments) : n.queue.push(arguments)};if(!f._fbq)f._fbq=n;n.push=n;n.loaded=!0;n.version="2.0";n.queue=[];t=b.createElement(e);t.async=!0;t.src=v;s=b.getElementsByTagName(e)[0];s.parentNode.insertBefore(t,s)}(window, document,"script","https://connect.facebook.net/en_US/fbevents.js");fbq("init", "142609553180925");fbq("track", "PageView");</script><noscript><img height="1" width="1" style="display:none" src="https://www.facebook.com/tr?id=142609553180925&ev=PageView&noscript=1" /></noscript>');
                    }
                } else
                    obterTraducaoFormulario();

                loadScripts([wgta.domainName + '/widget/kendo/scripts/bundleKendoJs.js'],
                    function () {

                        $('#idInputWgtaIda').kendoDatePicker({
                            value: new Date($.now()),
                            min: new Date($.now(-1)),
                            format: 'dd/MM/yyyy',
                            culture: 'pt-BR',
                            change: function () {
                                if ($('#idInputWgtaIda').data('kendoDatePicker').value() > $('#idInputWgtaVolta').data('kendoDatePicker').value()) {
                                    $('#idInputWgtaVolta').data('kendoDatePicker').value(this.value());
                                }
                                $('#idInputWgtaVolta').data('kendoDatePicker').min(this.value());
                            }
                        });

                        $('#idInputWgtaVolta').kendoDatePicker({
                            value: new Date($.now()),
                            min: new Date($.now(-1)),
                            format: 'dd/MM/yyyy',
                            culture: 'pt-BR'
                        });

                        $("#imgES").click(function (event) {
                            idioma = $(this).attr("idioma");
                            obterTraducaoFormulario();
                        });

                        $("#imgBR").click(function (event) {
                            idioma = $(this).attr("idioma");
                            obterTraducaoFormulario();
                        });

                        $("#imgUS").click(function (event) {
                            idioma = $(this).attr("idioma");
                            obterTraducaoFormulario();
                        });

                    });
            });

        });

    });

}

function loadScripts(array, callback) {
    var loader = function (src, handler) {
        var script = document.createElement('script');
        script.src = src;
        script.onload = script.onreadystatechange = function () {
            script.onreadystatechange = script.onload = null;
            handler();
        };
        var head = document.getElementById('widgetTAContainer');
        (head).appendChild(script);
    };
    (function run() {
        if (array.length !== 0) {
            loader(array.shift(), run);
        } else {
            callback && callback();
        }
    })();
}