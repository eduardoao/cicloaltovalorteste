using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioAcessoMap : DommelEntityMap<UsuarioAcesso>
    {
        public UsuarioAcessoMap()
        {
            ToTable("CPR_CAMPANHA_USUARIO_ACESSO");

            Map(p => p.UsuarioAcessoId).ToColumn("CPR_CAMPANHA_USUARIO_ACESSO_ID").IsIdentity().IsKey();
            Map(p => p.UsuarioId).ToColumn("CPR_USUARIO_ID");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
        }
    }

}
