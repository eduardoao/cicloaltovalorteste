using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    
    /// <summary>
    /// 
    /// </summary>
    public class ProdutoEstadoMap : DommelEntityMap<ProdutoEstado>
    {
        /// <summary>
        /// 
        /// </summary>
        public ProdutoEstadoMap()
        {
            ToTable("WMS_PRODUTO_ESTADO");

            Map(p => p.ProdutoEstadoId).ToColumn("WMS_PRODUTO_ESTADO_ID").IsKey().IsIdentity();
            Map(p => p.ProdutoId).ToColumn("PRODUTO_ID");
            Map(p => p.Uf).ToColumn("UF");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
            Map(p => p.UsuarioIdCriacao).ToColumn("USUARIO_ID_CRIACAO");
            Map(p => p.Preco).ToColumn("PRECO");
            Map(p => p.Frete).ToColumn("FRETE");
            Map(p => p.Margem).ToColumn("MARGEM");
            Map(p => p.PrecoVenda).ToColumn("PRECO_VENDA");
            Map(p => p.Pontos).ToColumn("PONTOS");
            Map(p => p.IdFatorConversao).ToColumn("ID_FATOR_CONVERSAO");
            Map(p => p.UsuarioIdAlteracao).ToColumn("USUARIO_ID_ALTERACAO");
            Map(p => p.DataAlteracao).ToColumn("DATA_ALTERACAO");
            Map(p => p.Observacao).ToColumn("OBSERVACAO");
            Map(p => p.DescontoPerc).ToColumn("DESCONTO_PERC");
            Map(p => p.UsrGrupoId).ToColumn("USR_GRUPO_ID");
            Map(p => p.Rowstamp).ToColumn("ROWSTAMP");
        }
    }
}
