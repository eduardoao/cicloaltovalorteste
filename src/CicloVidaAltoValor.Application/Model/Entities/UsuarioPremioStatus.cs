using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class UsuarioPremioStatus : IEntity
    {
        /// <summary>
        /// CPR_USUARIO_PREMIO_STATUS_ID
        /// </summary>
        public int UsuarioPremioStatusId { get; set; }

        /// <summary>
        /// STATUS_ID
        /// </summary>
        public string StatusId { get; set; }

        /// <summary>
        /// NOME
        /// </summary>
        public string Nome { get; set; }


        /// <summary>
        /// DATA_CRIACAO
        /// </summary>
        public DateTime DataCriacao { get; set; }
    }
}
