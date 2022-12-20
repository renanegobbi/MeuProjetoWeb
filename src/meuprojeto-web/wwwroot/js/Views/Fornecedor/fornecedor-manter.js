var FornecedorManter = function () {

    var _exibirPopupFornecedor = function (codigo = null) {
        AppModal.exibirPorRota(App.corrigirPathRota("fornecedor/exibir-popup-fornecedor?codigo=" + codigo), function () {

            let cadastro = codigo === null || codigo === undefined || codigo === 0;

            App.aplicarMascaraCnpj($("#iCnpjFornecedor"));

            $("#sStatus").select2({
                minimumResultsForSearch: Infinity,
                allowClear: true,
                placeholder: " ",
                dropdownParent: $(".jconfirm")
            });

            let frm = $('#form-manter-fornecedor');

            $('#btn-salvar').click(function () {
                frm.submit();
            });

            frm.validate({
                rules: {
                    iNomeFornecedor: {
                        required: true
                    },
                    iCnpjFornecedor: {
                        required: true
                    },
                    iDescricaoFornecedor: {
                        required: true
                    }
                },
                submitHandler: function () {
                    let entrada = {
                        Id: $("#iIdFornecedor").val(),
                        Nome: $("#iNomeFornecedor").val().trim(),
                        Descricao: $("#iDescricaoFornecedor").val().trim(),
                        Cnpj: $("#iCnpjFornecedor").val(),
                        Status: $("#sStatus").val()
                    };

                    $.post(App.corrigirPathRota(cadastro ? "fornecedor/cadastrar" : "fornecedor/alterar"), { model: entrada })
                        .done(function (feedbackResult) {
                            AppModal.ocultar();
                            let feedback = Feedback.converter(feedbackResult);
                            feedback.exibir(function () { $("#tblFornecedor").DataTable().ajax.reload(); });
                        })
                        .fail(function (jqXhr) {
                            let feedback = Feedback.converter(jqXhr.responseJSON);
                            feedback.exibir();
                        });
                }
            });
        });
    };

    var _excluirFornecedor = function (codigo) {
        AppModal.exibirConfirmacao("Deseja realmente excluir o fornecedor?", "Sim", "Não", function () {
            $.post(App.corrigirPathRota("fornecedor/excluir?codigo=" + codigo))
                .done(function (feedbackResult) {
                    let feedback = Feedback.converter(feedbackResult);
                    feedback.exibir(function () { $("#tblFornecedor").DataTable().ajax.reload(); });
                })
                .fail(function (jqXhr) {
                    App.exibirSweetAlertPorJqXHR(jqXhr);
                });
        });
    }

    var _initGrid = function () {

        $("#tblFornecedor").DataTable({
            ajax: {
                url: App.corrigirPathRota("fornecedor/listar"),
                type: "POST",
                error: function (jqXhr) {
                    App.desbloquear();
                    var feedback = Feedback.converter(jqXhr.responseJSON);
                    feedback.exibir();
                },
                data: function (data) {
                    data.id = $("#iProcurarId").val().replace(/[^0-9]/g, '');
                    data.nome = $("#iProcurarNome").val().trim();
                    data.descricao = $("#iProcurarDescricao").val().trim();
                    data.cnpj = $("#iProcurarCnpj").val().trim();
                    data.ativo = $("#sProcurarStatus").val();
                }
            },
            autoWidth: true,
            info: false,
            serverSide: true,
            columns: [
                {
                    data: "id",
                    class: "text-nowrap text-center",
                    orderable: true,
                },
                {
                    data: "nome",
                    class: "text-nowrap text-center",
                    orderable: true
                },
                {
                    data: "descricao",
                    class: "text-nowrap text-center",
                    orderable: true
                },
                {
                    data: "cnpj",
                    class: "text-nowrap text-center",
                    orderable: true
                },
                {
                    data: "ativo",
                    class: "text-nowrap text-center",
                    orderable: true
                },
                {
                    data: null,
                    width: "1px",
                    class: "text-nowrap text-center",
                    orderable: false,
                    render: function (data, type, row) {
                        return "<button type=\"button\" data-codigo=\"" + row.id + "\" class=\"btn btn-xs btn-primary tooltip-left alterar-fornecedor\" data-tooltip=\"Editar\"><i class=\"fa fa-edit fa-fw\" data-toogle=\"tooltip\"></i></button>";
                    }
                },
                {
                    data: null,
                    width: "1px",
                    class: "text-nowrap text-center",
                    orderable: false,
                    render: function (data, type, row) {
                        return "<button type=\"button\" data-codigo=\"" + row.id + "\" class=\"btn btn-xs btn-danger tooltip-left excluir-fornecedor\" data-tooltip=\"Excluir\"><i class=\"fa fa-trash fa-fw\"></i></button>";
                    }
                },
            ],
            order: [1, "asc"],
            searching: false,
            paging: true,
            pageLength: 25,
            lengthChange: false,
            processing: false
            //}).on("processing.dt", function () {
            //    App.bloquear();
        }).on('draw.dt', function () {
            $("#div-grid").show();
            $("#tblFornecedor").DataTable().columns.adjust();

            $("button[class*='alterar-fornecedor']").each(function () {
                let codigo = $(this).data('codigo');

                $(this).click(function () {
                    _exibirPopupFornecedor(codigo);
                });
            });

            $("button[class*='excluir-fornecedor']").each(function () {
                let codigo = $(this).data('codigo');

                $(this).click(function () {
                    _excluirFornecedor(codigo);
                });
            });
        });


    };

    return {
        init: function () {

            _initGrid();

            $(document).on("input", ".somenteNumero", function () { this.value = this.value.replace(/\D/g, ''); });
            $(document).on("input", ".somenteLetra", function () { this.value = this.value.replace(/\d/g, ''); });

            App.aplicarMascaraCnpj($("#iProcurarCnpj"));

            $("#btn-filtrar").click(function () {
                var table = $("#tblFornecedor").DataTable();
                table.page(1);
                table.ajax.reload();
            });

            $("#btn-cadastrar").click(function () {
                _exibirPopupFornecedor();
            });

            $("#sProcurarStatus").select2({
                minimumResultsForSearch: Infinity,
                placeholder: " ",
                allowClear: true
            });
        }
    };
}();

$(document).ready(function () {
    App.configuraDataTables();
    FornecedorManter.init();
});


