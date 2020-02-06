﻿
$(document).ready(function () {
    mudaPassosPF(1);

    //$(".single-slider").jRange({
    //    from: 2000000,
    //    to: 100000000,
    //    step: 1,
    //    scale: [2000000, 100000000],
    //    format: '%s',
    //    showLabels: true
    //});
    
    $('#btnFisica').click(function(){
        $('#hddPessoa').val("Física");
    }); 

    $('#btnJuridica').click(function(){
        $('#hddPessoa').val("Jurídica");
    });
    
    $('#btnPEP-Sim').click(function(){
        $('#hddPep').val("sim");
    });

    $('#btnPEP-Nao').click(function(){
        $('#hddPep').val("não");
    });

    $('#cmbGrauRelPF').change(function(){
        if($('#cmbGrauRelPF').val() == '1')
            $('#hddGrau').val("administrador");
        if($('#cmbGrauRelPF').val() == '2')
            $('#hddGrau').val("procurador");
        if($('#cmbGrauRelPF').val() == '3')
            $('#hddGrau').val("controlador");
    });

    //Consulta endereço pelo cep, caso exista preenche o formulário com os dados.
    $("#txtCEPPF").blur(function () {
        //retira a máscara
        var vlrcep = $("#txtCEPPF").val().replace(/[^\d]+/g, '');
        $.ajax({
            method: "POST",
            url: "calculoPessoa.aspx/ConsultaEnderecoSeguradoPorCepAsync",
            data: '{cep: "' + vlrcep + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);            
            if (retorno.d.Enderecos[0].Logradouro) {
                $('#txtLogrPF').val(retorno.d.Enderecos[0].Logradouro);
                $('#txtLogrPF').focus();
                $('#txtNumPF').val(retorno.d.Enderecos[0].NumeroLogr);
                $('#txtNumPF').focus();
                $('#txtBairroPF').val(retorno.d.Enderecos[0].Bairro);
                $('#txtBairroPF').focus();
                $('#txtCidadePF').val(retorno.d.Enderecos[0].Cidade);
                $('#txtCidadePF').focus();
                $('#txtUFPF').val(retorno.d.Enderecos[0].UF);
                $('#txtUFPF').focus();                
            }
        });
    });

    //Consulta endereço pelo cep, caso exista preenche o formulário com os dados.
    $("#txtRiscoCEP").blur(function () {
        //retira a máscara
        var vlrcep = $("#txtRiscoCEP").val().replace(/[^\d]+/g, '');
        $.ajax({
            method: "POST",
            url: "calculoPessoa.aspx/ConsultaEnderecoRiscoPorCepAsync",
            data: '{cep: "' + vlrcep + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);            
            if (retorno.d.LocalRisco) {
                $('#txtLocalRisco').val(retorno.d.LocalRisco);
                $('#txtLocalRisco').focus();
                $('#txtRiscoNum').val(retorno.d.NumeroLocalRisco);
                $('#txtRiscoNum').focus();
                $('#txtRiscoBairro').val(retorno.d.BairroRisco);
                $('#txtRiscoBairro').focus();
                $('#txtRiscoCidade').val(retorno.d.CidadeRisco);
                $('#txtRiscoCidade').focus();
                $('#txtRiscoUF').val(retorno.d.UF_Risco);
                $('#txtRiscoUF').focus();
            }
        });
    });

    //Consulta endereço pelo cep, caso exista preenche o formulário com os dados.
    $("#txtCEPPJ").blur(function () {
        //retira a máscara
        var vlrcep = $("#txtCEPPJ").val().replace(/[^\d]+/g, '');
        $.ajax({
            method: "POST",
            url: "calculoPessoa.aspx/ConsultaEnderecoSeguradoPorCepAsync",
            data: '{cep: "' + vlrcep + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);            
            if (retorno.d.Enderecos[0].Logradouro) {
                $('#txtLogrPJ').val(retorno.d.Enderecos[0].Logradouro);
                $('#txtLogrPJ').focus();
                $('#txtNumPJ').val(retorno.d.Enderecos[0].NumeroLogr);
                $('#txtNumPJ').focus();
                $('#txtBairroPJ').val(retorno.d.Enderecos[0].Bairro);
                $('#txtBairroPJ').focus();
                $('#txtCidadePJ').val(retorno.d.Enderecos[0].Cidade);
                $('#txtCidadePJ').focus();
                $('#txtUFPJ').val(retorno.d.Enderecos[0].UF);
                $('#txtUFPJ').focus();                
            }
        });
    });

    //Consulta endereço pelo cep, caso exista preenche o formulário com os dados.
    $("#txtRiscoCEPPJ").blur(function () {
        //retira a máscara
        var vlrcep = $("#txtRiscoCEPPJ").val().replace(/[^\d]+/g, '');
        $.ajax({
            method: "POST",
            url: "calculoPessoa.aspx/ConsultaEnderecoRiscoPorCepAsync",
            data: '{cep: "' + vlrcep + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);            
            if (retorno.d.LocalRisco) {
                $('#txtLocalRiscoPJ').val(retorno.d.LocalRisco);
                $('#txtLocalRiscoPJ').focus();
                $('#txtRiscoNumPJ').val(retorno.d.NumeroLocalRisco);
                $('#txtRiscoNumPJ').focus();
                $('#txtRiscoBairroPJ').val(retorno.d.BairroRisco);
                $('#txtRiscoBairroPJ').focus();
                $('#txtRiscoCidadePJ').val(retorno.d.CidadeRisco);
                $('#txtRiscoCidadePJ').focus();
                $('#txtRiscoUFPJ').val(retorno.d.UF_Risco);
                $('#txtRiscoUFPJ').focus();
            }
        });
    });

    $('#txtBuscaPessoaPF').blur(function () {
        //retira a máscara
        var vlrCpf = $("#txtBuscaPessoaPF").val().replace(/[^\d]+/g, '');
        if(vlrCpf.length == 0)
            return;
        $.ajax({
            method: "POST",
            url: "calculoPessoa.aspx/ConsultarSeguradoPorCpfAsync",
            data: '{cpf: "' + vlrCpf + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);            
            if (retorno.d.Nome) {
                $('#hddIdSegurado').val(retorno.d.IdSegurado);
                $('#txtNomePF').val(retorno.d.Nome);                
                $('#txtNomePF').focus();
                $('#txtCEPPF').val(retorno.d.Enderecos[0].CEP);
                $('#txtCEPPF').focus();
                $('#txtLogrPF').val(retorno.d.Enderecos[0].Logradouro);
                $('#txtLogrPF').focus();
                if($('#txtNumPF').val().length == 0){
                    $('#txtNumPF').val(retorno.d.Enderecos[0].NumeroLogr);
                    $('#txtNumPF').focus();
                } 
                $('#txtBairroPF').val(retorno.d.Enderecos[0].Bairro);
                $('#txtBairroPF').focus();
                $('#txtCidadePF').val(retorno.d.Enderecos[0].Cidade);
                $('#txtCidadePF').focus();
                $('#txtUFPF').val(retorno.d.Enderecos[0].UF);
                $('#txtUFPF').focus();             
                $('#txtCelularPF').val(retorno.d.Celular);
                $('#txtCelularPF').focus();
                $('#txtRGPF').val(retorno.d.RG);
                $('#txtRGPF').focus();
                $('#txtOrgExpPF').val(retorno.d.OrgaoExp);
                $('#txtOrgExpPF').focus();
                $('#txtDtExpedPF').val(retorno.d.STRDataExp);
                $('#txtDtExpedPF').focus();
                $('#txtDtNascPF').val(retorno.d.STRDataNascimento);
                $('#txtDtNascPF').focus();
                $('#txtProfissaoPF').val(retorno.d.Profissao);
                $('#txtProfissaoPF').focus();
            }
        })
    });


    $('#txtBuscaPessoaPJ').blur(function () {
        //retira a máscara
        var vlrCnpj = $("#txtBuscaPessoaPJ").val().replace(/[^\d]+/g, '');
        if(vlrCnpj.length == 0)
            return;

        $.ajax({
            method: "POST",
            url: "calculoPessoa.aspx/ConsultarSeguradoPorCnpjAsync",
            data: '{cnpj: "' + vlrCnpj + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (retorno) {
            //console.log(retorno.d);            
            if (retorno.d.RazaoSocial) {
                $('#hddIdSegurado').val(retorno.d.IdSegurado);
                $('#txtNomePJ').val(retorno.d.Nome);                
                $('#txtNomePJ').focus();
                $('#txtNomeFantasiaPJ').val(retorno.d.NomeFantasia);
                $('#txtNomeFantasiaPJ').focus();
                $('#txtRazaoSocialPJ').val(retorno.d.RazaoSocial);
                $('#txtRazaoSocialPJ').focus();
                $('#txtCEPPJ').val(retorno.d.Enderecos[0].CEP);
                $('#txtCEPPJ').focus();
                $('#txtLogrPJ').val(retorno.d.Enderecos[0].Logradouro);
                $('#txtLogrPJ').focus();
                $('#txtNumPJ').val(retorno.d.Enderecos[0].NumeroLogr);
                $('#txtNumPJ').focus();
                $('#txtBairroPJ').val(retorno.d.Enderecos[0].Bairro);
                $('#txtBairroPJ').focus();
                $('#txtCidadePJ').val(retorno.d.Enderecos[0].Cidade);
                $('#txtCidadePJ').focus();             
                $('#txtUFPJ').val(retorno.d.Enderecos[0].UF);
                $('#txtUFPJ').focus();                
            }
        })
    });

    $('#slValorProjPJ').on('change', function () {

        var vlrComp = $('#slValorProjPJ').val().replace(/[^\d]+/g, '');        
        var vlrTotal = parseFloat(vlrComp);

        $('#spVlrTotalPJ').text(String(vlrTotal).replace('.', ','));
        formataValorCotacao();
    });

    $('#txtDataInicialEquip').focus(function () {
        if ($('#txtDtFimObra').val().length > 0)
            populaCamposDataVigenciaPF();        
    });

    $('#txtVigEquipPJ').focus(function () {
        if ($('#txtDtPrazoFimObraPJ').val().length > 0)
            populaCamposDataVigenciaPJ();
    });

    $('ins').priceFormat({
        prefix: 'R$ ',
        centsSeparator: ',',
        thousandsSeparator: '.'
    });

    $('.valor-formato').priceFormat({
        prefix: 'R$ ',
        centsSeparator: ',',
        thousandsSeparator: '.'
    });

});

