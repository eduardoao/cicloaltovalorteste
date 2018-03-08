using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class UsuarioPremio : IEntity
    {
        public UsuarioPremio()
        {

        }

        public UsuarioPremio(int usuarioId, int campanhaProdutoId, int? statusId, DateTime dataPremioSelecionado)
        {

            UsuarioId = usuarioId;
            CampanhaProdutoId = campanhaProdutoId;
            DataPremioSelecionado = dataPremioSelecionado;
            StatusId = statusId;
        }

        /// <summary>
        /// CPR_USUARIO_PREMIO_ID
        /// </summary>
        public int UsuarioPremioId { get; set; }

        /// <summary>
        /// CPR_USUARIO_ID
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// CPR_CAMPANHA_PRODUTO_ID
        /// </summary>
        public int CampanhaProdutoId { get; set; }

        /// <summary>
        /// DH_PREMIO_SELECIONADO
        /// </summary>
        public DateTime DataPremioSelecionado { get; set; }

        /// <summary>
        /// CPR_CAMPANHA_EXTRATO_ID
        /// </summary>
        public int? CampanhaExtratoId { get; set; }

        /// <summary>
        /// PRIORIDADE
        /// </summary>
        public int? Prioridade { get; set; }

        /// <summary>
        /// STATUS_ID
        /// </summary>
        public int? StatusId { get; set; }

        /// <summary>
        /// TRANSACAO_FILA_ID
        /// </summary>
        public string TransacaoFilaId { get; set; }

        /// <summary>
        /// TRANSACAO_FILA_ID_DEBITO
        /// </summary>
        public string TransacaoFilaIdDebito { get; set; }

        /// <summary>
        /// PONTOS
        /// </summary>
        public int? Pontos { get; set; }


        /// <summary>
        /// DATA_ATUALIZACAO
        /// </summary>
        public DateTime? DataAtualizacao { get; set; }

        public Usuario Usuario { get; set; }

        public CampanhaProduto CampanhaProduto { get; set; }
    }
}
