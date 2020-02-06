<!DOCTYPE html>
<html class="k-ie k-ie11" xmlns="http://www.w3.org/1999/xhtml">
</title><script src="//poscompra.shopconvert.com.br/js/helper_2009.js" type="text/javascript" async=""></script><script src="//static.shopback.net/tags/polyfill/promise.js" async=""></script><script src="//static.shopback.net/tags/polyfill/fetch.js" async=""></script><script src="//static.shopback.net/tags/jquery.js" async=""></script><script src="https://nwlabel.travelace.com.br/widget/kendo/scripts/jquery.min.js" type="text/javascript"></script></head>
<body>
    <input type="hidden" name="hdnIdDestino" id="hdnIdDestino" />

    <!--widgetTAContainer-->
    <div class="coluna_dupla" id="idDivWidgetTAContainer">
        <div id="widgetTAContainer">
            <meta http-equiv="X-UA-Compatible" content="IE=edge">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <link href="https://nwlabel.travelace.com.br/widget/kendo/styles/bundleCssKendo.css" rel="stylesheet" type="text/css" media="all">
            <style>
                img, picture, video, embed {
                    height: auto;
                }

                img {
                    border: 0 none;
                }

                *, *:before, *:after {
                    -webkit-box-sizing: border-box;
                    -moz-box-sizing: border-box;
                    box-sizing: border-box;
                }

                .wgta_clearfix:before, .wgta_clearfix:after {
                    content: " ";
                    display: table;
                }

                .wgta_clearfix:after {
                    clear: both;
                }

                .wgta_clearfix {
                    *zoom: 1;
                }

                .wgta_row {
                    margin-left: -15px;
                    margin-right: -15px;
                }

                .wgta_col-md-2, .wgta_col-md-3, .wgta_col-md-4, .wgta_col-md-6, .wgta_col-md-8, .wgta_col-md-12 {
                    min-height: 1px;
                    padding-left: 0px;
                    padding-right: 0px;
                    margin: 0 0.5em 0 0;
                    position: relative;
                }

                .wgta_col-md-2 {
                    width: 11.8%;
                    float: left;
                    margin: 0 0.7em 0.5em 0;
                }

                    .wgta_col-md-2:last-child {
                        margin-right: 0;
                    }

                .wgta_col-md-3 {
                    width: 22%;
                    float: left;
                    margin: 0 0.7em 0.5em 0;
                }

                    .wgta_col-md-3:last-child {
                        margin-right: 0;
                    }

                .wgta_col-md-4 {
                    width: 30.3%;
                    float: left;
                }

                    .wgta_col-md-4:last-child {
                        margin-right: 0;
                    }

                .wgta_col-md-6 {
                    width: 46.3%;
                    float: left;
                }

                    .wgta_col-md-6:last-child {
                        margin-right: 0;
                    }

                .wgta_col-md-8 {
                    width: 62.3%;
                    float: left;
                }

                    .wgta_col-md-8:last-child {
                        margin-right: 0;
                    }

                .wgta_col-md-12 {
                    width: 93.8%;
                }

                    .wgta_col-md-12:last-child {
                        margin-right: 0;
                    }

                .wgta_form-control {
                    border-radius: 0px;
                    display: block;
                    width: 100%;
                    padding: 6px 12px;
                    line-height: 1.42857143;
                    color: #555;
                    background-color: #fff;
                    background-image: none;
                    border: 1px solid #ccc;
                    -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
                    box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
                    -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
                    -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
                    transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
                    box-sizing: border-box;
                }

                    .wgta_form-control:focus {
                        border-color: #66afe9;
                        outline: 0;
                        -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
                        box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
                        box-sizing: border-box;
                    }

                    .wgta_form-control::-moz-placeholder {
                        color: #999;
                        opacity: 1;
                        box-sizing: border-box;
                    }

                    .wgta_form-control:-ms-input-placeholder {
                        color: #999;
                        box-sizing: border-box;
                    }

                    .wgta_form-control::-webkit-input-placeholder {
                        color: #999;
                        box-sizing: border-box;
                    }

                    .wgta_form-control::-ms-expand {
                        background-color: transparent;
                        border: 0;
                        box-sizing: border-box;
                    }

                    .wgta_form-control[disabled], .wgta_form-control[readonly], fieldset[disabled] .wgta_form-control {
                        background-color: #eee;
                        opacity: 1;
                    }

                    .wgta_form-control[disabled], fieldset[disabled] .wgta_form-control {
                        cursor: not-allowed;
                    }

                .wgta_input {
                    margin: 0;
                    font: inherit;
                    color: #000000;
                    line-height: normal;
                    font-family: inherit;
                    font-size: inherit;
                    line-height: inherit;
                }

                .wgta_label, .wgta_h3, .wgta_h4 {
                    color: #ffffff;
                    font-weight: bold;
                }

                .wgta_label {
                    display: inline-block;
                    margin-bottom: 5px;
                    max-width: 100%;
                }

                .wgta_h3, .wgta_h4 {
                    margin-top: 20px;
                    font-family: inherit;
                    margin-bottom: 10px;
                    line-height: 1.1;
                }

                .wgta_h3 {
                    font-size: 24px;
                }

                .wgta_h4 {
                    font-size: 18px;
                }

                .wgta_margin_top_06 {
                    margin-top: 0.6em;
                }

                .wgta_margin_top_15 {
                    margin-top: 1.5em;
                }

                .text_right {
                    text-align: right;
                }

                input.date {
                    width: 150px;
                    color: #000;
                }

                input[type="image"]:focus {
                    border: thin solid white;
                }
            </style>
            <div id="overlay"></div>
            <div class="wgta_clearfix" style='width: 504px; line-height: 1.4285; padding-right: 15px; padding-left: 15px; font-family: "Helvetica Neue", Helvetica, Arial, sans-serif; font-size: 13px; margin-right: auto; margin-left: auto; max-width: 100%; background-color: rgb(0, 75, 153);'>
                <div class="wgta_row wgta_clearfix">
                    <img alt="Cabeçalho Widget Travel Ace" src="https://nwlabel.travelace.com.br/widget/images/orcamento_cabecalho.jpg">
                </div>
                <div style="padding: 0px 0px 1.5em 1.5em;">
                    <div class="wgta_row wgta_clearfix">
                        <div class="wgta_col-md-12">
                            <h3 class="wgta_h3">ESCOLHA SEU DESTINO</h3>
                        </div>
                    </div>
                    <div style="margin-top: -30px; position: relative; width: 100%; float: right; margin-right: 30px;">
                        <img idioma="ES" style="display: none; float: right; margin-left: 15px; cursor: pointer;" id="imgES" src="https://nwlabel.travelace.com.br/Content/images/flagES.png" title="Espanhol" alt="Espanhol"><img idioma="US" style="float: right; margin-left: 15px; cursor: pointer;" id="imgUS" src="https://nwlabel.travelace.com.br/Content/images/flagUS.png" title="English" alt="English"><img idioma="BR" style="float: right; cursor: pointer;" id="imgBR" src="https://nwlabel.travelace.com.br/Content/images/flagBR.png" title="Portugues(Brasil)" alt="Portugues(Brasil)"></div>

                    <div id="idDivBlocoDestinos">
                        <div class="wgta_row wgta_clearfix">
                            <div id="Div2" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino('idDivDestino_AMS', '250')">
                                <input type="image" src="https://nwlabel.travelace.com.br/widget/images/botao_america_do_sul.png" alt="botao_america_do_sul.png">
                                <div id="destinoAMS" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino('idDivDestino_AMS', '250')"><strong>AMÉRICA DO SUL</strong></div>
                            </div>
                            <div id="Div3" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino('idDivDestino_AMN', '249')">
                                <input type="image" src="https://nwlabel.travelace.com.br/widget/images/botao_america_do_norte.png" alt="botao_america_do_norte.png">
                                <div id="destinoAMN" style="position: absolute; top: 17px; left: 12px; width: 79px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino('idDivDestino_AMN', '249')"><strong>AMÉRICA DO NORTE</strong></div>
                            </div>
                            <div id="Div4" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino('idDivDestino_EU', '252')">
                                <input type="image" src="https://nwlabel.travelace.com.br/widget/images/botao_europa.png" alt="botao_europa.png">
                                <div id="destinoEU" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino('idDivDestino_EU', '252')"><strong>EUROPA</strong></div>
                            </div>
                            <div id="Div5" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino('idDivDestino_AMC', '248')">
                                <input type="image" src="https://nwlabel.travelace.com.br/widget/images/botao_america_central.png" alt="botao_america_central.png">
                                <div id="destinoAMC" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino('idDivDestino_AMC', '248')"><strong>AMÉRICA CENTRAL</strong></div>
                            </div>
                        </div>
                        <div class="wgta_row wgta_clearfix">
                            <div id="Div6" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino('idDivDestino_AS', '251')">
                                <input type="image" src="https://nwlabel.travelace.com.br/widget/images/botao_asia.png" alt="botao_asia.png">
                                <div id="destinoAS" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino('idDivDestino_AS', '251')"><strong>ÁSIA</strong></div>
                            </div>
                            <div id="Div7" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino('idDivDestino_OC', '253')">
                                <input type="image" src="https://nwlabel.travelace.com.br/widget/images/botao_oceania.png" alt="botao_oceania.png">
                                <div id="destinoOC" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino('idDivDestino_OC', '253')"><strong>OCEANIA</strong></div>
                            </div>
                            <div id="Div8" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino('idDivDestino_AF', '247')">
                                <input type="image" src="https://nwlabel.travelace.com.br/widget/images/botao_africa.png" alt="botao_africa.png">
                                <div id="destinoAF" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino('idDivDestino_AF', '247')"><strong>ÁFRICA</strong></div>
                            </div>
                            <div id="Div9" class="wgta_col-md-3 wgta_col-xs-6" style="position: relative; width: 109px; height: 65px; margin-left: -0.3em; margin-bottom: 0.3em;" onclick="wgta.selecionarDestino('idDivDestino_BR', '2')">
                                <input type="image" src="https://nwlabel.travelace.com.br/widget/images/botao_brasil.png" alt="botao_brasil.png">
                                <div id="destinoBR" style="position: absolute; top: 17px; left: 12px; width: 60px; font-family: VERDANA; font-size: 11px; color: #00489f; cursor: pointer;" onclick="wgta.selecionarDestino('idDivDestino_BR', '2')"><strong>BRASIL</strong></div>
                            </div>
                        </div>
                    </div>




                    <%--<div id="idDivBlocoDestinos">
                        <div class="wgta_row wgta_clearfix">
                            <div class="wgta_col-md-3 wgta_col-xs-6" id="idDivDestino_AMS" style="width: 109px; height: 65px; margin-bottom: 0.3em; position: relative;">
                                <input onclick="wgta.selecionarDestino('idDivDestino_AMS', '250')" type="image" alt="botao_america_do_sul.png" src="https://nwlabel.travelace.com.br/widget/images/botao_america_do_sul.png"><strong>AMÉRICA DO SUL</strong></div>
                            <div class="wgta_col-md-3 wgta_col-xs-6" id="idDivDestino_AMN" style="width: 109px; height: 65px; margin-bottom: 0.3em; margin-left: -0.3em; position: relative;">
                                <input onclick="wgta.selecionarDestino('idDivDestino_AMN', '249')" type="image" alt="botao_america_do_norte.png" src="https://nwlabel.travelace.com.br/widget/images/botao_america_do_norte.png"></div>
                            <div class="wgta_col-md-3 wgta_col-xs-6" id="idDivDestino_EU" style="width: 109px; height: 65px; margin-bottom: 0.3em; margin-left: -0.3em; position: relative;">
                                <input onclick="wgta.selecionarDestino('idDivDestino_EU', '252')" type="image" alt="botao_europa.png" src="https://nwlabel.travelace.com.br/widget/images/botao_europa.png"></div>
                            <div class="wgta_col-md-3 wgta_col-xs-6" id="idDivDestino_AMC" style="width: 109px; height: 65px; margin-bottom: 0.3em; margin-left: -0.3em; position: relative;">
                                <input onclick="wgta.selecionarDestino('idDivDestino_AMC', '248')" type="image" alt="botao_america_central.png" src="https://nwlabel.travelace.com.br/widget/images/botao_america_central.png"></div>
                        </div>
                        <div class="wgta_row wgta_clearfix">
                            <div class="wgta_col-md-3 wgta_col-xs-6" id="idDivDestino_AS" style="width: 109px; height: 65px; margin-bottom: 0.3em; position: relative;">
                                <input onclick="    wgta.selecionarDestino('idDivDestino_AS', '251')" type="image" alt="botao_asia.png" src="https://nwlabel.travelace.com.br/widget/images/botao_asia.png"></div>
                            <div class="wgta_col-md-3 wgta_col-xs-6" id="idDivDestino_OC" style="width: 109px; height: 65px; margin-bottom: 0.3em; margin-left: -0.3em; position: relative;">
                                <input onclick="    wgta.selecionarDestino('idDivDestino_OC', '253')" type="image" alt="botao_oceania.png" src="https://nwlabel.travelace.com.br/widget/images/botao_oceania.png"></div>
                            <div class="wgta_col-md-3 wgta_col-xs-6" id="idDivDestino_AF" style="width: 109px; height: 65px; margin-bottom: 0.3em; margin-left: -0.3em; position: relative;">
                                <input onclick="    wgta.selecionarDestino('idDivDestino_AF', '247')" type="image" alt="botao_africa.png" src="https://nwlabel.travelace.com.br/widget/images/botao_africa.png"></div>
                            <div class="wgta_col-md-3 wgta_col-xs-6" id="idDivDestino_BR" style="width: 109px; height: 65px; margin-bottom: 0.3em; margin-left: -0.3em; position: relative;">
                                <input onclick="    wgta.selecionarDestino('idDivDestino_BR', '2')" type="image" alt="botao_brasil.png" src="https://nwlabel.travelace.com.br/widget/images/botao_brasil.png"></div>
                        </div>
                    </div>--%>
                    <div class="wgta_row wgta_clearfix" id="idDivBlocoDatas" style="margin-top: 1.5em;">
                        <div class="wgta_col-md-6">
                            <label class="wgta_label" id="labelIda" for="idInputWgtaIda">IDA</label>
                            <span class="k-widget k-datepicker k-header" style="width: 100%;"><span class="k-picker-wrap k-state-default"><input name="idInputWgtaIda" class="k-input" id="idInputWgtaIda" role="combobox" aria-disabled="false" aria-expanded="false" aria-owns="idInputWgtaIda_dateview" style="width: 100%;" type="text" maxlength="10" data-role="datepicker"><span class="k-select" role="button" aria-controls="idInputWgtaIda_dateview" unselectable="on"><span class="k-icon k-i-calendar" unselectable="on">select</span></span></span></span>
                        </div>
                        <div class="wgta_col-md-6">
                            <label class="wgta_label" id="labelVolta" for="idInputWgtaVolta">VOLTA</label>
                            <span class="k-widget k-datepicker k-header" style="width: 100%;"><span class="k-picker-wrap k-state-default"><input name="idInputWgtaVolta" class="k-input" id="idInputWgtaVolta" role="combobox" aria-disabled="false" aria-expanded="false" aria-owns="idInputWgtaVolta_dateview" style="width: 100%;" type="text" maxlength="10" data-role="datepicker"><span class="k-select" role="button" aria-controls="idInputWgtaVolta_dateview" unselectable="on"><span class="k-icon k-i-calendar" unselectable="on">select</span></span></span></span>
                        </div>
                    </div>
                    <div class="wgta_row wgta_clearfix">
                        <div class="wgta_col-md-12">
                            <h4 id="labelPassageiros" class="wgta_h4">PASSAGEIROS</h4>                            
                        </div>
                    </div>
                    <div class="wgta_row wgta_clearfix">
                        <div class="wgta_col-md-12">
                            <label class="wgta_label" id="labelQuantidadePassageiros" for="idInputWgtaAte21">QUANTIDADE DE PASSAGEIROS</label>
                            <select class="wgta_input wgta_form-control" id="quantidadePassageiros" onchange="wgta.exibeCamposIdade()">
                                <option value="1">1 passageiro(s)</option>
                                <option value="2">2 passageiro(s)</option>
                                <option value="3">3 passageiro(s)</option>
                                <option value="4">4 passageiro(s)</option>
                                <option value="5">5 passageiro(s)</option>
                                <option value="6">6 passageiro(s)</option>
                                <option value="7">7 passageiro(s)</option>
                                <option value="8">8 passageiro(s)</option>
                                <option value="9">9 passageiro(s)</option>
                                <option value="10">10 passageiro(s)</option>
                                <option value="11">11 passageiro(s)</option>
                                <option value="12">12 passageiro(s)</option>
                                <option value="13">13 passageiro(s)</option>
                                <option value="14">14 passageiro(s)</option>
                                <option value="15">15 passageiro(s)</option>
                                <option value="16">16 passageiro(s)</option>
                                <option value="17">17 passageiro(s)</option>
                                <option value="18">18 passageiro(s)</option>
                                <option value="19">19 passageiro(s)</option>
                                <option value="20">20 passageiro(s)</option>
                                <option value="21">21 passageiro(s)</option>
                                <option value="22">22 passageiro(s)</option>
                                <option value="23">23 passageiro(s)</option>
                                <option value="24">24 passageiro(s)</option>
                                <option value="25">25 passageiro(s)</option>
                                <option value="26">26 passageiro(s)</option>
                                <option value="27">27 passageiro(s)</option>
                                <option value="28">28 passageiro(s)</option>
                                <option value="29">29 passageiro(s)</option>
                                <option value="30">30 passageiro(s)</option>
                                <option value="31">31 passageiro(s)</option>
                                <option value="32">32 passageiro(s)</option>
                                <option value="33">33 passageiro(s)</option>
                                <option value="34">34 passageiro(s)</option>
                                <option value="35">35 passageiro(s)</option>
                                <option value="36">36 passageiro(s)</option>
                                <option value="37">37 passageiro(s)</option>
                                <option value="38">38 passageiro(s)</option>
                                <option value="39">39 passageiro(s)</option>
                                <option value="40">40 passageiro(s)</option>
                                <option value="41">41 passageiro(s)</option>
                                <option value="42">42 passageiro(s)</option>
                                <option value="43">43 passageiro(s)</option>
                                <option value="44">44 passageiro(s)</option>
                                <option value="45">45 passageiro(s)</option>
                                <option value="46">46 passageiro(s)</option>
                                <option value="47">47 passageiro(s)</option>
                                <option value="48">48 passageiro(s)</option>
                                <option value="49">49 passageiro(s)</option>
                                <option value="50">50 passageiro(s)</option>
                            </select>
                        </div>
                    </div>
                    <div class="wgta_row wgta_clearfix" id="divIdades" style="margin-top: 1.5em;">
                        <div class="wgta_col-md-2">
                            <label class="wgta_label labelIdade" for="idInputIdade">IDADE</label>
                            <input name="idIdade" class="wgta_input wgta_form-control" id="idIdade" onkeypress="wgta.somenteNumeros(event)" type="text" maxlength="2" value="">
                        </div>
                    </div>
                    <div class="wgta_row wgta_clearfix" id="idDivBlocoCupom" style="margin-top: 1.5em; display: none;">
                        <div class="wgta_col-md-12">
                            <label class="wgta_label" for="idInputWgtaCupom">CUPOM</label><input name="idInputWgtaCupom" class="wgta_input wgta_form-control" id="idInputWgtaCupom" type="text" maxlength="80">
                        </div>
                    </div>
                    <div class="wgta_row wgta_clearfix">
                        <div class="wgta_col-md-12">
                            <div class="text_right wgta_margin_top_15">
                                <%--<button type="button" style="display: inline-block; padding: 6px 12px; margin-bottom: 0; font-size: 14px; font-weight: 400; line-height: 1.42857143; text-align: center; white-space: nowrap; vertical-align: middle; -ms-touch-action: manipulation; touch-action: manipulation; cursor: pointer; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none; background-image: none;  border: 1px solid transparent;  border-radius: 4px; color: #fff; background-color: #de880e; border-color: #eea236; font-weight: bold;" id="btnCotar" onclick="wgta.cotar();">QUOTE <i class="fa fa-globe fa-4"></i></button>--%>
                                <input id="btnCotar" onclick="wgta.cotar();" type="image" alt="botao_cotar.png" src="https://nwlabel.travelace.com.br/widget/images/botao_cotar.png">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script src="https://nwlabel.travelace.com.br/widget/kendo/scripts/jquery.min.js"></script>
            <script src="https://nwlabel.travelace.com.br/widget/kendo/scripts/bundleKendoJs.js"></script>
        </div>
        <script src="//poscompra.shopconvert.com.br/js/cm.js" type="text/javascript" async=""></script>
        <script src="//static.shopback.net/tags/init.js" defer="" type="text/javascript" async="" data-client="200979e8ae63ac10bad9b6c6"></script>
        <script src="//app.shoptarget.com.br/js/tracking.js" type="text/javascript" async=""></script>
        <script src="../Content/js/widgetTAInit.js?un=4CA3A80D224992BEE18439B47EA3E3D4BF9EAA6CEC01FDFF&amp;pw=E02DDAABEFAE52816732E997BE42E4EE&amp;dp=false&amp;co=false&amp;ac=false&amp;skin=true"></script>
        <%--<script type="text/javascript" src="https://nwlabel.travelace.com.br/widget/widgetTAInit.js?un=4CA3A80D224992BEE18439B47EA3E3D4BF9EAA6CEC01FDFF&amp;pw=E02DDAABEFAE52816732E997BE42E4EE&amp;dp=false&amp;co=false&amp;ac=false&amp;skin=true"></script>--%>
        <script type="text/javascript">wgta.scriptLoadHandler();</script>
    </div>
</body>
</html>
