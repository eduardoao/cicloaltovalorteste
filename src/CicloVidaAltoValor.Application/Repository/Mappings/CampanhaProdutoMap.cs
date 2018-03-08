using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class CampanhaProdutoMap : DommelEntityMap<CampanhaProduto>
    {
        public CampanhaProdutoMap()
        {
            ToTable("CPR_CAMPANHA_PRODUTO");

            Map(p => p.CampanhaProdutoId).ToColumn("CPR_CAMPANHA_PRODUTO_ID").IsKey().IsIdentity();
            Map(p => p.CampanhaId).ToColumn("CPR_CAMPANHA_ID");
            Map(p => p.ProdutoId).ToColumn("WMS_PRODUTO_ID");
            Map(p => p.PremioPadrao).ToColumn("PREMIO_PADRAO");
            Map(p => p.LojaId).ToColumn("LOJA_ID");
            Map(p => p.MecanicaId).ToColumn("MECANICA_ID");

        }
    }
}
