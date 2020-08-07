$(document).ready(() => {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)

    $('#icon-notifs').click(() => {
        AtualizaVisualizacaoNotifs();
    });

    // #region Módulo Dashboard
    if (url.includes('/Dashboard')) {
        // Checa o menu
        $('#anc-dashboard').click();
        $('#ul-dashboard').addClass("in");
        $('#sb-mn-dashboard').addClass('active');

        // #region Carregando a paginação
        $('.dataTables-example').dataTable({
            responsive: true,
            "dom": 'T<"clear">lfrtip',
            "tableTools": {
                "sSwfPath": "js/plugins/dataTables/swf/copy_csv_xls_pdf.swf"
            }
        });

        /* Init DataTables */
        var oTable = $('#editable').dataTable();
        /* Apply the jEditable handlers to the table */
        oTable.$('td').editable('../example_ajax.php', {
            "callback": function (sValue, y) {
                var aPos = oTable.fnGetPosition(this);
                oTable.fnUpdate(sValue, aPos[0], aPos[1]);
            },
            "submitdata": function (value, settings) {
                return {
                    "row_id": this.parentNode.getAttribute('id'),
                    "column": oTable.fnGetPosition(this)[2]
                };
            },

            "width": "90%",
            "height": "100%"
        });

        function fnClickAddRow() {
            $('#editable').dataTable().fnAddData([
                "Custom row",
                "New row",
                "New row",
                "New row",
                "New row"]);
        }

        // #endregion

        // Oculta os detalhes desnecessários da paginação
        $('.DTTT_container').hide();
        $('.dataTables_length').hide();
        $('input[type="search"]').css('font-weight', '100');
        $('#DataTables_Table_0_filter').hide();
        $('#DataTables_Table_1_filter').hide();
    }
    // #endregion
    // #region Módulo Compartilhar
    // Ativação/desativação dos links do menu
    if (url.includes('Compartilhar/Formulario')) {
        // Eventos
        $('#btn-compartilhar-form').click((e) => {
            ValidaCompartilhamentoFormularios();            
            e.preventDefault();
        });
        // Checa o menu
        $('#anc-compartilhar').click();
        $('#ul-compartilhar').addClass("in");
        $('#sb-mn-formularios').addClass('active');
    }
    // #endregion
    // #region Módulo Admin
    // Ativação/desativação dos links do menu
    if (url.includes('Admin/Lead')) {       
        
        // Eventos
        $('#btn-consultar-leads').click((ev) => {
            ListaLeads();
            ev.preventDefault();
        });
        
        // Checa o menu
        $('#anc-admin').click();
        $('#ul-admin').addClass("in");
        $('#sb-mn-leads').addClass('active');
    }
    if (url.includes('Admin/PermissaoFormularios')) {
        // Eventos
        $('#empresa-permissao-formularios').change(() => {
            let idEmp = $('#empresa-permissao-formularios').val();
            CarregaListaPermissaoFormularios(idEmp);
        });
        // Checa o menu
        $('#anc-permissoes').click();
        $('#ul-permissoes').addClass("in");
        $('#sb-mn-pform').addClass('active');
    }
    if (url.includes('Admin/Empresa')) {
        // Eventos
        // Checa o menu
        $('#anc-permissoes').click();
        $('#ul-permissoes').addClass("in");
        $('#sb-mn-empresa').addClass('active');        
        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-empresa-form-state', 'hdd-empresa-error', 'Seus dados foram salvos com sucesso!');
    }
    if (url.includes('Admin/Usuario')) {
        // Eventos
        $('#btn-consultar-apr-usuario').click((ev) => {
            ListaUsuarios();
            ev.preventDefault();
        });
        // Toggle config
        var elem_3 = document.querySelector('.js-switch_3');
        var switchery_3 = new Switchery(elem_3, { color: '#1AB394' });
        // Checa o menu
        $('#anc-permissoes').click();
        $('#ul-permissoes').addClass("in");
        $('#sb-mn-usuario').addClass('active');
        $('#sb-mn-questionario, #sb-mn-perguntas, #sb-mn-respostas').removeClass('active');
        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-usuario-form-state', 'hdd-usuario-error', 'Seus dados foram salvos com sucesso!');
        // Estilo da validação de obrigatoriedade
        if ($('#sp-nome-usuario-error').text() == 'Nome da classificação obrigatório.')
            $('#fg-nome-usuario').addClass('has-error');
    }
    if (url.includes('Admin/Classificacao')) {
        // Eventos
        $('#questionario-classificacao').change(() => {
            let quest = $('#questionario-classificacao').val();
            CarregaListaClassificacao(quest);
        });

        if ($('#questionario-classificacao').val() != '')
            CarregaListaClassificacao($('#questionario-classificacao').val());

        $('#btn-editar-classificacao').click((e) => {
            e.preventDefault();
            editaClassificacao();
        });
        // foco no campo classificação
        $('#txt-nome-classificacao').focus();
        // Checa o menu
        $('#anc-admin').click();
        $('#ul-admin').addClass("in");
        $('#sb-mn-classificacao').addClass('active');
        $('#sb-mn-questionario, #sb-mn-perguntas, #sb-mn-respostas').removeClass('active');
        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-classificacao-form-state', 'hdd-classificacao-error', 'Seus dados foram salvos com sucesso!');
        // Estilo da validação de obrigatoriedade
        if ($('#sp-nome-classificacao-error').text() == 'Nome da classificação obrigatório.')
            $('#fg-nome-classificacao').addClass('has-error');

        if ($('#sp-questionario-classificacao-error').text() == 'Selecione um questionário.')
            $('#fg-questionario-classificacao').addClass('has-error');
    }
    if (url.includes('Admin/Questionario') || url.includes('Admin/EditarQuestionario')) {
        // Checa o menu
        $('#anc-admin').click();
        $('#ul-admin').addClass("in");
        $('#sb-mn-questionario').addClass('active');
        $('#sb-mn-classificacao, #sb-mn-perguntas, #sb-mn-respostas').removeClass('active');        
        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-questionario-form-state', 'hdd-questionario-error', 'Seus dados foram salvos com sucesso!');
        // Estilo da validação de obrigatoriedade
        if ($('#sp-nome-questionario-error').text() == 'Nome do questionário obrigatório')
            $('#fg-nome-questionario').addClass('has-error');
    }
    if (url.includes('Admin/Pergunta')) {

        // Eventos
        $('#questionario-pergunta').change(() => {
            CarregaComboClassificacao($('#questionario-pergunta').val());
            CarregaListaPerguntas($('#questionario-pergunta').val());
        });

        if ($('#questionario-pergunta').val() != '') {
            CarregaComboClassificacao($('#questionario-pergunta').val());
            CarregaListaPerguntas($('#questionario-pergunta').val());
        }

        // foco padrão no campo pergunta
        $('#txt-descricao-pergunta').focus();

        
        // Checa o menu
        $('#anc-admin').click();
        $('#ul-admin').addClass("in");
        $('#sb-mn-perguntas').addClass('active');
        $('#sb-mn-classificacao, #sb-mn-questionario, #sb-mn-respostas').removeClass('active');
        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-pergunta-form-state', 'hdd-pergunta-error', 'Seus dados foram salvos com sucesso!');
        // Estilo da validação de obrigatoriedade
        if ($('#sp-nome-pergunta-error').text() == 'Descrição da pergunta obrigatória.')
            $('#fg-nome-pergunta').addClass('has-error');

        if ($('#sp-questionario-classificacao-error').text() == 'Selecione um questionário.')
            $('#fg-questionario-classificacao').addClass('has-error');
    }
    if (url.includes('Admin/Resposta')) {
        // Eventos
        $('#questionario-resposta').change(() => {            
            CarregaListaRespostas($('#questionario-resposta').val());            
            LimparFormularioAdmin();
        });

        if ($('#questionario-resposta').val() != '') {            
            CarregaListaRespostas($('#questionario-resposta').val());
        }
        // Verifica se a pagina é de edição. se sim, carrega os valores respostas
        // Para Múltiplos valores
        //if (url.includes('IdR')) {
        //    let arrUrlParam = url.split('&');
        //    let IdResposta = arrUrlParam[1].split('=')[1];
        //    listaValorResposta(IdResposta);
        //}
        // Para único valor
        listaValorResposta();

        // Foco padrão no campo Resposta
        $('#txt-descricao-resposta').focus();

        $('#btn-nova-resposta').click((e) => {
            e.preventDefault();
            var url = 'http://' + window.location.hostname + ':' + window.location.port + '/Admin/Resposta';
            window.location.replace(url);
        });

        // Checa o menu        
        $('#anc-admin').click();
        $('#ul-admin').addClass("in");
        $('#sb-mn-respostas').addClass('active');
        $('#sb-mn-classificacao, #sb-mn-questionario, #sb-mn-perguntas').removeClass('active');
        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-resposta-form-state', 'hdd-resposta-error', 'Seus dados foram salvos com sucesso!');
        // Estilo da validação de obrigatoriedade        
        if ($('#sp-questionario-pergunta-error').text() == 'Selecione um questionário.')
            $('#fg-questionario-pergunta').addClass('has-error');
    }
    if (url.includes('Admin/Resultados') || url.includes('Admin/ConsultarResultados')) {
        // Eventos        
        $('#empresa-filtro-resultados').change(function () {
            let IdEmpresa = $(this).val();
            // CarregaComboDepto(IdEmpresa);
        });
        $('#Exibicao').change(() => {
            if ($('#Exibicao').val() == 'consolidado')
                $('#partial-cmb-respondente-resultados').hide();
            else
                $('#partial-cmb-respondente-resultados').show();
        });


        $('#questionario-filtro-resultados, #empresa-filtro-resultados').change(() => {
            CarregaComboRespondente($('#questionario-filtro-resultados').val(), $('#empresa-filtro-resultados').val());
        });
        if (!isNullOrEmpty($('#questionario-filtro-resultados').val())) {
            CarregaComboRespondente($('#questionario-filtro-resultados').val(), $('#empresa-filtro-resultados').val()); 
        }
        if (!isNullOrEmpty($('#Exibicao').val())) {
            if ($('#Exibicao').val() == 'consolidado')
                $('#partial-cmb-respondente-resultados').hide();
            else
                $('#partial-cmb-respondente-resultados').show();
        }

        // Checa o menu        
        $('#anc-admin').click();
        $('#ul-admin').addClass("in");
        $('#sb-mn-resultados').addClass('active');
        $('#sb-mn-classificacao, #sb-mn-questionario, #sb-mn-perguntas').removeClass('active');
        // #region Carregando a paginação
        $('.dataTables-example').dataTable({
            responsive: true,
            "dom": 'T<"clear">lfrtip',
            "tableTools": {
                "sSwfPath": "js/plugins/dataTables/swf/copy_csv_xls_pdf.swf"
            }
        });

        /* Init DataTables */
        var oTable = $('#editable').dataTable();
        /* Apply the jEditable handlers to the table */
        oTable.$('td').editable('../example_ajax.php', {
            "callback": function (sValue, y) {
                var aPos = oTable.fnGetPosition(this);
                oTable.fnUpdate(sValue, aPos[0], aPos[1]);
            },
            "submitdata": function (value, settings) {
                return {
                    "row_id": this.parentNode.getAttribute('id'),
                    "column": oTable.fnGetPosition(this)[2]
                };
            },

            "width": "90%",
            "height": "100%"
        });

        function fnClickAddRow() {
            $('#editable').dataTable().fnAddData([
                "Custom row",
                "New row",
                "New row",
                "New row",
                "New row"]);
        }

        // #endregion
        // Oculta os detalhes desnecessários da paginação
        $('.DTTT_container').hide();
        $('.dataTables_length').hide();
        $('input[type="search"]').css('font-weight', '100');
        $('#DataTables_Table_0_filter').hide();
    }
    // #endregion      
    // #region Módulo Questionários
    if (url.includes('/Questionarios/')) {
        // Eventos
        $('#btn-salvar-questionario').click((e) => {
            e.preventDefault();            
            //if (!ValidarPreenchimentoQ()) {
            //    ShowToastrError('Erro!', 'Preencha todos os campos do questionário', 3000);                
            //    return;
            //}   


            SalvarQuestionarioMindset();
        });

        ExibeMsgValidacao('hdd-formulario-form-state', 'hdd-formulario-form-error', 'Seus dados foram salvos com sucesso!');

        // Ativa menu
        $('#anc-forms').click();
        $('#ul-forms').addClass("in");
        // Captura o Id pela url
        var arrUrl = url.split('/');
        var IdQuest = arrUrl[5];
        // Ativa sub-menu
        // Carrega os questionários para verificar dinamicamente qual está ativado.
        var urlLsQuest = "/Formulario/ListarQuestionarios";
        $.get(urlLsQuest, null, function (data) {
            var return_quests = data;
            return_quests.forEach((item) => {
                if (item.Id === parseInt(IdQuest))
                    $(`#sb-mn-${item.Nome.replace(' ', '')}`).addClass('active');
                else
                    $(`#sb-mn-${item.Nome.replace(' ', '')}`).removeClass('active');
            });
        });
        // Estilo da validação de obrigatoriedade
        if ($('#sp-nome-classificacao-error').text() == 'Nome da classificação obrigatório.')
            $('#fg-nome-classificacao').addClass('has-error');

        if ($('#sp-questionario-classificacao-error').text() == 'Selecione um questionário.')
            $('#fg-questionario-classificacao').addClass('has-error');
    }
    if (url.includes('/Questionario/Form')) {
        // Checa o menu
        $('#anc-forms').click();
        $('#ul-forms').addClass("in");
        $('#sb-mn-corg').addClass('active');

        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-user-register-state', 'hdd-user-unicity-error', 'Você receberá um e-mail para confirmação do seu acesso!', 'Seus dados foram salvos!');
    }
    // #endregion
    // #region Módulo Login
    // Se for localhost não há verificação de sessão, para agilizar a manutenção. Caso haja necessidade de manutenção na sessão, comentar o fluxo de condição
    // Só não há verificação de sessão na página principal, login e registro.
    let urlLocal = `http://localhost:${window.location.port}/`;
    if (url == 'http://opportunatecnologia.com.br/' || url == urlLocal) {

        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-login-form-state', 'hdd-login-error', '', 'Seus dados foram salvos!');
    }

    if (url.includes('/Login/RedefinicaoSenha') || url.includes('/Login/SolicitacaoSenha')) {
        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-redefinicao-form-state', 'hdd-redefinicao-error', 'Seus dados foram salvos com sucesso!');
    }

    if (url.includes('Login/AcessoRestrito')) {
        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-login-form-state', 'hdd-login-error', 'Seus dados foram salvos com sucesso!');
    }

    if (url.includes('/Registro')) {
        // Verifica o status do evento e exibe a msg adequada.        
        // #region Controle Wizard Steps        
        $("#w-registro").steps({
            bodyTag: "fieldset",               
            onStepChanging: function (event, currentIndex, newIndex) {
                if (currentIndex == 0) {
                    $("#w-registro .actions a[href='#finish']").show();
                }
                if (currentIndex == 1) {
                    $("#w-registro .actions a[href='#finish']").hide();
                }
                // Always allow going backward even if the current step contains invalid fields!
                if (currentIndex > newIndex) {
                    return true;
                }

                var form = $(this);

                // Clean up if user went backward before
                if (currentIndex < newIndex) {
                    // To remove error styles
                    $(".body:eq(" + newIndex + ") label.error", form).remove();
                    $(".body:eq(" + newIndex + ") .error", form).removeClass("error");
                }

                let Consentimento = $("#Consentimento").is(":checked");
                if (!Consentimento) {
                    $('#msg-consentimento').show();
                    return false;
                }

                // Disable validation on fields that are disabled or hidden.
                form.validate().settings.ignore = ":disabled,:hidden";

                // Start validation; Prevent going forward if false
                return form.valid();
            },
            onStepChanged: function (event, currentIndex, priorIndex) {
                // Suppress (skip) "Warning" step if the user is old enough.
                if (currentIndex === 2) {
                    $(this).steps("next");                                               
                }

                // Suppress (skip) "Warning" step if the user is old enough and wants to the previous step.
                if (currentIndex === 2 && priorIndex === 3) {
                    $(this).steps("previous");                    
                }                
                $('#finish').addClass('disabled');
            },
            onFinishing: function (event, currentIndex) {
                var form = $(this);

                form.validate().settings.ignore = ":disabled";
                return form.valid();
            },
            onCanceled: function (event) {
                $('.reg-input').val('');
                $('#estado-empresa').val('');
                $('#ramo-empresa').val('');
            },
            onFinished: function (event, currentIndex) {
                SalvarRegistroUsuario();
            },
            labels: {
                cancel: "Limpar", current: "current step:", pagination: "Pagination", finish: "Registrar",
                next: "Seguinte", previous: "Anterior", loading: "Carregando ..."
            }
        }).validate({
            errorPlacement: function (error, element) {
                element.before(error);
            },
            rules: {
                confirm: {
                    equalTo: "#password"
                }
            }
        });
        $("#w-registro .actions a[href='#finish']").hide();
        $("#w-registro .actions a[href='#cancel']").hide();
        // #endregion
    }
    // #endregion      
    // #region Eventos Gerais
    // botões limpar form.
    $('.btn-limpar-form').click((e) => {
        e.preventDefault();
        LimparFormularioAdmin();
    });
    // #endregion    
});

