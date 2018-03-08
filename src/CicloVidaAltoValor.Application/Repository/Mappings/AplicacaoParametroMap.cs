using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class AplicacaoParametroMap : DommelEntityMap<AplicacaoParametro>
    {
        public AplicacaoParametroMap()
        {
            ToTable("SYS_APLICACAO_PARAMETRO");

            Map(p => p.AplicacaoParametroId).ToColumn("SYS_APLICACAO_PARAMETRO_ID").IsKey().IsIdentity();
            Map(p => p.AplicacaoId).ToColumn("SYS_APLICACAO_ID");
            Map(p => p.ChaveParametro).ToColumn("CHAVE_PARAMETRO");
            Map(p => p.Ativo).ToColumn("ATIVO");
            Map(p => p.Valor).ToColumn("VALOR");
            Map(p => p.Observacao).ToColumn("OBSERVACAO");
            Map(p => p.AplicacaoParametroTipoDadosId).ToColumn("SYS_APLICACAO_PARAMETRO_TIPO_DADOS_ID");
            Map(p => p.AplicacaoParametroCategoriaId).ToColumn("SYS_APLICACAO_PARAMETRO_CATEGORIA_ID");
            Map(p => p.OrigemCadastroId).ToColumn("ORIGEM_CADASTRO_ID");
        }
    }
}
