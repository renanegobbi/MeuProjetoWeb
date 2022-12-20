using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace meuprojeto_web.Comandos.Entrada.Fornecedor
{
    public class AlterarFornecedorEntrada
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Cnpj { get; set; }

        public bool Ativo { get; set; }

        public AlterarFornecedorEntrada(int id, string nome, string descricao, string ativo, string cnpj)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Cnpj = cnpj;
            this.Ativo = ativo == "S" ? true : false;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
