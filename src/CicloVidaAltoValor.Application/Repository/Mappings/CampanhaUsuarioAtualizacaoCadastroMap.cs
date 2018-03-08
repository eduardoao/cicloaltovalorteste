using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class CampanhaUsuarioAtualizacaoCadastroMap : DommelEntityMap<CampanhaUsuarioAtualizacaoCadastro>
    {
        public CampanhaUsuarioAtualizacaoCadastroMap()
        {
            ToTable("CPR_CAMPANHA_USUARIO_ATUALIZACAO_CADASTRO");

            Map(p => p.CampanhaId).ToColumn("CPR_CAMPANHA_ID").IsKey();
            Map(p => p.UsuarioId).ToColumn("CPR_USUARIO_ID");
            Map(p => p.Bonificado).ToColumn("BONIFICADO");
            Map(p => p.DataAtualizacao).ToColumn("DATA_ATUALIZACAO");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
        }
    }
}
