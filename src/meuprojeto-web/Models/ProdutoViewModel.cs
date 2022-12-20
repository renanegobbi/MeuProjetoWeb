using System;

namespace meuprojeto_web.Models
{
    public class ManterProdutoViewModel
    {
        public int? Id { get; set; }

        public int FornecedorId { get; set; }

        public string Descricao { get; set; }

        public DateTime DataFabricacao { get; set; }

        public DateTime DataValidade { get; set; }

        public string Status { get; set; }
    }
}

