using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioPremioHistoricoStatusMap : DommelEntityMap<UsuarioPremioHistoricoStatus>
    {
        public UsuarioPremioHistoricoStatusMap()
        {
            ToTable("CPR_USUARIO_PREMIO_HISTORICO_STATUS");

            
            Map(p => p.UsuarioPremioId).ToColumn("CPR_USUARIO_PREMIO_ID");
            Map(p => p.StatusIdOld).ToColumn("STATUS_ID_OLD");
            Map(p => p.StatusId).ToColumn("STATUS_ID");
            Map(p => p.Descricao).ToColumn("DESCRICAO");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
        }
    }
}