let url = new URL(window.location)
url = String(url)

// #region methods
const VerificarSession = () => {
    $.ajax(
        {
            type: 'GET',
            url: `/Common/VerificarSessionAsync`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data);
                try {
                    if (data.Error) {
                        var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port;
                        window.location.replace(urlLogin);
                    }                        
                }
                catch (err) {

                }
            }
        });
}
const CarregaListaClassificacao = (IdQ, IdC = 0) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/_ListarClassificacao/${IdQ}`,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#lista-classificacao').html(data);
            }
        });
}
const ListaLeads = () => {
    let TpLead = $('#tp-lead').val();
    let data = { TpLead };

    $.ajax(
        {
            type: 'POST',
            url: `/Admin/_ListarLeads/`,
            data,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#partial-lista-leads').html(data);
            }
        });
}
const ListaUsuarios = () => {

    let data = {
        VM: {
            Nome: $('#nome-apr-usuario').val(),
            Aprovado: $('#situacao-apr-usuario').val()
        }
    };

    $.ajax(
        {
            type: 'POST',
            url: `/Admin/_ListarUsuarios/`,
            data,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#lista-apr-usuario').html(data);
            }
        });
}
const SalvarPermissaoForm = (idEQ) => {
    let isChecked = false;
    // Verifica qual estado do check após o click, se é p vincular ou desvincular
    if ($(`#chk-permissao-form-${idEQ}`).is(':checked'))
        isChecked = true;

    let data = {
        VM: {
            Id: idEQ,
            Ativo: isChecked
        }
    };

    $.ajax({
        type: 'POST',
        data,
        url: '/admin/SalvarPermissaoFormulario',
        dataType: 'json',
        cache: false,
        async: true,
        success: (data) => {
            // console.log(data);            
        }
    });
}
const SalvarRegistroUsuario = () => {
    //#region dummy data
    //let data = {
    //    VMU: {
    //        CPF: '280.698.390-82',
    //        Nome: 'Mário Santos',
    //        Email: 'mario12345@santos.com',
    //        DtNascimento: '12/05/1981',
    //        Telefone: '(61) 99905-9066',
    //        Senha: '123',
    //        SenhaConfirmacao: '123'
    //    },
    //    VME: {
    //        CNPJ: '61.961.314/0001-45',
    //        Nome: 'mc donalds',
    //        Cidade: 'Brasília',
    //        Estado: 'DF',
    //        Ramo: 'Financeiro'
    //    }
    //};
    //#endregion

    let Consentimento = $("#Consentimento").is(":checked");

    let data = {
        VMU: {
            CPF: $('#CPF_usuario_registro').val().replace(/[^\d]+/g, ''),
            Nome: $('#Nome').val(),
            Email: $('#Email').val(),
            DtNascimento: $('#DtNascimento').val(),
            Telefone: $('#Telefone').val().replace(/[^\d]+/g, ''),
            Senha: $('#Senha').val(),
            SenhaConfirmacao: $('#SenhaConfirmacao').val(),
            Consentimento
        }
        ,
        VME: {
            CNPJ: $('#CNPJ').val().replace(/[^\d]+/g, ''),
            Nome: $('#Empresa').val(),
            Cidade: $('#Cidade').val(),
            Estado: $('#estado-empresa').val(),
            Ramo: $('#ramo-empresa').val()
        }
    };

    

    $.ajax({
        type: 'POST',
        data,
        url: '/Registro/Index',
        dataType: 'json',
        cache: false,
        async: true,
        success: (data) => {            
            try {                
                if (data.Success) {
                    $('.reg-input').val('');
                    $('#estado-empresa').val('');
                    $('#ramo-empresa').val('');
                    $('#dv-success').show();
                    $('#dv-w-registro').hide();
                }

            } catch { }
            try {
                if (data.Error) {
                    ShowToastrError('Erro!', data.Error, 10000);
                    if (data.Error == 'Consentimento obrigatório.') {
                        $('#msg-consentimento').show();
                        $("#w-registro .actions a[href='#previous']").click();
                    }
                }
                    
            } catch { }
        }
    });
}

