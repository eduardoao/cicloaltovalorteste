using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class CampanhaTipo : IEntity
    {
        /// <summary>
        ///  CPR_CAMPANHA_TIPO_ID
        /// </summary>
        public int CampanhaTipoId { get; set; }


        /// <summary>
        /// DESCRICAO
        /// </summary>
        public string Descricao { get; set; }
    }
}
