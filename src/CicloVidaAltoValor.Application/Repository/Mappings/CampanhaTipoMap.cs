using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class CampanhaTipoMap : DommelEntityMap<CampanhaTipo>
    {
        public CampanhaTipoMap()
        {
            
            ToTable("CPR_CAMPANHA_TIPO");

            Map(p => p.CampanhaTipoId).ToColumn("CPR_CAMPANHA_TIPO_ID").IsKey().IsIdentity();
            Map(p => p.Descricao).ToColumn("DESCRICAO");
        }
    }
}
