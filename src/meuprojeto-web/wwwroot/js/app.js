// Tipos de notificação
var TipoNotificacao = {
    Info: { Modal: 'info' },
    Aviso: { Modal: 'warning' },
    Erro: { Modal: 'danger' },
    Sucesso: { Modal: 'success' },
};

// Funções globais para serem utilizadas por todo o sistema
var App = function () {

    return {

        // Bloqueia a página ou um elemento específico, indicando que algum processamento está sendo realizado
        bloquear: function (options) {
            options = $.extend(true, {}, options);

            var html = '<div class="loading-message loading-message-boxed"><img src="' + _appPath + "images/loading.gif" + '"/></div>';

            if (options.target) { // element blocking
                var el = $(options.target);
                if (el.height() <= ($(window).height())) {
                    options.cenrerY = true;
                }
                el.block({
                    message: html,
                    baseZ: options.zIndex ? options.zIndex : 10000000000000000,
                    centerY: options.cenrerY !== undefined ? options.cenrerY : false,
                    css: {
                        top: '10%',
                        border: '0',
                        padding: '0',
                        backgroundColor: 'none'
                    },
                    overlayCSS: {
                        backgroundColor: options.overlayColor ? options.overlayColor : '#ccc',
                        opacity: 0.4,
                        cursor: 'wait'
                    }
                });
            } else { // page blocking
                $.blockUI({
                    message: html,
                    baseZ: options.zIndex ? options.zIndex : 10000000000000000,
                    css: {
                        border: '0',
                        padding: '0',
                        backgroundColor: 'none'
                    },
                    overlayCSS: {
                        backgroundColor: options.overlayColor ? options.overlayColor : '#ccc',
                        opacity: 0.4,
                        cursor: 'wait'
                    }
                });
            }
        },

        // Desbloqueia a página ou um elemento específico
        desbloquear: function (target) {
            if (target) {
                $(target).unblock({
                    onUnblock: function () {
                        $(target).css('position', '');
                        $(target).css('zoom', '');
                    }
                });
            }
            else {
                $.unblockUI();
            }
        },

        exibirSweetAlert: function (tipo, titulo, mensagem, fecharCallback) {
            var icone = "info";

            if (tipo === TipoNotificacao.Aviso)
                icone = "warning";
            else if (tipo === TipoNotificacao.Erro)
                icone = "error";
            else if (tipo === TipoNotificacao.Sucesso)
                icone = "success";

            swal({
                title: titulo,
                html: mensagem,
                type: icone,
                confirmButtonColor: "#337AB7",
                showCancelButton: false,
                allowOutsideClick: false
            }).then(function () {
                if (fecharCallback != null) {
                    fecharCallback();
                    swal.close();
                } else {
                    swal.close();
                }
            });
        },

        exibirSweetAlertPorMensagem: function (mensagem, fecharCallback) {
            var tipo = TipoNotificacao.Sucesso;

            switch (mensagem.Tipo) {
                case 1: { tipo = TipoNotificacao.Info; break; }
                case 2: { tipo = TipoNotificacao.Aviso; break; }
                case 3: { tipo = TipoNotificacao.Erro; break; }
                default: { tipo = TipoNotificacao.Info; break; }
            }

            this.exibirSweetAlert(tipo, mensagem.Titulo, mensagem.Mensagem, function () {
                if (fecharCallback != null) {
                    fecharCallback();
                } else {
                    if (mensagem.TipoAcao == null)
                        swal.close();
                    else {
                        switch (mensagem.TipoAcao) {
                            case 1: { location.href = Cade.obterUrlPortal(); break; }
                            case 2: { window.history.back(); break; }
                            case 3: { window.close(); break; }
                            case 4: { location.href = "procurar-protocolo"; break; }
                            case 5: { swal.close(); break; }
                            default: { swal.close(); break; }
                        }
                    }
                }
            });
        },

        exibirSweetAlertPorJqXHR: function (jqXhr, fecharCallback) {
            if (jqXhr.responseJSON != null) {
                var feedback = Feedback.converter(jqXhr.responseJSON);

                feedback.exibir(fecharCallback);
            } else {
                this.exibirSweetAlert(TipoNotificacao.Erro, "Erro", jqXhr.statusText, fecharCallback);
            }
        },

        exibirSweetAlertConfirmacao: function (mensagem, titulo, textoBotaoSim, textoBotaoNao, simCallback, naoCallback) {
            swal({
                title: titulo,
                html: mensagem,
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: textoBotaoSim,
                cancelButtonText: textoBotaoNao,
                allowOutsideClick: false
            }).then(function () {
                if (simCallback != null)
                    simCallback();
            }, function () {
                if (naoCallback != null)
                    naoCallback();
            });
        },

        corrigirPathRota: function (rota) {
            return _appPath + rota;
        },

        aplicarMascaraInscricaoEstadual: function (input) {
            $(input).inputmask({ "mask": "999.999.99-9", "clearIncomplete": true });
        },

        aplicarMascaraCnpj: function (input) {
            $(input).inputmask({ "mask": "99.999.999/9999-99", "clearIncomplete": true });
        },

        aplicarMascaraRaizCnpj: function (input) {
            $(input).inputmask({ "mask": "99.999.999" });
        },

        aplicarMascaraAno: function (input) {
            $(input).inputmask({ "mask": "(9999)" });
        },

        aplicarMascaraDiaMesAno: function (input) {
            $(input).inputmask({ "mask": "99/99/9999" });
        },

        configuraDataTables: function () {
            $.extend(true, $.fn.dataTable.defaults, {
                language: {
                    "lengthMenu": "Apresentar _MENU_ linhas por página",
                    "zeroRecords": "Não há registros",
                    "search": "Pesquisar:",
                    "emptyTable": "Nenhum registro encontrado",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                    "infoEmpty": "Não há registros",
                    "infoFiltered": "(Filtrando de _MAX_ registros)",
                    "loadingRecords": "Carregando...",
                    "processing": "Processando...",
                    "paginate": {
                        "first": "Primeiro",
                        "last": "Último",
                        "next": "Próximo",
                        "previous": "Anterior"
                    }
                }
            });
        }
    };
}();

