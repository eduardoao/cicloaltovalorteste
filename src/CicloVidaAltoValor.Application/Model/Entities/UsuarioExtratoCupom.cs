using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class UsuarioExtratoCupom : IEntity
    {
        /// <summary>
        /// CPR_USUARIO_EXTRATO_ID
        /// </summary>
        public int UsuarioExtratoId { get; set; }

        /// <summary>
        /// CPR_CUPOM_ID
        /// </summary>
        public int CupomId { get; set; }


        /// <summary>
        /// GEROU_CUPOM
        /// </summary>
        public bool GerouCupom { get; set; }
    }
}
