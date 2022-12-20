// Para o funcionamento desse script é obrigatório a utilização do componente Jquery-Confirm disponível em https://craftpip.github.io/jquery-confirm/
var AppModal = function () {

    //$(".jconfirm-title-c").remove();

    var arrModal = [];
    var arrModalPermanecerAberto = [];

    return {

        // Exibe um popup utilizando o plugin Jquery Confirm
        exibirPorHtml: function (conteudoHtml, openCallback, fecharAoClicarBg, permanecerAberto, titulo) {

            let jc = $.dialog({
                boxWidth: '95%',
                useBootstrap: false ,
                content: conteudoHtml,
                title: false,
                closeIcon: false,
                backgroundDismiss: (fecharAoClicarBg == null ? false : fecharAoClicarBg),
                columnClass: 'xlarge',
                offsetTop: 10,
                offsetBottom: 10,
                backgroundDismissAnimation: 'none',
                onOpen: function () {
                    $("body").css("overflow", "hidden");

                    this.$content.find(".btn-fechar").click(function () {
                        $("body").css("overflow", "auto");
                        jc.close();
                    });

                    if (openCallback != null) {
                        openCallback();
                    }
                }
            });

            // Quando o método "ocultarModal" for chamado, ocultará todos os modais com exceção dos que a propriedade "permanecerAberto" for true
            if (permanecerAberto == null || !permanecerAberto)
                arrModal.push(jc);
            else {
                arrModalPermanecerAberto.push(jc);
            }

            return jc;
        },

        // Exibe um modal baseado no contéudo de uma rota
        exibirPorRota: function (rota, openCallback, permanecerAberto, titulo) {
            $.get(rota, function (html) {
                AppModal.exibirPorHtml(html, openCallback, false, permanecerAberto, titulo);
            }).fail(function (jqXhr) {
                let feedback = Feedback.converter(jqXhr.responseJSON);
                feedback.exibir();
            });
        },

        // Exibe um modal de confirmação utilizando o plugin "jQuery-Confirm"
        exibirConfirmacao: function (mensagem, textoBotaoSim, textoBotaoNao, simCallback, naoCallback, titulo) {
            swal({
                title: titulo ?? 'Você tem certeza?',
                html: mensagem,
                type: "question",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: textoBotaoSim,
                cancelButtonText: textoBotaoNao,
                // allowOutsideClick: true
                allowOutsideClick: false
            }).then(function () {
                if (simCallback != null)
                    simCallback();
            }, function () {
                if (naoCallback != null)
                    naoCallback();
            });
        },

        // Oculta todos os modais exibidos
        ocultar: function (fecharTudo) {
            $.each(arrModal, function (i, modal) {
                modal.close();
            });

            if (fecharTudo != null && fecharTudo) {
                $.each(arrModalPermanecerAberto, function (i, modal) {
                    modal.close();
                });
            }

            $("body").css("overflow", "auto");
        },

        // Oculta um model a partir do seu título
        ocultarPorTitulo: function (titulo) {
            $.each(arrModal, function (i, modal) {
                if (modal.title === titulo) {
                    modal.close();
                    return;
                }
            });

            $.each(arrModalPermanecerAberto, function (i, modal) {
                if (modal.title === titulo) {
                    modal.close();
                    return;
                }
            });
        }
    }
}();