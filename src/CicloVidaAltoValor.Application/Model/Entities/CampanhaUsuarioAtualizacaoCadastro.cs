using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [CPR_CAMPANHA_USUARIO_ATUALIZACAO_CADASTRO]
    /// </summary>
    public class CampanhaUsuarioAtualizacaoCadastro : IEntity
    {
        /// <summary>
        /// [CPR_CAMPANHA_ID]
        /// </summary>
        public int CampanhaId { get; set; }

        /// <summary>
        /// [CPR_USUARIO_ID]
        /// </summary>
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        /// <summary>
        /// [BONIFICADO]
        /// </summary>
        public bool Bonificado { get; set; }

        /// <summary>
        /// [DATA_ATUALIZACAO]
        /// </summary>
        public DateTime? DataAtualizacao { get; set; }


        /// <summary>
        /// [DATA_CRIACAO]
        /// </summary>
        public DateTime DataCriacao { get; set; }
    }
}
