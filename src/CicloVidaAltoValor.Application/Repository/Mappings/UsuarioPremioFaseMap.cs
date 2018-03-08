using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioPremioFaseMap : DommelEntityMap<UsuarioPremioFase>
    {
        public UsuarioPremioFaseMap()
        {
            ToTable("CPR_USUARIO_PREMIO_FASE");

            Map(p => p.UsuarioPremioFaseId).ToColumn("CPR_USUARIO_PREMIO_FASE_ID").IsKey().IsIdentity();
            Map(p => p.CampanhaFaseId).ToColumn("CPR_CAMPANHA_FASE_ID");
            Map(p => p.CampanhaProdutoId).ToColumn("CPR_CAMPANHA_PRODUTO_ID");
            Map(p => p.CampanhaProdutoFaseId).ToColumn("CPR_CAMPANHA_PRODUTO_FASE_ID");
            Map(p => p.UsuarioId).ToColumn("CPR_USUARIO_ID");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
        }
    }
}
