using System;
using System.Collections.Generic;
using System.Linq;
using CicloVidaAltoValor.Application.Contracts.Campanha;
using CicloVidaAltoValor.Application.Contracts.Produto;
using CicloVidaAltoValor.Application.Contracts.UsuarioComplemento;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Extensions;


namespace CicloVidaAltoValor.Application.Contracts.ComoFunciona
{
    public class ComoFuncionaViewModel
    {
        public CatalogoSeuDesejo CatalogoUsuarioSeuDesejo { get; set; }

        public bool DesejouFaseAtual { get; set; }


        public Carteira Carteira { get; set; }

        public IEnumerable<UsuarioComplementoViewModel> Cartoes { get; set; } = new List<UsuarioComplementoViewModel>();

        public IList<ProdutoViewModel> ListaDesejos { get; set; } = new List<ProdutoViewModel>();

        public int Fase { get; set; }

      

        public bool PossuiCarrossel()
        {
            return ListaDesejos.Count > 3;
        }
        public bool PossuiCarrosselMobile()
        {
            return ListaDesejos.Count > 1;
        }

        public bool IsFirst(ProdutoViewModel item)
        {
            return ListaDesejos.IndexOf(item) == 0;
        }

        public bool IsFirstMobile(ProdutoViewModel item)
        {
            return ListaDesejos.IndexOf(item) < 2;
        }


        public IEnumerable<IGrouping<int, UsuarioComplementoViewModel>> GetGroupCards()
        {
            return Cartoes.GroupByCount(3);
        }

        public IEnumerable<ProdutoAgrupadoViewModel> ProdutosAgrupadosPorQuantidade()
        {
            return ListaDesejos.GroupByCount(3)
                .Select((x, i) => new ProdutoAgrupadoViewModel { Indice = i, Quantidade = x.Key, Produtos = x.ToList() })
                .OrderBy(x => x.GetCatalogoSeuDesejo());

        }

   



        public string GetBtnClass()
        {
            switch (Carteira)
            {
                case Carteira.VAREJO:
                    return "btn-azul-simples";

                case Carteira.ESTILO:
                    return "btn-azul-simples";

                case Carteira.PRIVATE:
                    return "btn-amarelo-simples";
                default:
                    return "btn-azul-simples";

            }
        }

    }
}
