using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class CampanhaProdutoFaseMap : DommelEntityMap<CampanhaProdutoFase>
    {
        public CampanhaProdutoFaseMap()
        {

            ToTable("CPR_CAMPANHA_PRODUTO_FASE");

            Map(p => p.CampanhaProdutoFaseId).ToColumn("CPR_CAMPANHA_PRODUTO_FASE_ID").IsKey().IsIdentity();
            Map(p => p.CampanhaFaseId).ToColumn("CPR_CAMPANHA_FASE_ID");
            Map(p => p.CampanhaProdutoId).ToColumn("CPR_CAMPANHA_PRODUTO_ID");
            Map(p => p.ArquivoId).ToColumn("CPR_ARQUIVO_ID");
            Map(p => p.FaixaMeta).ToColumn("FAIXA_META");
            Map(p => p.Carteira).ToColumn("CARTEIRA");
            Map(p => p.Catalogo).ToColumn("CATALOGO");
            Map(p => p.Voltagem).ToColumn("VOLTAGEM");
            Map(p => p.UrlImagem).ToColumn("URL_IMAGEM");
            //Map(p => p.Base64Imagem).ToColumn("BASE64_IMAGEM");
            Map(p => p.Periodo).ToColumn("PERIODO");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");

        }
    }
}
