using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class CampanhaFaseUsuarioAcessoMap : DommelEntityMap<CampanhaFaseUsuarioAcesso>
    {
        public CampanhaFaseUsuarioAcessoMap()
        {
            ToTable("CPR_CAMPANHA_FASE_USUARIO_ACESSO");

            Map(p => p.CampanhaFaseId).ToColumn("CPR_CAMPANHA_FASE_ID").IsKey();
            Map(p => p.UsuarioId).ToColumn("CPR_USUARIO_ID");
            Map(p => p.Bonificado).ToColumn("BONIFICADO");
            Map(p => p.DataAtualizacao).ToColumn("DATA_ATUALIZACAO");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
        }
    }
}
