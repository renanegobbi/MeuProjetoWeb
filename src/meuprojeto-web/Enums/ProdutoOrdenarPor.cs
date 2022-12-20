using System.ComponentModel;

namespace meuprojeto_web.Enums
{
    // classe do tipo enumerable utilizada para ordenar o produto
    public enum ProdutoOrdenarPor
    {
        [Description("Id do produto")]
        Id,

        [Description("Id do fornecedor")]
        FornecedorId,

        [Description("Data de fabricação do produto")]
        DataFabricacao,

        [Description("Data de validade do produto")]
        DataValidade,

        [Description("Status de ativação")]
        Ativo,
    }
}