using meuprojeto_web.Comandos.Entrada.Produto;
using meuprojeto_web.Comandos.Saida;
using meuprojeto_web.Utils;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace meuprojeto_web.Proxies
{
    public partial class MinhaApiProxy: Notificavel
    {
        /// <summary>
        /// Obtém os produtos baseados nos parâmetros de procura
        /// </summary>
        /// <param name="entrada">Parâmetros de entrada utilizados para realizar a procura.</param>
        public async Task<ProcurarSaida<ProdutoSaida>> ProcurarProdutos(ProcurarProdutoEntrada entrada)
        {
            HttpResponseMessage response = null;

            var content = new StringContent(entrada.ToString(), Encoding.UTF8, "application/json");

            try
            {
                response = await ObterHttpClient().PostAsync($"{_urlMinhaApi}produto/listar", content);

                var saida = await Saida.ExtrairSaidaPorResponse(response, content);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<ProcurarSaida<ProdutoSaida>>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível procurar por produto pela API do MinhaApi: {ex.GetBaseException().Message}", response, ex, content);
            }
        }

        /// <summary>
        /// Obtém um produto a partir do seu id.
        /// </summary>
        /// <param name="id">Id do produto</param>
        public async Task<ProdutoSaida> ObterProdutoPorId(int id)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await ObterHttpClient().GetAsync($"{_urlMinhaApi}produto/obter-por-id?id={id}");

                var saida = await Saida.ExtrairSaidaPorResponse(response);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<ProdutoSaida>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível obter o produto pelo id \"{id}\" através da API MinhaApi: {ex.GetBaseException().Message}", response, ex);
            }
        }

        /// <summary>
        /// Realiza o cadastro de um produto
        /// </summary>
        /// <param name="fornecedorId">Id do fornecedor.</param>
        /// <param name="descricao">Descrição do produto.</param>
        /// <param name="dataFabricacao">Data de fabricação do produto.</param>
        /// <param name="dataValidade">Data de validade do produto.</param>
        public async Task<ProdutoSaida> CadastrarProduto(CadastrarProdutoEntrada entrada)
        {
            HttpResponseMessage response = null;

            var content = new StringContent(entrada.ToString(), Encoding.UTF8, "application/json");

            try
            {
                var client = ObterHttpClient();

                response = await client.PostAsync($"{_urlMinhaApi}produto/salvar", content);

                var saida = await Saida.ExtrairSaidaPorResponse(response);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<ProdutoSaida>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível cadastrar o produto através da API MinhaApi: {ex.GetBaseException().Message}", response, ex);
            }
        }

        /// <summary>
        /// Realiza a alteração de um produto
        /// </summary>
        /// <param name="id">Id do produto.</param>
        /// <param name="fornecedorId">Id do produto.</param>
        /// <param name="descricao">Descrição do produto.</param>
        /// <param name="DataFabricacao">Data de fabricação do produto.</param>
        /// <param name="DataValidade">Data de validade do produto.</param>
        /// <param name="status">Status de ativação.</param>
        public async Task<ProdutoSaida> AlterarProduto(AlterarProdutoEntrada entrada)
        {
            HttpResponseMessage response = null;

            var content = new StringContent(entrada.ToString(), Encoding.UTF8, "application/json");

            try
            {
                var client = ObterHttpClient();

                response = await client.PutAsync($"{_urlMinhaApi}produto/alterar", content);

                var saida = await Saida.ExtrairSaidaPorResponse(response);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<ProdutoSaida>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível alterar o produto através da API MinhaApi: {ex.GetBaseException().Message}", response, ex);
            }
        }

        /// <summary>
        /// Realiza a exclusão de um produto
        /// </summary>
        /// <param name="idProduto">ID do produto que será excluído.</param>
        public async Task<ProdutoSaida> ExcluirProduto(int idProduto)
        {
            HttpResponseMessage response = null;

            try
            {
                var client = ObterHttpClient();

                response = await client.DeleteAsync($"{_urlMinhaApi}produto/excluir?id={idProduto}");

                var saida = await Saida.ExtrairSaidaPorResponse(response);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<ProdutoSaida>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível excluir o produto através da API MinhaApi: {ex.GetBaseException().Message}", response, ex);
            }
        }
    }
}