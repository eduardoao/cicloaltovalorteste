using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class CampanhaPrevisaoFaturaMap : DommelEntityMap<CampanhaPrevisaoFatura>
    {
        public CampanhaPrevisaoFaturaMap()
        {
            ToTable("CPR_CAMPANHA_PREVISAO_FATURA");

            Map(p => p.CampanhaPrevisaoFaturaId).ToColumn("CPR_CAMPANHA_PREVISAO_FATURA_ID").IsKey().IsIdentity();
            Map(p => p.CampanhaId).ToColumn("CPR_CAMPANHA_ID");
            Map(p => p.DataPrevisaoGeracaoFatura).ToColumn("DATA_PREVISAO_GERACAO_FATURA");
            Map(p => p.DataRealGeracaoFatura).ToColumn("DATA_REAL_GERACAO_FATURA");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
        }
    }
}
