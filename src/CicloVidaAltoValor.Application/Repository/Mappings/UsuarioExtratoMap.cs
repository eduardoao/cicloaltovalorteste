using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioExtratoMap : DommelEntityMap<UsuarioExtrato>
    {
        public UsuarioExtratoMap()
        {

            ToTable("CPR_USUARIO_EXTRATO");

            Map(p => p.UsuarioExtratoId).ToColumn("CPR_USUARIO_EXTRATO_ID").IsKey().IsIdentity();
            Map(p => p.UsuarioId).ToColumn("CPR_USUARIO_ID");
            Map(p => p.TransacaoFilaId).ToColumn("TRANSACAO_FILA_ID");
            Map(p => p.NomeParceiro).ToColumn("NOME_PARCEIRO");
            Map(p => p.DescricaoTransacao).ToColumn("DESC_TRANSACAO");
            Map(p => p.DataTicketCheckout).ToColumn("DT_TICKET_CHECKOUT");
            Map(p => p.PontosTotal).ToColumn("PONTOS_TOTAL");
            Map(p => p.PontosUsados).ToColumn("PONTOS_USADOS");
            
        }
    }
}
