using System.Collections.Generic;
using CicloVidaAltoValor.Application.Contracts.Produto;

namespace CicloVidaAltoValor.Application.Contracts.Desejos
{
    public class DesejoProdutoEscolhaViewModel
    {
        public DesejoProdutoEscolhaViewModel()
        {
            
        }

        public DesejoProdutoEscolhaViewModel(string nome)
        {
            Nome = nome;
        }

        
        
        public string Nome { get; set; }
        public IList<ProdutoViewModel> Produtos { get; set; } = new List<ProdutoViewModel>();

        public bool IsFirst(ProdutoViewModel item)
        {
            return Produtos.IndexOf(item) == 0;
        }
        
    }
}
