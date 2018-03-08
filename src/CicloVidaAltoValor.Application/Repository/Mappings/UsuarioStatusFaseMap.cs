using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioStatusFaseMap : DommelEntityMap<UsuarioStatusFase>
    {
        public UsuarioStatusFaseMap()
        {
            ToTable("CPR_USUARIO_STATUS_FASE");

            
            Map(x => x.CampanhaFaseId).ToColumn("CPR_CAMPANHA_FASE_ID").IsKey();
            Map(x => x.ArquivoId).ToColumn("CPR_ARQUIVO_ID");
            Map(x => x.UsuarioId).ToColumn("CPR_USUARIO_ID");
            Map(x => x.Periodo).ToColumn("PERIODO");
            Map(x => x.Meta).ToColumn("META");
            Map(x => x.FaixaMeta).ToColumn("FAIXA_META");
            Map(x => x.Gasto).ToColumn("GASTO");
            Map(x => x.GastoPercentual).ToColumn("GASTO_PERCENTUAL");
            Map(x => x.Desafio1).ToColumn("DESAFIO_1");
            Map(x => x.Desafio2).ToColumn("DESAFIO_2");
            Map(x => x.Desafio3).ToColumn("DESAFIO_3");
            Map(x => x.Desafio4).ToColumn("DESAFIO_4");
            Map(x => x.Desafio5).ToColumn("DESAFIO_5");
            Map(x => x.Desafio6).ToColumn("DESAFIO_6");
            Map(x => x.Desafio7).ToColumn("DESAFIO_7");
            Map(x => x.Catalogo).ToColumn("CATALOGO");
            Map(x => x.Ativo).ToColumn("ATIVO");
            Map(x => x.DataCriacao).ToColumn("DATA_CRIACAO");
            Map(x => x.DataAtualizacao).ToColumn("DATA_ATUALIZACAO");
            
    }
    }
}
