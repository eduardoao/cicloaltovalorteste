using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Interfaces.Model;

namespace CicloVidaAltoValor.Application.Model.ValueObject
{
    public class UsuarioComplementoArquivo : IArquivo
    {
        public UsuarioComplementoArquivo()
        {

        }

        public UsuarioComplementoArquivo(TipoComplemento usuarioTipoComplemento, string nome, string valor)
        {
            UsuarioTipoComplemento = usuarioTipoComplemento;
            Nome = nome;
            Valor = valor;
        }

        public TipoComplemento UsuarioTipoComplemento { get; set; }

        public int TipoComplementoId => UsuarioTipoComplemento.GetHashCode();
        public string Nome { get; set; }
        public string Valor { get; set; }
        public string Erro { get; set; }
        public bool Valido { get; set; }

        public IArquivo Clone()
        {
            return (UsuarioComplementoArquivo) this.MemberwiseClone();
        }

        public void Read(string[] lines)
        {
            throw new System.NotImplementedException();
        }


        public string Set(string key, object value, string line)
        {
            throw new System.NotImplementedException();
        }
    }
}
