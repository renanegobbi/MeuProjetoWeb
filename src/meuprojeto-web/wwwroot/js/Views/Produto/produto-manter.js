var ProdutoManter = function () {

    var _exibirPopupProduto = function (codigo = null) {
        AppModal.exibirPorRota(App.corrigirPathRota("produto/exibir-popup-produto?codigo=" + codigo), function () {

            let cadastro = codigo === null || codigo === undefined || codigo === 0;

            App.aplicarMascaraDiaMesAno($("#iDataFabricacao"));
            App.aplicarMascaraDiaMesAno($("#iDataValidade"));

            $(document).on("input", ".somenteNumero", function () { this.value = this.value.replace(/\D/g, ''); });

            $("#sStatus").select2({
                minimumResultsForSearch: Infinity,
                allowClear: true,
                placeholder: " ",
                dropdownParent: $(".jconfirm")
            });

            let frm = $('#form-manter-produto');

            $('#btn-salvar').click(function () {
                frm.submit();
            });

            frm.validate({
                rules: {
                    iDescricaoFornecedor: {
                        required: true
                    },
                    iFornecedorId: {
                        required: true
                    },
                    iDescricao: {
                        required: true
                    },
                    iDataFabricacao: {
                        required: true
                    },
                    iDataValidade: {
                        required: true
                    },
                    sStatus: {
                        required: true
                    }
                },
                submitHandler: function () {
                    let entrada = {
                        Id: $("#iId").val(),
                        FornecedorId: $("#iFornecedorId").val(),
                        Descricao: $("#iDescricaoFornecedor").val().trim(),
                        DataFabricacao: $("#iDataFabricacao").val(),
                        DataValidade: $("#iDataValidade").val(),
                        Status: $("#sStatus").val()
                    };

                    $.post(App.corrigirPathRota(cadastro ? "produto/cadastrar" : "produto/alterar"), { model: entrada })
                        .done(function (feedbackResult) {
                            AppModal.ocultar();
                            let feedback = Feedback.converter(feedbackResult);
                            feedback.exibir(function () { $("#tblProduto").DataTable().ajax.reload(); });
                        })
                        .fail(function (jqXhr) {
                            let feedback = Feedback.converter(jqXhr.responseJSON);
                            feedback.exibir();
                        });
                }
            });
        });
    };

    var _excluirProduto = function (codigo) {
        AppModal.exibirConfirmacao("Deseja realmente excluir o produto?", "Sim", "Não", function () {
            $.post(App.corrigirPathRota("produto/excluir?codigo=" + codigo))
                .done(function (feedbackResult) {
                    let feedback = Feedback.converter(feedbackResult);
                    feedback.exibir(function () { $("#tblProduto").DataTable().ajax.reload(); });
                })
                .fail(function (jqXhr) {
                    App.exibirSweetAlertPorJqXHR(jqXhr);
                });
        });
    }

    var _initGrid = function () {

        $("#tblProduto").DataTable({
            ajax: {
                url: App.corrigirPathRota("produto/listar"),
                type: "POST",
                error: function (jqXhr) {
                    App.desbloquear();
                    var feedback = Feedback.converter(jqXhr.responseJSON);
                    feedback.exibir();
                },
                data: function (data) {
                    data.id = $("#iProcurarId").val().replace(/[^0-9]/g, '');
                    data.fornecedorId = $("#iProcurarFornecedor").val().replace(/[^0-9]/g, '');
                    data.descricao = $("#iProcurarDescricao").val().trim();
                    data.dataFabricacao = $("#iProcurarFabricacao").val();
                    data.dataValidade = $("#iProcurarValidade").val();
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
                    data: "fornecedorId",
                    class: "text-nowrap text-center",
                    orderable: true
                },
                {
                    data: "descricao",
                    class: "text-nowrap text-center",
                    orderable: true
                },
                {
                    data: "dataFabricacao",
                    class: "text-nowrap text-center",
                    orderable: true
                },
                {
                    data: "dataValidade",
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
                        return "<button type=\"button\" data-codigo=\"" + row.id + "\" class=\"btn btn-xs btn-primary tooltip-left alterar-produto\" data-tooltip=\"Editar\"><i class=\"fa fa-edit fa-fw\"></i></button>";
                    }
                },
                {
                    data: null,
                    width: "1px",
                    class: "text-nowrap text-center",
                    orderable: false,
                    render: function (data, type, row) {
                        return "<button type=\"button\" data-codigo=\"" + row.id + "\" class=\"btn btn-xs btn-danger tooltip-left excluir-produto\" data-tooltip=\"Excluir\"><i class=\"fa fa-trash fa-fw\"></i></button>";
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
            $("#tblProduto").DataTable().columns.adjust();

            $("button[class*='alterar-produto']").each(function () {
                let codigo = $(this).data('codigo');

                $(this).click(function () {
                    _exibirPopupProduto(codigo);
                });
            });

            $("button[class*='excluir-produto']").each(function () {
                let codigo = $(this).data('codigo');

                $(this).click(function () {
                    _excluirProduto(codigo);
                });
            });
        });


    };

    return {
        init: function () {

            _initGrid();

            $(document).on("input", ".somenteNumero", function () { this.value = this.value.replace(/\D/g, ''); });

            App.aplicarMascaraDiaMesAno($("#iProcurarFabricacao"));
            App.aplicarMascaraDiaMesAno($("#iProcurarValidade"));

            $("#btn-filtrar").click(function () {
                var table = $("#tblProduto").DataTable();
                table.page(1);
                table.ajax.reload();
            });

            $("#btn-cadastrar").click(function () {
                _exibirPopupProduto();
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
    ProdutoManter.init();
});