using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class ProdutoEstado : IEntity
    {
        /// <summary>
        /// [WMS_PRODUTO_ESTADO_ID] 
        /// </summary>
        public int ProdutoEstadoId { get; set; }

        /// <summary>
        /// [PRODUTO_ID] 
        /// </summary>
        public int ProdutoId { get; set; }

        /// <summary>
        /// [UF] 
        /// </summary>
        public string Uf { get; set; }

        /// <summary>
        /// [DATA_CRIACAO] 
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// [USUARIO_ID_CRIACAO] 
        /// </summary>
        public int? UsuarioIdCriacao { get; set; }

        /// <summary>
        /// [PRECO] 
        /// </summary>
        public decimal? Preco { get; set; }

        /// <summary>
        /// [FRETE] 
        /// </summary>
        public decimal? Frete { get; set; }

        /// <summary>
        /// [MARGEM] 
        /// </summary>
        public decimal? Margem { get; set; }

        /// <summary>
        /// [PRECO_VENDA] 
        /// </summary>
        public decimal? PrecoVenda { get; set; }

        /// <summary>
        /// [PONTOS] 
        /// </summary>
        public decimal? Pontos { get; set; }

        /// <summary>
        /// [ID_FATOR_CONVERSAO] 
        /// </summary>
        public int? IdFatorConversao { get; set; }

        /// <summary>
        /// [USUARIO_ID_ALTERACAO] 
        /// </summary>
        public int? UsuarioIdAlteracao { get; set; }

        /// <summary>
        /// [DATA_ALTERACAO] 
        /// </summary>
        public DateTime? DataAlteracao { get; set; }

        /// <summary>
        /// [OBSERVACAO] 
        /// </summary>
        public string Observacao { get; set; }

        /// <summary>
        /// [DESCONTO_PERC] 
        /// </summary>
        public decimal? DescontoPerc { get; set; }

        /// <summary>
        /// [USR_GRUPO_ID] 
        /// </summary>
        public int? UsrGrupoId { get; set; }

        /// <summary>
        /// [ROWSTAMP] 
        /// </summary>
        public byte[] Rowstamp { get; set; }
    }
}
