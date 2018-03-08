using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class CampanhaFaseMap : DommelEntityMap<CampanhaFase>
    {
        public CampanhaFaseMap()
        {
            ToTable("CPR_CAMPANHA_FASE");

            Map(p => p.CampanhaFaseId).ToColumn("CPR_CAMPANHA_FASE_ID").IsKey().IsIdentity();
            Map(p => p.CampanhaId).ToColumn("CPR_CAMPANHA_ID");
            Map(p => p.Fase).ToColumn("FASE");
            Map(p => p.Periodo).ToColumn("PERIODO");
            Map(p => p.DataInicioResgate).ToColumn("DATA_INICIO_RESGATE");
            Map(p => p.DataFimResgate).ToColumn("DATA_FIM_RESGATE");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
            
        }
    }
}
