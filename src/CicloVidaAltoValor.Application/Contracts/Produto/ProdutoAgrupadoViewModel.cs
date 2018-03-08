using System.Collections.Generic;
using System.Linq;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Extensions;

namespace CicloVidaAltoValor.Application.Contracts.Produto
{
    public class ProdutoAgrupadoViewModel
    {

        public string Mes { get; set; }
        public bool Atual { get; set; }
        public string Catalogo { get; set; }
        public int Quantidade { get; set; }
        public int Indice { get; set; }
        
        public IList<ProdutoViewModel> Produtos { get; set; } = new List<ProdutoViewModel>();


        public CatalogoSeuDesejo? GetCatalogoSeuDesejo()
        {
            if (!string.IsNullOrEmpty(Catalogo))
            {
                return (CatalogoSeuDesejo?)System.Enum.Parse(typeof(CatalogoSeuDesejo), this.Catalogo, true);
            }

            return null;
        }

      

        public IEnumerable<ProdutoAgrupadoViewModel> ProdutosAgrupadosPorQuantidade()
        {
            return Produtos.GroupByCount(3)
                .Select((x, i) => new ProdutoAgrupadoViewModel { Indice = i, Produtos = x.ToList() }).OrderBy(x => x.GetCatalogoSeuDesejo());

        }


        public bool IsFirst(ProdutoViewModel @item)
        {
            return Produtos.IndexOf(@item) == 0;
        }


        public string GetMonthLowerCase()
        {
            return this.Mes.ToLower();
        }

        public string GetMonthUpperCase()
        {
            return this.Mes.ToUpper();
        }



        public string GetCatalogLowerCase()
        {
            return this.Catalogo.ToLower();
        }

        public string GetCatalogUpperCase()
        {
            return this.Catalogo.ToUpper();
        }

        public string GetQuantidade()
        {
            return (Indice + 1).IntegerToWritten();
        }

    }
}
