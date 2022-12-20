using System;
using System.Net.Http;

namespace meuprojeto_web.Proxies
{
    public partial class MinhaApiProxy
    {
        private readonly string _urlMinhaApi;
        private readonly IHttpClientFactory _httpClientFactory;

        public MinhaApiProxy(string ambiente, IHttpClientFactory httpClientFactory)
        {
            switch (ambiente)
            {
                case "DES":
                    _urlMinhaApi = "https://localhost:5001/api/minhaapi/v1/";
                    break;
                case "HOM":
                    _urlMinhaApi = "https://localhost:5001/api/minhaapi/v1/";
                    break;
                case "PRD":
                    _urlMinhaApi = "https://localhost:5001/api/minhaapi/v1/";
                    break;
                default:
                    if (string.IsNullOrEmpty(ambiente))
                        throw new MinhaApiException("Informe o ambiente da API MinhaApi que deverá ser utilizado (DES, HOM, PRD ou a URL para acesso a API).");

                    _urlMinhaApi = ambiente;
                    break;
            }

            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Obtém um HttpClient a apartir da fábrica
        /// </summary>
        private HttpClient ObterHttpClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = new TimeSpan(0, 5, 0);

            return client;
        }
    }
}