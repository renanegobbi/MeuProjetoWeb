using meuprojeto_web.Comandos.Entrada.Fornecedor;
using meuprojeto_web.Enums;
using meuprojeto_web.Helpers;
using meuprojeto_web.Models;
using meuprojeto_web.Proxies;
using meuprojeto_web.Results;
using meuprojeto_web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace meuprojeto_web.Controllers
{
    [Route("fornecedor")]
    [Route("")]
    public class FornecedorController : Controller
    {
        private readonly MinhaApiProxy _minhaApiProxy;
        private readonly DatatablesHelper _datatablesHelper;
        private readonly ILogger<FornecedorController> _logger;

        public FornecedorController(
            MinhaApiProxy minhaApiProxy,
            DatatablesHelper datatablesHelper,
            ILogger<FornecedorController> logger)
        {
            _minhaApiProxy = minhaApiProxy;
            _datatablesHelper = datatablesHelper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("IndexFornecedor");
        }

        [HttpPost]
        [Route("listar")]
        public async Task<IActionResult> ListarFornecedores(int? id, string nome, string descricao, string cnpj, string ativo)
        {
            var ordenarPor = FornecedorOrdenarPor.Nome;

            Enum.TryParse(_datatablesHelper.OrdenarPor, true, out ordenarPor);

            var filtro = new ProcurarFornecedorEntrada(ordenarPor, _datatablesHelper.OrdenarSentido, _datatablesHelper.PaginaIndex, _datatablesHelper.PaginaTamanho)
            {
                Id = id,
                Nome = nome,
                Descricao = descricao,
                Cnpj = cnpj.RemoverFormatacao(),
                Ativo = string.IsNullOrEmpty(ativo) ? null : (ativo.ToUpper() == "S" ? true : false),
            };

            var pesquisa = await _minhaApiProxy.ProcurarFornecedores(filtro);

            if (_minhaApiProxy.Invalido)
                return new FeedbackResult(new Feedback(TipoFeedback.Atencao, "Ocorreu um erro ao realizar a pesquisa pelos Fornecedores.", _minhaApiProxy.Mensagens, TipoAcaoAoOcultarFeedback.Ocultar));

            return new DatatablesResult(_datatablesHelper.Draw, pesquisa.TotalRegistros, pesquisa.Registros.Select(x => new
            {
                Id        = x.Id,
                Nome      = x.Nome,
                Descricao = x.Descricao,
                Cnpj      = x.Cnpj,
                Ativo     = x.Ativo == true ? "Ativo" : "Inativo"
            }).ToArray());
        }

        [HttpGet]
        [Route("exibir-popup-fornecedor")]
        public async Task<IActionResult> ExibirPopupFornecedor(int? codigo)
        {
            if (!codigo.HasValue)
                return PartialView("PopupFornecedor", null);

            var fornecedor = await _minhaApiProxy.ObterFornecedorPorId(codigo.Value);

            if (fornecedor == null)
                return new FeedbackResult(new Feedback(TipoFeedback.Atencao, "O fornecedor não foi encontrado."));

            return PartialView("PopupFornecedor", fornecedor);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> CadastrarFornecedor(ManterFornecedorViewModel model)
        {
            var entrada = new CadastrarFornecedorEntrada(model.Nome, model.Descricao, model.Cnpj.RemoverFormatacao());

            await _minhaApiProxy.CadastrarFornecedor(entrada);

            return _minhaApiProxy.Invalido
                    ? new FeedbackResult(new Feedback(TipoFeedback.Atencao, "Ocorreu um erro ao cadastrar o fornecedor.", _minhaApiProxy.Mensagens, TipoAcaoAoOcultarFeedback.Ocultar))
                    : new FeedbackResult(new Feedback(TipoFeedback.Sucesso, "Fornecedor cadastrado com sucesso.", null, TipoAcaoAoOcultarFeedback.Ocultar));
        }

        [HttpPost]
        [Route("alterar")]
        public async Task<IActionResult> AlterarFornecedor(ManterFornecedorViewModel model)
        {
            var entrada = new AlterarFornecedorEntrada(model.Id.Value, model.Nome, model.Descricao, model.Status, model.Cnpj.RemoverFormatacao());

            await _minhaApiProxy.AlterarFornecedor(entrada);

            return _minhaApiProxy.Invalido
                    ? new FeedbackResult(new Feedback(TipoFeedback.Atencao, "Ocorreu um erro ao alterar o fornecedor.", _minhaApiProxy.Mensagens, TipoAcaoAoOcultarFeedback.Ocultar))
                    : new FeedbackResult(new Feedback(TipoFeedback.Sucesso, "Fornecedor alterado com sucesso.", null, TipoAcaoAoOcultarFeedback.Ocultar));
        }

        [HttpPost]
        [Route("excluir")]
        public async Task<IActionResult> ExcluirFornecedor(int codigo)
        {
            await _minhaApiProxy.ExcluirFornecedor(codigo);

            return _minhaApiProxy.Invalido
                    ? new FeedbackResult(new Feedback(TipoFeedback.Atencao, "Ocorreu um erro ao excluir o fornecedor.", _minhaApiProxy.Mensagens, TipoAcaoAoOcultarFeedback.Ocultar))
                    : new FeedbackResult(new Feedback(TipoFeedback.Sucesso, "Fornecedor excluído com sucesso.", null, TipoAcaoAoOcultarFeedback.Ocultar));
        }
    }
}
