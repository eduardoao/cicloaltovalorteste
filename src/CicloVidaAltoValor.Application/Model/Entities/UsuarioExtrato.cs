using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class UsuarioExtrato : IEntity
    {
        /// <summary>
        /// CPR_USUARIO_EXTRATO_ID
        /// </summary>
        public int UsuarioExtratoId { get; set; }

        /// <summary>
        /// CPR_USUARIO_ID
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// TRANSACAO_FILA_ID
        /// </summary>
        public string TransacaoFilaId { get; set; }

        /// <summary>
        /// NOME_PARCEIRO
        /// </summary>
        public string NomeParceiro { get; set; }

        /// <summary>
        /// DESC_TRANSACAO
        /// </summary>
        public string DescricaoTransacao { get; set; }

        /// <summary>
        /// DT_TICKET_CHECKOUT
        /// </summary>
        public DateTime DataTicketCheckout { get; set; }

        /// <summary>
        /// PONTOS_TOTAL
        /// </summary>
        public int PontosTotal { get; set; }

        /// <summary>
        /// PONTOS_USADOS
        /// </summary>
        public int PontosUsados { get; set; }

    }
}