$(function () {

    //Abre campos para PF
    $(document).on('click', '#lkCpfPF, #lkCpfPJ', function () {
        $("#btnPessoaPF, #btnPessoaPJ").text('CPF');

        //$('#txtBuscaPessoaPF, #txtBuscaPessoaPJ').removeClass("cnpj");
        //$('#txtBuscaPessoaPF, #txtBuscaPessoaPJ').addClass("cpf");

        $('#txtCPF_CNPJ').show();
        $('#txtBuscaPessoaPF').focus();

        $('.cpf').on('keydown.mask', function (e) {
            var target, cpf, elemento;
            target = $(this);
            cpf = target.val().replace(/\D/g, '');
            elemento = $(target);
            if (cpf.length > 11) {
                elemento.mask("9?99.999.999-99");
            } else {
                elemento.mask("9?99.999.999-99");
            }
        });
        mudaPassosPF(3);
    });


    $('#txtVigEquip').change(function () {
        $('#txtVigEquip').focus();
    });
    $('#txtDtPriVig').change(function () {
        $('#txtDtPriVig').focus();
    });


    //$("#btnContratarPF").click(function () {
    //    var data = {
    //        objOrcamento: {
    //            IdUsuario: 23,
    //            IdEmpresa: 1,
    //            IdCondicao: 11,
    //            IdSegurado: 1,
    //            DataCalculo: '01-01-2000',
    //            DataInicioObra: '01-01-2000',
    //            DataFinalObra: '01-01-2000',
    //            DataEmissaoApolice: '01-01-2000',
    //            DataInicioEquip: '01-01-2000',
    //            DataFinalEquip: '01-01-2000',
    //            ValorEquip: 1000,
    //            ValorProjeto: 1000,
    //            ValorOrcTotal: 1000,
    //            LocalRisco: "xxxxx",
    //            NumeroLocalRisco: 123,
    //            ComplementoLocalRisco: "xxxx",
    //            CepLocalRisco: "70640030",
    //            BairroRisco: "xxxxxx",
    //            CidadeRisco: "xxxxxxx",
    //            UF_Risco: "DF",
    //            ValorPremioLiquido: 500,
    //            IOF: 0.0,
    //            ValorPremioTotal: 200.00,
    //            IdFormaPagamento: 1,
    //            IdStatus: 1,
    //            CodigoBanco: "001",
    //            IdOpcaoPagamento: 1,
    //            Segurado: {
    //                IdOrcamento: 1,
    //                Nome: "Joaquim",
    //                CPF_CNPJ: "77755566666",
    //                RazaoSocial: "",
    //                NomeFantasia: "",
    //                DataAbertura: '01-01-2000',
    //                NaturezaAtividade: "advogado",
    //                RG: "1626665",
    //                OrgaoExp: "SSP-DF",
    //                DataExp: '01-01-2000',
    //                Profissao: "advogado",
    //                DataNascimento: '01-01-2000',
    //                Celular: "619999999",
    //                FoneComercial: "61322233",
    //                FoneResidencial: "61355555",
    //                Pep: "",
    //                PepProfissao: "",
    //                Deletado: false
    //            }
    //        }
    //    }

    //    $.ajax({
    //        method: "POST",
    //        url: "calculoPessoa.aspx/SalvarCalculo",
    //        data: JSON.stringify(data),
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json"
    //    }).done(function (info) {
    //        //console.log(info.d);
    //        mudaPassos(5);
    //    })
    //});


    //Abre campos para PJ
    $(document).on('click', '#lkCnpjPJ, #lkCnpjPF', function () {
        $("#btnPessoaPF, #btnPessoaPJ").text('CNPJ');

        //$('#txtBuscaPessoaPF, #txtBuscaPessoaPJ').removeClass("cpf");
        //$('#txtBuscaPessoaPF, #txtBuscaPessoaPJ').addClass("cnpj");

        $('.cnpj').on('keydown.mask', function (e) {
            var target, cnpj, elemento;
            target = $(this);
            cnpj = target.val().replace(/\D/g, '');
            elemento = $(target);
            if (cnpj.length > 14) {
                elemento.mask("9?9.999.999/9999-99");
            } else {
                elemento.mask("9?9.999.999/9999-99");
            }
        });

        mudaPassosPJ(3);
    });
    
    //Máscara CNPJ
    $('.cnpj').on('keydown.mask', function (e) {
        var target, cnpj, elemento;
        target = $(this);
        cnpj = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
        elemento = $(target);
        if (cnpj.length > 10) {
            elemento.mask("9?9.999.999/9999-99");
        } else {
            elemento.mask("9?9.999.999/9999-99");
        }
    });
    
    // Máscara Data
    $('.data').on('keydown.mask', function (e) {
        var target, nascimento, elemento;
        target = $(this);
        nascimento = target.val().replace(/\D/g, ''); //Remove tudo que não é digito
        elemento = $(target);
        if (nascimento.length > 10) {
            elemento.mask("9?9/99/9999");
        } else {
            elemento.mask("9?9/99/9999");
        }
    });

    //Adiciona classe active para botão do TIPO PESSOA FISICA/JURIDICA
    $(document).on('click', "#btnFisica", function () {

        $('#btnJuridica').removeClass("active");
        $('#btnFisica').addClass("active");

    });

    //Adiciona classe active para botão do TIPO PESSOA FISICA/JURIDICA
    $(document).on('click', "#btnJuridica", function () {
        $('#btnFisica').removeClass("active");
        $('#btnJuridica').addClass("active");
    });

    //Adiciona classe active para botão do TIPO PESSOA FISICA/JURIDICA
    $(document).on('click', "#btnPEP-Sim", function () {

        $('#btnPEP-Nao').removeClass("active");
        $('#btnPEP-Sim').addClass("active");

    });

    //Adiciona classe active para botão do TIPO PESSOA FISICA/JURIDICA
    $(document).on('click', "#btnPEP-Nao", function () {
        $('#btnPEP-Sim').removeClass("active");
        $('#btnPEP-Nao').addClass("active");
    });

    $(document).on('click', "#btnPEP-Sim, #btnPEP-Nao", function () {
        var rdSim = $("#rdPEP-Sim").is(":checked");
        var rdNao = $("#rdPEP-Nao").is(":checked");

        if (rdSim) {
            $("#dvCargoPEP").css("display", "block");
        } else if (rdNao)
            $("#dvCargoPEP").css("display", "none");
        else
            $("#dvCargoPEP").css("display", "none");

    });

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
});

