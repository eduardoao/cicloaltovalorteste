using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class ProdutoMap : DommelEntityMap<Produto>
    {
        public ProdutoMap()
        {
            ToTable("WMS_PRODUTO");

            Map(p => p.ProdutoId).ToColumn("PRODUTO_ID").IsKey();

            Map(p => p.ProdutoIdPai).ToColumn("PRODUTO_ID_PAI");

            Map(p => p.Nome).ToColumn("NOME");

            Map(p => p.DataCriacao).ToColumn("DATA_CRIACAO");

            Map(p => p.UsuarioIdCriacao).ToColumn("USUARIO_ID_CRIACAO");

            Map(p => p.ProdutoStatusId).ToColumn("PRODUTO_STATUS_ID");

            Map(p => p.Observacao).ToColumn("OBSERVACAO");

            Map(p => p.FlgEstoque).ToColumn("FLG_ESTOQUE");

            Map(p => p.Ordem).ToColumn("ORDEM");

            Map(p => p.PrazoEntrega).ToColumn("PRAZO_ENTREGA");

            Map(p => p.PalavrasChave).ToColumn("PALAVRAS_CHAVE");

            Map(p => p.FornecedorId).ToColumn("FORNECEDOR_ID");

            Map(p => p.DataAtualizacao).ToColumn("DATA_ATUALIZACAO");

            Map(p => p.UsuarioIdAtualizacao).ToColumn("USUARIO_ID_ATUALIZACAO");

            Map(p => p.PermiteDesconto).ToColumn("PERMITE_DESCONTO");

            Map(p => p.TipoProdutoId).ToColumn("TIPO_PRODUTO_ID");

            Map(p => p.FlgProdutoVenda).ToColumn("FLG_PRODUTO_VENDA");

            Map(p => p.TipoProdutoOperacaoId).ToColumn("TIPO_PRODUTO_OPERACAO_ID");

            Map(p => p.PermiteDotzBaixo).ToColumn("PERMITE_DOTZ_BAIXO");

            Map(p => p.NomeFull).ToColumn("NOME_FULL");

            Map(p => p.ClickRank).ToColumn("CLICK_RANK");

            Map(p => p.FlgPermiteTrocaPos).ToColumn("FLG_PERMITE_TROCA_POS");

            Map(p => p.TipoTrocaId).ToColumn("TIPO_TROCA_ID");

            Map(p => p.PermiteTransmissaoDotz).ToColumn("PERMITE_TRANSMISSAO_DOTZ");

            Map(p => p.LimiteTrocaPeriodo).ToColumn("LIMITE_TROCA_PERIODO");

            Map(p => p.PeriodoDias).ToColumn("PERIODO_DIAS");

            Map(p => p.MsgProcessoIdCustomizado).ToColumn("MSG_PROCESSO_ID_CUSTOMIZADO");

            Map(p => p.MsgProcessoIdPosAuditoria).ToColumn("MSG_PROCESSO_ID_POS_AUDITORIA");

            Map(p => p.DataAtivacaoInicio).ToColumn("DATA_ATIVACAO_INICIO");

            Map(p => p.DataAtivacaoFim).ToColumn("DATA_ATIVACAO_FIM");

            Map(p => p.Agrupador).ToColumn("AGRUPADOR");

            Map(p => p.EstoqueMinimo).ToColumn("ESTOQUE_MINIMO");

            Map(p => p.TipoArredondamentoId).ToColumn("TIPO_ARREDONDAMENTO_ID");

            Map(p => p.MsgProcessoSmsId).ToColumn("MSG_PROCESSO_SMS_ID");

            Map(p => p.TipoEnvioDisponiveis).ToColumn("TIPO_ENVIO_DISPONIVEIS");

            Map(p => p.TipoDescricao).ToColumn("TIPO_DESCRICAO");

            Map(p => p.NomeReduzido).ToColumn("NOME_REDUZIDO");

            Map(p => p.CargaArquivoId).ToColumn("CARGA_ARQUIVO_ID");

            Map(p => p.UsuarioIdParceiroRestrito).ToColumn("USUARIO_ID_PARCEIRO_RESTRITO");

            Map(p => p.NumeroPeriodoDiasValor).ToColumn("NUMERO_PERIODO_DIAS_VALOR");

            Map(p => p.ValorMaximoPeriodoDias).ToColumn("VALOR_MAXIMO_PERIODO_DIAS");

            Map(p => p.FlagHabilitaTrocaProgramada).ToColumn("FLAG_HABILITA_TROCA_PROGRAMADA");

            Map(p => p.CodigoProdutoAntecessor).ToColumn("CODIGO_PRODUTO_ANTECESSOR");

            Map(p => p.LimiteTrocasPeriodoMensal).ToColumn("LIMITE_TROCAS_PERIODO_MENSAL");

            Map(p => p.NumeroPeriodoDiasValorMensal).ToColumn("NUMERO_PERIODO_DIAS_VALOR_MENSAL");

            Map(p => p.Descricao).ToColumn("DESCRICAO");

            Map(p => p.CodigoEan).ToColumn("CODIGO_EAN");

            Map(p => p.FlagPrecificarExterno).ToColumn("FLG_PRECIFICAREXTERNO");

            Map(p => p.CategoriaPadraoId).ToColumn("WMS_CATEGORIA_ID_PADRAO");
        }
    }
}
