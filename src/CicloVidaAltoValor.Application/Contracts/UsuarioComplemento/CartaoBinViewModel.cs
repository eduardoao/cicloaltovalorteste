using System;

namespace CicloVidaAltoValor.Application.Contracts.UsuarioComplemento
{
    public class CartaoBinViewModel
    {
        public int CartaoBinId { get; set; }
        public int Bin { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string Carteira { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