function mudaPassosPF(passo) {

    var dvBuscaPessoa = document.getElementById("dvBuscaPessoa");

    var dvProponente1 = document.getElementById("dvProponentesPF1");
    var dvProponente2 = document.getElementById("dvProponentesPF2");
    var dvSeguro = document.getElementById("dvSeguroPF");
    var dvValorTotalPF = document.getElementById("dvValorTotalPF");


    var dvProponentePJ = document.getElementById("dvProponentesPJ");
    var dvProponenteSoc = document.getElementById("dvProponentePJSoc");
    var dvPessoaJuridica = document.getElementById("dvPessoaJuridica");

    $('#txtCPF_CNPJ').show();
    $('#txtBuscaPessoaPF').hide();


    if ($('#btnPessoaPF').text() == 'CPF') {
        $('#txtBuscaPessoaPF').show();
        $('#txtBuscaPessoaPF').focus();
    }


    switch (passo) {
        case "1":
        case 1:            
            dvPessoaJuridica.style.display = 'none';
            dvPessoaFisica.style.display = 'block';
            dvProponente1.style.display = 'none';
            dvProponente2.style.display = 'none';
            dvSeguro.style.display = 'block';
            dvValorTotalPF.style.display = 'none';

            dvProponentePJ.style.display = 'none';
            dvProponenteSoc.style.display = 'none';
            break;


        case "2":
        case 2:
            if (validaSeguro()) {       
                CalcularCotacao();
                
                dvPessoaJuridica.style.display = 'none';
                dvPessoaFisica.style.display = 'block';

                dvProponente1.style.display = 'none';
                dvProponente2.style.display = 'none';
                dvSeguro.style.display = 'block';
                dvValorTotalPF.style.display = 'block';

                dvProponentePJ.style.display = 'none';
                dvProponenteSoc.style.display = 'none';
            }
            break;

        case "3":
        case 3:
            $('#nav-form1').text('Cotação');
            $('#nav-form2').text('Dados do Proponente');

            dvPessoaJuridica.style.display = 'none';
            dvPessoaFisica.style.display = 'block';

            dvProponente1.style.display = 'block';
            dvProponente2.style.display = 'none';
            dvSeguro.style.display = 'none';
            dvValorTotalPF.style.display = 'none';

            dvProponentePJ.style.display = 'none';
            dvProponenteSoc.style.display = 'none';
            break;

        case "4":
        case 4:
            if (validaProponentePF()) {
                $('#nav-form1').text('Cotação');
                $('#nav-form2').text('Dados do Proponente');
                
                dvPessoaJuridica.style.display = 'none';
                dvPessoaFisica.style.display = 'block';

                dvProponente1.style.display = 'none';
                dvProponente2.style.display = 'block';
                dvSeguro.style.display = 'none';

                dvValorTotalPF.style.display = 'none';

                dvProponentePJ.style.display = 'none';
                dvProponenteSoc.style.display = 'none';
            }
            break;

        case "5":
        case 5:
            if(validaProponentePF2()){                
                SalvarCalculoPF();                        
            //    $('#nav-form1').text('Cotação');
            //    $('#nav-form2').text('Valor da Cotação');
            //    dvPessoaJuridica.style.display = 'none';
            //    dvPessoaFisica.style.display = 'block';
            //    dvProponente1.style.display = 'none';
            //    dvProponente2.style.display = 'none';
            //    dvSeguro.style.display = 'none';
            //    dvValorTotalPF.style.display = 'block';
            
            //    dvProponentePJ.style.display = 'none';
            //    dvProponenteSoc.style.display = 'none';
            //    dvSeguroPJ.style.display = 'none';
            //    dvValorTotalPJ.style.display = 'none';

            }
    }
}

function CalcularCotacao(){
    var vlrEquip = $('#txtValorEqui').val();
    vlrEquip = (vlrEquip.replace(/[^\d]+/g, '') / 100).toFixed(2);
    
    var vlrProj = $('#txtValorProj').val();
    vlrProj = (vlrProj.replace(/[^\d]+/g, '') / 100).toFixed(2);

    
    $.ajax({
        method: "POST",
        url: "calculoPessoa.aspx/CalcularCotacao",
        data: '{vlrEquip: ' + vlrEquip + ', vlrProj: ' + vlrProj + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"

    }).done(function (retorno) {        
        //console.log(retorno.d);                        
        $('#spVlrTotal, #spVlrTotalFt').text(retorno.d); 
    })
}

function SalvarCalculoPF() {    
    var DataInicioObra = $('#txtDtInicioObra').val();    
    DataInicioObra = DataInicioObra.split('/');
    DataInicioObra = DataInicioObra[1] + '-' + DataInicioObra[0] + '-' + DataInicioObra[2];

    var DataFinalObra = $('#txtDtFimObra').val();
    DataFinalObra = DataFinalObra.split('/');
    DataFinalObra = DataFinalObra[1] + '-' + DataFinalObra[0] + '-' + DataFinalObra[2];
    
    var DataInicialVigEquip = $('#txtDataInicialEquip').val();
    DataInicialVigEquip = DataInicialVigEquip.split('/');
    DataInicialVigEquip = DataInicialVigEquip[1] + '-' + DataInicialVigEquip[0] + '-' + DataInicialVigEquip[2];

    var DataFinalVigEquip = $('#txtDataFinalEquip').val();
    DataFinalVigEquip = DataFinalVigEquip.split('/');
    DataFinalVigEquip = DataFinalVigEquip[1] + '-' + DataFinalVigEquip[0] + '-' + DataFinalVigEquip[2];

    var DataExpedPF = $('#txtDtExpedPF').val();
    DataExpedPF = DataExpedPF.split('/');
    DataExpedPF = DataExpedPF[1] + '-' + DataExpedPF[0] + '-' + DataExpedPF[2];

    var DataNasc = $('#txtDtNascPF').val();
    DataNasc = DataNasc.split('/');
    DataNasc = DataNasc[1] + '-' + DataNasc[0] + '-' + DataNasc[2];
    
    var vlrEquip = $('#txtValorEqui').val();
    vlrEquip = (vlrEquip.replace(/[^\d]+/g, '') / 100).toFixed(2);
    
    var vlrProj = $('#txtValorProj').val();
    vlrProj = (vlrProj.replace(/[^\d]+/g, '') / 100).toFixed(2);

    var ValorOrcTotal = $('#spVlrTotal').text();
    ValorOrcTotal = (ValorOrcTotal.replace(/[^\d]+/g, '') / 100).toFixed(2);


    var vlrIdSegurado = $('#hddIdSegurado').val();
    if(vlrIdSegurado.length == 0)
        vlrIdSegurado = 0;


    var data = {
        objOrcamento: {
            IdSegurado: vlrIdSegurado,
            DataCalculo: '',
            DataInicioObra: DataInicioObra,                           
            DataFinalObra: DataFinalObra,                             
            DataEmissaoApolice: '',                                   
            DataInicioEquip: DataInicialVigEquip,                     
            DataFinalEquip: DataFinalVigEquip,
            ValorEquip: vlrEquip,                                            
            ValorProjeto: vlrProj,          
            ValorOrcTotal: ValorOrcTotal,
            LocalRisco: $('#txtLocalRisco').val(),
            NumeroLocalRisco: $('#txtRiscoNum').val(),
            ComplementoLocalRisco: $('#txtRiscoComp').val(),
            CepLocalRisco: $('#txtRiscoCEP').val().replace(/[^\d]+/g, ''),
            BairroRisco: $('#txtRiscoBairro').val(),
            CidadeRisco: $('#txtRiscoCidade').val(),
            UF_Risco: $('#txtRiscoUF').val(),
            ValorPremioLiquido: 0,
            IOF: 0,
            ValorPremioTotal: 0,
            IdFormaPagamento: 1,
            IdStatus: 3,                //status = 3: calculado.
            CodigoBanco: "001",
            IdOpcaoPagamento: 1,
            Segurado: {
                IdSegurado: vlrIdSegurado,
                IdOrcamento: 1,
                Nome: $('#txtNomePF').val(),
                CPF_CNPJ: $('#txtBuscaPessoaPF').val().replace(/[^\d]+/g, ''),
                RazaoSocial: '',
                NomeFantasia: '',
                DataAbertura: '',
                NaturezaAtividade: '',
                RG: $('#txtRGPF').val(),
                OrgaoExp: $('#txtOrgExpPF').val(),
                DataExp: DataExpedPF,                   
                Profissao: $('#txtProfissaoPF').val(),
                DataNascimento: DataNasc,    
                Celular: $('#txtCelularPF').val().replace(/[^\d]+/g, ''),
                FoneComercial: '',
                FoneResidencial: '',
                Pep: "",
                PepProfissao: "",
                Deletado: false,
                Enderecos: [
                    {
                        Logradouro: $('#txtLogrPF').val(),
                        Numero: $('#txtNumPF').val(),
                        Bairro: $('#txtBairroPF').val(),
                        Cidade: $('#txtCidadePF').val(),
                        IdTipoEndereco: 4,
                        IdSegurado: vlrIdSegurado,
                        UF: $('#txtUFPF').val(),
                        CEP: $('#txtCEPPF').val().replace(/[^\d]+/g, ''),
                        Deletado: false
                    }
                ],
                PatrimonioSegurado: [
                    {
                        IdPatrimonio: $('input[name=radioPatrimonio]:checked').val(),
                        IdSegurado: vlrIdSegurado,
                        IdTipoPatrimonio: 1,
                        Deletado: false
                    },
                    {
                        IdPatrimonio: $('input[name=radioRenda]:checked').val(),
                        IdSegurado: vlrIdSegurado,
                        IdTipoPatrimonio: 2,
                        Deletado: false
                    }
                ]
            }
        }
    }

    //MOCK
    //var data = {
    //    objOrcamento: {
    //        IdUsuario: 23,
    //        IdEmpresa: 1,
    //        IdCondicao: 11,
    //        IdSegurado: 1,
    //        DataCalculo: '',
    //        DataInicioObra: "12-12-2017",                      //<mm/dd/yyyy>
    //        DataFinalObra: "05-22-2018",                       //<mm/dd/yyyy>
    //        DataEmissaoApolice: '',                               //<mm/dd/yyyy>
    //        DataInicioEquip: "05-22-2018",                     //<mm/dd/yyyy>
    //        DataFinalEquip: "05-22-2019",                       //<mm/dd/yyyy>
    //        ValorEquip: 0,
    //        ValorProjeto: 0,
    //        ValorOrcTotal: 0,
    //        LocalRisco: "fasdfdsdsfdfdsf",
    //        NumeroLocalRisco: "234",
    //        ComplementoLocalRisco: "asdfasdfdsafsdf",
    //        CepLocalRisco: "54564564",
    //        BairroRisco: '"adsfsdafsdaff"',
    //        CidadeRisco: "sadfasdf",
    //        UF_Risco: 'DF',
    //        ValorPremioLiquido: 0,
    //        IOF: 0,
    //        ValorPremioTotal: 0,
    //        IdFormaPagamento: 1,
    //        IdStatus: 0,
    //        CodigoBanco: "001",
    //        IdOpcaoPagamento: 1 
    //    }
    //}

    //console.log(data);

    $.ajax({
        method: "POST",
        url: "calculoPessoa.aspx/SalvarCalculo",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"

    }).done(function (retorno) {        
        //console.log(retorno.d);                
        if(retorno.d.MsgErro)
        {
            alert(retorno.d.MsgErro);
            //Redirect
            var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Administracao/ManterUsuario.aspx';            
            window.location.replace(urlLogin);  
        }
        resultado = fomatarMoeda(retorno.d.ValorOrcTotal);    
        $('#spVlrTotal, #spVlrTotalFt').text(String(resultado));
    })

}

