using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [CPR_USUARIO_PREMIO_FASE]
    /// </summary>
    public class UsuarioPremioFase : IEntity
    {
        public UsuarioPremioFase()
        {

        }

        public UsuarioPremioFase(int usuarioId, int campanhaProdutoId, int campanhaFaseId)
        {
            UsuarioId = usuarioId;
            CampanhaProdutoId = campanhaProdutoId;
            CampanhaFaseId = campanhaFaseId;
        }

        public UsuarioPremioFase(int usuarioId, int campanhaProdutoId, int campanhaFaseId, int campanhaProdutoFaseId)
        {
            UsuarioId = usuarioId;
            CampanhaProdutoId = campanhaProdutoId;
            CampanhaFaseId = campanhaFaseId;
            CampanhaProdutoFaseId = campanhaProdutoFaseId;
        }

        /// <summary>
        /// [CPR_USUARIO_PREMIO_FASE_ID]
        /// </summary>
        public int UsuarioPremioFaseId { get; set; }

        /// <summary>
        /// [CPR_CAMPANHA_PRODUTO_FASE_ID]
        /// </summary>
        public int CampanhaProdutoFaseId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public CampanhaProdutoFase CampanhaProdutoFase { get; set; }

        /// <summary>
        /// [CPR_USUARIO_ID]
        /// </summary>
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        /// <summary>
        /// [CPR_CAMPANHA_PRODUTO_ID]
        /// </summary>
        public int CampanhaProdutoId { get; set; }

        public CampanhaProduto CampanhaProduto { get; set; }


        /// <summary>
        /// [CPR_CAMPANHA_FASE_ID]
        /// </summary>
        public int CampanhaFaseId { get; set; }


        public CampanhaFase CampanhaFase { get; set; }


      


        /// <summary>
        /// [DATA_CRIACAO]
        /// </summary>
        public DateTime DataCriacao { get; set; }
    }
}
