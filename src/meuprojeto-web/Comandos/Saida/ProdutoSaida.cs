using Newtonsoft.Json;
using System;

namespace meuprojeto_web.Comandos.Saida
{
    public class ProdutoSaida
    {
        /// <summary>
        /// ID do produto
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Id do fornecedor
        /// </summary>
        [JsonProperty("fornecedorId")]
        public int FornecedorId { get; set; }

        /// <summary>
        /// Descrição do produto
        /// </summary>
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        /// <summary>
        /// Data de fabricação do produto
        /// </summary>
        [JsonProperty("dataFabricacao")]
        public string DataFabricacao { get; set; }

        /// <summary>
        /// Data de validade do produto
        /// </summary>
        [JsonProperty("dataValidade")]
        public string DataValidade { get; set; }

        /// <summary>
        /// Ativação do produto
        /// </summary>
        [JsonProperty("ativo")]
        public bool? Ativo { get; set; }
    }
}