function SalvarCalculoPJ() {    
    var DataInicioObra = $('#txtDtInicioObra').val();    
    DataInicioObra = DataInicioObra.split('/');
    DataInicioObra = DataInicioObra[1] + '-' + DataInicioObra[0] + '-' + DataInicioObra[2];

    var DataFinalObra = $('#txtDtFimObra').val();
    DataFinalObra = DataFinalObra.split('/');
    DataFinalObra = DataFinalObra[1] + '-' + DataFinalObra[0] + '-' + DataFinalObra[2];
    
    var DataInicialVigEquip = $('#txtDataInicialEquip').val();
    DataInicialVigEquip = DataInicialVigEquip.split('/');
    DataInicialVigEquip = DataInicialVigEquip[1] + '-' + DataInicialVigEquip[0] + '-' + DataInicialVigEquip[2];

    var DataFinalVigEquip = $('#txtDataFinalEquip').val();
    DataFinalVigEquip = DataFinalVigEquip.split('/');
    DataFinalVigEquip = DataFinalVigEquip[1] + '-' + DataFinalVigEquip[0] + '-' + DataFinalVigEquip[2];

    var DataAberturaPJ = $('#txtDtAberturaPJ').val();
    DataAberturaPJ = DataAberturaPJ.split('/');
    DataAberturaPJ = DataAberturaPJ[1] + '-' + DataAberturaPJ[0] + '-' + DataAberturaPJ[2];
    
    var vlrEquip = $('#txtValorEqui').val();
    vlrEquip = vlrEquip.replace(/[^\d]+/g, '');
    
    var vlrProj = $('#txtValorProj').val();
    vlrProj = vlrProj.replace(/[^\d]+/g, '');

    var ValorOrcTotal = $('#spVlrTotal').text();
    ValorOrcTotal = (ValorOrcTotal.replace(/[^\d]+/g, '') / 100).toFixed(2);


    var vlrIdSegurado = $('#hddIdSegurado').val();
    if(vlrIdSegurado.length == 0)
        vlrIdSegurado = 0;


    var data = {
        objOrcamento: {
            IdSegurado: vlrIdSegurado,
            DataCalculo: '',
            DataInicioObra: DataInicioObra,                           
            DataFinalObra: DataFinalObra,                                         
            DataInicioEquip: DataInicialVigEquip,
            DataFinalEquip: DataFinalVigEquip,
            ValorOrcTotal: ValorOrcTotal,
            ValorEquip: vlrEquip,                                            
            ValorProjeto: vlrProj,                        
            LocalRisco: $('#txtLocalRisco').val(),
            NumeroLocalRisco: $('#txtRiscoNum').val(),
            ComplementoLocalRisco: $('#txtRiscoComp').val(),
            CepLocalRisco: $('#txtRiscoCEP').val().replace(/[^\d]+/g, ''),
            BairroRisco: $('#txtRiscoBairro').val(),
            CidadeRisco: $('#txtRiscoCidade').val(),
            UF_Risco: $('#txtRiscoUF').val(),
            ValorPremioLiquido: 0,
            IOF: 0,
            ValorPremioTotal: 0,
            IdFormaPagamento: 1,
            IdStatus: 3,                //status = 3: calculado.
            CodigoBanco: "001",
            IdOpcaoPagamento: 1,
            Segurado: {
                IdSegurado: vlrIdSegurado,                
                Nome: $('#txtNomePJ').val(),
                CPF_CNPJ: $('#txtBuscaPessoaPJ').val().replace(/[^\d]+/g, ''),
                RazaoSocial: $('#txtRazaoSocialPJ').val(),                    //TODO: inserir campo do form.
                NomeFantasia: $('#txtNomeFantasiaPJ').val(),                   //TODO: inserir campo do form.
                DataAbertura: DataAberturaPJ,                   //TODO: inserir campo do form.
                NaturezaAtividade: $('#txtNtAtividade').val(),
                RG: $('#txtRGPJ').val(),
                OrgaoExp: $('#txtOrgExpPJ').val(),                            
                Profissao: $('#txtProfissaoPJ').val(),
                DataNascimento: '',                    
                FoneComercial: '',
                FoneResidencial: '',
                Pep: "",
                PepProfissao: "",
                Deletado: false,
                Enderecos: [
                    {
                        Logradouro: $('#txtLogrPJ').val(),
                        Numero: $('#txtNumPJ').val(),
                        Bairro: $('#txtBairroPJ').val(),
                        Cidade: $('#txtCidadePJ').val(),
                        IdTipoEndereco: 4,
                        IdSegurado: vlrIdSegurado,
                        UF: $('#txtUFPJ').val(),
                        CEP: $('#txtCEPPJ').val().replace(/[^\d]+/g, ''),
                        Deletado: false
                    }
                ],
                PatrimonioSegurado: [
                    {
                        IdPatrimonio: $('input[name=radioFaturamento]:checked').val(),
                        IdSegurado: vlrIdSegurado,
                        IdTipoPatrimonio: 3,
                        Deletado: false
                    }
                ]   
            }
        }
    }

    //MOCK
    //var data = {
    //    objOrcamento: {
    //        IdUsuario: 23,
    //        IdEmpresa: 1,
    //        IdCondicao: 11,
    //        IdSegurado: 1,
    //        DataCalculo: '',
    //        DataInicioObra: "12-12-2017",                      //<mm/dd/yyyy>
    //        DataFinalObra: "05-22-2018",                       //<mm/dd/yyyy>
    //        DataEmissaoApolice: '',                               //<mm/dd/yyyy>
    //        DataInicioEquip: "05-22-2018",                     //<mm/dd/yyyy>
    //        DataFinalEquip: "05-22-2019",                       //<mm/dd/yyyy>
    //        ValorEquip: 0,
    //        ValorProjeto: 0,
    //        ValorOrcTotal: 0,
    //        LocalRisco: "fasdfdsdsfdfdsf",
    //        NumeroLocalRisco: "234",
    //        ComplementoLocalRisco: "asdfasdfdsafsdf",
    //        CepLocalRisco: "70640030",
    //        BairroRisco: "Cruzeiro Velho",
    //        CidadeRisco: "Brasília",
    //        UF_Risco: 'DF',
    //        ValorPremioLiquido: 0,
    //        IOF: 0,
    //        ValorPremioTotal: 0,
    //        IdFormaPagamento: 1,
    //        IdStatus: 0,
    //        CodigoBanco: "001",
    //        IdOpcaoPagamento: 1,
    //        Segurado: {
    //            IdOrcamento: 1,
    //            Nome: "Mario Santos",
    //            CPF_CNPJ: "08601256000166",
    //            RazaoSocial: '',
    //            NomeFantasia: '',
    //            DataAbertura: "12-30-2000",
    //            NaturezaAtividade: '',
    //            RG: "1626665",
    //            OrgaoExp: "sssp-df",
    //            DataExp: "11-23-2017",                      //<mm/dd/yyyy>
    //            Profissao: '',
    //            DataNascimento: "",                     //<mm/dd/yyyy>
    //            Celular: "61999559955",
    //            FoneComercial: '',
    //            FoneResidencial: '',
    //            Pep: "",
    //            PepProfissao: "",
    //            Deletado: false,
    //            Enderecos: [
    //                {
    //                    Logradouro: "Taguatinga",
    //                    Numero: "123",
    //                    Bairro: "Aguas claras",
    //                    Cidade: "Brasilia",
    //                    UF: "DF",
    //                    CEP: "70640030",
    //                    Deletado: false
    //                }
    //            ],
    //            PatrimonioSegurado: [
    //            {
    //                IdPatrimonio: "2",
    //                IdSegurado: "1",
    //                IdTipoPatrimonio: 1,
    //                Deletado: false
    //            },
    //            {
    //                IdPatrimonio: "8",
    //                IdSegurado: "1",
    //                IdTipoPatrimonio: 2,
    //                Deletado: false
    //            }
    //            ]
    //        }
    //    }
    //}
    //var soc1 = {Nome: 'Mario', GrauRelacionamento: 'adm'};
    //var soc2 = {Nome: 'Mario', GrauRelacionamento: 'adm'};
    //var soc3 = {Nome: 'Mario', GrauRelacionamento: 'adm'};
    //objSocietariosList.push(soc1);
    //objSocietariosList.push(soc2);
    //objSocietariosList.push(soc3);

    //Carregando os objetos societarios no json principal. Se atribuir o objSocietariosList direto, o bind não é realizado.    
    data.objOrcamento.Segurado.Societarios = new Array();

    objSocietariosList.forEach(function(item, index){        
        //var Soc = { Nome: item.Nome, GrauRelacionamento: item.GrauRelacionamento, Modalidade: item.Modalidade, Pep: item.Pep, PepProfissao: item.PepProfissao };    
        var vlrPep = item.Pep == 'não' ? false : true;
        

        var Soc = { Nome: item.Nome, GrauRelacionamento: item.GrauRelacionamento, Modalidade: item.Modalidade, Pep: vlrPep, PepProfissao: item.PepProfissao, Deletado: item.Deletado };    
        data.objOrcamento.Segurado.Societarios.push(Soc); 
    });  

    //console.log(data);

    $.ajax({
        method: "POST",
        url: "calculoPessoa.aspx/SalvarCalculo",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"

    }).done(function (retorno) {        
        //console.log(retorno.d);                
        if(retorno.d.MsgErro)
        {
            alert(retorno.d.MsgErro);
            //Redirect
            var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Administracao/ManterUsuario.aspx';            
            window.location.replace(urlLogin);  
        }
        resultado = fomatarMoeda(retorno.d.ValorOrcTotal);    
        $('#spVlrTotal, #spVlrTotalFt').text(String(resultado));
    })

}


