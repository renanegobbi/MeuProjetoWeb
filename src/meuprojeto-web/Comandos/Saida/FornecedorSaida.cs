using Newtonsoft.Json;

namespace meuprojeto_web.Comandos.Saida
{
    public class FornecedorSaida
    {
        /// <summary>
        /// ID do fornecedor
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome do fornecedor
        /// </summary>
        [JsonProperty("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Descrição do fornecedor
        /// </summary>
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        /// <summary>
        /// Cnpj do fornecedor
        /// </summary>
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        /// <summary>
        /// Ativação do fornecedor
        /// </summary>
        [JsonProperty("ativo")]
        public bool? Ativo { get; set; }
    }
}
