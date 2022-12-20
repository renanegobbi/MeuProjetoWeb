using meuprojeto_web.Comandos.Entrada.Fornecedor;
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
        /// Obtém os forncedores baseados nos parâmetros de procura
        /// </summary>
        /// <param name="entrada">Parâmetros de entrada utilizados para realizar a procura.</param>
        public async Task<ProcurarSaida<FornecedorSaida>> ProcurarFornecedores(ProcurarFornecedorEntrada entrada)
        {
            HttpResponseMessage response = null;

            var content = new StringContent(entrada.ToString(), Encoding.UTF8, "application/json");

            try
            {
                response = await ObterHttpClient().PostAsync($"{_urlMinhaApi}fornecedor/listar", content);

                var saida = await Saida.ExtrairSaidaPorResponse(response, content);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<ProcurarSaida<FornecedorSaida>>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível procurar por fornecedor pela API do MinhaApi: {ex.GetBaseException().Message}", response, ex, content);
            }
        }

        /// <summary>
        /// Obtém um fornecedor a partir do seu id.
        /// </summary>
        /// <param name="id">Id do fornecedor</param>
        public async Task<FornecedorSaida> ObterFornecedorPorId(int id)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await ObterHttpClient().GetAsync($"{_urlMinhaApi}fornecedor/obter-por-id?id={id}");

                var saida = await Saida.ExtrairSaidaPorResponse(response);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<FornecedorSaida>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível obter o fornecedor pelo id \"{id}\" através da API MinhaApi: {ex.GetBaseException().Message}", response, ex);
            }
        }

        /// <summary>
        /// Realiza o cadastro de um fornecedor
        /// </summary>
        /// <param name="nome">Nome do fornecedor.</param>
        /// <param name="descricao">Descrição do fornecedor.</param>
        /// <param name="cnpj">Cnpj do fornecedor.</param>
        public async Task<FornecedorSaida> CadastrarFornecedor(CadastrarFornecedorEntrada entrada)
        {
            HttpResponseMessage response = null;

            var content = new StringContent(entrada.ToString(), Encoding.UTF8, "application/json");

            try
            {
                var client = ObterHttpClient();

                response = await client.PostAsync($"{_urlMinhaApi}fornecedor/salvar", content);

                var saida = await Saida.ExtrairSaidaPorResponse(response);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<FornecedorSaida>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível cadastrar o fornecedor através da API MinhaApi: {ex.GetBaseException().Message}", response, ex);
            }
        }

        /// <summary>
        /// Realiza a alteração de um fornecedor
        /// </summary>
        /// <param name="id">Id do fornecedor.</param>
        /// <param name="nome">Nome do fornecedor.</param>
        /// <param name="descricao">Descrição do fornecedor.</param>
        /// <param name="status">Status de ativação.</param>
        public async Task<FornecedorSaida> AlterarFornecedor(AlterarFornecedorEntrada entrada)
        {
            HttpResponseMessage response = null;

            var content = new StringContent(entrada.ToString(), Encoding.UTF8, "application/json");

            try
            {
                var client = ObterHttpClient();

                response = await client.PutAsync($"{_urlMinhaApi}fornecedor/alterar", content);

                var saida = await Saida.ExtrairSaidaPorResponse(response);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<FornecedorSaida>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível alterar o fornecedor através da API MinhaApi: {ex.GetBaseException().Message}", response, ex);
            }
        }

        /// <summary>
        /// Realiza a exclusão de um fornecedor
        /// </summary>
        /// <param name="idFornecedor">ID do fornecedor que será excluído.</param>
        public async Task<FornecedorSaida> ExcluirFornecedor(int idFornecedor)
        {
            HttpResponseMessage response = null;

            try
            {
                var client = ObterHttpClient();

                response = await client.DeleteAsync($"{_urlMinhaApi}fornecedor/excluir?id={idFornecedor}");

                var saida = await Saida.ExtrairSaidaPorResponse(response);

                if (saida.Sucesso)
                    return Saida.ExtrairRetornoPorSaida<FornecedorSaida>(saida);

                this.AdicionarNotificacoes(saida.Mensagens.Select(x => new Notificacao(x))?.ToArray());

                return null;
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Não foi possível excluir o fornecedor através da API MinhaApi: {ex.GetBaseException().Message}", response, ex);
            }
        }
    }
}