using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [CPR_CAMPANHA_FASE_USUARIO_ACESSO]
    /// </summary>
    public class CampanhaFaseUsuarioAcesso : IEntity
    {
        /// <summary>
        /// [CPR_CAMPANHA_FASE_ID]
        /// </summary>
        public int CampanhaFaseId { get; set; }


        /// <summary>
        /// [CPR_USUARIO_ID]
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