function mudaPassosPJ(passo) {


    var dvProponente1 = document.getElementById("dvProponentesPF1");
    var dvProponente2 = document.getElementById("dvProponentesPF2");
    var dvSeguro = document.getElementById("dvSeguroPF");
    var dvValorTotalPF = document.getElementById("dvValorTotalPF");

    var dvProponentePJ = document.getElementById("dvProponentesPJ");
    var dvProponenteSoc = document.getElementById("dvProponentePJSoc");
    var dvPessoaFisica = document.getElementById("dvPessoaFisica");


    $('#txtCPF_CNPJ').show();
    $('#txtBuscaPessoaPJ').focus();

    switch (passo) {

        case "1":
        case 1:
            $('#txtCPF_CNPJ').show();
            $('#txtBuscaPessoaPF').focus();

            $('#nav-form1').text('Cotação');
            $('#nav-form2').text('Dados do Seguro');
            dvPessoaJuridica.style.display = 'block';
            dvPessoaFisica.style.display = 'none';

            dvProponente1.style.display = 'none';
            dvProponente2.style.display = 'none';
            dvSeguro.style.display = 'block';
            dvValorTotalPF.style.display = 'none';

            dvProponentePJ.style.display = 'none';
            dvProponenteSoc.style.display = 'none';
            break;

        case "2":
        case 2:
            if (validaSeguro()) {
                $('#nav-form1').text('Cotação');
                $('#nav-form2').text('Dados do Seguro');

                dvPessoaJuridica.style.display = 'block';
                dvPessoaFisica.style.display = 'block';

                dvProponente1.style.display = 'none';
                dvProponente2.style.display = 'none';
                dvSeguro.style.display = 'block';
                dvValorTotalPF.style.display = 'block';

                dvProponentePJ.style.display = 'none';
                dvProponenteSoc.style.display = 'none';
            }
            break;

        case "3":
        case 3:
            $('#nav-form1').text('Cotação');
            $('#nav-form2').text('Dados do Proponente');

            dvPessoaJuridica.style.display = 'block';
            dvPessoaFisica.style.display = 'none';

            dvProponente1.style.display = 'none';
            dvProponente2.style.display = 'none';
            dvSeguro.style.display = 'none';
            dvValorTotalPF.style.display = 'none';

            dvProponentePJ.style.display = 'block';
            dvProponenteSoc.style.display = 'none';
            break;

        case "4":
        case 4:       
            if(validaProponentePJ()){
                $('#nav-form1').text('Cotação');
                $('#nav-form2').text('Dados do Proponente');
                dvPessoaJuridica.style.display = 'block';
                dvPessoaFisica.style.display = 'none';

                dvProponente1.style.display = 'none';
                dvProponente2.style.display = 'none';
                dvSeguro.style.display = 'none';
                dvValorTotalPF.style.display = 'none';

                dvProponentePJ.style.display = 'none';
                dvProponenteSoc.style.display = 'block';
            }
            break;

        case "5":
        case 5:
            SalvarCalculoPJ();
            //if (validaSeguroPJ()) {
            //    $('#nav-form1').text('Cotação');
            //    $('#nav-form2').text('Painel do Valor Total');
            //    dvPessoaJuridica.style.display = 'block';
            //    dvPessoaFisica.style.display = 'none';                
            //    dvProponente1.style.display = 'none';
            //    dvProponente2.style.display = 'none';
            //    dvSeguro.style.display = 'none';
            //    dvValorTotalPF.style.display = 'none';

            //    dvProponentePJ.style.display = 'none';
            //    dvProponenteSoc.style.display = 'none';
            //    dvSeguroPJ.style.display = 'none';
            //    dvValorTotalPJ.style.display = 'none';
            //}
            break;

        case "6":
        case 6:
            //if(validaSeguroPJ()){
            //    SalvarCalculoPJ();
            //    $('#nav-form1').text('Cotação');
            //    $('#nav-form2').text('Valor da Cotação');
            //    dvPessoaJuridica.style.display = 'block';
            //    dvPessoaFisica.style.display = 'none';            
            //    dvProponente1.style.display = 'none';
            //    dvProponente2.style.display = 'none';
            //    dvSeguro.style.display = 'none';
            //    dvValorTotalPF.style.display = 'none';
            //    dvProponentePJ.style.display = 'none';
            //    dvProponenteSoc.style.display = 'none';
            //    dvSeguroPJ.style.display = 'none';
            //    dvValorTotalPJ.style.display = 'block';
            //}
    }
}

function validaData(idCampoData) {
    var txtData = $('#' + idCampoData.id);
    var date = txtData.val();
    var dtAtual = new Date();
    var dia = dtAtual.getDate();
    var mes = dtAtual.getMonth() + 1;
    var ano = dtAtual.getFullYear();
    var str_ano = dia + '/' + mes + '/' + ano;
    var arrDT = new Array;
    var arrDT2 = new Array;
    arrDT = date.split("/");
    arrDT2 = str_ano.split("/");
    if (txtData.val() == "" || txtData.val().length < 10) {
        $('#' + idCampoData.id).css("border-color", "red");
        $('#SP' + idCampoData.id).show();
        $('#SP' + idCampoData.id).text("Insira uma data de válida!");
        return false;
    } else if (txtData.val() != "") {
        if (arrDT[0] > 31) {
            $('#' + idCampoData.id).css("border-color", "red");
            $('#SP' + idCampoData.id).show();
            $('#SP' + idCampoData.id).text("Data inválida, por favor verifique!");
            return false;
        }

        if (arrDT[1] > 12) {
            $('#' + idCampoData.id).css("border-color", "red");
            $('#SP' + idCampoData.id).show();
            $('#SP' + idCampoData.id).text("Data inválida, por favor verifique!");
            return false;
        }

        if (idCampoData.id == "txtDtExpedPF" || idCampoData.id == "txtDtNascPF" || idCampoData.id == "txtDtAberturaPJ") {
            if (arrDT[2] > ano) {
                $('#' + idCampoData.id).css("border-color", "red");
                $('#SP' + idCampoData.id).show();
                $('#SP' + idCampoData.id).text("Data inválida, por favor verifique!");
                return false;
            }

            //Comparação se o mês digitado é maior que o mês atual
            if (arrDT[1] > arrDT2[1]) {
                // Comparação se o ano digitado é maior ou igual ao mês atual.
                if (arrDT[2] >= ano) {
                    $('#' + idCampoData.id).css("border-color", "red");
                    $('#SP' + idCampoData.id).show();
                    $('#SP' + idCampoData.id).text("Data não pode ser maior ou a mesma de hoje!");
                    return false;
                }
            }
            //Comparação se o ano digitado é maior ou o mesmo que o ano atual.
            if (arrDT[2] == ano) {
                //Comparação se o mês digitado é maior ou o mesmo que o mês atual.
                if (arrDT[1] >= arrDT2[1]) {
                    //Comparação se o dia digitado é o maior ou o mesmo que o dia atual.
                    if (parseInt(arrDT[0]) >= parseInt(arrDT2[0])) {
                        $('#' + idCampoData.id).css("border-color", "red");
                        $('#SP' + idCampoData.id).show();
                        $('#SP' + idCampoData.id).text("Data não pode ser maior ou a mesma de hoje!");
                        return false;
                    }
                }
            }
        }
    }
    else {
        $('#' + idCampoData.id).css("border-color", "");
        $('#SP' + idCampoData.id).hide();
        $('#SP' + idCampoData.id).text("");
        return true;
    }
}


