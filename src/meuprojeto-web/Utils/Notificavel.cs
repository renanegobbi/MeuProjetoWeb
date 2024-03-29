﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
namespace meuprojeto_web.Utils
{
    /// <summary>
    /// Classe que quando herdada, permite a utilização de notificações (domain notifications)
    /// </summary>
    public abstract class Notificavel : INotificavel
    {
        private List<Notificacao> _notificacoes;
        /// <summary>
        /// Coleção de notificações adicionadas
        /// </summary>
        [JsonIgnore]
        public IReadOnlyCollection<Notificacao> Notificacoes => _notificacoes;
        /// <summary>
        /// Coleção de todas as mensagens geradas pelas notificações.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyCollection<string> Mensagens => _notificacoes.Any() ? _notificacoes.Select(x => x.Mensagem).ToList() : new List<string>();
        /// <summary>
        /// Indica a existência de pelo menos uma notificação. Havendo uma notificação, é considerado inválido.
        /// </summary>
        [JsonIgnore]
        public bool Invalido => _notificacoes.Any();
        protected Notificavel()
        {
            _notificacoes = new List<Notificacao>();
        }
        /// <summary>
        /// Adiciona uma notificação
        /// </summary>
        /// <param name="mensagem">Mensagem da notificação</param>
        public void AdicionarNotificacao(string mensagem)
        {
            _notificacoes.Add(new Notificacao(mensagem));
        }
        /// <summary>
        /// Adiciona uma notificação
        /// </summary>
        /// <param name="mensagem">Mensagem da notificação</param>
        /// <param name="informacoesAdicionais">Coleção de informações adicionais relacionadas a notificação</param>
        public void AdicionarNotificacao(string mensagem, Dictionary<string, string> informacoesAdicionais)
        {
            _notificacoes.Add(new Notificacao(mensagem, informacoesAdicionais));
        }
        /// <summary>
        /// Adiciona uma notificação
        /// </summary>
        /// <param name="notificacao">Notificação a ser adicionada</param>
        public void AdicionarNotificacao(Notificacao notificacao)
        {
            if (notificacao != null)
                _notificacoes.Add(notificacao);
        }
        /// <summary>
        /// Adiciona uma coleção de notificações
        /// </summary>
        /// <param name="notificacoes">Notificações que serão adicionadas</param>
        public void AdicionarNotificacoes(IReadOnlyCollection<Notificacao> notificacoes)
        {
            if (notificacoes != null && notificacoes.Any())
                _notificacoes.AddRange(notificacoes);
        }
        /// <summary>
        /// Adiciona uma coleção de notificações, a partir de outro elemento notificável
        /// </summary>
        /// <param name="notificavel">Elemento notificável</param>
        public void AdicionarNotificacoes(Notificavel notificavel)
        {
            if (notificavel != null)
                _notificacoes.AddRange(notificavel.Notificacoes);
        }
        /// <summary>
        /// Adiciona uma coleção de notificações, a partir de uma coleção de elementos notificáveis
        /// </summary>
        /// <param name="notificaveis">Coleção de notificáveis</param>
        public void AdicionarNotificacoes(params Notificavel[] notificaveis)
        {
            if (notificaveis == null)
                return;
            foreach (var notificavel in notificaveis)
            {
                AdicionarNotificacoes(notificavel);
            }
        }
        /// <summary>
        /// Limpa todas as notificações
        /// </summary>
        public void LimparNotificacoes()
        {
            this._notificacoes = new List<Notificacao>();
        }
    }
}
