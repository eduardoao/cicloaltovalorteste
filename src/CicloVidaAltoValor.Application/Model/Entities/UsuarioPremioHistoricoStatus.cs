using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class UsuarioPremioHistoricoStatus : IEntity
    {
        /// <summary>
        /// CPR_USUARIO_PREMIO_ID
        /// </summary>
        public int UsuarioPremioId { get; set; }

        /// <summary>
        /// STATUS_ID_OLD
        /// </summary>
        public int StatusIdOld { get; set; }

        /// <summary>
        /// STATUS_ID
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// DESCRICAO
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// DATA_CRIACAO
        /// </summary>
        public DateTime DataCriacao { get; set; }

    }
}
