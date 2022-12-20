using meuprojeto_web.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace meuprojeto_web.Comandos.Entrada.Fornecedor
{
    public class ProcurarFornecedorEntrada
    {
        public int? Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Cnpj { get; set; }

        public bool? Ativo { get; set; }

        public int? PaginaIndex { get; }

        public int? PaginaTamanho { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FornecedorOrdenarPor? OrdenarPor { get; }

        public string OrdenarSentido { get; }

        public ProcurarFornecedorEntrada(FornecedorOrdenarPor? ordenarPor = FornecedorOrdenarPor.Nome, string ordenarSentido = "ASC", int? paginaIndex = null, int? paginaTamanho = null)
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
