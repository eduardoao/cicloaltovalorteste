using System.ComponentModel.DataAnnotations;
using CicloVidaAltoValor.Application.Contracts.Campanha;

namespace CicloVidaAltoValor.Application.Contracts.Produto
{

    public class CampanhaProdutoViewModel
    {
        /// <summary>
        /// CPR_CAMPANHA_PRODUTO_ID
        /// </summary>
        public int CampanhaProdutoId { get; set; }

        /// <summary>
        /// CPR_CAMPANHA_ID
        /// </summary>
        public int CampanhaId { get; set; }

        public CampanhaViewModel Campanha { get; set; }


        /// <summary>
        /// WMS_PRODUTO_ID
        /// </summary>
        [Display(Name = "Código do Produto")]
        public int ProdutoId { get; set; }

        public ProdutoViewModel Produto { get; set; }

        /// <summary>
        /// PREMIO_PADRAO
        /// </summary>
        public bool PremioPadrao { get; set; }

        /// <summary>
        /// LOJA_ID
        /// </summary>
        [Display(Name = "Código da Loja")]
        public int? LojaId { get; set; }

        /// <summary>
        /// MECANICA_ID
        /// </summary>
        [Display(Name = "Código da Mecânica (PID)")]
        public int? MecanicaId { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public MecanicaViewModel Mecanica { get; set; }
    }
}
