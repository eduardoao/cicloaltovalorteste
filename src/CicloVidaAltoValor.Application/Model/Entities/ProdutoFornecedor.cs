using Dharma.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [WMS_PRODUTO_FORNECEDOR]
    /// </summary>
    public class ProdutoFornecedor : IEntity
    {
        public int ProdutoId { get; set; }

        public int FornecedorId { get; set; }

        public string ProdutoIdFornecedor { get; set; }

        public decimal Preco { get; set; }

        public decimal Frete { get; set; }

        public decimal? FatorConversaoPontos { get; set; }

        public decimal? Pontos { get; set; }

        public decimal Margem { get; set; }

        public bool Aprovado { get; set; }

        public bool? Disponivel { get; set; }

        public int? IdFatorConversao { get; set; }

        public bool? AprovadoBkp { get; set; }

        public decimal? PrecoVenda { get; set; }

        public int? EstoqueMinimo { get; set; }

        public int? UsuarioIdAlteracao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Observacao { get; set; }

        public short? IdTipoPrioridadeFornecedor { get; set; }

        public short? IdStatusImportacao { get; set; }

        public bool? PrecoFixo { get; set; }

        public int? Unificador { get; set; }

        public DateTime? DataAtualizacaoUnificador { get; set; }

        public bool? ProcessaUnificador { get; set; }

        public bool? FlagFreteFixo { get; set; }

        public bool DisponibilidadeObrigatoria { get; set; }

        public string UnificadorTxt { get; set; }

        public bool? FlagRecalculoPontos { get; set; }

        public bool? HabilitaDzReais { get; set; }

        public bool? HabilitaDotzReaisResultado { get; set; }

        public DateTime? DataProcessamentoDotzReais { get; set; }

        public bool? DePor { get; set; }

        public decimal? ValorDe { get; set; }

        public decimal? DescontoPerc { get; set; }

        public int? PrecoCustom { get; set; }

        public decimal? FreteCalculado { get; set; }

        public bool? Atualizado { get; set; }

        public decimal? PrecoVendaFornecedor { get; set; }

        public decimal? PrecoVendaFornecedorDe { get; set; }

        public string UrlSiteVendaProdutoFornecedor { get; set; }

        public string CodigoEan { get; set; }

        public int? Prioridade { get; set; }

        public int? FatorConversaoConfiguracaoId { get; set; }

        public int? FatorConversaoConfiguracaoIdFrete { get; set; }
    }
}
