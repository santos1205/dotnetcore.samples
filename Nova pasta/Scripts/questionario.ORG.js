$(document).ready(() => {
    $('#btn-resultados').click(() => {        
        exibeGridResultados();
    });

    
    $('.btn-salvar-form').click((event) => {
        SalvarQuestionario();
        
        $('.btn-salvar-form').attr('disabled', 'true');
        
        event.preventDefault();
    });


    loadAllCombosAtual();    

    // #region combos_listeners
    $('#cmb1A_atual, #cmb1B_atual, #cmb1C_atual, #cmb1D_atual').change(() => {
        onChangeQ1CmbAtual();
    });

    $('#cmb2A_atual, #cmb2B_atual, #cmb2C_atual, #cmb2D_atual').change(() => {
        onChangeQ2CmbAtual();
    });

    $('#cmb3A_atual, #cmb3B_atual, #cmb3C_atual, #cmb3D_atual').change(() => {
        onChangeQ3CmbAtual();
    });

    $('#cmb4A_atual, #cmb4B_atual, #cmb4C_atual, #cmb4D_atual').change(() => {
        onChangeQ4CmbAtual();
    });

    $('#cmb5A_atual, #cmb5B_atual, #cmb5C_atual, #cmb5D_atual').change(() => {
        onChangeQ5CmbAtual();
    });

    $('#cmb6A_atual, #cmb6B_atual, #cmb6C_atual, #cmb6D_atual').change(() => {
        onChangeQ6CmbAtual();
    });

    loadAllCombosDesejado();

    $('#cmb1A_desejado, #cmb1B_desejado, #cmb1C_desejado, #cmb1D_desejado').change(() => {
        onChangeQ1CmbDesejado();
    });

    $('#cmb2A_desejado, #cmb2B_desejado, #cmb2C_desejado, #cmb2D_desejado').change(() => {
        onChangeQ2CmbDesejado();
    });

    $('#cmb3A_desejado, #cmb3B_desejado, #cmb3C_desejado, #cmb3D_desejado').change(() => {
        onChangeQ3CmbDesejado();
    });

    $('#cmb4A_desejado, #cmb4B_desejado, #cmb4C_desejado, #cmb4D_desejado').change(() => {
        onChangeQ4CmbDesejado();
    });

    $('#cmb5A_desejado, #cmb5B_desejado, #cmb5C_desejado, #cmb5D_desejado').change(() => {
        onChangeQ5CmbDesejado();
    });

    $('#cmb6A_desejado, #cmb6B_desejado, #cmb6C_desejado, #cmb6D_desejado').change(() => {
        onChangeQ6CmbDesejado();
    });

    // #endregion


    $('#input-nome , #input-depto, #input-cargo, #input-empresa, #input-email').keypress(() => {
        $('.msg-formParticipante-error').hide();
    });

    $('#btn-go-form').click((event) => {
        //alert('go to form');

        if (validaFormParticipante()) {
            $('.msg-formParticipante-error').hide();

            localStorage.setItem('isFormOk', true);
            localStorage.setItem('PNome', $('#input-nome').val());
            localStorage.setItem('PDepto', $('#input-depto').val());
            localStorage.setItem('PCargo', $('#input-cargo').val());
            localStorage.setItem('PEmpresa', $('#input-empresa').val());
            localStorage.setItem('PEmail', $('#input-email').val());

            var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Questionario/Form';
            window.location.replace(urlLogin);
        } else {
            $('.msg-formParticipante-error').show();
        }

        
        event.preventDefault();
    });

    $("#form").submit((event) => {        
        SalvarQuestionario();        
        event.preventDefault();
    });
});

const totalQ1VlAtual = 100;
var vlQ1AtualRestante = totalQ1VlAtual;
const totalVlQ1Desejado = 100;
var vlQ1DesejadoRestante = totalVlQ1Desejado;

const totalQ2VlAtual = 100;
var vlQ2AtualRestante = totalQ2VlAtual;
const totalVlQ2Desejado = 100;
var vlQ2DesejadoRestante = totalVlQ2Desejado;

const totalQ3VlAtual = 100;
var vlQ3AtualRestante = totalQ3VlAtual;
const totalVlQ3Desejado = 100;
var vlQ3DesejadoRestante = totalVlQ3Desejado;

const totalQ4VlAtual = 100;
var vlQ4AtualRestante = totalQ4VlAtual;
const totalVlQ4Desejado = 100;
var vlQ4DesejadoRestante = totalVlQ4Desejado;

const totalQ5VlAtual = 100;
var vlQ5AtualRestante = totalQ5VlAtual;
const totalVlQ5Desejado = 100;
var vlQ5DesejadoRestante = totalVlQ5Desejado;

const totalQ6VlAtual = 100;
var vlQ6AtualRestante = totalQ6VlAtual;
const totalVlQ6Desejado = 100;
var vlQ6DesejadoRestante = totalVlQ6Desejado;

