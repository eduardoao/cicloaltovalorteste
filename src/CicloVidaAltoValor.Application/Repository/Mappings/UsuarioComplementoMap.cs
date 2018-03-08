using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioComplementoMap : DommelEntityMap<UsuarioComplemento>
    {
        public UsuarioComplementoMap()
        {
            ToTable("CPR_USUARIO_COMPLEMENTO");

            Map(p => p.UsuarioId).ToColumn("CPR_USUARIO_ID").IsKey();
            Map(p => p.TipoComplementoId).ToColumn("TIPO_COMPLEMENTO_ID");
            Map(p => p.Nome).ToColumn("NOME");
            Map(p => p.Valor).ToColumn("VALOR");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
        }
    }
}
