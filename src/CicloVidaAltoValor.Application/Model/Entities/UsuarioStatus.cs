using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [CPR_USUARIO_STATUS]
    /// </summary>
    public class UsuarioStatus : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// [CPR_USUARIO_ID]
        /// </summary>
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
        
        public decimal Gasto { get; set; }

        public string GastoPercentual { get; set; }

        /// <summary>
        /// REVER O NOME APTO_TROCAR
        /// </summary>

        public bool Trocar { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }
        
        public string GetGastoPercentual()
        {
            return $"{this.GastoPercentual}%";
        }

    }
}