// #region Atual_functions

const loadAllCombosAtual = (nrQuestao = 0) => {

    if (nrQuestao == 1 || nrQuestao == 0) {
        loadComboAtual('cmb1A_atual', vlQ1AtualRestante, $('#cmb1A_atual').val());
        loadComboAtual('cmb1B_atual', vlQ1AtualRestante, $('#cmb1B_atual').val());
        loadComboAtual('cmb1C_atual', vlQ1AtualRestante, $('#cmb1C_atual').val());
        loadComboAtual('cmb1D_atual', vlQ1AtualRestante, $('#cmb1D_atual').val());
    }

    if (nrQuestao == 2 || nrQuestao == 0) {
        loadComboAtual('cmb2A_atual', vlQ2AtualRestante, $('#cmb2A_atual').val());
        loadComboAtual('cmb2B_atual', vlQ2AtualRestante, $('#cmb2B_atual').val());
        loadComboAtual('cmb2C_atual', vlQ2AtualRestante, $('#cmb2C_atual').val());
        loadComboAtual('cmb2D_atual', vlQ2AtualRestante, $('#cmb2D_atual').val());
    }

    if (nrQuestao == 3 || nrQuestao == 0) {
        loadComboAtual('cmb3A_atual', vlQ3AtualRestante, $('#cmb3A_atual').val());
        loadComboAtual('cmb3B_atual', vlQ3AtualRestante, $('#cmb3B_atual').val());
        loadComboAtual('cmb3C_atual', vlQ3AtualRestante, $('#cmb3C_atual').val());
        loadComboAtual('cmb3D_atual', vlQ3AtualRestante, $('#cmb3D_atual').val());
    }

    if (nrQuestao == 4 || nrQuestao == 0) {
        loadComboAtual('cmb4A_atual', vlQ4AtualRestante, $('#cmb4A_atual').val());
        loadComboAtual('cmb4B_atual', vlQ4AtualRestante, $('#cmb4B_atual').val());
        loadComboAtual('cmb4C_atual', vlQ4AtualRestante, $('#cmb4C_atual').val());
        loadComboAtual('cmb4D_atual', vlQ4AtualRestante, $('#cmb4D_atual').val());
    }

    if (nrQuestao == 5 || nrQuestao == 0) {
        loadComboAtual('cmb5A_atual', vlQ5AtualRestante, $('#cmb5A_atual').val());
        loadComboAtual('cmb5B_atual', vlQ5AtualRestante, $('#cmb5B_atual').val());
        loadComboAtual('cmb5C_atual', vlQ5AtualRestante, $('#cmb5C_atual').val());
        loadComboAtual('cmb5D_atual', vlQ5AtualRestante, $('#cmb5D_atual').val());
    }

    if (nrQuestao == 6 || nrQuestao == 0) {
        loadComboAtual('cmb6A_atual', vlQ6AtualRestante, $('#cmb6A_atual').val());
        loadComboAtual('cmb6B_atual', vlQ6AtualRestante, $('#cmb6B_atual').val());
        loadComboAtual('cmb6C_atual', vlQ6AtualRestante, $('#cmb6C_atual').val());
        loadComboAtual('cmb6D_atual', vlQ6AtualRestante, $('#cmb6D_atual').val());
    }
};

const resetAllCombosAtual = (nrQuestao) => {

    if (nrQuestao == 1) {
        resetComboAtual('cmb1A_atual', $('#cmb1A_atual').val());
        resetComboAtual('cmb1B_atual', $('#cmb1B_atual').val());
        resetComboAtual('cmb1C_atual', $('#cmb1C_atual').val());
        resetComboAtual('cmb1D_atual', $('#cmb1D_atual').val());
    }

    if (nrQuestao == 2) {
        resetComboAtual('cmb2A_atual', $('#cmb2A_atual').val());
        resetComboAtual('cmb2B_atual', $('#cmb2B_atual').val());
        resetComboAtual('cmb2C_atual', $('#cmb2C_atual').val());
        resetComboAtual('cmb2D_atual', $('#cmb2D_atual').val());
    }

    if (nrQuestao == 3) {
        resetComboAtual('cmb3A_atual', $('#cmb3A_atual').val());
        resetComboAtual('cmb3B_atual', $('#cmb3B_atual').val());
        resetComboAtual('cmb3C_atual', $('#cmb3C_atual').val());
        resetComboAtual('cmb3D_atual', $('#cmb3D_atual').val());
    }

    if (nrQuestao == 4) {
        resetComboAtual('cmb4A_atual', $('#cmb4A_atual').val());
        resetComboAtual('cmb4B_atual', $('#cmb4B_atual').val());
        resetComboAtual('cmb4C_atual', $('#cmb4C_atual').val());
        resetComboAtual('cmb4D_atual', $('#cmb4D_atual').val());
    }

    if (nrQuestao == 5) {
        resetComboAtual('cmb5A_atual', $('#cmb5A_atual').val());
        resetComboAtual('cmb5B_atual', $('#cmb5B_atual').val());
        resetComboAtual('cmb5C_atual', $('#cmb5C_atual').val());
        resetComboAtual('cmb5D_atual', $('#cmb5D_atual').val());
    }

    if (nrQuestao == 6) {
        resetComboAtual('cmb6A_atual', $('#cmb6A_atual').val());
        resetComboAtual('cmb6B_atual', $('#cmb6B_atual').val());
        resetComboAtual('cmb6C_atual', $('#cmb6C_atual').val());
        resetComboAtual('cmb6D_atual', $('#cmb6D_atual').val());
    }
};

