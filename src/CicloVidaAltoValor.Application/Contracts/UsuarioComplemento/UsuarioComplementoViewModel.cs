using System;
using System.IO;
using System.Linq;
using CicloVidaAltoValor.Application.Contracts.Usuario;

namespace CicloVidaAltoValor.Application.Contracts.UsuarioComplemento
{
    public class UsuarioComplementoViewModel
    {
        public UsuarioComplementoViewModel()
        {
        }


        /// <summary>
        /// [CPR_USUARIO_ID] 
        /// </summary>
        public int UsuarioId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public UsuarioViewModel Usuario { get; set; }

        /// <summary>
        /// [TIPO_COMPLEMENTO_ID]
        /// </summary>
        public int TipoComplementoId { get; set; }

        /// <summary>
        /// [NOME]
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// [VALOR]
        /// </summary>
        public string Valor { get; set; }


        /// <summary>
        /// [DATA_CRIACAO]
        /// </summary>
        public DateTime DataCriacao { get; set; }

        public CartaoBinViewModel CartaoBin { get; set; }



        private string GetBin()
        {
            return !string.IsNullOrEmpty(this.Valor) && this.Valor.Length >= 6 ? this.Valor.Substring(0, 6) : string.Empty;
        }

        public string GetCardImage(string carteira)
        {
            var basePath = Path.Combine("wwwroot", $"images-{carteira}");

            var imageDefaultPath = Path.Combine(basePath, "ourocard.png");

            if (string.IsNullOrEmpty(GetBin()))
            {
                return imageDefaultPath;
            }

            var path = Directory.GetFileSystemEntries(basePath, $"{GetBin()}*.png", SearchOption.AllDirectories).FirstOrDefault();

            return path == null ? imageDefaultPath.Replace("wwwroot","") : path.Replace("wwwroot", "");
        }


    }
}
