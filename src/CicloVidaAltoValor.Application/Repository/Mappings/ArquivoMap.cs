using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class ArquivoMap : DommelEntityMap<Arquivo>
    {
        public ArquivoMap()
        {
            ToTable("CPR_ARQUIVO");

            Map(p => p.Id).ToColumn("CPR_ARQUIVO_ID").IsKey().IsIdentity();
            Map(x => x.Nome).ToColumn("NOME");
            Map(x => x.QtdeRegistros).ToColumn("QTDE_REGISTROS");
            Map(x => x.QtdeRejeitados).ToColumn("QTDE_REJEITADOS");
            Map(x => x.NomeProcessado).ToColumn("NOME_PROCESSADO");
            Map(x => x.QtdeValidos).ToColumn("QTDE_VALIDOS");
            Map(x => x.DataInicioProcessamento).ToColumn("DATA_INICIO_PROCESSAMENTO");
            Map(x => x.DataFimProcessamento).ToColumn("DATA_FIM_PROCESSAMENTO");
            Map(x => x.TipoArquivo).ToColumn("TIPO_ARQUIVO");
            Map(x => x.Erro).ToColumn("ERRO");
            Map(x => x.DataCriacao).ToColumn("DATA_CRIACAO");
        }
    }
}