class ObjLead {
    constructor(
        nome,
        email,
        telefone,
        nomeEmpresa,
        CNPJ,
        cidadeEmpresa,
        estadoEmpresa,
        cargo,
        ramo,
        tiTerceirizada,
        adequacaoLGPD,
        situacaoTI,
        areaJuridica,
        qtdeClientes
    ) {  // Constructor
        this.nome = nome;
        this.email = email;
        this.telefone = telefone;
        this.nomeEmpresa = nomeEmpresa;
        this.CNPJ = CNPJ;
        this.cidadeEmpresa = cidadeEmpresa;
        this.estadoEmpresa = estadoEmpresa;
        this.cargo = cargo;
        this.ramo = ramo;
        this.tiTerceirizada = tiTerceirizada,
        this.adequacaoLGPD = adequacaoLGPD,
        this.situacaoTI = situacaoTI,
        this.areaJuridica = areaJuridica,
        this.qtdeClientes = qtdeClientes
    }
}

const carregaMdLead = (idLead) => {
    // #region dummydata
    //let objLead = new ObjLead('Mário Santos',
    //    'mariosantos@gmail.com',
    //    '61 99905-9066',
    //    'Proseg',
    //    '23545.43535.342',
    //    'Brasília',
    //    'DF',
    //    'Analista',
    //    'Alimentação',
    //    'sim',
    //    'não',
    //    'ambos',
    //    'própria',
    //    '200');

    //let deptos = [];
    //deptos.push('Comercial');
    //deptos.push('RH');
        // #endregion

    $.ajax(
        {
            type: 'GET',
            url: `/LGPD/CarregarDadosEtapaLGPD/${idLead}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {                
                //console.log(data);
                CarregaDetalhesMdLead(data.Lead, data.Empresa);
                CarregaDeptosLead(data.Deptos);
            }
        });
}
const CarregaDetalhesMdLead = (objLead, objEmpresa) => {
    $('#sp-nome').text(objLead.nome_completo);
    $('#sp-email').text(objLead.email);
    $('#sp-telefone').text(objLead.telefone);
    $('#sp-nomeEmpresa').text(objEmpresa.nome);
    $('#sp-CNPJ').text(objEmpresa.CNPJ);
    $('#sp-cidadeEmpresa').text(objEmpresa.cidadeEmpresa);
    $('#sp-estadoEmpresa').text(objEmpresa.estadoEmpresa);
    $('#sp-cargo').text(objLead.cargo);    
    $('#sp-ramo').text(objLead.ramo);
    $('#sp-tiTerceirizada').text(objLead.compartilha_dados);
    $('#sp-adequacaoLGPD').text(objLead.iniciou_adequacao);
    $('#sp-situacaoTI').text(objLead.lgpd_situacao_ti);
    $('#sp-areaJuridica').text(objLead.lgpd_situacao_juridico);
    $('#sp-qtdeClientes').text(objLead.qnt_colaborador);
}
const SalvarQuestionarioMindset = () => {
    let arrRespostas = [];
    
    var options = $('.cls-respostas');

    $.map(options, function (option) {
        // desmembra o value '<id pergunta>#<valor resposta>'
        let idPergunta = option.value.split('#')[0];
        let idResposta = option.value.split('#')[1];
        let vlrResposta = option.value.split('#')[2];
        arrRespostas.push({
            rpu_usu_id: $('#hdd-id-usuario').val(),            
            rpu_qst_id: $('#hdd-id-questionario').val(),
            rpu_rsp_id: idResposta,
            rpu_prg_id: idPergunta,
            rpu_valor_resposta: vlrResposta
        });        
    });

    let data = {
        Respostas: arrRespostas
    };

    //console.log(arrResultado);
    $.ajax({
        type: 'POST',
        data,
        url: '/Formulario/Salvar',
        dataType: 'json',
        cache: false,
        async: true,
        success: (data) => {
            // console.log(data);
            // msg. success
            $("html, body").animate({ scrollTop: 0 }, "fast");
            $('#dv-forms').hide();
            $('#dv-msg-error-form').hide();
            $('#dv-msg-success-form').show();
        }
    });

    // msg. acesso negado
    //$("html, body").animate({ scrollTop: 0 }, "fast");
    //$('#dv-forms').hide();
    //$('#dv-msg-success-form').hide();
    //$('#dv-msg-error-form').show();
}
const excluirRegistro = (View) => {
    let IdQ = $(`#questionario-${View.toLowerCase()}`).val();

    let urlRemover = '';

    switch (View) {
        case 'Questionario':
            urlRemover = `/Admin/Remover${View}/${$('#hdd-id-registro-selected').val()}`;
            break;
        case 'Classificacao':
            urlRemover = `/Admin/Remover${View}/?IdC=${$('#hdd-id-registro-selected').val()}&IdQ=${IdQ}`;
            break;
        case 'Pergunta':
            urlRemover = `/Admin/Remover${View}/?IdP=${$('#hdd-id-registro-selected').val()}&IdQ=${IdQ}`;            
            break;
        case 'Resposta':
            urlRemover = `/Admin/Remover${View}/?IdR=${$('#hdd-id-registro-selected').val()}&IdQ=${IdQ}`;            
            break;
        case 'Empresa':
            urlRemover = `/Admin/Remover${View}/?IdE=${$('#hdd-id-registro-selected').val()}`;
            break;
        default:
    }
    
    $.ajax(
        {
            type: 'GET',
            url: urlRemover,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                if (data.Error) {
                    ShowToastrError('Erro ao Excluir', data.Error, 5000);
                    return;
                }
                $('#hdd-id-classificacao').val($('#hdd-id-registro-selected').val());                                   
                if (data.Empty == true) {                        
                    var url = 'http://' + window.location.hostname + ':' + window.location.port + '/Admin/' + View + '?success=True&IdQ=' + IdQ;
                    window.location.replace(url);
                }

                switch (View) {
                    case 'Questionario':
                        var url = 'http://' + window.location.hostname + ':' + window.location.port + '/Admin/' + View;
                        window.location.replace(url);
                        return;
                    case 'Classificacao':
                        CarregaListaClassificacao(IdQ);
                        return;
                    case 'Pergunta':
                        CarregaListaPerguntas($('#questionario-pergunta').val());
                        return;
                    case 'Resposta':
                        CarregaListaRespostas($('#questionario-resposta').val());
                        return;
                    case 'Empresa':
                        var url = 'http://' + window.location.hostname + ':' + window.location.port + '/Admin/' + View;
                        window.location.replace(url);
                        return;
                }
            }
        });
}
const CarregaListaRespostas = (Id) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/_ListarRespostas/${Id}`,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#lista-resposta').html(data);
            }
        });
}
const CarregaListaPerguntas = (Id) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/_ListarPerguntas/${Id}`,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#lista-pergunta').html(data);
            }
        });
}
const CarregaComboDepto = (IdEmpresa = 0) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/_ComboDepartamento/${IdEmpresa}`,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#partial-cmb-departamento-resultados').html(data);
            }
        });
}
const CarregaComboRespondente = (IdQ, IdE = null) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/_ComboRespondente/?IdQ=${IdQ}&IdE=${IdE}`,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#partial-cmb-respondente-resultados').html(data);
                let idResp = $('#hdd-id-respondente-selected').val();
                if (!isNullOrEmpty(idResp)) {
                    $('#respondente-filtro-resultados').val(idResp);
                }
            }
        });
}
const ExibirSideMenu = () => {
    $.ajax(
        {
            type: 'GET',
            url: `/Common/_SideMenu`,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#partial-side-menu').html(data);
                // Checa menu
                if (url.includes('Admin/Questionario') || url.includes('Admin/EditarQuestionario')) {
                    $('#anc-admin').click();
                    $('#ul-admin').addClass("in");
                    $('#sb-mn-questionario').addClass('active');
                }                
            }
        });
}
const SalvarAprovacaoUsuario = (idUsuario) => {
    let Checked = $("#chk-apr-usuario-" + idUsuario).is(':checked');


    $.ajax(
        {
            type: 'GET',
            url: `/Admin/SalvarAprovacaoUsuario/${idUsuario}/?Checked=${Checked}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data.Id);                
            }
        });
}
const ValidarPreenchimentoQ = () => {
    var valida = true;
    $('.cls-respostas').each((index, value) => {
        // console.log(`${index} - ${$(value).val()}`);
        if (isNullOrEmpty($(value).val()))
            valida = false;
    });
    return valida;
}
const CarregaComboClassificacao = (Id = 0) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/_ComboClassificacao/${Id}`,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#partial-cmb-classificacao-pergunta').html(data);
                // Atualiza seta combo classificiação, caso haja Id
                if (url.includes('IdC')) {
                    let IdClassificacao = getUrlParams(url, 'IdC');
                    $('#IdClassificacao').val(IdClassificacao);
                }
            }
        });
}
const ShowToastrError = (msgTitle, msg, timeOut = 2000) => {
    // https://github.com/CodeSeven/toastr        
    toastr.options = { "timeOut": timeOut };
    toastr.error(msg, msgTitle);
}
const ShowToastrSuccess = (msgTitle, msg, timeOut = 2000) => {
    // https://github.com/CodeSeven/toastr        
    toastr.options = {        
        "timeOut": timeOut        
    }
    toastr.success(msg, msgTitle);
}
const editaClassificacao = () => {    

    let Id = $('#hdd-id-classificacao').val();
    let Nome = $('#txt-nome-classificacao').val();

    $.ajax(
        {
            type: 'GET',
            url: `/Admin/SalvarClassificacao/?Id=${Id}&Nome=${Nome}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                //console.log(data);
                try {
                    if (data.Ok) {
                        CarregaListaClassificacao($('#questionario-classificacao').val());
                    }

                } catch { }
                try {
                    if (data.Error)
                        ShowToastrError('Erro!', data.Error, 10000);
                } catch { }
            }
        });
}
const CarregaListaPermissaoFormularios = (idEmpresa) => {        
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/_ListarPermissaoFormularios/${idEmpresa}`,            
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#partial-lista-formularios-permissao').html(data);
            }
        });
}
const AdicionarValorResposta = (idResposta) => {
    let vlr = $('#valor-resposta').val();
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/AdicionarValorResposta/?IdResposta=${idResposta}&valor=${vlr}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data);
                try {
                    if (data.Ok) {
                        $('#valor-resposta').val('');
                        listaValorResposta(idResposta);
                    }

                } catch { }
                try {
                    if (data.Error)
                        ShowToastrError('Erro!', data.Error, 10000);
                } catch { }
            }
        });
}
const excluiValorResposta = (idValorResposta) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/ExcluirValorResposta/${idValorResposta}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data);
                try {
                    if (data.Ok) {                        
                        listaValorResposta(idValorResposta);
                    }

                } catch { }
                try {
                    if (data.Error)
                        ShowToastrError('Erro!', data.Error, 10000);
                } catch { }
            }
        });
}
const ValidaCompartilhamentoFormularios = () => {
    if (FormShareSelected.length == 0) {
        ShowToastrError('Erro', 'Selecione pelo menos um formulário a ser compartilhado', 3000);
        return;
    }

    if (isNullOrEmpty($('#Emails').val())) {
        ShowToastrError('Erro', 'Infome pelo menos um email a ser compartilhado', 3000);
        return;
    }

    $('#btn-compartilhar-form').attr('disabled', 'true');
    $('#dv-processando').show();
    let VM = [];

    // Itera os formulários selecionados para carregar o objeto
    FormShareSelected.forEach((value) => {
        VM.push({ IdFormulario: value, Emails: $('#Emails').val() });
    });

    // #region Dummy Data
    //VM.push({
    //    IdFormulario: 1,
    //    Selecionado: true,
    //    Emails: 'mario@santos.com; mariazinha@silva.com'
    //});
    //VM.push({
    //    IdFormulario: 1,
    //    Selecionado: true,
    //    Emails: 'mario@santos.com; mariazinhasilva.com'
    //});
    // #endregion

    let data = {
        VM: VM
    };
    // Validando pelo backend
    $.ajax(
        {
            type: 'POST',
            url: `/Compartilhar/Formularios`,
            data,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data);
                $('#btn-compartilhar-form').removeAttr('disabled', 'true');                
                $('#dv-processando').hide();                
                try {
                    if (data.Success) {
                        ShowToastrSuccess('Sucesso!', data.Success, 3000);
                        $('#Emails').val('');
                    }
                    if (data.Error) {
                        if (data.Error.includes('Email inválido!'))
                            ShowToastrError('Erro!', data.Error, 5000);                            
                        else
                            ShowToastrError('Erro!', 'problemas no compartilhamento de formulários', 5000);
                        //console.log(`erro: ${data.Error}`);
                    }
                }                                
                catch { }
            }
        });
}
var FormShareSelected = [];
const SelecionarShareForm = (IdQ) => {
    if ($(`#chk-share-form-${IdQ}`).is(':checked'))
        FormShareSelected.push(IdQ);
    else {
        FormShareSelected = RemoverArrayElement(FormShareSelected, IdQ)        
    }
    // console.log(FormShareSelected);
}
const AtualizaVisualizacaoNotifs = () => {
    
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/AtualizarNotificacoes/`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data);
                try {
                    if (data.Success)
                        $('#notif-count').hide();
                } catch { }
                try {
                    if (data.Error)
                        ShowToastrError('Erro!', data.Error, 3000);                    

                } catch { }
            }
        });
}
const listaValorResposta = (idResposta = null) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/_ListarValorResposta/${idResposta}`,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#partial-lista-valor-reposta').html(data);
            }
        });
}
const ValidaCNPJ = (cnpj) => {

    if (isNullOrEmpty(cnpj))
        return;

    let data = {
        CNPJ: cnpj
    }

    $.ajax(
        {
            type: 'POST',
            url: `/Common/CnpjValido`,
            data,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data);
                try {
                    if (data.Result)
                        $('#hdd-empresa-form-state').val(data.Result);
                    

                } catch { }
                try {
                    if (data.Error) {
                        $('#hdd-empresa-error').val(data.Error);
                        $('#cnpj-msg').text(data.Error);
                    }
                        
                } catch { }
            }
        });
}
const ValidarEmail = (InputEmail) => {    
    $.ajax(
        {
            type: 'GET',
            url: `/Registro/ValidarEmail/?Email=${InputEmail.value}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data);
                $("#w-registro .actions a[href='#next']").show();
                // $('#invalid-email-error').hide();
                try {
                    $('#CPF_label_error').text('');
                    if (data.Success) {                     
                        $('#invalid-email-error').text('');
                        $("#w-registro .actions a[href='#next']").show();                    
                    }
                    if (data.Error) {
                        if (data.Error == 'Email inválido.') {
                            $('#invalid-email-error').text(data.Error);
                            $("#w-registro .actions a[href='#next']").hide();
                        } 
                    }
                } catch { }
            }
        });
}
const VerificaUnicidadeUsuarioCpf = (InputCpf) => {   

    $.ajax(
        {
            type: 'GET',
            url: `/Registro/VerificarUnicidadeUsuarioPorCpf/?CPF=${InputCpf.value}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data);
                $("#w-registro .actions a[href='#next']").show();
                try {
                    $('#CPF_label_error').text('');
                    if (data.Success) {
                        $('#CPF_label_error').text('');
                    }
                    if (data.Error) {
                        if (data.Error == 'CPF já cadastrado no sistema.') {
                            $('#CPF_label_error').text(data.Error);
                            $("#w-registro .actions a[href='#next']").hide();
                        }
                    }
                } catch { }
            }
        });
}
const ativaNivelAcesso = (Nvl, IdUsuario) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/SalvarNivelAcesso/?Acesso=${Nvl}&IdUsuario=${IdUsuario}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                // console.log(data);
                try {
                    if (data.Ok) {
                        ListaUsuarios();
                    }

                }catch {}
                try {
                    if (data.Error)
                        ShowToastrError('Erro!', data.Error, 10000);
                }catch {}
            }
        });
}
const LimparFormularioAdmin = () => {
    $('.qst-input').val('');
}
const OcultaMsgConsent = () => {
    if ($('#Consentimento').is(':checked'))
        $('#msg-consentimento').hide();
    else
        $('#msg-consentimento').show();
}
const ExibeMsgValidacao = (hiddenSuccess, hiddenError, msg, titulo = 'Parabéns!') => {
    try {
        if (!isNullOrEmpty(hiddenSuccess))
            if ($(`#${hiddenSuccess}`).val() == 'True') {
                ShowToastrSuccess(titulo, msg, 6000);
                // LimparFormularioAdmin();
            }
        if (!isNullOrEmpty(hiddenError))
            if ($(`#${hiddenError}`).val().length)
                ShowToastrError('Erro!', $(`#${hiddenError}`).val(), 10000);
    }
    catch (err) {
        return;
    }    
}
const CarregaDeptosLead = (deptos) => {
    let listDeptos = '';
    deptos.forEach((item) => {
        listDeptos += `<li>${item.nome}</li>`;
    });    
    let htmlList = `<ul>${listDeptos}</ul>`;
    $('#dv-deptos').html(htmlList);
}
// #endregion
