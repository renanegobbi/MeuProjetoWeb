using System.Collections.Generic;

namespace meuprojeto_web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int ErroCode { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }

    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
    }

    public class ResponseErrorMessages
    {
        public List<string> Mensagens { get; set; }
    }
}