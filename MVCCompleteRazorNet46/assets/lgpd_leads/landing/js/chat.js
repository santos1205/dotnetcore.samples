let sizeScroll = 0;
let etapa = 0;

$(document).ready(() => {    
    //#region events    
    $('#btn-enviar').click(() => {
        // Interações:            
        sizeScroll += 1000;
        interacao('replies', $('#field-chat-msg').val(), Roteiro[etapa].errorMsg, Roteiro[etapa].opcoes);
    });

    $(document).on('keypress', function (e) {
        if (e.which == 13) {
            // Interações:            
            sizeScroll += 1000;
            interacao('replies', $('#field-chat-msg').val(), Roteiro[etapa].errorMsg, Roteiro[etapa].opcoes);
        }
    });


    // carrega de onde parou o usuário
    CarregarEtapa();
    //#endregion events        
});

async function interacaoInicial() {
    // primeira interação
    // se começar do zero, segue as 4 interações iniciais
    if (etapa == 0) {
        for (let i = 0; i <= 3; i++) {
            await sleep(1000);
            interacao('sent', Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);
            if (i < 3)
                etapa++;
        }         
    } else {        
        enviaMensagem('sent', 'Bem-vindo de volta! \<br /> Vamos começar de onde paramos...');
        await sleep(1000);
        interacao('sent', Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);
    }        
}

async function interacao(tipo, mensagem, errorMsg, opcoes) {
    let resposta = '';
    if (mensagem == '')
        return;    
    if (tipo == 'sent') {
        await sleep(1000);
        enviaMensagem(tipo, mensagem);        
        await RealizarRegrasEtapas(tipo, mensagem);
    }
    else {
        enviaMensagem('replies', mensagem);        
        // interação do robo. algoritmo
        resposta = $('#field-chat-msg').val().toLowerCase();
        $('#field-chat-msg').val('');
        $('#field-chat-msg').focus();
        // interação de negação
        let negacao = true;
        let arrOpcoes = opcoes.split(';');
        negacao = !arrOpcoes.includes(resposta);
        // senão tiver opcoes negacao é false
        if (opcoes == '')
            negacao = false;
        if (negacao == true) {
            enviaMensagem('sent', errorMsg);
        }
        else
        {   
            if (await RealizarRegrasEtapas(tipo, mensagem) != false) {
                etapa++;       
                interacao('sent', Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);
            }

            let andamento = percentualConcluido(etapa);
            let arrAux = andamento.toString().split('.');
            let msgConcluido = `${arrAux[0]}% concluído`;
            $('#sp-chat-concluido').text(msgConcluido);     
        }
    }

    $('#field-chat-msg').val('');
    $('#field-chat-msg').focus();
}

async function enviaMensagem(tipo, conteudo) { // tipo: replies | sent    
    //console.log($("#PossuiJuridico").val());
    if (conteudo == '')
        return;
    // RN - Pergunta condicionada: caso empresa n tenha Juridico, n exibir
    if (conteudo.includes('E a área jurídica, pertence')) {
        if ($('#PossuiJuridico').val() == 'não') {            
            etapa++;
            interacao(tipo, Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);
            return;
        }
    }

    let avatar = '';
    if (tipo == 'sent')
        avatar = '/assets/lgpd_leads/landing/imgs/anna-avatar.png';
    if (tipo == 'replies')
        avatar = '/assets/lgpd_leads/landing/imgs/user-avatar.png';

    if (tipo == 'replies') {
        $('#chat-msgs').append(
            `        
        <li class="${tipo}">
            <img src="${avatar}" alt="">
            <p style='font-size: 0.8em;'>${conteudo}</p>
        </li>
        <br /><br /><br /><br />
        `);
    }

    if (tipo == 'sent') {
        $('#chat-msgs').append(
            `        
        <li class="${tipo}">
            <img src="${avatar}" alt="">
            <p style='font-size: 0.8em;'>${conteudo}</p>
            <br /><br />
        </li>
        `);
    }

    var n = sizeScroll;
    $('.messages').animate({ scrollTop: n }, 50);
}

