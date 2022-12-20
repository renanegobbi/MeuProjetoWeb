using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace meuprojeto_web.Comandos.Entrada.Fornecedor
{
    public class CadastrarFornecedorEntrada
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Cnpj { get; set; }

        public CadastrarFornecedorEntrada(string nome, string descricao, string cnpj)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Cnpj = cnpj;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
