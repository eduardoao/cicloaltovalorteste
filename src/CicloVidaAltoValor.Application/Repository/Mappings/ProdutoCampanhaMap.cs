using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class ProdutoCampanhaMap : DommelEntityMap<ProdutoCampanha>
    {
        public ProdutoCampanhaMap()
        {
            ToTable("CPR_PRODUTO_CAMPANHA");

            Map(p => p.ProdutoCampanhaId).ToColumn("CPR_PRODUTO_CAMPANHA_ID").IsIdentity().IsKey();
            Map(p => p.CampanhaProdutoId).ToColumn("CPR_CAMPANHA_PRODUTO_ID");
            Map(p => p.Carteira).ToColumn("CARTEIRA");
            Map(p => p.UrlImagem).ToColumn("URL_IMAGEM");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
            Map(p => p.DataAlteracao).ToColumn("DATA_ALTERACAO");
            Map(p => p.Ativo).ToColumn("ATIVO");


        }
    }
}
