using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace meuprojeto_web.Comandos.Entrada.Produto
{
    public class CadastrarProdutoEntrada
    {
        public int? FornecedorId { get; set; }

        public string Descricao { get; set; }

        public DateTime DataFabricacao { get; set; }

        public DateTime DataValidade { get; set; }

        public CadastrarProdutoEntrada(int? fornecedorId, string descricao, DateTime dataFabricacao, DateTime dataValidade)
        {
            this.FornecedorId = fornecedorId;
            this.Descricao = descricao;
            this.DataFabricacao = dataFabricacao;
            this.DataValidade = dataValidade;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
