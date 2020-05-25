$(document).ready(() => {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)
        
    //ExibirSideMenu();

    // #region Módulo Admin
    // Ativação/desativação dos links do menu
    if (url.includes('Admin/Usuario')) {
        // Checa o menu
        $('#anc-admin').click();
        $('#ul-admin').addClass("in");
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
        $('#questionario-classificacao').change(function () {
            CarregaListaClassificacao($(this).val());
        });

        if ($('#questionario-classificacao').val() != '')
            CarregaListaClassificacao($('#questionario-classificacao').val());

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
        // Toggle config
        var elem_3 = document.querySelector('.js-switch_3');
        var switchery_3 = new Switchery(elem_3, { color: '#1AB394' });
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
    if (url.includes('Admin/Resultados')) {
        // Eventos        
        $('#empresa-filtro-resultados').change(function () {
            let IdEmpresa = $(this).val();
            CarregaComboDepto(IdEmpresa);
        });
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
    if (url.includes('/Formulario/')) {
        // Eventos
        $('#btn-salvar-questionario').click(() => {
            SalvarQuestionarioMindset();
        });

        ExibeMsgValidacao('hdd-formulario-form-state', '', 'Seus dados foram salvos com sucesso!');

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
    if (!url.includes('/Login'))    
        VerificarSession();

    if (url.includes('/Login/Registro')) {
        // Eventos
        $('#cpf-usuario-signup').keypress(() => $('#sp-cpf-signup-error').hide());
        $('#nome-usuario-signup').keypress(() => $('#sp-nome-signup-error').hide());
        $('#email-usuario-signup').keypress(() => $('#sp-email-signup-error').hide());
        $('#data-nascimento-usuario-signup').keypress(() => $('#sp-dt-nascimento-signup-error').hide());
        $('#telefone-usuario-signup').keypress(() => $('#sp-telefone-signup-error').hide());
        $('#senha-usuario-signup').keypress(() => $('#sp-senha-signup-error').hide());

        // Verifica o status do evento e exibe a msg adequada.
        ExibeMsgValidacao('hdd-user-register-state', 'hdd-user-unicity-error', 'Você receberá um e-mail para confirmação do seu acesso!', 'Seus dados foram salvos!');
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

// #region methods
const VerificarSession = () => {
    $.ajax(
        {
            type: 'GET',
            url: `/Home/VerificarSessionAsync`,
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

const CarregaListaClassificacao = (Id) => {
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/_ListarClassificacao/${Id}`,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                $('#lista-classificacao').html(data);
            }
        });
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
            ShowToastrSuccess('Parabéns!', 'Seus dados foram salvos com sucesso!', 3000);
            var url = 'http://' + window.location.hostname + ':' + window.location.port + '/Formulario/Index/' + $('#hdd-id-questionario').val() + '/?success=true';
            window.location.replace(url);
        }
    });
}

const excluirRegistro = (View) => {    
    $.ajax(
        {
            type: 'GET',
            url: `/Admin/Remover${View}/${$('#hdd-id-registro-selected').val()}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                if (data.Error) {
                    ShowToastrError('Erro ao Excluir', data.Error, 5000);
                    return;
                }
                $('#hdd-id-classificacao').val($('#hdd-id-registro-selected').val());
                var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Admin/' + View + '/' + data.Removed_id;
                window.location.replace(urlLogin);
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

const ExibirSideMenu = () => {
    $.ajax(
        {
            type: 'GET',
            url: `/Home/_SideMenu`,
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
        //"closeButton": true,
        //"debug": false,
        //"progressBar": true,
        //"positionClass": "toast-top-right",
        //"onclick": null,
        //"showDuration": "100",
        //"hideDuration": "1000",
        "timeOut": timeOut,
        //"extendedTimeOut": "1000",
        //"showEasing": "swing",
        //"hideEasing": "linear",
        //"showMethod": "fadeIn",
        //"hideMethod": "fadeOut"
    }
    toastr.success(msg, msgTitle);
}
const ExibeMsgValidacao = (hiddenSuccess, hiddenError, msg, titulo = 'Parabéns!') => {
    try {
        if (!isNullOrEmpty(hiddenSuccess))
            if ($(`#${hiddenSuccess}`).val() == 'True') {
                ShowToastrSuccess(titulo, msg, 6000);
                LimparFormularioAdmin();
            }
        if (!isNullOrEmpty(hiddenError))
            if ($(`#${hiddenError}`).val().length)
                ShowToastrError('Erro!', $(`#${hiddenError}`).val(), 10000);
    }
    catch (err) {
        return;
    }    
}

// #endregion
