$(document).ready(() => {

    $('#chk-consentimento').click(() => {
        $('#msg-consentimento').hide();
    });

    $('#lgpd-submit').click((e) => {
        e.preventDefault();        
        if ($('#chk-consentimento').is(':checked')) {            
            $("form").submit();
        }            
        else
            $('#msg-consentimento').show();
    });
});
