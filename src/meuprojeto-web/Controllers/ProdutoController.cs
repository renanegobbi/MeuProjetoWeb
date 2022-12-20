using meuprojeto_web.Comandos.Entrada.Produto;
using meuprojeto_web.Enums;
using meuprojeto_web.Helpers;
using meuprojeto_web.Models;
using meuprojeto_web.Proxies;
using meuprojeto_web.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace meuprojeto_web.Controllers
{
    [Route("produto")]
    [Route("")]
    public class ProdutoController : Controller
    {
        private readonly MinhaApiProxy _minhaApiProxy;
        private readonly DatatablesHelper _datatablesHelper;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(
            MinhaApiProxy minhaApiProxy,
            DatatablesHelper datatablesHelper,
            ILogger<ProdutoController> logger)
        {
            _minhaApiProxy = minhaApiProxy;
            _datatablesHelper = datatablesHelper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("IndexProduto");
        }

        [HttpPost]
        [Route("listar")]
        public async Task<IActionResult> ListarProdutos(int? id, int? fornecedorId, string descricao, DateTime? dataFabricacao, DateTime? dataValidade, string ativo)
        {
            var ordenarPor = ProdutoOrdenarPor.DataFabricacao;

            Enum.TryParse(_datatablesHelper.OrdenarPor, true, out ordenarPor);

            var filtro = new ProcurarProdutoEntrada(ordenarPor, _datatablesHelper.OrdenarSentido, _datatablesHelper.PaginaIndex, _datatablesHelper.PaginaTamanho)
            {
                Id = id,
                FornecedorId = fornecedorId,
                Descricao = descricao,
                DataFabricacao = dataFabricacao,
                DataValidade = dataValidade,
                Ativo = string.IsNullOrEmpty(ativo) ? null : (ativo.ToUpper() == "S" ? true : false),
            };

            var pesquisa = await _minhaApiProxy.ProcurarProdutos(filtro);

            if (_minhaApiProxy.Invalido)
                return new FeedbackResult(new Feedback(TipoFeedback.Atencao, "Ocorreu um erro ao realizar a pesquisa pelos produtos.", _minhaApiProxy.Mensagens, TipoAcaoAoOcultarFeedback.Ocultar));

            return new DatatablesResult(_datatablesHelper.Draw, pesquisa.TotalRegistros, pesquisa.Registros.Select(x => new
            {
                Id = x.Id,
                FornecedorId = x.FornecedorId,
                Descricao = x.Descricao,
                DataFabricacao = x.DataFabricacao,
                DataValidade = x.DataValidade,
                Ativo = x.Ativo == true ? "Ativo" : "Inativo"
            }).ToArray());
        }

        [HttpGet]
        [Route("exibir-popup-produto")]
        public async Task<IActionResult> ExibirPopupProduto(int? codigo)
        {
            if (!codigo.HasValue)
                return PartialView("PopupProduto", null);

            var produto = await _minhaApiProxy.ObterProdutoPorId(codigo.Value);

            if (produto == null)
                return new FeedbackResult(new Feedback(TipoFeedback.Atencao, "O produto não foi encontrado."));

            return PartialView("PopupProduto", produto);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> CadastrarProduto(ManterProdutoViewModel model)
        {
            var entrada = new CadastrarProdutoEntrada(model.FornecedorId, model.Descricao, model.DataFabricacao, model.DataValidade);

            await _minhaApiProxy.CadastrarProduto(entrada);

            return _minhaApiProxy.Invalido
                    ? new FeedbackResult(new Feedback(TipoFeedback.Atencao, "Ocorreu um erro ao cadastrar o produto.", _minhaApiProxy.Mensagens, TipoAcaoAoOcultarFeedback.Ocultar))
                    : new FeedbackResult(new Feedback(TipoFeedback.Sucesso, "Produto cadastrado com sucesso.", null, TipoAcaoAoOcultarFeedback.Ocultar));
        }

        [HttpPost]
        [Route("alterar")]
        public async Task<IActionResult> AlterarProduto(ManterProdutoViewModel model)
        {
            var entrada = new AlterarProdutoEntrada(model.Id, model.FornecedorId, model.Descricao, model.DataFabricacao, model.DataValidade, model.Status);

            await _minhaApiProxy.AlterarProduto(entrada);

            return _minhaApiProxy.Invalido
                    ? new FeedbackResult(new Feedback(TipoFeedback.Atencao, "Ocorreu um erro ao alterar o produto.", _minhaApiProxy.Mensagens, TipoAcaoAoOcultarFeedback.Ocultar))
                    : new FeedbackResult(new Feedback(TipoFeedback.Sucesso, "Produto alterado com sucesso.", null, TipoAcaoAoOcultarFeedback.Ocultar));
        }

        [HttpPost]
        [Route("excluir")]
        public async Task<IActionResult> ExcluirProduto(int codigo)
        {
            await _minhaApiProxy.ExcluirProduto(codigo);

            return _minhaApiProxy.Invalido
                    ? new FeedbackResult(new Feedback(TipoFeedback.Atencao, "Ocorreu um erro ao excluir o produto.", _minhaApiProxy.Mensagens, TipoAcaoAoOcultarFeedback.Ocultar))
                    : new FeedbackResult(new Feedback(TipoFeedback.Sucesso, "Produto excluído com sucesso.", null, TipoAcaoAoOcultarFeedback.Ocultar));
        }
    }
}
