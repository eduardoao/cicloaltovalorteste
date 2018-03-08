using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class ProdutoFornecedorMap : DommelEntityMap<ProdutoFornecedor>
    {
        public ProdutoFornecedorMap()
        {
            ToTable("WMS_PRODUTO_FORNECEDOR");

            Map(p => p.ProdutoId).ToColumn("PRODUTO_ID");
            Map(p => p.FornecedorId).ToColumn("FORNECEDOR_ID");
            Map(p => p.ProdutoIdFornecedor).ToColumn("PRODUTO_ID_FORNECEDOR");
            Map(p => p.Preco).ToColumn("PRECO");
            Map(p => p.Frete).ToColumn("FRETE");
            Map(p => p.FatorConversaoPontos).ToColumn("FATOR_CONVERSAO_PONTOS");
            Map(p => p.Pontos).ToColumn("PONTOS");
            Map(p => p.Margem).ToColumn("MARGEM");
            Map(p => p.Aprovado).ToColumn("APROVADO");
            Map(p => p.Disponivel).ToColumn("DISPONIVEL");
            Map(p => p.IdFatorConversao).ToColumn("ID_FATOR_CONVERSAO");
            Map(p => p.AprovadoBkp).ToColumn("APROVADO_BKP");
            Map(p => p.PrecoVenda).ToColumn("PRECO_VENDA");
            Map(p => p.EstoqueMinimo).ToColumn("ESTOQUE_MINIMO");
            Map(p => p.UsuarioIdAlteracao).ToColumn("USUARIO_ID_ALTERACAO");
            Map(p => p.DataAlteracao).ToColumn("DATA_ALTERACAO");
            Map(p => p.Observacao).ToColumn("OBSERVACAO");
            Map(p => p.IdTipoPrioridadeFornecedor).ToColumn("ID_TIPO_PRIORIDADE_FORNECEDOR");
            Map(p => p.IdStatusImportacao).ToColumn("ID_STATUS_IMPORTACAO");
            Map(p => p.PrecoFixo).ToColumn("PRECO_FIXO");
            Map(p => p.Unificador).ToColumn("UNIFICADOR");
            Map(p => p.DataAtualizacaoUnificador).ToColumn("DATA_ATUALIZACAO_UNIFICADOR");
            Map(p => p.ProcessaUnificador).ToColumn("PROCESSA_UNIFICADOR");
            Map(p => p.FlagFreteFixo).ToColumn("FLAG_FRETE_FIXO");
            Map(p => p.DisponibilidadeObrigatoria).ToColumn("DISPONIBILIDADE_OBRIGATORIA");
            Map(p => p.UnificadorTxt).ToColumn("UNIFICADOR_TXT");
            Map(p => p.FlagRecalculoPontos).ToColumn("FLAG_RECALCULO_PONTOS");
            Map(p => p.HabilitaDzReais).ToColumn("HABILITA_DOTZ_REAIS");
            Map(p => p.HabilitaDotzReaisResultado).ToColumn("HABILITA_DOTZ_REAIS_RESULTADO");
            Map(p => p.DataProcessamentoDotzReais).ToColumn("DATA_PROCESSAMENTO_DOTZ_REAIS");
            Map(p => p.DePor).ToColumn("DE_POR");
            Map(p => p.ValorDe).ToColumn("VALOR_DE");
            Map(p => p.DescontoPerc).ToColumn("DESCONTO_PERC");
            Map(p => p.PrecoCustom).ToColumn("PRECO_CUSTOM");
            Map(p => p.FreteCalculado).ToColumn("FRETE_CALCULADO");
            Map(p => p.Atualizado).ToColumn("ATUALIZADO");
            Map(p => p.PrecoVendaFornecedor).ToColumn("PRECO_VENDA_FORNECEDOR");
            Map(p => p.PrecoVendaFornecedorDe).ToColumn("PRECO_VENDA_FORNECEDOR_DE");
            Map(p => p.UrlSiteVendaProdutoFornecedor).ToColumn("URL_SITE_VENDA_PRODUTO_FORNECEDOR");
            Map(p => p.CodigoEan).ToColumn("CODIGO_EAN");
            Map(p => p.Prioridade).ToColumn("PRIORIDADE");
            Map(p => p.FatorConversaoConfiguracaoId).ToColumn("WMS_FATOR_CONVERSAO_CONFIGURACAO_ID");
            Map(p => p.FatorConversaoConfiguracaoIdFrete).ToColumn("WMS_FATOR_CONVERSAO_CONFIGURACAO_ID_FRETE");
        }
    }
}