const onChangeQ1CmbAtual = () => {
    let cmb1A_atual = $('#cmb1A_atual').val() == '' ? 0 : parseInt($('#cmb1A_atual').val());
    loadAvgByItem('A', 'atual');
    let cmb1B_atual = $('#cmb1B_atual').val() == '' ? 0 : parseInt($('#cmb1B_atual').val());
    loadAvgByItem('B', 'atual');
    let cmb1C_atual = $('#cmb1C_atual').val() == '' ? 0 : parseInt($('#cmb1C_atual').val());
    loadAvgByItem('C', 'atual');
    let cmb1D_atual = $('#cmb1D_atual').val() == '' ? 0 : parseInt($('#cmb1D_atual').val());
    loadAvgByItem('D', 'atual');

    
    vlQ1AtualRestante = totalQ1VlAtual - (cmb1A_atual + cmb1B_atual + cmb1C_atual + cmb1D_atual);
    resetAllCombosAtual(1);
    loadAllCombosAtual(1);    
    
    $('#total-atual-q1').text(cmb1A_atual + cmb1B_atual + cmb1C_atual + cmb1D_atual + '/100');
}

const onChangeQ2CmbAtual = () => {    
    let cmb2A_atual = $('#cmb2A_atual').val() == '' ? 0 : parseInt($('#cmb2A_atual').val());
    loadAvgByItem('A', 'atual');
    let cmb2B_atual = $('#cmb2B_atual').val() == '' ? 0 : parseInt($('#cmb2B_atual').val());
    loadAvgByItem('B', 'atual');
    let cmb2C_atual = $('#cmb2C_atual').val() == '' ? 0 : parseInt($('#cmb2C_atual').val());
    loadAvgByItem('C', 'atual');
    let cmb2D_atual = $('#cmb2D_atual').val() == '' ? 0 : parseInt($('#cmb2D_atual').val());
    loadAvgByItem('D', 'atual');

    vlQ2AtualRestante = totalQ2VlAtual - (cmb2A_atual + cmb2B_atual + cmb2C_atual + cmb2D_atual);
    resetAllCombosAtual(2);
    loadAllCombosAtual(2);
    //loadAvgByItem('B');
    $('#total-atual-q2').text(cmb2A_atual + cmb2B_atual + cmb2C_atual + cmb2D_atual + '/100');
}

const onChangeQ3CmbAtual = () => {
    let cmb3A_atual = $('#cmb3A_atual').val() == '' ? 0 : parseInt($('#cmb3A_atual').val());
    loadAvgByItem('A', 'atual');
    let cmb3B_atual = $('#cmb3B_atual').val() == '' ? 0 : parseInt($('#cmb3B_atual').val());
    loadAvgByItem('B', 'atual');
    let cmb3C_atual = $('#cmb3C_atual').val() == '' ? 0 : parseInt($('#cmb3C_atual').val());
    loadAvgByItem('C', 'atual');
    let cmb3D_atual = $('#cmb3D_atual').val() == '' ? 0 : parseInt($('#cmb3D_atual').val());
    loadAvgByItem('D', 'atual');

    vlQ3AtualRestante = totalQ3VlAtual - (cmb3A_atual + cmb3B_atual + cmb3C_atual + cmb3D_atual);
    resetAllCombosAtual(3);
    loadAllCombosAtual(3);
    $('#total-atual-q3').text(cmb3A_atual + cmb3B_atual + cmb3C_atual + cmb3D_atual + '/100');
}

const onChangeQ4CmbAtual = () => {
    let cmb4A_atual = $('#cmb4A_atual').val() == '' ? 0 : parseInt($('#cmb4A_atual').val());
    loadAvgByItem('A', 'atual');
    let cmb4B_atual = $('#cmb4B_atual').val() == '' ? 0 : parseInt($('#cmb4B_atual').val());
    loadAvgByItem('B', 'atual');
    let cmb4C_atual = $('#cmb4C_atual').val() == '' ? 0 : parseInt($('#cmb4C_atual').val());
    loadAvgByItem('C', 'atual');
    let cmb4D_atual = $('#cmb4D_atual').val() == '' ? 0 : parseInt($('#cmb4D_atual').val());
    loadAvgByItem('D', 'atual');

    vlQ4AtualRestante = totalQ4VlAtual - (cmb4A_atual + cmb4B_atual + cmb4C_atual + cmb4D_atual);
    resetAllCombosAtual(4);
    loadAllCombosAtual(4);
    $('#total-atual-q4').text(cmb4A_atual + cmb4B_atual + cmb4C_atual + cmb4D_atual + '/100');
}

