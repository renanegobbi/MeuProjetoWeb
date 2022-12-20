using System.ComponentModel;

namespace meuprojeto_web.Enums
{
    // classe do tipo enumerable utilizada para ordenar o fornecedor
    public enum FornecedorOrdenarPor
    {
        [Description("Id do fornecedor")]
        Id,

        [Description("Nome do fornecedor")]
        Nome,

        [Description("Descrição do fornecedor")]
        Descricao,

        [Description("Cnpj do fornecedor")]
        Cnpj,

        [Description("Status de ativação")]
        Ativo,
    }
}