async function RealizarRegrasEtapas(tipo, mensagem) {
    if (tipo == 'sent') {
        if (mensagem == 'Quais os departamentos existentes na sua empresa?') {
            etapa++;            
            await sleep(1000);
            interacao(tipo, Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);            
        }
        if (mensagem.includes('Então a lista de departamentos é essa')) {
            etapa++;
            
            await sleep(1000);
            interacao(tipo, Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);
        }
    }    
    //console.log(etapa);
    if (tipo == 'replies') {
        // pergunta: Quais os departamentos existentes na sua empresa?
        // verificação da resposta da etapa: 
        switch (etapa) {
            case 7:
                if (mensagem.toLowerCase() == 'sim')
                    SalvarDeptosMemoriaChat('Administrativo');
                return;
            case 8:
                if (mensagem.toLowerCase() == 'sim')
                    SalvarDeptosMemoriaChat('Tecnologia da Informação');                    
                return;
            case 9:
                if (mensagem.toLowerCase() == 'sim')
                    SalvarDeptosMemoriaChat('Comercial');
                return;
            case 10:
                if (mensagem.toLowerCase() == 'sim')
                    SalvarDeptosMemoriaChat('Recursos Humanos');
                return;
            case 11:
                if (mensagem.toLowerCase() == 'sim')
                    SalvarDeptosMemoriaChat('Jurídico');
                return;
            case 12:
                if (mensagem.toLowerCase() == 'sim')
                    SalvarDeptosMemoriaChat('Financeiro');
                return;
            case 13:
                if (mensagem.toLowerCase() == 'sim')
                    SalvarDeptosMemoriaChat('Contabilidade');
                return;
            case 14:
                if (mensagem.toLowerCase() == 'sim')
                    SalvarDeptosMemoriaChat('Marketing');
                return;

            // pergunta: Existe algum outro departamento que gostaria de mencionar?
            case 15:
                if (mensagem.toLowerCase() == 'sim') {
                    // se a resposta é sim, vai pra etapa de confirmação dos departamentos.
                    etapa = 16;
                    await sleep(1000);                    
                    interacao('sent', Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);
                    return false;
                } else {
                    // pula etapa para seguir em frente
                    etapa = etapa + 3;
                    // se resposta é não, vai p etapa de lista e confirmar os deptos
                    //etapa++;
                    // lista os deptos
                    let msgLstDeptos =
                        `
                        Então a lista de departamentos é essa: \<br />
                        @lstDeptos                        
                    `;
                    let lstDeptos = '';
                    Deptos.forEach((item) => {
                        lstDeptos += `${item} \<br />`;
                    });

                    msgLstDeptos = msgLstDeptos.replace('@lstDeptos', lstDeptos);
                    await sleep(1000);
                    interacao('sent', msgLstDeptos, Roteiro[etapa].errorMsg, Roteiro.opcoes);
                    return false;
                }
                return;
            // pergunta: Qual departamento? (fora os da lista)
            case 16:
                if (mensagem.length > 0) {
                    // salva o departamento informado.
                    SalvarDeptosMemoriaChat(mensagem);                   
                }
                return;
            // pergunta: Certo, tem mais algum departamento que queira adicionar?
            case 17:
                if (mensagem.toLowerCase() == 'sim') {
                    // se a resposta é sim, volta para etapa dos departamentos
                    etapa = 16;
                    await sleep(1000);
                    interacao('sent', Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);
                    return false;
                } else {
                    // se resposta é não, vai p etapa de lista e confirmar os deptos
                    etapa++;
                    // lista os deptos
                    //let msgLstDeptos =
                    //    `
                    //    Então a lista de departamentos é essa: \<br />
                    //    @lstDeptos                        
                    //`;
                    //let lstDeptos = '';
                    //Deptos.forEach((item) => {
                    //    lstDeptos += `${item} \<br />`;
                    //});

                    //msgLstDeptos = msgLstDeptos.replace('@lstDeptos', lstDeptos);
                    //await sleep(1000);
                    //interacao('sent', msgLstDeptos, Roteiro[etapa].errorMsg, Roteiro.opcoes);
                    //return false;
                }
                return;
            // pergunta: podemos prosseguir?
            // se não, voltar para o inicio dos departamentos.
            case 19:
                if (mensagem.toLowerCase() == 'nao' || mensagem.toLowerCase() == 'não') {
                    enviaMensagem('sent', 'Ok, então vamos começar de novo....');
                    etapa = 6;
                    Deptos = [];
                    await sleep(1000);
                    interacao('sent', Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);
                    return false;
                // se sim, salva os departamentos
                } else {
                    SalvarDeptosChat(Deptos, etapa);
                }                   
                return;
            // pergunta: A empresa possui área de tecnologia própria, terceirizada ou ambos?            
            case 20:
            case 21:
                switch (mensagem.toLowerCase()) {
                    case 'própria':
                    case 'propria':
                        mensagem = 'própria';
                        break;
                    case 'ambos':
                        mensagem = 'ambos';
                        break;
                    case 'terceirizado':
                    case 'terceirizada':
                        mensagem = 'terceirizada';
                        break;
                    default:
                        break;
                }
                SalvarDadosChat(mensagem, etapa);
                return;
            case 22:
                // pergunta: Qual a quantidade de clientes ativos e inativos hoje na empresa?
                // Verifica se o valor é numérico
                if ($.isNumeric(mensagem)) {
                    SalvarDadosChat(mensagem, etapa);
                    // Finalizando o chat, desativa o hash
                    DesativarHash();
                } else {
                    enviaMensagem('sent', 'O valor deve ser numérico, tente novamente.');
                    etapa = 22;                    
                    await sleep(1000);
                    interacao('sent', Roteiro[etapa].mensagem, Roteiro[etapa].errorMsg, Roteiro.opcoes);                    
                    return false;
                }                
                return;                
            default:
                if (mensagem.toLowerCase() == 'sim')
                    mensagem = 'Sim';
                else 
                    mensagem = 'Não';
                SalvarDadosChat(mensagem, etapa);
        }        
    }        
}