// Parametrização do plugin JQuery Validation
if ($.validator != null) {
    $.extend($.validator, {
        messages: {
            required: "Obrigat&oacute;rio.",
            remote: "Por favor, corrija este campo.",
            email: "Forne&ccedil;a um endere&ccedil;o eletr&ocirc;nico v&aacute;lido.",
            url: "Forne&ccedil;a uma URL v&aacute;lida.",
            date: "Forne&ccedil;a uma data v&aacute;lida.",
            dateITA: "Forne&ccedil;a uma data v&aacute;lida.",
            dateISO: "Forne&ccedil;a uma data v&aacute;lida (ISO).",
            number: "Forne&ccedil;a um n&uacute;mero v&aacute;lido.",
            digits: "Forne&ccedil;a somente d&iacute;gitos.",
            creditcard: "Forne&ccedil;a um cart&atilde;o de cr&eacute;dito v&aacute;lido.",
            equalTo: "Forne&ccedil;a o mesmo valor novamente.",
            accept: "Selecione um arquivo com uma extens&atilde;o v&aacute;lida.",
            maxlength: $.validator.format("Forne&ccedil;a n&atilde;o mais que {0} caracteres."),
            minlength: $.validator.format("Forne&ccedil;a ao menos {0} caracteres."),
            rangelength: $.validator.format("Forne&ccedil;a um valor entre {0} e {1} caracteres de comprimento."),
            range: $.validator.format("Forne&ccedil;a um valor entre {0} e {1}."),
            max: $.validator.format("Forne&ccedil;a um valor menor ou igual a {0}."),
            min: $.validator.format("Forne&ccedil;a um valor maior ou igual a {0}.")
        }
    });

    $.validator.setDefaults({
        errorElement: 'label',
        errorClass: 'error',
        focusInvalid: false,
        ignore: "",
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },

        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },

        success: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        }
    });
}