const onChangeQ5CmbAtual = () => {
    let cmb5A_atual = $('#cmb5A_atual').val() == '' ? 0 : parseInt($('#cmb5A_atual').val());
    loadAvgByItem('A', 'atual');
    let cmb5B_atual = $('#cmb5B_atual').val() == '' ? 0 : parseInt($('#cmb5B_atual').val());
    loadAvgByItem('B', 'atual');
    let cmb5C_atual = $('#cmb5C_atual').val() == '' ? 0 : parseInt($('#cmb5C_atual').val());
    loadAvgByItem('C', 'atual');
    let cmb5D_atual = $('#cmb5D_atual').val() == '' ? 0 : parseInt($('#cmb5D_atual').val());
    loadAvgByItem('D', 'atual');

    vlQ5AtualRestante = totalQ5VlAtual - (cmb5A_atual + cmb5B_atual + cmb5C_atual + cmb5D_atual);
    resetAllCombosAtual(5);
    loadAllCombosAtual(5);
    $('#total-atual-q5').text(cmb5A_atual + cmb5B_atual + cmb5C_atual + cmb5D_atual + '/100');
}

const onChangeQ6CmbAtual = () => {
    let cmb6A_atual = $('#cmb6A_atual').val() == '' ? 0 : parseInt($('#cmb6A_atual').val());
    loadAvgByItem('A', 'atual');
    let cmb6B_atual = $('#cmb6B_atual').val() == '' ? 0 : parseInt($('#cmb6B_atual').val());
    loadAvgByItem('B', 'atual');
    let cmb6C_atual = $('#cmb6C_atual').val() == '' ? 0 : parseInt($('#cmb6C_atual').val());
    loadAvgByItem('C', 'atual');
    let cmb6D_atual = $('#cmb6D_atual').val() == '' ? 0 : parseInt($('#cmb6D_atual').val());
    loadAvgByItem('D', 'atual');

    vlQ6AtualRestante = totalQ6VlAtual - (cmb6A_atual + cmb6B_atual + cmb6C_atual + cmb6D_atual);
    resetAllCombosAtual(6);
    loadAllCombosAtual(6);
    $('#total-atual-q6').text(cmb6A_atual + cmb6B_atual + cmb6C_atual + cmb6D_atual + '/100');
}


const loadComboAtual = (comboName, vlRestante = 100, vlAtual = 0) => {
    let vlrComboAtual = vlAtual == ('' || null) ? 0 : vlAtual;
    let vlRestanteLocal = vlRestante + parseInt(vlrComboAtual);
    
    for (var i = 0; i <= vlRestanteLocal; i++) {
        if (vlAtual === i)
            $('#' + comboName).append(`<option value='${i}' selected>${i}</option>`);
        else
            $('#' + comboName).append(`<option value='${i}'>${i}</option>`);
    }
        
}

const resetComboAtual = (comboName, vlAtual = 0) => {
    if (vlAtual == '' || vlAtual == null)
        $('#' + comboName).html(`<select id="${comboName}"><option value="" selected>Atual</option></select>`);
    $('#' + comboName).html(`<select id="${comboName}"><option value="${vlAtual}">${vlAtual}</option></select>`);
}

// #endregion

