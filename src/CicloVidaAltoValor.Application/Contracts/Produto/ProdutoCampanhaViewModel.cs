using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CicloVidaAltoValor.Application.Enum;

namespace CicloVidaAltoValor.Application.Contracts.Produto
{
    public class ProdutoCampanhaViewModel
    {
        public int ProdutoCampanhaId { get; set; }
        public int CampanhaProdutoId { get; set; }

        public CampanhaProdutoViewModel CampanhaProduto { get; set; }

        public Carteira Carteira { get; set; }
        [Display(Name = "Url Imagem")]
        public string UrlImagem { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
