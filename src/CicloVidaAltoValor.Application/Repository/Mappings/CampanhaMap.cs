using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class CampanhaMap : DommelEntityMap<Campanha>
    {
        public CampanhaMap()
        {
            ToTable("CPR_CAMPANHA");

            Map(p => p.CampanhaId).ToColumn("CPR_CAMPANHA_ID").IsKey().IsIdentity();
            Map(p => p.Nome).ToColumn("NOME");
            Map(p => p.DataInicio).ToColumn("DT_INICIO");
            Map(p => p.DataFim).ToColumn("DT_FIM");
            Map(p => p.Ativo).ToColumn("ATIVO");
            Map(p => p.CampanhaTipoId).ToColumn("CPR_CAMPANHA_TIPO_ID");
            Map(p => p.UsuarioIdParceiro).ToColumn("USUARIO_ID_PARCEIRO");
            Map(p => p.GerarPrevisaoFauturaAutomatico).ToColumn("GERAR_PREVISAO_FATURA_AUTOMATICO");
            Map(p => p.GerarPrevisaoFaturaRealizado).ToColumn("GERAR_PREVISAO_FATURA_REALIZADO");
            Map(p => p.DesabilitaResgate).ToColumn("DESABILITA_RESGATE");

        }
    }
}
