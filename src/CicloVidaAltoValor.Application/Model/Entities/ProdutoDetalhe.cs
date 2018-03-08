using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class ProdutoDetalhe : IEntity
    {
        public int ProdutoDetalheId { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int TipoProdutoDetalheId { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCriacao { get; set; }
        public int UsuarioCriacaoId { get; set; }
        public string ConteudoTeste { get; set; }
        public DateTime? DataVerificacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public int? UsuarioIdAtualizacao { get; set; }
        public int? CodigoProdutoDetalheOpcao { get; set; }

    }
}
