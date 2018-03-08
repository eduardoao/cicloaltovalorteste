using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioPremioMap : DommelEntityMap<UsuarioPremio>
    {
        public UsuarioPremioMap()
        {
            ToTable("CPR_USUARIO_PREMIO");

            Map(p => p.UsuarioPremioId).ToColumn("CPR_USUARIO_PREMIO_ID").IsKey().IsIdentity();
            Map(p => p.UsuarioId).ToColumn("CPR_USUARIO_ID");
            Map(p => p.CampanhaProdutoId).ToColumn("CPR_CAMPANHA_PRODUTO_ID");
            Map(p => p.DataPremioSelecionado).ToColumn("DH_PREMIO_SELECIONADO");
            Map(p => p.CampanhaExtratoId).ToColumn("CPR_CAMPANHA_EXTRATO_ID");
            Map(p => p.Prioridade).ToColumn("PRIORIDADE");
            Map(p => p.StatusId).ToColumn("STATUS_ID");
            Map(p => p.TransacaoFilaId).ToColumn("TRANSACAO_FILA_ID");
            Map(p => p.TransacaoFilaIdDebito).ToColumn("TRANSACAO_FILA_ID_DEBITO");
            Map(p => p.Pontos).ToColumn("PONTOS");
            Map(p => p.DataAtualizacao).ToColumn("DATA_ATUALIZACAO");
        }
    }
}
