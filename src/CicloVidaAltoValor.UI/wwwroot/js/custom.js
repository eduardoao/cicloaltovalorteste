(function () {

    //$('article').css({ 'max-height': '258px' });

    /**
     * NAVBAR ACTIVE
     */
    $('li').each(function () {
        $(this).removeClass('active');
    });
    $('ul.nav.navbar-nav').find('a[href="' + location.pathname + '"]').closest('li').addClass('active');


    ///* TERMO DE ACEITE CADASTRO */
    //if ($('body.cadastro #aceite').is(':checked')) {
    //    $('body.cadastro .btn-amarelo-simples').removeAttr('disabled');
    //} else {
    //    $('body.cadastro .btn-amarelo-simples').attr('disabled', 'disabled');
    //}

    /*MOMENT*/
    moment.locale('pt-BR');


    
})();