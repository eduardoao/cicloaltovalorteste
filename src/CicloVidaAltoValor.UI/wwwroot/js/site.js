$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip({ trigger: "hover" });
    $(function () { function a(a, n) { a.each(function () { var a = $(this), i = a.attr("data-ms-animation"), t = a.attr("data-ms-animation-duration"); msAnimationDelay = $.browser.mobile ? .6 : a.attr("data-ms-animation-delay"), a.css({ "-webkit-animation-delay": msAnimationDelay, "-moz-animation-delay": msAnimationDelay, "animation-delay": msAnimationDelay, "-webkit-animation-duration": t, "-moz-animation-duration": t, "-ms-animation-duration": t, "animation-duration": t }); var o = n ? n : a; o.waypoint(function () { a.addClass("animated").addClass(i) }, { triggerOnce: !0, offset: "90%" }) }) } a($(".ms-animation")), a($(".staggered-animation"), $(".staggered-animation-container")) });
    $('input[type="tel"]').mask('(00) 0000-00009');
    $('#cpf').mask('999.999.999-99');
    $('#dataNascimento').mask('99/99/9999');
    $('#cep').mask('99999-999');
    $('#data_nascimento').mask('99/99/9999');
    $('input[type="tel"]').blur(function (event) {
        if ($(this).val().length == 15) { // Celular com 9 dígitos + 2 dígitos DDD e 4 da máscara
            $('input[type="tel"]').mask('(00) 00000-0009');
        } else {
            $('input[type="tel"]').mask('(00) 0000-00009');
        }
    });
    $('.slider_principal').carousel({
        interval: 4500
    });

    $(".login .form-group").click(function () {
        $(".error").addClass("hidden");
        $(this).find("input").focus();
    });


    // $('.ms-animation').addClass('animated');



    $(window).scroll(function () {
        if ($(".progress-bar").length) {
            var hT = $('.progress-bar').offset().top,
                hH = $('.progress-bar').outerHeight(),
                wH = $(window).height(),
                wS = $(this).scrollTop();
            if (wS > (hT + hH - wH)) {
                var progresso = $('.alcacando b').text();
                $(".progress-bar").animate({
                    width: progresso
                }, 2500);
            }
        }
    });





    function limpa_formulario_cep() {
        // Limpa valores do formulário de cep.
        $("#endereco").val("");
        $("#bairro").val("");
        $("#cidade").val("");
        $("#estado").val("");
    }

    function habilita_edicao_cep() {
        $("#endereco").removeAttr("readonly")
        $("#bairro").removeAttr("readonly")
        //$("#cidade").removeAttr("readonly")
        //$("#estado").removeAttr("readonly")
    }
    function desabilita_edicao_cep() {
        $("#endereco").prop("readonly", true)
        $("#bairro").prop("readonly", true)
        //$("#cidade").prop("readonly", true)
        //$("#estado").prop("readonly", true)
    }
    function hasWhiteSpace(s) {
        return s.indexOf(' ') >= 0;
    }

    if ($("#cep").val()) {
        //Nova variável "cep" somente com dígitos.
        var cep = $("#cep").val().replace(/\D/g, '');
        //Verifica se campo cep possui valor informado.
        if (cep != "") {
            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;
            //Valida o formato do CEP.
            if (validacep.test(cep)) {

                if (cep.substring(5) === "000") {
                    $.getJSON("/cep/" + cep, function (response) {
                        if (cep.substring(5) === "000" && response.content.endereco.trim().length == 0 && response.content.bairro.trim().length == 0) {
                            habilita_edicao_cep();
                        } else {
                            desabilita_edicao_cep();
                        }
                    });
                }
            }
        }
    }


    //Quando o campo cep perde o foco.
    $("#cep").blur(function () {

        if (this.value.length == 9) {

            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {

                //Expressão regular para validar o CEP.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#endereco").val("...");
                    $("#bairro").val("...");
                    $("#cidade").val("...");
                    $("#estado").val("...");

                    //Consulta o webservice viacep.com.br/
                    //$.getJSON("//viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {
                    $.getJSON($('#urlCep').val() + cep, function (response) {

                        if (response.valid) {
                            //Atualiza os campos com os valores da consulta.
                            $("#endereco").val(response.content.endereco);
                            $("#bairro").val(response.content.bairro);
                            $("#cidade").val(response.content.cidade);
                            $("#estado").val(response.content.estado);

                            if (cep.substring(5) === "000" && response.content.endereco.trim().length == 0 && response.content.bairro.trim().length == 0) {
                                habilita_edicao_cep();
                            } else {
                                desabilita_edicao_cep();
                                $("#numero").focus();
                            }

                        } else {
                            //CEP pesquisado não foi encontrado.
                            limpa_formulario_cep();
                            $("#cep").val("");
                            //alert("CEP não encontrado.");
                            $('#cep').closest("div").find("span").html("'CEP' inválido.")
                        }
                    }).fail(function (xhr, status) {
                        limpa_formulario_cep();
                        desabilita_edicao_cep();
                    });

                } //end if.
                else {
                    //cep é inválido.
                    limpa_formulario_cep();
                    $("#cep").val("");
                    //alert("Formato de CEP inválido.");
                    $('#cep').closest("div").find("span").html("'CEP' inválido.")
                    desabilita_edicao_cep();
                }
            } //end if.
            else {
                //cep sem valor, limpa formulário.
                limpa_formulario_cep();
                desabilita_edicao_cep();
            }
        }
    });






    /* Ação responsável pelo envio do desejo*/



    /* FIM */





    /* TERMO DE ACEITE CADASTRO 
    $('body.cadastro #aceite').click(function() {
        if ($(this).is(':checked')) {
            $('body.cadastro .btn-amarelo-simples').removeAttr('disabled');
        } else {
            $('body.cadastro .btn-amarelo-simples').attr('disabled', 'disabled');
        }
    });
    FIM TERMO DE ACEITE CADASTRO */


    /* OCULTAR CONTROLES DO BS NO CAROUSEL */
    $('.carousel').carousel({
        interval: false,
        wrap: false
    })
    /* FIM OCULTAR CONTROLES DO BS NO CAROUSEL */



    /* FIM OCULTAR CONTROLES DO BS NO CAROUSEL */
    $('#CarouselBronze').on('slid.bs.carousel', '', checkitemBronze);
    $('#CarouselPrata').on('slid.bs.carousel', '', checkitemPrata);
    $('#CarouselOuro').on('slid.bs.carousel', '', checkitemOuro);

    $('#CarouselBronzeMobileUm').on('slid.bs.carousel', '', checkitemBronzeMobileUm);
    $('#CarouselPrataMobileUm').on('slid.bs.carousel', '', checkitemPrataMobileUm);
    $('#CarouselOuroMobileUm').on('slid.bs.carousel', '', checkitemOuroMobileUm);

    $('#CarouselBronzeMobileDois').on('slid.bs.carousel', '', checkitemBronzeMobileDois);
    $('#CarouselPrataMobileDois').on('slid.bs.carousel', '', checkitemPrataMobileDois);
    $('#CarouselOuroMobileDois').on('slid.bs.carousel', '', checkitemOuroMobileDois);

    $('#CarouselBronzeMobileTres').on('slid.bs.carousel', '', checkitemBronzeMobileTres);
    $('#CarouselPrataMobileTres').on('slid.bs.carousel', '', checkitemPrataMobileTres);
    $('#CarouselOuroMobileTres').on('slid.bs.carousel', '', checkitemOuroMobileTres);

    $(document).ready(function () {               // on document ready
        checkitemOuro();
        checkitemPrata();
        checkitemBronze();

        checkitemBronzeMobileUm();
        checkitemPrataMobileUm();
        checkitemOuroMobileUm();

        checkitemBronzeMobileDois();
        checkitemPrataMobileDois();
        checkitemOuroMobileDois();

        checkitemBronzeMobileTres();
        checkitemPrataMobileTres();
        checkitemOuroMobileTres();

        checkitemListaDeDesejos(); //Pagina Como Funciona
        checkitemListaDeDesejosM(); //Pagina Como Funciona
    });

    function checkitemBronze() {
        var $this = $(this);
        if ($("#CarouselBronze .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselBronze .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };

    function checkitemPrata() {
        var $this = $(this);
        if ($("#CarouselPrata .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselPrata .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };


    function checkitemOuro() {
        var $this = $(this);
        if ($("#CarouselOuro .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselOuro .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };


    function checkitemBronzeMobileUm() {
        var $this = $(this);
        if ($("#CarouselBronzeMobileUm .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselBronzeMobileUm .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };

    function checkitemPrataMobileUm() {
        var $this = $(this);
        if ($("#CarouselPrataMobileUm .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselPrataMobileUm .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };


    function checkitemOuroMobileUm() {
        var $this = $(this);
        if ($("#CarouselOuroMobileUm .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselOuroMobileUm .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };


    function checkitemBronzeMobileDois() {
        var $this = $(this);
        if ($("#CarouselBronzeMobileDois .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselBronzeMobileDois .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };

    function checkitemPrataMobileDois() {
        var $this = $(this);
        if ($("#CarouselPrataMobileDois .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselPrataMobileDois .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };


    function checkitemOuroMobileDois() {
        var $this = $(this);
        if ($("#CarouselOuroMobileDois .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselOuroMobileDois .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };


    function checkitemBronzeMobileTres() {
        var $this = $(this);
        if ($("#CarouselBronzeMobileTres .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselBronzeMobileTres .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };

    function checkitemPrataMobileTres() {
        var $this = $(this);
        if ($("#CarouselPrataMobileTres .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselPrataMobileTres .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };


    function checkitemOuroMobileTres() {
        var $this = $(this);
        if ($("#CarouselOuroMobileTres .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#CarouselOuroMobileTres .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };


    function checkitemListaDeDesejos() {//Pagina Como Funciona
        var $this = $(this);
        if ($("#ListadeDesejosC .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#ListadeDesejosC .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };

    function checkitemListaDeDesejosM() {//Pagina Como Funciona
        var $this = $(this);
        if ($("#ListadeDesejosCM .carousel-inner .item:first").hasClass("active")) {
            $this.children(".left").hide();
            $this.children(".right").show();
        } else if ($("#ListadeDesejosCM .carousel-inner .item:last").hasClass("active")) {
            $this.children(".right").hide();
            $this.children(".left").show();
        } else {
            $this.children(".carousel-control").show();
        }
    };


    checkitemBronze();
    checkitemPrata();
    checkitemOuro();

    checkitemBronzeMobileUm();
    checkitemPrataMobileUm();
    checkitemOuroMobileUm();

    checkitemBronzeMobileDois();
    checkitemPrataMobileDois();
    checkitemOuroMobileDois();

    checkitemBronzeMobileTres();
    checkitemPrataMobileTres();
    checkitemOuroMobileTres();

    checkitemListaDeDesejos(); //Pagina Como Funciona
    checkitemListaDeDesejosM(); //Pagina Como Funciona

    $('#CarouselBronze').on('slid.bs.carousel', '', checkitemBronze);
    $('#CarouselPrata').on('slid.bs.carousel', '', checkitemPrata);
    $('#CarouselOuro').on('slid.bs.carousel', '', checkitemOuro);

    $('#CarouselBronzeMobileUm').on('slid.bs.carousel', '', checkitemBronzeMobileUm);
    $('#CarouselPrataMobileUm').on('slid.bs.carousel', '', checkitemPrataMobileUm);
    $('#CarouselOuroMobileUm').on('slid.bs.carousel', '', checkitemOuroMobileUm);

    $('#CarouselBronzeMobileDois').on('slid.bs.carousel', '', checkitemBronzeMobileDois);
    $('#CarouselPrataMobileDois').on('slid.bs.carousel', '', checkitemPrataMobileDois);
    $('#CarouselOuroMobileDois').on('slid.bs.carousel', '', checkitemOuroMobileDois);

    $('#CarouselBronzeMobileTres').on('slid.bs.carousel', '', checkitemBronzeMobileTres);
    $('#CarouselPrataMobileTres').on('slid.bs.carousel', '', checkitemPrataMobileTres);
    $('#CarouselOuroMobileTres').on('slid.bs.carousel', '', checkitemOuroMobileTres);

    $('#ListadeDesejosC').on('slid.bs.carousel', '', checkitemListaDeDesejos); //Pagina Como Funciona
    $('#ListadeDesejosCM').on('slid.bs.carousel', '', checkitemListaDeDesejosM); //Pagina Como Funciona

    /* FIM OCULTAR CONTROLES DO BS NO CAROUSEL */



    /* ACCORDION FECHANDO OUTROS ELEMENTOS */
    var $dotzAccordion = $('#accordion');
    $dotzAccordion.on('show.bs.collapse', '.collapse', function () {
        $dotzAccordion.find('.collapse.in').collapse('hide');
    });
    /* ACCORDION FECHANDO OUTROS ELEMENTOS */



    /* ANCHOR ANIMADO */
    $(document).ready(function () {
        $('body.desafio-do-genio ul.tarefas li a').click(function () {
            var conteudo = $(this).attr("href");
            $(conteudo).collapse('show');
            var target = $(this.hash);
            $('html,body').animate({
                scrollTop: $(conteudo).offset().top - 164
            }, 800);
            return false;
        });
    });
    /* FIM ANCHOR ANIMADO */



    /* EXECUTA SCRIPT AO TERMINO DO VIDEOS - DESEJOS */
    $(document).ready(function () {

        $(".modal-video .btn-amarelo-simples").click(function () {
            $('.modal-video video')[0].play();
            $('video').on('ended', function () {
                $('.modalPct').modal('hide');
            });
        });
    });
    /* FIM EXECUTA SCRIPT AO TERMINO DO VIDEOS - DESEJOS */



    if ($.fn.cssOriginal != undefined) $.fn.css = $.fn.cssOriginal;
    /* Initialize Document Smooth Scroll for Chrome */
    var ie = (function () {
        var undef,
            v = 3,
            div = document.createElement('div'),
            all = div.getElementsByTagName('i');
        while (
            div.innerHTML = '<!--[if gt IE ' + (++v) + ']><i></i><![endif]-->',
            all[0]
        );
        return v > 4 ? v : undef;
    }());
    /* Initialize Document Smooth Scroll for Chrome */
    if (ie > 9 || !ie) { $.smoothScroll(); }
});