// Toggle
(function (theme, $) {

    theme = theme || {};

    var instanceName = '__toggle';

    var PluginToggle = function ($el, opts) {
        return this.initialize($el, opts);
    };

    PluginToggle.defaults = {
        duration: 350,
        isAccordion: false,
        addIcons: true
    };

    PluginToggle.prototype = {
        initialize: function ($el, opts) {
            if ($el.data(instanceName)) {
                return this;
            }

            this.$el = $el;

            this
                .setData()
                .setOptions(opts)
                .build();

            return this;
        },

        setData: function () {
            this.$el.data(instanceName, this);

            return this;
        },

        setOptions: function (opts) {
            this.options = $.extend(true, {}, PluginToggle.defaults, opts, {
                wrapper: this.$el
            });

            return this;
        },

        build: function () {
            var self = this,
                $wrapper = this.options.wrapper,
                $items = $wrapper.find('.toggle'),
                $el = null;

            $items.each(function () {
                $el = $(this);

                if ($el.hasClass('active')) {
                    $el.find('> p').addClass('preview-active');
                    $el.find('> .toggle-content').slideDown(self.options.duration);
                }

                self.events($el);
            });

            if (self.options.isAccordion) {
                self.options.duration = self.options.duration / 2;
            }

            return this;
        },

        events: function ($el) {
            var self = this,
                previewParCurrentHeight = 0,
                previewParAnimateHeight = 0,
                toggleContent = null;

            $el.find('> label').click(function (e) {

                var $this = $(this),
                    parentSection = $this.parent(),
                    parentWrapper = $this.parents('.toggle'),
                    previewPar = null,
                    closeElement = null;

                if (self.options.isAccordion && typeof (e.originalEvent) != 'undefined') {
                    closeElement = parentWrapper.find('.toggle.active > label');

                    if (closeElement[0] == $this[0]) {
                        return;
                    }
                }

                parentSection.toggleClass('active');

                // Preview Paragraph
                if (parentSection.find('> p').get(0)) {

                    previewPar = parentSection.find('> p');
                    previewParCurrentHeight = previewPar.css('height');
                    previewPar.css('height', 'auto');
                    previewParAnimateHeight = previewPar.css('height');
                    previewPar.css('height', previewParCurrentHeight);

                }

                // Content
                toggleContent = parentSection.find('> .toggle-content');

                if (parentSection.hasClass('active')) {

                    $(previewPar).animate({
                        height: previewParAnimateHeight
                    }, self.options.duration, function () {
                        $(this).addClass('preview-active');
                    });

                    toggleContent.slideDown(self.options.duration, function () {
                        if (closeElement) {
                            closeElement.trigger('click');
                        }
                    });

                } else {

                    $(previewPar).animate({
                        height: 0
                    }, self.options.duration, function () {
                        $(this).removeClass('preview-active');
                    });

                    toggleContent.slideUp(self.options.duration);

                }

            });
        }
    };

    // expose to scope
    $.extend(theme, {
        PluginToggle: PluginToggle
    });

    // jquery plugin
    $.fn.themePluginToggle = function (opts) {
        return this.map(function () {
            var $this = $(this);

            if ($this.data(instanceName)) {
                return $this.data(instanceName);
            } else {
                return new PluginToggle($this, opts);
            }

        });
    }

}).apply(this, [window.theme, jQuery]);

// Toggle
(function ($) {

    'use strict';

    $(function () {
        $('[data-plugin-toggle]').each(function () {
            var $this = $(this),
                opts = {};

            var pluginOptions = $this.data('plugin-options');
            if (pluginOptions)
                opts = pluginOptions;

            $this.themePluginToggle(opts);
        });
    });

}).apply(this, [jQuery]);

