using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class AplicacaoMap : DommelEntityMap<Aplicacao>
    {
        public AplicacaoMap()
        {
            ToTable("SYS_APLICACAO");

            Map(p => p.AplicacaoId).ToColumn("SYS_APLICACAO_ID");
            Map(p => p.Nome).ToColumn("NOME");
            Map(p => p.Descricao).ToColumn("DESCRICAO");
            Map(p => p.Ativo).ToColumn("ATIVO");
            Map(p => p.NomeAssembly).ToColumn("DESC_NOME_ASSEMBLY");
            Map(p => p.OrigemCadastroId).ToColumn("ORIGEM_CADASTRO_ID");
        }
    }
}
