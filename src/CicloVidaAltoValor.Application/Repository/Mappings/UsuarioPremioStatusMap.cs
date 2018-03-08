using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioPremioStatusMap : DommelEntityMap<UsuarioPremioStatus>
    {
        public UsuarioPremioStatusMap()
        {
            ToTable("CPR_USUARIO_PREMIO_STATUS");

            Map(p => p.UsuarioPremioStatusId).ToColumn("CPR_USUARIO_PREMIO_STATUS_ID").IsKey().IsIdentity();
            Map(p => p.StatusId).ToColumn("STATUS_ID");
            Map(p => p.Nome).ToColumn("NOME");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
        }
    }
}
