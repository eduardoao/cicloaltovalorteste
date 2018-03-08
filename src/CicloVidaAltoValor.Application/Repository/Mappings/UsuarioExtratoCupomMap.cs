using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioExtratoCupomMap : DommelEntityMap<UsuarioExtratoCupom>
    {
        public UsuarioExtratoCupomMap()
        {
            ToTable("CPR_USUARIO_EXTRATO_CUPOM");

            Map(p => p.UsuarioExtratoId).ToColumn("CPR_USUARIO_EXTRATO_ID").IsKey().IsIdentity();
            Map(p => p.CupomId).ToColumn("CPR_CUPOM_ID");
            Map(p => p.GerouCupom).ToColumn("GEROU_CUPOM");
        }
    }
}
