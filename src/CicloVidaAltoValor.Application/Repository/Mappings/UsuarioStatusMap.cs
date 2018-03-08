using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioStatusMap : DommelEntityMap<UsuarioStatus>
    {
        public UsuarioStatusMap()
        {
            ToTable("CPR_USUARIO_STATUS");

            //Map(x => x.Id).ToColumn("CPR_USUARIO_STATUS_ID").IsKey();
            Map(x => x.UsuarioId).ToColumn("CPR_USUARIO_ID");
            Map(x => x.GastoPercentual).ToColumn("GASTO_PERCENTUAL");
            Map(x => x.Gasto).ToColumn("GASTO");
            Map(x => x.Trocar).ToColumn("HABILITAR_TROCAR");
            Map(x => x.DataAtualizacao).ToColumn("DATA_ATUALIZACAO");
            Map(x => x.DataCriacao).ToColumn("DATA_CRIACAO");
         
        }

    }
}