function percentualConcluido(nrEtapa) {
    let percent = (nrEtapa / Roteiro.length) * 100;
    return percent;
}

function SalvarDeptosMemoriaChat(resposta) {
    Deptos.push(resposta);
    //console.log(Deptos);
}

function CarregarEtapa() {
    $.ajax(
        {
            type: 'GET',
            url: `/LGPD/CarregarDadosEtapaLGPD/${$('#Id').val()}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                //console.log(data);
                try {
                    if (data.Ok) {
                        etapa = data.Etapa;
                        // Carrega se possui juridico, para a pergunta condicional
                        $('#PossuiJuridico').val(data.PossuiJuridico);
                        interacaoInicial();    
                    }

                } catch { }
                try {
                    if (data.Error)
                        ShowToastrError('Erro!', data.Error, 10000);
                } catch { }
            }
        });
}

function DesativarHash() {
    $.ajax(
        {
            type: 'GET',
            url: `/LGPD/DesativarHash/${$('#Id').val()}`,
            dataType: 'json',
            cache: false,
            async: true,
            success: (data) => {
                //console.log(data);
                //try {
                //    if (data.Ok) {
                //        CarregaListaClassificacao($('#questionario-classificacao').val());
                //    }

                //} catch { }
                //try {
                //    if (data.Error)
                //        ShowToastrError('Erro!', data.Error, 10000);
                //} catch { }
            }
        });
}

function SalvaDepartamentoAvulso(mensagem, etapa) {
    if (mensagem.length == 0)
        return;

    let data = {
        Id: $('#Id').val(),
        Departamento: mensagem,
        Etapa: etapa
    };

    $.ajax(
        {
            type: 'POST',
            url: `/LGPD/SalvarDeptoAvulsoChat/`,
            data,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                //$('#lista-apr-usuario').html(data);
            }
        });
}

function SalvarDeptosChat(Deptos, etapa) {
    
    if (Deptos.length == 0)
        return; 
    
    let data = {        
        Id: $('#Id').val(),
        Departamentos: Deptos,
        Etapa: etapa        
    };

    $.ajax(
        {
            type: 'POST',
            url: `/LGPD/SalvarDeptosChat/`,
            data,
            dataType: 'html',
            cache: false,
            async: true,
            success: (data) => {
                //console.log('etapa depto', etapa);
                Deptos.forEach((depto) => {
                    if (depto == 'Jurídico') {
                        $('#PossuiJuridico').val('sim');
                        return;
                    }                        
                    else
                        $('#PossuiJuridico').val('não');
                });                
            }
        });
    
}

function SalvarDadosChat(resposta, etapa) {
    $.ajax(
        {
            type: 'GET',
            url: `/LGPD/SalvarDadosChat/${$('#Id').val()}?Resposta=${resposta}&Etapa=${etapa}`,
            dataType: 'json',
            cache: false,
            async: true,            
            success: (data) => {
                //console.log('resposta e etapa:', resposta, etapa);
                //try {
                //    if (data.Ok) {
                //        CarregaListaClassificacao($('#questionario-classificacao').val());
                //    }

                //} catch { }
                //try {
                //    if (data.Error)
                //        ShowToastrError('Erro!', data.Error, 10000);
                //} catch { }
            }
        });
}

const Roteiro = [
    {
        mensagem: ` Olá, ${$('#Nome').val()}. Tudo bem? \<br />
                        Meu nome é Anna, sou sua assistente virtual e estou aqui para bater um papo contigo.
                    `,
        opcoes: '',
        errorMsg: ''
    },
    {
        mensagem: `
                    Para entender melhor o contexto da sua empresa em relação à LGPD, preciso te fazer algumas perguntas. Vai ser rapidinho.
                    `,
        opcoes: '',
        errorMsg: ''
    },
    {
        mensagem: `Vamos lá ...`,
        opcoes: '',
        errorMsg: ''
    },
    {
        mensagem: `
                    A ${$('#Empresa').val()} faz armazenamento de dados dos clientes?
                    `,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: 'Sua empresa utiliza serviço de parceiros/terceiros para realizar o processamento/trabalho de dados de seus clientes?',
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: 'Sua empresa já iniciou a adequação da LGPD?',
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Quais os departamentos existentes na sua empresa?`,
        opcoes: '',
        errorMsg: ''
    },
    {
        mensagem: `Administrativo?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Tecnologia da Informação?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Comercial?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Recursos Humanos?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Jurídico?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Financeiro?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Contabilidade?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Marketing?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Existe algum outro departamento que gostaria de mencionar?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Qual o nome do departamento?`,
        opcoes: '',
        errorMsg: ''
    },
    {
        mensagem: `Certo, tem mais algum departamento que queira adicionar?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `
                    Então a lista de departamentos é essa: \<br /><br />
                    Administrativo \<br />
                    Tecnologia da Informação \<br />
                    Comercial \<br />
                    `,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `Podemos prosseguir?`,
        opcoes: 'sim;não;nao',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda sim ou não'
    },
    {
        mensagem: `A empresa possui área de tecnologia própria, terceirizada ou ambos?`,
        opcoes: 'própria;propria;terceirizada;terceirizado;ambos',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda própria, terceirizada ou ambos'

    },
    {
        mensagem: `E a área jurídica, pertence a própria empresa é terceirizada ou ambos?`,
        opcoes: 'própria;propria;terceirizada;terceirizado;ambos',
        errorMsg: 'desculpe, mas não entendi sua resposta. Responda própria, terceirizada ou ambos'
    },
    {
        mensagem: `Qual a quantidade de clientes ativos e inativos hoje na empresa?`,
        opcoes: '',
        errorMsg: ''
    },
    {
        mensagem: `Ótimo! Finalizamos o perfil da empresa. Falta pouco, agora vamos falar sobre LGPD`,
        opcoes: '',
        errorMsg: ''
    }
];

var Deptos = [];

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}