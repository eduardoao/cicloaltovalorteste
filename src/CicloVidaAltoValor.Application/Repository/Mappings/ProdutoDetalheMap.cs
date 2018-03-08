using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class ProdutoDetalheMap : DommelEntityMap<ProdutoDetalhe>
    {
        public ProdutoDetalheMap()
        {
            ToTable("WMS_PRODUTO_DETALHE");

            Map(p => p.ProdutoDetalheId).ToColumn("ID_PRODUTO_DETALHE").IsKey().IsIdentity();
            Map(p => p.ProdutoId).ToColumn("PRODUTO_ID");
            Map(p => p.TipoProdutoDetalheId).ToColumn("TIPO_PRODUTO_DETALHE_ID");
            Map(p => p.Conteudo).ToColumn("CONTEUDO");
            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");
            Map(p => p.UsuarioCriacaoId).ToColumn("USUARIO_CRIACAO_ID");
            Map(p => p.ConteudoTeste).ToColumn("CONTEUDO_TESTE");
            Map(p => p.DataVerificacao).ToColumn("DATA_VERIFICACAO");
            Map(p => p.DataAtualizacao).ToColumn("DATA_ATUALIZACAO");
            Map(p => p.UsuarioIdAtualizacao).ToColumn("USUARIO_ID_ATUALIZACAO");
            Map(p => p.CodigoProdutoDetalheOpcao).ToColumn("CODIGO_PRODUTO_DETALHE_OPCAO");
            



        }
    }
}
