using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace meuprojeto_web.Comandos.Saida
{
    public class Saida
    {
        public bool Sucesso { get; }

        public IEnumerable<string> Mensagens { get; }

        public object Retorno { get; }

        public Saida(bool sucesso, IEnumerable<string> mensagens, object retorno)
        {
            this.Sucesso = sucesso;
            this.Mensagens = mensagens;
            this.Retorno = retorno;
        }

        public static T ExtrairRetornoPorSaida<T>(Saida saida)
        {
            return saida?.Sucesso != true || saida.Retorno == null
                ? default
                : ((JObject)saida.Retorno).ToObject<T>();
        }

        public static List<T> ExtrairListaRetornoPorSaida<T>(Saida saida)
        {
            return saida?.Sucesso != true || saida.Retorno == null
                ? new List<T>()
                : ((JArray)saida.Retorno).ToObject<List<T>>();
        }

        public static async Task<Saida> ExtrairSaidaPorResponse(HttpResponseMessage response, HttpContent requestContent = null)
        {
            try
            {
                Saida saida;

                if (!response.IsSuccessStatusCode)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Forbidden:
                        case HttpStatusCode.BadRequest:

                            saida = await response.Content.ReadAsAsync<Saida>();

                            if (saida != null)
                                return saida;
                            else
                                throw new MinhaApiException($"Falha no request para a rota {response.RequestMessage.RequestUri.AbsoluteUri} da API MinhaAPi.", response, requestContent: requestContent);

                        default:
                            throw new MinhaApiException($"Falha no request para a rota {response.RequestMessage.RequestUri.AbsoluteUri} da API MinhaAPi.", response, requestContent: requestContent);
                    }
                }

                saida = await response.Content.ReadAsAsync<Saida>();

                if (saida != null)
                    return saida;

                throw new MinhaApiException($"Falha no request para a rota {response.RequestMessage.RequestUri.AbsoluteUri} da API MinhaAPi.", response, requestContent: requestContent);
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Falha no request para a rota {response.RequestMessage.RequestUri.AbsoluteUri} da API MinhaAPi.", response, ex, requestContent);
            }
        }

        public static async Task<TSaida> ExtrairSaidaPorResponse<TSaida>(HttpResponseMessage response)
        {
            try
            {
                if (response == null)
                    return default;

                if (response.IsSuccessStatusCode)
                {
                    TSaida saida;

                    try
                    {
                        saida = await response.Content.ReadAsAsync<TSaida>();

                        if (saida != null)
                            return saida;

                        throw new MinhaApiException($"Falha no request para a rota {response.RequestMessage.RequestUri.AbsoluteUri} da API MinhaAPi: Não foi possível deserializar o JSON recebido em um objeto do tipo \"{typeof(TSaida).ToString()}\" O objeto retornado é nulo.", response);
                    }
                    catch (Exception ex)
                    {
                        throw new MinhaApiException($"Falha no request para a rota {response.RequestMessage.RequestUri.AbsoluteUri} da API MinhaAPi: Não foi possível deserializar o JSON recebido em um objeto do tipo \"{typeof(TSaida).ToString()}\".", response, ex);
                    }
                }

                throw new MinhaApiException($"Falha no request para a rota {response.RequestMessage.RequestUri.AbsoluteUri} da API MinhaAPi.", response);
            }
            catch (MinhaApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MinhaApiException($"Falha no request para a rota {response.RequestMessage.RequestUri.AbsoluteUri} da API MinhaAPi.", response, ex);
            }
        }
    }
}