//Cancela reload da tela quando clicado no enter
function kH(e) {
    var pK = e ? e.which : window.event.keyCode;
    return pK != 13;
}

document.onkeypress = kH;
if (document.layers)
    document.captureEvents(Event.KEYPRESS);


////////////////////////////////////////////////////////////////////////
///////////////       Validações PESSOA FÍSICA      ///////////////////
///////////////////////////////////////////////////////////////////////

function validaProponentePF() {
    var validaPassou = true;

    var cpf = document.getElementById("txtBuscaPessoaPF");
    if (cpf.value == "" || cpf.value.length < 2) {
        $('#txtBuscaPessoaPF').css("border-color", "red");
        $('#SPtxtBuscaPessoaPF').text('Preencha esse campo com um CPF válido!');
        $('#SPtxtBuscaPessoaPF').show();
        validaPassou = false;
    } else if (verificarCPF(cpf.value.replace(/[^\d]+/g, '')) == false) {
        $('#txtBuscaPessoaPF').css("border-color", "red");
        $('#SPtxtBuscaPessoaPF').text('CPF digitado inválido, por favor verifique!');
        $('#SPtxtBuscaPessoaPF').show();
        validaPassou = false;
    } else {
        $('#txtBuscaPessoaPF').css("border-color", "");
        $('#SPtxtBuscaPessoaPF').text('');
        $('#SPtxtBuscaPessoaPF').hide();
    }

    var nome = document.getElementById("txtNomePF");
    if (nome.value == "" || nome.value.length < 4) {
        $('#txtNomePF').css("border-color", "red");
        $('#SPtxtNomePF').show();
        validaPassou = false;
    } else {
        $('#txtNomePF').css("border-color", "");
        $('#SPtxtNomePF').hide();
    }

    var logr = document.getElementById("txtLogrPF");
    if (logr.value == "" || logr.value.length < 2) {
        $('#txtLogrPF').css("border-color", "red");
        $('#SPtxtLogrPF').show();
        validaPassou = false;
    } else {
        $('#txtLogrPF').css("border-color", "");
        $('#SPtxtLogrPF').hide();
    }

    var num = document.getElementById("txtNumPF");
    if (num.value == "" || num.value.length < 1) {
        $('#txtNumPF').css('border-color', 'red');
        $('#SPtxtNumPF').show();
        validaPassou = false;
    } else {
        $('#txtNumPF').css('border-color', '');
        $('#SPtxtNumPF').hide();
    }

    var uf = document.getElementById("txtUFPF");
    if (uf.value == "" || uf.value.length < 2) {
        $('#txtUFPF').css('border-color', 'red');
        $('#SPtxtUFPF').show();
        validaPassou = false;
    } else {
        $('#txtUFPF').css('border-color', '');
        $('#SPtxtUFPF').hide();
    }

    var bairro = document.getElementById("txtBairroPF");
    if (bairro.value == "" || bairro.value.length < 2) {
        $('#txtBairroPF').css('border-color', 'red');
        $('#SPtxtBairroPF').show();
        validaPassou = false;
    } else {
        $('#txtBairroPF').css('border-color', '');
        $('#SPtxtBairroPF').hide();
    }

    var cidade = document.getElementById("txtCidadePF");
    if (cidade.value == "" || cidade.value.length < 2) {
        $('#txtCidadePF').css('border-color', 'red');
        $('#SPtxtCidadePF').show();
        validaPassou = false;
    } else {
        $('#txtCidadePF').css('border-color', '');
        $('#SPtxtCidadePF').hide();
    }

    var cep = document.getElementById("txtCEPPF").value.replace(/[^\d]+/g, '');
    if (cep == "" || cep.length < 2) {
        $('#txtCEPPF').css('border-color', 'red');
        $('#SPtxtCEPPF').show();
        validaPassou = false;
    } else if (cep.length < 8) {
        $('#txtCEPPF').css('border-color', 'red');
        $('#SPtxtCEPPF').text('CEP inválido, por favor verifique para prosseguir!');
        $('#SPtxtCEPPF').show();
        validaPassou = false;
    } else {
        $('#txtCEPPF').css('border-color', '');
        $('#SPtxtCEPPF').hide();
    }

    //var email = document.getElementById("txtEmailPF");
    //if (validaEmail(email) == false) {
    //    $('#txtEmailPF').css('border-color', 'red');
    //    $('#SPtxtEmailPF').show();
    //    validaPassou = false;
    //} else {
    //    $('#txtEmailPF').css('border-color', '');
    //    $('#SPtxtEmailPF').hide();
    //}

    var celular = document.getElementById("txtCelularPF").value.replace(/[^\d]+/g, '');
    if (celular == "" || celular.length < 1) {
        $('#txtCelularPF').css('border-color', 'red');
        $('#SPtxtCelularPF').show();
        validaPassou = false;
    } else if (celular.length < 11) {
        $('#txtCelularPF').css('border-color', 'red');
        $('#SPtxtCelularPF').text('Número de celular inválido, por favor verifique!');
        $('#SPtxtCelularPF').show();
        validaPassou = false;
    } else {
        $('#txtCelularPF').css('border-color', '');
        $('#SPtxtCelularPF').hide();
    }

    return validaPassou;
}

function validaProponentePF2() {
    var validaPassou = true;

    var rg = document.getElementById("txtRGPF").value.replace(/[^\d]+/g, '');
    if (rg == "" || rg.length < 4) {
        $("#txtRGPF").css('border-color', 'red');
        $('#SPtxtRGPF').show();
        validaPassou = false;
    } else {
        $("#txtRGPF").css('border-color', '');
        $('#SPtxtRGPF').hide();
    }

    var orgexp = document.getElementById("txtOrgExpPF");
    if (orgexp.value == "" || orgexp.value.length < 2) {
        $('#txtOrgExpPF').css('border-color', 'red');
        $('#SPtxtOrgExpPF').show();
        validaPassou = false;
    } else {
        $("#txtOrgExpPF").css('border-color', '');
        $('#StxtOrgExpPF').hide();
    }

    var dtexped = document.getElementById("txtDtExpedPF");
    if (validaData(dtexped) == false) {
        $('#txtDtExpedPF').css('border-color', 'red');
        $('#SPtxtDtExpedPF').show();
        validaPassou = false;
    } else {
        $('#txtDtExpedPF').css('border-color', '');
        $('#SPtxtDtExpedPF').hide();
    }

    var dtnasc = document.getElementById("txtDtNascPF");
    if (validaData(dtnasc) == false) {
        $('#txtDtNascPF').css('border-color', 'red');
        $('#SPtxtDtNascPF').show();
        validaPassou = false;
    } else {
        $('#txtDtNascPF').css('border-color', '');
        $('#SPtxtDtNascPF').hide();
    }

    var profissao = document.getElementById("txtProfissaoPF");
    if (profissao.value == "" || profissao.value.length < 2) {
        $('#txtProfissaoPF').css('border-color', 'red');
        $('#SPtxtProfissaoPF').show();
        validaPassou = false;
    } else {
        $('#txtProfissaoPF').css('border-color', '');
        $('#SPtxtProfissaoPF').hide();
    }

    var price1 = document.getElementById("rdPrice1").checked;
    var price2 = document.getElementById("rdPrice2").checked;
    var price3 = document.getElementById("rdPrice3").checked;
    var price4 = document.getElementById("rdPrice4").checked;
    var price5 = document.getElementById("rdPrice5").checked;
    var price6 = document.getElementById("rdPrice6").checked;

    if (price1 != true && price2 != true &&
        price3 != true && price4 != true &&
        price5 != true && price6 != true) {
        $('#SPrdPatrimonio').show();
        validaPassou = false;
    } else {
        $('#SPrdPatrimonio').hide();
    }

    var renda1 = document.getElementById("rdRenda1").checked;
    var renda2 = document.getElementById("rdRenda2").checked;
    var renda3 = document.getElementById("rdRenda3").checked;
    var renda4 = document.getElementById("rdRenda4").checked;
    var renda5 = document.getElementById("rdRenda5").checked;
    var renda6 = document.getElementById("rdRenda6").checked;

    if (renda1 != true && renda2 != true &&
        renda3 != true && renda4 != true &&
        renda5 != true && renda6 != true) {
        $('#SPrdRendaMensal').show();
        validaPassou = false;
    } else {
        $('#SPrdRendaMensal').hide();
    }

    return validaPassou;
}

function populaCamposDataVigenciaPF(){
    var dataFimObra = $('#txtDtFimObra').val();   
    $('#txtDataInicialEquip').val(dataFimObra);
    var dataAux = dataFimObra.split('/');
    //regra: o campo data vigência principal tem um ano a mais que o campo data vigência equipamento.
    var anoAux = parseInt(dataAux[2]) + 1;
    dataAux = dataAux[0] + '/' + dataAux[1] + '/' + anoAux;
    $('#txtDataFinalEquip').val(dataAux);
    $('#txtDataFinalEquip').focus();    
}


