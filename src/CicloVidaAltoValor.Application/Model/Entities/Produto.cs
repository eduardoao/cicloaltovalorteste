using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{

    /// <summary>
    /// [WMS_PRODUTO]
    /// </summary>
    public class Produto : IEntity
    {
        public int ProdutoId { get; set; }

        public int? ProdutoIdPai { get; set; }

        public string Nome { get; set; }

        public DateTime DataCriacao { get; set; }

        public int UsuarioIdCriacao { get; set; }

        public int ProdutoStatusId { get; set; }

        public string Observacao { get; set; }

        public bool FlgEstoque { get; set; }

        public int Ordem { get; set; }

        public string Descricao { get; set; }

        public string PrazoEntrega { get; set; }

        public string PalavrasChave { get; set; }

        public int? FornecedorId { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public int? UsuarioIdAtualizacao { get; set; }

        public bool? PermiteDesconto { get; set; }

        public int? TipoProdutoId { get; set; }

        public bool? FlgProdutoVenda { get; set; }

        public int TipoProdutoOperacaoId { get; set; }

        public byte? PermiteDotzBaixo { get; set; }

        public string NomeFull { get; set; }

        public decimal ClickRank { get; set; }

        public bool? FlgPermiteTrocaPos { get; set; }

        public int? TipoTrocaId { get; set; }

        public bool? PermiteTransmissaoDotz { get; set; }

        public int? LimiteTrocaPeriodo { get; set; }

        public int? PeriodoDias { get; set; }

        public int? MsgProcessoIdCustomizado { get; set; }

        public int? MsgProcessoIdPosAuditoria { get; set; }

        public DateTime? DataAtivacaoInicio { get; set; }

        public DateTime? DataAtivacaoFim { get; set; }

        public bool? Agrupador { get; set; }

        public int? EstoqueMinimo { get; set; }

        public int? TipoArredondamentoId { get; set; }

        public int? MsgProcessoSmsId { get; set; }

        public int TipoEnvioDisponiveis { get; set; }

        public int TipoDescricao { get; set; }

        public string NomeReduzido { get; set; }

        public int? CargaArquivoId { get; set; }

        public int? UsuarioIdParceiroRestrito { get; set; }

        public int? NumeroPeriodoDiasValor { get; set; }

        public decimal? ValorMaximoPeriodoDias { get; set; }

        public bool? FlagHabilitaTrocaProgramada { get; set; }

        public int? CodigoProdutoAntecessor { get; set; }

        public int CategoriaPadraoId { get; set; }

        public bool? LimiteTrocasPeriodoMensal { get; set; }

        public string CodigoEan { get; set; }

        public bool? NumeroPeriodoDiasValorMensal { get; set; }

        public char? FlagPrecificarExterno { get; set; }

        public ProdutoFornecedor ProdutoFornecedor { get; set; }
    }
}
