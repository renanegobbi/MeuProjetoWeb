using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace meuprojeto_web.Comandos.Entrada.Produto
{
    public class AlterarProdutoEntrada
    {
        public int? Id { get; set; }

        public int FornecedorId { get; set; }

        public string Descricao { get; set; }

        public DateTime DataFabricacao { get; set; }

        public DateTime DataValidade { get; set; }

        public bool Ativo { get; set; }

        public AlterarProdutoEntrada(int? id, int fornecedorId, string descricao, DateTime dataFabricacao, DateTime dataValidade, string ativo)
        {
            this.Id = id;
            this.FornecedorId = fornecedorId;
            this.Descricao = descricao;
            this.DataFabricacao = dataFabricacao;
            this.DataValidade = dataValidade;
            this.Ativo = ativo == "S" ? true : false;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