function populaCamposDataVigenciaPJ() {
    var dataFimObra = $('#txtDtPrazoFimObraPJ').val();
    $('#txtVigEquipPJ').val(dataFimObra);
    var dataAux = dataFimObra.split('/');
    //regra: o campo data vigência principal tem um ano a mais que o campo data vigência equipamento.
    var anoAux = parseInt(dataAux[2]) + 1;
    dataAux = dataAux[0] + '/' + dataAux[1] + '/' + anoAux;
    $('#txtDtPriVigPJ').val(dataAux);
    $('#txtDtPriVigPJ').focus();
}


function validaSeguro() {
    validaPassou = true;

    var obra = document.getElementById("txtDtInicioObra");
    if (validaData(obra) == false) {
        $("#txtDtInicioObra").css('border-color', 'red');
        $('#SPtxtDtInicioObra').show();
        validaPassou = false;
    } else {
        $("#txtDtInicioObra").css('border-color', '');
        $('#SPtxtDtInicioObra').hide();
    }

    obra = document.getElementById("txtDtFimObra");
    if (validaData(obra) == false) {
        $("#txtDtFimObra").css('border-color', 'red');
        $('#SPtxtDtFimObra').show();
        validaPassou = false;
    } else {
        $("#txtDtFimObra").css('border-color', '');
        $('#SPtxtDtFimObra').hide();
    }

    valorproj = document.getElementById("txtValorProj");
    if(valorproj.value == "" || valorproj.value.length < 4){
        $("#txtValorProj").css('border-color', 'red');
        $('#SPtxtValorProj').show();
        validaPassou = false;
    } else {
        $("#txtValorProj").css('border-color', '');
        $('#SPtxtValorProj').hide();
    }

    valorequip = document.getElementById("txtValorEqui");
    if(valorproj.value == "" || valorproj.value.length < 4){
        $("#txtValorEqui").css('border-color', 'red');
        $('#SPtxtValorEqui').show();
        validaPassou = false;
    } else {
        $("#txtValorEqui").css('border-color', '');
        $('#SPtxtValorEqui').hide();
    }
   
   
    var equip = document.getElementById("txtDataInicialEquip");
    if (validaData(equip) == false) {
        $("#txtDataInicialEquip").css('border-color', 'red');
        $('#SPtxtDataInicialEquip').show();
        validaPassou = false;
    } else {
        $("#txtDataInicialEquip").css('border-color', '');
        $('#SPtxtDataInicialEquip').hide();
    }

    var vigInicialEquip = document.getElementById("txtDataFinalEquip");
    if (validaData(vigInicialEquip) == false) {
        $("#txtDataFinalEquip").css('border-color', 'red');
        $('#SPtxtDataFinalEquip').show();
        validaPassou = false;
    } else {
        $("#txtDataFinalEquip").css('border-color', '');
        $('#SPtxtDataFinalEquip').hide();
    }

    var vigFinalEquip = document.getElementById("txtDataFinalEquip");
    if (validaData(vigFinalEquip) == false) {
        $("#txtDataFinalEquip").css('border-color', 'red');
        $('#SPtxtDataFinalEquip').show();
        validaPassou = false;
    } else {
        $("#txtDataFinalEquip").css('border-color', '');
        $('#SPtxtDataFinalEquip').hide();
    }

    var logr = document.getElementById("txtLocalRisco");
    if (logr.value == "" || logr.value.length < 2) {
        $('#txtLocalRisco').css('border-color', 'red');
        $('#SPtxtLocalRisco').show();
        validaPassou = false;
    } else {
        $("#txtLocalRisco").css('border-color', '');
        $('#SPtxtLocalRisco').hide();
    }

    var logrNum = document.getElementById("txtRiscoNum");
    if (logrNum.value == "") {
        $('#txtRiscoNum').css('border-color', 'red');
        $('#SPtxtRiscoNum').show();
        validaPassou = false;
    } else {
        $("#txtRiscoNum").css('border-color', '');
        $('#SPtxtRiscoNum').hide();
    }

   

    //var compl = document.getElementById("txtLocalRisco");
    //if (compl.value == "" || compl.value.length < 2) {
    //    $('#txtRiscoComp').css('border-color', 'red');
    //    $('#SPtxtRiscoComp').show();
    //    validaPassou = false;
    //} else {
    //    $("#txtRiscoComp").css('border-color', '');
    //    $('#SPtxtRiscoComp').hide();
    //}

    var cep = document.getElementById("txtRiscoCEP").value.replace(/[^\d]+/g, '');
    if (cep == "" || cep.length < 2) {
        $('#txtRiscoCEP').css('border-color', 'red');
        $('#SPtxtRiscoCEP').show();
        validaPassou = false;
    } else if (cep.length < 8) {
        $('#txtRiscoCEP').css('border-color', 'red');
        $('#SPtxtRiscoCEP').text('CEP inválido, por favor verifique para prosseguir!');
        $('#SPtxtRiscoCEP').show();
        validaPassou = false;
    } else {
        $('#txtRiscoCEP').css('border-color', '');
        $('#SPtxtRiscoCEP').hide();
    }

    var bairro = document.getElementById("txtRiscoBairro");
    if (bairro.value == "" || bairro.value.length < 2) {
        $('#txtRiscoBairro').css('border-color', 'red');
        $('#SPtxtRiscoBairro').show();
        validaPassou = false;
    } else {
        $("#txtRiscoBairro").css('border-color', '');
        $('#SPtxtRiscoBairro').hide();
    }

    var uf = document.getElementById("txtRiscoUF");
    if (uf.value == "" || uf.value.length < 2) {
        $('#txtRiscoUF').css('border-color', 'red');
        $('#SPtxtRiscoUF').show();
        validaPassou = false;
    } else {
        $("#txtRiscoUF").css('border-color', '');
        $('#SPtxtRiscoUF').hide();
    }

    var cidade = document.getElementById("txtRiscoCidade");
    if (cidade.value == "" || cidade.value.length < 2) {
        $('#txtRiscoCidade').css('border-color', 'red');
        $('#SPtxtRiscoCidade').show();
        validaPassou = false;
    } else {
        $("#txtRiscoCidade").css('border-color', '');
        $('#SPtxtRiscoCidade').hide();
    }

    return validaPassou;
}

////////////////////////////////////////////////////////////////////////
///////////////       Validações PESSOA JURÍDICA     //////////////////
///////////////////////////////////////////////////////////////////////

