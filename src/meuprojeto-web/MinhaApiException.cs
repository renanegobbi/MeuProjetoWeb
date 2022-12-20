using System;
using System.Collections;
using System.Net.Http;

namespace meuprojeto_web
{

    [Serializable]
    public class MinhaApiException : Exception
    {
        public MinhaApiException(string mensagem) : base(mensagem) { }

        public MinhaApiException(string mensagem, HttpResponseMessage response, Exception exception = null, HttpContent requestContent = null)
            : base(mensagem + (!string.IsNullOrEmpty(exception?.GetBaseException()?.Message) ? $" => {exception.GetBaseException().Message}" : string.Empty), exception)
        {
            if (response != null)
            {
                this.Data["Request URL"] = response.RequestMessage?.RequestUri?.ToString();

                this.Data["HTTP Status Code"] = $"{(int)response.StatusCode} - {response.StatusCode}";

                if (requestContent != null)
                    this.Data["Request Content"] = requestContent.ReadAsStringAsync()?.Result;

                var responseContent = response.Content?.ReadAsStringAsync()?.Result;

                if (!string.IsNullOrEmpty(responseContent))
                    this.Data["Response Content"] = responseContent;

                var responseContentType = response.Content?.Headers?.ContentType?.MediaType;

                if (!string.IsNullOrEmpty(responseContentType))
                    this.Data["Response ContentType"] = responseContentType;
            }

            if (exception?.Data != null)
            {
                var nomeExceptionType = exception.GetType().Name;

                foreach (DictionaryEntry data in exception.Data)
                {
                    this.Data[$"{nomeExceptionType} - {data.Key}"] = data.Value?.ToString();
                }
            }
        }
    }
}
