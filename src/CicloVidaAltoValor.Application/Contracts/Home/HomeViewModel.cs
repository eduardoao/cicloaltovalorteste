using System.Collections.Generic;
using CicloVidaAltoValor.Application.Contracts.Campanha;
using CicloVidaAltoValor.Application.Contracts.Produto;
using CicloVidaAltoValor.Application.Contracts.Usuario;

namespace CicloVidaAltoValor.Application.Contracts.Home
{
    public class HomeViewModel
    {
        public UsuarioViewModel Usuario { get; set; }

        public IEnumerable<ProdutoCampanhaViewModel> Produtos { get; set; }

        public bool CompletouCadastro { get; set; }
        public bool PodeResgatar { get; set; }

        public CampanhaViewModel Campanha { get; set; }
    }
}