function validaProponentePJ() {
    var validaPassou = true;

    var cnpj = document.getElementById("txtBuscaPessoaPJ");
    if (cnpj.value == "" || cnpj.value.length < 2) {
        $('#txtBuscaPessoaPJ').css("border-color", "red");
        $('#SPtxtBuscaPessoaPJ').text('Preencha esse campo com um CNPJ válido!');
        $('#SPtxtBuscaPessoaPJ').show();
        validaPassou = false;
    } else if (validarCNPJ(cnpj) == false) {
        $('#txtBuscaPessoaPJ').css("border-color", "red");
        $('#SPtxtBuscaPessoaPJ').text('CNPJ digitado inválido, por favor verifique!');
        $('#SPtxtBuscaPessoaPJ').show();
        validaPassou = false;
    } else {
        $('#txtBuscaPessoaPJ').css("border-color", "");
        $('#SPtxtBuscaPessoaPJ').text('');
        $('#SPtxtBuscaPessoaPJ').hide();
    }

    var nome = document.getElementById("txtNomeFantasiaPJ");
    if (nome.value == "" || nome.value.length < 4) {
        $('#txtNomeFantasiaPJ').css("border-color", "red");
        $('#SPtxtNomeFantasiaPJ').show();
        validaPassou = false;
    } else {
        $('#txtNomeFantasiaPJ').css("border-color", "");
        $('#SPtxtNomeFantasiaPJ').hide();
    }

    var razaosoc = document.getElementById("txtRazaoSocialPJ");
    if (razaosoc.value == "" || razaosoc.value.length < 4) {
        $('#txtRazaoSocialPJ').css("border-color", "red");
        $('#SPtxtRazaoSocialPJ').show();
        validaPassou = false;
    } else {
        $('#txtRazaoSocialPJ').css("border-color", "");
        $('#SPtxtRazaoSocialPJ').hide();
    }



    var logr = document.getElementById("txtLogrPJ");
    if (logr.value == "" || logr.value.length < 2) {
        $('#txtLogrPJ').css("border-color", "red");
        $('#SPtxtLogrPJ').show();
        validaPassou = false;
    } else {
        $('#txtLogrPJ').css("border-color", "");
        $('#SPtxtLogrPJ').hide();
    }

    var num = document.getElementById("txtNumPJ");
    if (num.value == "" || num.value.length < 1) {
        $('#txtNumPJ').css('border-color', 'red');
        $('#SPtxtNumPJ').show();
        validaPassou = false;
    } else {
        $('#txtNumPJ').css('border-color', '');
        $('#SPtxtNumPJ').hide();
    }

    var uf = document.getElementById("txtUFPJ");
    if (uf.value == "" || uf.value.length < 2) {
        $('#txtUFPJ').css('border-color', 'red');
        $('#SPtxtUFPJ').show();
        validaPassou = false;
    } else {
        $('#txtUFPJ').css('border-color', '');
        $('#SPtxtUFPJ').hide();
    }

    var bairro = document.getElementById("txtBairroPJ");
    if (bairro.value == "" || bairro.value.length < 2) {
        $('#txtBairroPJ').css('border-color', 'red');
        $('#SPtxtBairroPJ').show();
        validaPassou = false;
    } else {
        $('#txtBairroPJ').css('border-color', '');
        $('#SPtxtBairroPJ').hide();
    }

    var cidade = document.getElementById("txtCidadePJ");
    if (cidade.value == "" || cidade.value.length < 2) {
        $('#txtCidadePJ').css('border-color', 'red');
        $('#SPtxtCidadePJ').show();
        validaPassou = false;
    } else {
        $('#txtCidadePJ').css('border-color', '');
        $('#SPtxtCidadePJ').hide();
    }

    var cep = document.getElementById("txtCEPPJ").value.replace(/[^\d]+/g, '');
    if (cep == "" || cep.length < 2) {
        $('#txtCEPPJ').css('border-color', 'red');
        $('#SPtxtCEPPJ').show();
        validaPassou = false;
    } else if (cep.length < 8) {
        $('#txtCEPPJ').css('border-color', 'red');
        $('#SPtxtCEPPJ').text('CEP inválido, por favor verifique para prosseguir!');
        $('#SPtxtCEPPJ').show();
        validaPassou = false;
    } else {
        $('#txtCEPPJ').css('border-color', '');
        $('#SPtxtCEPPJ').hide();
    }

    //var email = document.getElementById("txtEmailPJ");
    //if (validaEmail(email) == false) {
    //    $('#txtEmailPJ').css('border-color', 'red');
    //    $('#SPtxtEmailPJ').show();
    //    validaPassou = false;
    //} else {
    //    $('#txtEmailPJ').css('border-color', '');
    //    $('#SPtxtEmailPJ').hide();
    //}

    //var celular = document.getElementById("txtCelularPJ").value.replace(/[^\d]+/g, '');
    //if (celular == "" || celular.length < 1) {
    //    $('#txtCelularPJ').css('border-color', 'red');
    //    $('#SPtxtCelularPJ').show();
    //    validaPassou = false;
    //} else if (celular.length < 11) {
    //    $('#txtCelularPJ').css('border-color', 'red');
    //    $('#SPtxtCelularPJ').text('Número de celular inválido, por favor verifique!');
    //    $('#SPtxtCelularPJ').show();
    //    validaPassou = false;
    //} else {
    //    $('#txtCelularPJ').css('border-color', '');
    //    $('#SPtxtCelularPJ').hide();
    //}

    var dtAbertura = document.getElementById("txtDtAberturaPJ");
    if (validaData(dtAbertura) == false) {
        $("#txtDtAberturaPJ").css('border-color', 'red');
        $('#SPtxtDtAberturaPJ').show();
        validaPassou = false;
    } else {
        $("#txtDtAberturaPJ").css('border-color', '');
        $('#SPtxtDtAberturaPJ').hide();
    }

    var atividade = document.getElementById("txtNtAtividade");
    if (atividade.value == "" || atividade.value.length < 2) {
        $('#txtNtAtividade').css('border-color', 'red');
        $('#SPtxtNtAtividade').show();
        validaPassou = false;
    } else {
        $('#txtNtAtividade').css('border-color', '');
        $('#SPtxtNtAtividade').hide();
    }

    var fatu1 = document.getElementById("rdFatur1").checked;
    var fatu2 = document.getElementById("rdFatur2").checked;
    var fatu3 = document.getElementById("rdFatur3").checked;
    var fatu4 = document.getElementById("rdFatur4").checked;
    var fatu5 = document.getElementById("rdFatur5").checked;
    var fatu6 = document.getElementById("rdFatur6").checked;

    if (fatu1 != true && fatu2 != true &&
        fatu3 != true && fatu4 != true &&
        fatu5 != true && fatu6 != true) {
        $('#SPrdFaturamento').show();
        validaPassou = false;
    } else {
        $('#SPrdFaturamento').hide();
    }

    return validaPassou;
}

function validaCamposSocietario() {
    validaPassou = true;

    var nome = document.getElementById("txtNomeSoc");
    if (nome.value == "" || nome.value.length < 2) {
        $('#txtNomeSoc').css('border-color', 'red');
        $('#SPtxtNomeSoc').show();
        validaPassou = false;
    } else {
        $('#txtNomeSoc').css('border-color', '');
        $('#SPtxtNomeSoc').hide();
    }

    var rel = document.getElementById("cmbGrauRelPF");
    if (rel.value == "" || rel.value.length < 1) {
        $('#cmbGrauRelPF').css('border-color', 'red');
        $('#SPcmbGrauRel').show();
        validaPassou = false;
    } else {
        $('#cmbGrauRelPF').css('border-color', '');
        $('#SPcmbGrauRel').hide();
    }

    var fisica = document.getElementById("rdFisica").checked;
    var juridica = document.getElementById("rdJuridica").checked;

    if (fisica != true && juridica != true) {
        $('#btnFisica').css('border-color', 'red');
        $('#btnJuridica').css('border-color', 'red');
        $('#SPtxtPFPJ').show();
        validaPassou = false;
    } else {
        $('#btnFisica').css('border-color', '');
        $('#btnJuridica').css('border-color', '');
        $('#SPtxtPFPJ').hide();
    }

    var sim = document.getElementById("rdPEP-Sim").checked;
    var nao = document.getElementById("rdPEP-Nao").checked;

    if (sim != true && nao != true) {
        $('#btnPEP-Sim').css('border-color', 'red');
        $('#btnPEP-Nao').css('border-color', 'red');
        $('#SPtxtPEP').show();
        validaPassou = false;
    } else {
        $('#btnPEP-Sim').css('border-color', '');
        $('#btnPEP-Nao').css('border-color', '');
        $('#SPtxtPEP').hide();
    }

    if (sim == true) {
        var cargo = document.getElementById("txtCargoPEP");
        if (cargo.value == "" || cargo.value.length < 2) {
            $('#txtCargoPEP').css('border-color', 'red');
            $('#SPtxtCargoPEP').show();
            validaPassou = false;
        } else {
            $('#txtCargoPEP').css('border-color', '');
            $('#SPtxtCargoPEP').hide();
        }
    }
    return validaPassou;
}

function validaSocietario()
{
    var validaSocietario = true;
    if(objSocietariosList.length == 0)    
    {
        $('#SPListSocietario').show();
        validaSocietario = false;
    }        
    else
        $('#SPListSocietario').hide();
    return validaSocietario;
}



//**************************** Tabela de societários ***********************************


function limparCamposSocietario(){
    $('#txtNomeSoc').val('');
    $('#cmbGrauRelPF').val('0');
    $('#btnFisica').removeClass('active');
    $('#btnJuridica').removeClass('active');
    $('#btnPEP-Sim').removeClass('active');
    $('#btnPEP-Nao').removeClass('active');
    $('#txtCargoPEP').val('');
    $('#dvCargoPEP').hide();     
    $('#txtNomeSoc').focus();
}


function adicionarItemLista()
{
    if(validaCamposSocietario())
    {
        $('#SPListSocietario').hide(); 
        var objSocietario = new Societario();    
        objSocietario.CarregaDados();
        objSocietario.IdSocietario = objSocietariosList.length + 1;   //+ 1 pra não começar do zero.
        objSocietariosList.push(objSocietario);
        limparCamposSocietario();
        listar();
    }    
}

function listar()
{
    limparLista();
    objSocietariosList.forEach(function(item, index){        
        $('#idtbody').append('<tr><td>' + item.IdSocietario + '</td><td>' + item.Nome + '</td><td>' + item.GrauRelacionamento + '</td><td>' + item.Modalidade + '</td><td>' + item.Pep + '</td><td>' + item.PepProfissao + '</td><td style="text-align:center"><img onclick="excluirItemLista(' + index + ')" src="../ContentAdm/img/imgExcluir.png" alt="X" /></td></tr>');
    });    
    //console.log(objSocietariosList);
}

function excluirItemLista(index)
{
    objSocietariosList.splice(index, 1);
    listar();
}

function limparLista() {    
    $('#idtbody').html('');
}
//************************************************************************


let objSocietariosList = new Array();
//******************** Classes JSON *************************************
class Societario{
    constructor(){        
        this.IdSocietario;        
        this.IdSegurado; 
        this.Nome; 
        this.GrauRelacionamento; 
        this.Modalidade; 
        this.Pep; 
        this.PepProfissao; 
        this.Deletado; 
    }   
    CarregaDados(){        
        this.IdSegurado = $('#hddIdSegurado').val();
        this.Nome = $('#txtNomeSoc').val(); 
        this.GrauRelacionamento = $('#hddGrau').val();
        this.Modalidade = $('#hddPessoa').val(); 
        this.Pep = $('#hddPep').val();
        this.PepProfissao = $('#txtCargoPEP').val(); 
        this.Deletado = false; 
    }
}
