using meuprojeto_web.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;

namespace meuprojeto_web.Comandos.Entrada.Produto
{
    public class ProcurarProdutoEntrada
    {
        public int? Id { get; set; }

        public int? FornecedorId { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataFabricacao { get; set; }

        public DateTime? DataValidade { get; set; }

        public bool? Ativo { get; set; }

        public int? PaginaIndex { get; }

        public int? PaginaTamanho { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProdutoOrdenarPor? OrdenarPor { get; }

        public string OrdenarSentido { get; }

        public ProcurarProdutoEntrada(ProdutoOrdenarPor? ordenarPor = ProdutoOrdenarPor.DataFabricacao, string ordenarSentido = "ASC", int? paginaIndex = null, int? paginaTamanho = null)
        {
            this.OrdenarPor = ordenarPor;
            this.OrdenarSentido = ordenarSentido;
            this.PaginaIndex = paginaIndex;
            this.PaginaTamanho = paginaTamanho;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}