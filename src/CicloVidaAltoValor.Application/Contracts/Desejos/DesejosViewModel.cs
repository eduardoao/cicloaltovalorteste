using System.Collections.Generic;
using CicloVidaAltoValor.Application.Contracts.Produto;
using CicloVidaAltoValor.Application.Enum;
using System.Linq;
using CicloVidaAltoValor.Application.Contracts.Campanha;

namespace CicloVidaAltoValor.Application.Contracts.Desejos
{
    public class DesejosViewModel
    {

        /// <summary>
        /// 
        /// </summary>
        public bool DesejouFaseAtual { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CompletouCadastro { get; set; }

        /// <summary>
        /// Lista de Desejos (PRODUTOS)
        /// </summary>
        public List<ProdutoViewModel> Produtos { get; set; } = new List<ProdutoViewModel>();

        /// <summary>
        /// Desejos Realizados
        /// </summary>
        public IList<ProdutoViewModel> DesejosRealizados { get; set; } = new List<ProdutoViewModel>();

        /// <summary>
        /// Catálogo Usuário
        /// </summary>
        public CatalogoSeuDesejo CatalogoUsuarioSeuDesejo { get; set; }


        public bool PossuiStatus { get; set; }

        public string CatalogoUsuarioSeuDesejoToString()
        {
            return CatalogoUsuarioSeuDesejo.ToString();
        }

        public bool PossuiCatalogo()
        {
            return CatalogoUsuarioSeuDesejo.GetHashCode() != 0;
        }

        public string CatalogoUsuarioSeuDesejoToStringLower()
        {
            return CatalogoUsuarioSeuDesejo.ToString().ToLower();
        }
    


     
    }


}
