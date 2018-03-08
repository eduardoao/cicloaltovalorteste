using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class ProdutoCampanha : IEntity
    {
        public int ProdutoCampanhaId { get; set; }
        public int CampanhaProdutoId { get; set; }
        public CampanhaProduto CampanhaProduto { get; set; }
        public string Carteira { get; set; }
        public string UrlImagem { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Ativo { get; set; }



    }
}
