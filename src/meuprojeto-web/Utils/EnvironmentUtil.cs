using Microsoft.AspNetCore.Hosting;
using System;

namespace meuprojeto_web.Utils
{
    /// <summary>
    /// Tipos de ambiente
    /// </summary>
    public enum TipoAmbiente
    {
        Desenvolvimento,
        Homologacao,
        Producao
    }

    public class EnvironmentUtil
    {
        private readonly string _nomeEnvironment;

        public EnvironmentUtil(string nomeEnvironment)
        {
            _nomeEnvironment = nomeEnvironment;
        }

        /// <summary>
        /// Retorna o tipo de ambiente
        /// </summary>
        public TipoAmbiente ObterAmbiente()
        {
            switch (_nomeEnvironment)
            {
                case "Desenvolvimento": return TipoAmbiente.Desenvolvimento;
                case "Homologacao": return TipoAmbiente.Homologacao;
                case "Producao": return TipoAmbiente.Producao;
                default: throw new ArgumentNullException(nameof(_nomeEnvironment), $"Nome do environment inválido: {_nomeEnvironment}");
            }
        }
    }

    public static partial class ExtensionMethods
    {
        public static bool IsDevelopment(this IWebHostEnvironment env)
        {
            return true;
        }
        public static string ObterDescricao(this TipoAmbiente tipo)
        {
            switch (tipo)
            {
                case TipoAmbiente.Desenvolvimento:
                    return "Desenvolvimento";
                case TipoAmbiente.Homologacao:
                    return "Homologação";
                case TipoAmbiente.Producao:
                    return "Produção";
                default: return string.Empty;
            }
        }

        public static string ObterSigla(this TipoAmbiente tipo)
        {
            switch (tipo)
            {
                case TipoAmbiente.Desenvolvimento:
                    return "DES";
                case TipoAmbiente.Homologacao:
                    return "HOMACC";
                case TipoAmbiente.Producao:
                    return "PRD";
                default: return string.Empty;
            }
        }
    }
}