// #region desejado_functions
const ShowToastr = (msgTitle, msg, timeOut = 2000) => {
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


const loadAllCombosDesejado = (nrQuestao = 0) => {
    if (nrQuestao == 1 || nrQuestao == 0) {
        loadComboDesejado('cmb1A_desejado', vlQ1DesejadoRestante, $('#cmb1A_desejado').val());
        loadComboDesejado('cmb1B_desejado', vlQ1DesejadoRestante, $('#cmb1B_desejado').val());
        loadComboDesejado('cmb1C_desejado', vlQ1DesejadoRestante, $('#cmb1C_desejado').val());
        loadComboDesejado('cmb1D_desejado', vlQ1DesejadoRestante, $('#cmb1D_desejado').val());
    }

    if (nrQuestao == 2 || nrQuestao == 0) {
        loadComboDesejado('cmb2A_desejado', vlQ2DesejadoRestante, $('#cmb2A_desejado').val());
        loadComboDesejado('cmb2B_desejado', vlQ2DesejadoRestante, $('#cmb2B_desejado').val());
        loadComboDesejado('cmb2C_desejado', vlQ2DesejadoRestante, $('#cmb2C_desejado').val());
        loadComboDesejado('cmb2D_desejado', vlQ2DesejadoRestante, $('#cmb2D_desejado').val());
    }

    if (nrQuestao == 3 || nrQuestao == 0) {
        loadComboDesejado('cmb3A_desejado', vlQ3DesejadoRestante, $('#cmb3A_desejado').val());
        loadComboDesejado('cmb3B_desejado', vlQ3DesejadoRestante, $('#cmb3B_desejado').val());
        loadComboDesejado('cmb3C_desejado', vlQ3DesejadoRestante, $('#cmb3C_desejado').val());
        loadComboDesejado('cmb3D_desejado', vlQ3DesejadoRestante, $('#cmb3D_desejado').val());
    }

    if (nrQuestao == 4 || nrQuestao == 0) {
        loadComboDesejado('cmb4A_desejado', vlQ4DesejadoRestante, $('#cmb4A_desejado').val());
        loadComboDesejado('cmb4B_desejado', vlQ4DesejadoRestante, $('#cmb4B_desejado').val());
        loadComboDesejado('cmb4C_desejado', vlQ4DesejadoRestante, $('#cmb4C_desejado').val());
        loadComboDesejado('cmb4D_desejado', vlQ4DesejadoRestante, $('#cmb4D_desejado').val());
    }

    if (nrQuestao == 5 || nrQuestao == 0) {
        loadComboDesejado('cmb5A_desejado', vlQ5DesejadoRestante, $('#cmb5A_desejado').val());
        loadComboDesejado('cmb5B_desejado', vlQ5DesejadoRestante, $('#cmb5B_desejado').val());
        loadComboDesejado('cmb5C_desejado', vlQ5DesejadoRestante, $('#cmb5C_desejado').val());
        loadComboDesejado('cmb5D_desejado', vlQ5DesejadoRestante, $('#cmb5D_desejado').val());
    }

    if (nrQuestao == 6 || nrQuestao == 0) {
        loadComboDesejado('cmb6A_desejado', vlQ6DesejadoRestante, $('#cmb6A_desejado').val());
        loadComboDesejado('cmb6B_desejado', vlQ6DesejadoRestante, $('#cmb6B_desejado').val());
        loadComboDesejado('cmb6C_desejado', vlQ6DesejadoRestante, $('#cmb6C_desejado').val());
        loadComboDesejado('cmb6D_desejado', vlQ6DesejadoRestante, $('#cmb6D_desejado').val());
    }
};

const resetAllCombosDesejado = (nrQuestao) => {

    if (nrQuestao == 1) {
        resetComboDesejado('cmb1A_desejado', $('#cmb1A_desejado').val());
        resetComboDesejado('cmb1B_desejado', $('#cmb1B_desejado').val());
        resetComboDesejado('cmb1C_desejado', $('#cmb1C_desejado').val());
        resetComboDesejado('cmb1D_desejado', $('#cmb1D_desejado').val());
    }

    if (nrQuestao == 2) {
        resetComboDesejado('cmb2A_desejado', $('#cmb2A_desejado').val());
        resetComboDesejado('cmb2B_desejado', $('#cmb2B_desejado').val());
        resetComboDesejado('cmb2C_desejado', $('#cmb2C_desejado').val());
        resetComboDesejado('cmb2D_desejado', $('#cmb2D_desejado').val());
    }

    if (nrQuestao == 3) {
        resetComboDesejado('cmb3A_desejado', $('#cmb3A_desejado').val());
        resetComboDesejado('cmb3B_desejado', $('#cmb3B_desejado').val());
        resetComboDesejado('cmb3C_desejado', $('#cmb3C_desejado').val());
        resetComboDesejado('cmb3D_desejado', $('#cmb3D_desejado').val());
    }

    if (nrQuestao == 4) {
        resetComboDesejado('cmb4A_desejado', $('#cmb4A_desejado').val());
        resetComboDesejado('cmb4B_desejado', $('#cmb4B_desejado').val());
        resetComboDesejado('cmb4C_desejado', $('#cmb4C_desejado').val());
        resetComboDesejado('cmb4D_desejado', $('#cmb4D_desejado').val());
    }

    if (nrQuestao == 5) {
        resetComboDesejado('cmb5A_desejado', $('#cmb5A_desejado').val());
        resetComboDesejado('cmb5B_desejado', $('#cmb5B_desejado').val());
        resetComboDesejado('cmb5C_desejado', $('#cmb5C_desejado').val());
        resetComboDesejado('cmb5D_desejado', $('#cmb5D_desejado').val());
    }

    if (nrQuestao == 6) {
        resetComboDesejado('cmb6A_desejado', $('#cmb6A_desejado').val());
        resetComboDesejado('cmb6B_desejado', $('#cmb6B_desejado').val());
        resetComboDesejado('cmb6C_desejado', $('#cmb6C_desejado').val());
        resetComboDesejado('cmb6D_desejado', $('#cmb6D_desejado').val());
    }
};


const onChangeQ1CmbDesejado = () => {
    let cmb1A_Desejado = $('#cmb1A_desejado').val() == '' ? 0 : parseInt($('#cmb1A_desejado').val());
    loadAvgByItem('A', 'desejado');
    let cmb1B_Desejado = $('#cmb1B_desejado').val() == '' ? 0 : parseInt($('#cmb1B_desejado').val());
    loadAvgByItem('B', 'desejado');
    let cmb1C_Desejado = $('#cmb1C_desejado').val() == '' ? 0 : parseInt($('#cmb1C_desejado').val());
    loadAvgByItem('C', 'desejado');
    let cmb1D_Desejado = $('#cmb1D_desejado').val() == '' ? 0 : parseInt($('#cmb1D_desejado').val());
    loadAvgByItem('D', 'desejado');

    vlQ1DesejadoRestante = totalVlQ1Desejado - (cmb1A_Desejado + cmb1B_Desejado + cmb1C_Desejado + cmb1D_Desejado);
    resetAllCombosDesejado(1);
    loadAllCombosDesejado(1);
    $('#total-desejado-q1').text(cmb1A_Desejado + cmb1B_Desejado + cmb1C_Desejado + cmb1D_Desejado + '/100'); 
}

const onChangeQ2CmbDesejado = () => {
    let cmb2A_Desejado = $('#cmb2A_desejado').val() == '' ? 0 : parseInt($('#cmb2A_desejado').val());
    loadAvgByItem('A', 'desejado');
    let cmb2B_Desejado = $('#cmb2B_desejado').val() == '' ? 0 : parseInt($('#cmb2B_desejado').val());
    loadAvgByItem('B', 'desejado');
    let cmb2C_Desejado = $('#cmb2C_desejado').val() == '' ? 0 : parseInt($('#cmb2C_desejado').val());
    loadAvgByItem('C', 'desejado');
    let cmb2D_Desejado = $('#cmb2D_desejado').val() == '' ? 0 : parseInt($('#cmb2D_desejado').val());
    loadAvgByItem('D', 'desejado');

    vlQ2DesejadoRestante = totalVlQ2Desejado - (cmb2A_Desejado + cmb2B_Desejado + cmb2C_Desejado + cmb2D_Desejado);
    resetAllCombosDesejado(2);
    loadAllCombosDesejado(2);
    $('#total-desejado-q2').text(cmb2A_Desejado + cmb2B_Desejado + cmb2C_Desejado + cmb2D_Desejado + '/100');
}

const onChangeQ3CmbDesejado = () => {
    let cmb3A_Desejado = $('#cmb3A_desejado').val() == '' ? 0 : parseInt($('#cmb3A_desejado').val());
    loadAvgByItem('A', 'desejado');
    let cmb3B_Desejado = $('#cmb3B_desejado').val() == '' ? 0 : parseInt($('#cmb3B_desejado').val());
    loadAvgByItem('B', 'desejado');
    let cmb3C_Desejado = $('#cmb3C_desejado').val() == '' ? 0 : parseInt($('#cmb3C_desejado').val());
    loadAvgByItem('C', 'desejado');
    let cmb3D_Desejado = $('#cmb3D_desejado').val() == '' ? 0 : parseInt($('#cmb3D_desejado').val());
    loadAvgByItem('D', 'desejado');

    vlQ3DesejadoRestante = totalVlQ3Desejado - (cmb3A_Desejado + cmb3B_Desejado + cmb3C_Desejado + cmb3D_Desejado);
    resetAllCombosDesejado(3);
    loadAllCombosDesejado(3);
    $('#total-desejado-q3').text(cmb3A_Desejado + cmb3B_Desejado + cmb3C_Desejado + cmb3D_Desejado + '/100');
}

const onChangeQ4CmbDesejado = () => {
    let cmb4A_Desejado = $('#cmb4A_desejado').val() == '' ? 0 : parseInt($('#cmb4A_desejado').val());
    loadAvgByItem('A', 'desejado');
    let cmb4B_Desejado = $('#cmb4B_desejado').val() == '' ? 0 : parseInt($('#cmb4B_desejado').val());
    loadAvgByItem('B', 'desejado');
    let cmb4C_Desejado = $('#cmb4C_desejado').val() == '' ? 0 : parseInt($('#cmb4C_desejado').val());
    loadAvgByItem('C', 'desejado');
    let cmb4D_Desejado = $('#cmb4D_desejado').val() == '' ? 0 : parseInt($('#cmb4D_desejado').val());
    loadAvgByItem('D', 'desejado');

    vlQ4DesejadoRestante = totalVlQ4Desejado - (cmb4A_Desejado + cmb4B_Desejado + cmb4C_Desejado + cmb4D_Desejado);
    resetAllCombosDesejado(4);
    loadAllCombosDesejado(4);
    $('#total-desejado-q4').text(cmb4A_Desejado + cmb4B_Desejado + cmb4C_Desejado + cmb4D_Desejado + '/100');
}

const onChangeQ5CmbDesejado = () => {
    let cmb5A_Desejado = $('#cmb5A_desejado').val() == '' ? 0 : parseInt($('#cmb5A_desejado').val());
    loadAvgByItem('A', 'desejado');
    let cmb5B_Desejado = $('#cmb5B_desejado').val() == '' ? 0 : parseInt($('#cmb5B_desejado').val());
    loadAvgByItem('B', 'desejado');
    let cmb5C_Desejado = $('#cmb5C_desejado').val() == '' ? 0 : parseInt($('#cmb5C_desejado').val());
    loadAvgByItem('C', 'desejado');
    let cmb5D_Desejado = $('#cmb5D_desejado').val() == '' ? 0 : parseInt($('#cmb5D_desejado').val());
    loadAvgByItem('D', 'desejado');

    vlQ5DesejadoRestante = totalVlQ5Desejado - (cmb5A_Desejado + cmb5B_Desejado + cmb5C_Desejado + cmb5D_Desejado);
    resetAllCombosDesejado(5);
    loadAllCombosDesejado(5);
    $('#total-desejado-q5').text(cmb5A_Desejado + cmb5B_Desejado + cmb5C_Desejado + cmb5D_Desejado + '/100');
}

const onChangeQ6CmbDesejado = () => {
    let cmb6A_Desejado = $('#cmb6A_desejado').val() == '' ? 0 : parseInt($('#cmb6A_desejado').val());
    loadAvgByItem('A', 'desejado');
    let cmb6B_Desejado = $('#cmb6B_desejado').val() == '' ? 0 : parseInt($('#cmb6B_desejado').val());
    loadAvgByItem('B', 'desejado');
    let cmb6C_Desejado = $('#cmb6C_desejado').val() == '' ? 0 : parseInt($('#cmb6C_desejado').val());
    loadAvgByItem('C', 'desejado');
    let cmb6D_Desejado = $('#cmb6D_desejado').val() == '' ? 0 : parseInt($('#cmb6D_desejado').val());
    loadAvgByItem('D', 'desejado');

    vlQ6DesejadoRestante = totalVlQ6Desejado - (cmb6A_Desejado + cmb6B_Desejado + cmb6C_Desejado + cmb6D_Desejado);
    resetAllCombosDesejado(6);
    loadAllCombosDesejado(6);
    $('#total-desejado-q6').text(cmb6A_Desejado + cmb6B_Desejado + cmb6C_Desejado + cmb6D_Desejado + '/100');
}

const loadComboDesejado = (comboName, vlRestante = 100, vlDesejado = 0) => {
    let vlrComboDesejado = vlDesejado == ('' || null) ? 0 : vlDesejado;
    let vlRestanteDesejado = vlRestante + parseInt(vlrComboDesejado);
    
    for (var i = 0; i <= vlRestanteDesejado; i++) {
        if (vlDesejado === i)
            $('#' + comboName).append(`<option value='${i}' selected>${i}</option>`);
        else
            $('#' + comboName).append(`<option value='${i}'>${i}</option>`);
    }

}

const resetComboDesejado = (comboName, vlDesejado = 0) => {
    if (vlDesejado == '')
        $('#' + comboName).html(`<select id="${comboName}"><option value="" selected>Desejado</option></select>`);
    $('#' + comboName).html(`<select id="${comboName}"><option value="${vlDesejado}">${vlDesejado}</option></select>`);
}
// #endregion


// #region private_functions
const SalvarQuestionario = () => {
    
    let Medias = new Array();
    let VlrMdA = $('#itemA-avg').text();
    let VlrMdA_desejado = $('#itemA-avg-desejado').text();
    let IdUsuario = $('#hdd-user-id-session').val() || 0;
    let MdA = {
        IdMedia: 0,
        IdUsuario,       
        Item: 'A',
        ValorAtual: parseFloat(VlrMdA),
        ValorDesejado: parseFloat(VlrMdA_desejado)
    };

    let MdB = {
        IdMedia: 0,
        IdUsuario,       
        Item: 'B',
        ValorAtual: parseFloat($('#itemB-avg').text()),
        ValorDesejado: parseFloat($('#itemB-avg-desejado').text())
    };

    let MdC = {
        IdMedia: 0,
        IdUsuario,       
        Item: 'C',
        ValorAtual: parseFloat($('#itemC-avg').text()),
        ValorDesejado: parseFloat($('#itemC-avg-desejado').text())
    };

    let MdD = {
        IdMedia: 0,
        IdUsuario,       
        Item: 'D',
        ValorAtual: parseFloat($('#itemD-avg').text()),
        ValorDesejado: parseFloat($('#itemD-avg-desejado').text())
    };

    Medias.push(MdA);
    Medias.push(MdB);
    Medias.push(MdC);
    Medias.push(MdD);


    var data = {
        Medias
    };

    localStorage.clear();

    $.ajax({
        method: "POST",
        url: "/Common/SalvarQuestionario",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (data) => {

            try {
                if (data.Return == "ok")
                    ShowToastr('Parabéns!', 'Seus dados foram salvos com sucesso!', 3000);
                if (data.Error)
                    ShowToastrError('Erro ao salvar as médias', data.Error, 6000);
            }
            catch (err) {
                ShowToastrError('Erro ao salvar as médias', data.Error, 6000);
            }
        }
    });

    event.preventDefault();
};

const HandlerEnterParticipante = (e) => {
    e.which = e.which || e.keyCode;
    if (e.which == 13) {
        if ($('#nome-input').val() == undefined || $('#nome-input').val() == '') {
            alert('Informe o nome do participante');
        }else
            GoToPart2();
    }
}

const validaFormParticipante = () => {
    let isValid = true;
    if ($('#input-nome').val() == '' || $('#input-nome').val() == undefined)
        return false;
    if ($('#input-empresa').val() == '' || $('#input-empresa').val() == undefined)
        return false;
    if ($('#input-depto').val() == '' || $('#input-depto').val() == undefined)
        return false;
    if ($('#input-cargo').val() == '' || $('#input-cargo').val() == undefined)
        return false;
    if ($('#input-email').val() == '' || $('#input-email').val() == undefined)
        return false;

    return isValid;
}

const validaEntradaPaginaForm = () => {
    // Verificar modulo por url.
    let url = new URL(window.location)
    url = String(url)
    
    if (url.includes('/form') || url.includes('/Form'))
        if (localStorage.getItem('isFormOk') != 'true') {
            localStorage.clear();
            var urlLogin = 'http://' + window.location.hostname + ':' + window.location.port + '/Questionario/Index';
            window.location.replace(urlLogin);
            return;
        }
    
};

const exibeGridResultados = () => {
    $('.grid-resultado').show();
    $('.btn-salvar-form').show();    
    $('#btn-resultados').attr('disabled', true);
}

const GoToPart1 = () => {
    $('.input-name-form').show();
    $('.inputs-form').hide();
    $('.msg-sucesso').hide();
}

const GoToPart2 = () => {
    $('.input-name-form').hide();
    $('.inputs-form').show();
    $('.msg-sucesso').hide();
}

const GoToPart3 = () => {
    $('.input-name-form').hide();
    $('.inputs-form').hide();
    $('.msg-sucesso').show();
}

const loadAvgByItem = (item, tpAtual_Desejado = 'atual') => {    
    let Q1 = 0;
    let Q2 = 0;
    let Q3 = 0;
    let Q4 = 0;
    let Q5 = 0;
    let Q6 = 0;

    let result_avg = 0;
    let tpInput = tpAtual_Desejado == 'atual' ? 'atual' : 'desejado';

    // #region load_avg
        Q1 = $(`#cmb1${item}_${tpAtual_Desejado}`).val();
        if (Q1 != undefined && Q1 != '')
            Q1 = parseInt(Q1);
        else
            Q1 = 0;

        Q2 = $(`#cmb2${item}_${tpAtual_Desejado}`).val();
        if (Q2 != undefined && Q2 != '')
            Q2 = parseInt(Q2);
        else
            Q2 = 0;

        Q3 = $(`#cmb3${item}_${tpAtual_Desejado}`).val();
        if (Q3 != undefined && Q3 != '')
            Q3 = parseInt(Q3);
        else
            Q3 = 0;

        Q4 = $(`#cmb4${item}_${tpAtual_Desejado}`).val();
        if (Q4 != undefined && Q4 != '')
            Q4 = parseInt(Q4);
        else
            Q4 = 0;

        Q5 = $(`#cmb5${item}_${tpAtual_Desejado}`).val();
        if (Q5 != undefined && Q5 != '')
            Q5 = parseInt(Q5);
        else
            Q5 = 0;

        Q6 = $(`#cmb6${item}_${tpAtual_Desejado}`).val();
        if (Q6 != undefined && Q6 != '')
            Q6 = parseInt(Q6);
        else
            Q6 = 0;

        result_avg = (Q1 + Q2 + Q3 + Q4 + Q5 + Q6) / 6;
        result_avg = result_avg.toFixed(3) + '';
        result_avg = result_avg.slice(0, -1);

        
        if (tpAtual_Desejado == 'atual')
            $(`#item${item}-avg`).text(result_avg);
        else
            $(`#item${item}-avg-desejado`).text(result_avg);
    // #endregion   
}

// #endregion