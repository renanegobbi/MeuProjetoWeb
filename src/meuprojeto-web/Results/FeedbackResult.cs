using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;
using System.Threading.Tasks;

namespace meuprojeto_web.Results
{
    /// <summary>
    /// ActionResult que busca padronizar as mensagens de feedback
    /// </summary>
    public class FeedbackResult : IActionResult
    {
        private readonly Feedback _feedback;

        public FeedbackResult(Feedback feedback)
        {
            _feedback = feedback;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var jsonResult = new JsonResult(_feedback);

                if (_feedback.Tipo == TipoFeedback.Erro || _feedback.Tipo == TipoFeedback.Atencao)
                    jsonResult.StatusCode = (int)HttpStatusCode.BadRequest;

                await jsonResult.ExecuteResultAsync(context);
            }
            else
            {
                var viewResult = new ViewResult
                {
                    ViewName = "Feedback",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
                    {
                        Model = _feedback
                    }
                };

                await viewResult.ExecuteResultAsync(context);
            }
        }
    }
}