// Datepicker
(function (theme, $) {

    theme = theme || {};

    var instanceName = '__datepicker';

    var PluginDatePicker = function ($el, opts) {
        return this.initialize($el, opts);
    };

    PluginDatePicker.defaults = {
    };

    PluginDatePicker.prototype = {
        initialize: function ($el, opts) {
            if ($el.data(instanceName)) {
                return this;
            }

            this.$el = $el;

            this
                .setVars()
                .setData()
                .setOptions(opts)
                .build();

            return this;
        },

        setVars: function () {
            this.skin = this.$el.data('plugin-skin');

            return this;
        },

        setData: function () {
            this.$el.data(instanceName, this);

            return this;
        },

        setOptions: function (opts) {
            this.options = $.extend(true, {}, PluginDatePicker.defaults, opts);

            return this;
        },

        build: function () {
            this.$el.datepicker(this.options);

            if (!!this.skin) {
                this.$el.data('datepicker').picker.addClass('datepicker-' + this.skin);
            }

            return this;
        }
    };

    // expose to scope
    $.extend(theme, {
        PluginDatePicker: PluginDatePicker
    });

    // jquery plugin
    $.fn.themePluginDatePicker = function (opts) {
        return this.each(function () {
            var $this = $(this);

            if ($this.data(instanceName)) {
                return $this.data(instanceName);
            } else {
                return new PluginDatePicker($this, opts);
            }

        });
    }

}).apply(this, [window.theme, jQuery]);

// Datepicker
(function ($) {

    'use strict';

    if ($.isFunction($.fn['datepicker'])) {

        $(function () {
            $('[data-plugin-datepicker]').each(function () {
                var $this = $(this),
                    opts = {};

                var pluginOptions = $this.data('plugin-options');
                if (pluginOptions)
                    opts = pluginOptions;

                $this.themePluginDatePicker(opts);
            });
        });

    }

}).apply(this, [jQuery]);

$.extend($.fn.datepicker.defaults, {
    autoclose: true,
    language: 'pt-BR',
    format: 'dd/mm/yyyy',
    todayBtn: 'linked',
    todayHighlight: true,
    orientation: "auto right"
});

// Select2 pt-BR
(function () { if (jQuery && jQuery.fn && jQuery.fn.select2 && jQuery.fn.select2.amd) var e = jQuery.fn.select2.amd; return e.define("select2/i18n/pt-BR", [], function () { return { errorLoading: function () { return "Os resultados não puderam ser carregados." }, inputTooLong: function (e) { var t = e.input.length - e.maximum, n = "Apague " + t + " caracter"; return t != 1 && (n += "es"), n }, inputTooShort: function (e) { var t = e.minimum - e.input.length, n = "Digite " + t + " ou mais caracteres"; return n }, loadingMore: function () { return "Carregando mais resultados…" }, maximumSelected: function (e) { var t = "Você só pode selecionar " + e.maximum + " ite"; return e.maximum == 1 ? t += "m" : t += "ns", t }, noResults: function () { return "Nenhum resultado encontrado" }, searching: function () { return "Buscando…" } } }), { define: e.define, require: e.require } })();

$(document).ready(function () {
    $('.somenteNumero').on('keypress', function (evt) {
        let theEvent = evt || window.event;
        let key = theEvent.key;
        //Permitir utilização de atalhos com Ctrl
        let ctrl = evt.ctrlKey;
        //Não permitir espaço
        let espaco = key == ' ' || key == 'Spacebar';
        //let regex = /[a-zA-Z]/;
        let regex = /\D+/g;
        if ((regex.test(key) && key.length == 1 && !ctrl) || espaco) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
        }
    });

    $('.somenteNumero').bind("paste", function (e) {
        e.preventDefault();
        let pastedData = e.originalEvent.clipboardData.getData('Text');
        $(this).val(pastedData.replace(/\D/g, ''));
    });
});