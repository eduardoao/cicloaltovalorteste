using Dharma.Repository;
using CicloVidaAltoValor.Application.Model.ValueObject;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [CPR_CAMPANHA_PRODUTO]
    /// </summary>
    public class CampanhaProduto : IEntity
    {
        /// <summary>
        /// CPR_CAMPANHA_PRODUTO_ID
        /// </summary>
        public int CampanhaProdutoId { get; set; }

        /// <summary>
        /// CPR_CAMPANHA_ID
        /// </summary>
        public int CampanhaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Campanha Campanha { get; set; }

        /// <summary>
        /// WMS_PRODUTO_ID
        /// </summary>
        public int ProdutoId { get; set; }

        public Produto Produto { get; set; }

        /// <summary>
        /// PREMIO_PADRAO
        /// </summary>
        public bool PremioPadrao { get; set; }

        /// <summary>
        /// LOJA_ID
        /// </summary>
        public int? LojaId { get; set; }

        /// <summary>
        /// MECANICA_ID
        /// </summary>
        public int? MecanicaId { get; set; }


        public bool MadeChanges(ProdutoArquivo item)
        {
            var changes = false;
            if (LojaId.GetValueOrDefault() != item.LojaId)
            {
                LojaId = item.LojaId;
                changes = true;

            }

            if (MecanicaId.GetValueOrDefault() != item.Pid)
            {
                MecanicaId = item.Pid;
                changes = true;
            }

            return changes;
        }
    }
}
