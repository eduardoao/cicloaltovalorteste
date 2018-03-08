using System;
using System.IO;
using Dharma.Repository;
using CicloVidaAltoValor.Application.Enum;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class Arquivo : IEntity
    {
        public Arquivo()
        {

        }

        public Arquivo(string nome, int tipoArquivo)
        {
            Nome = nome;
            TipoArquivo = tipoArquivo;
        }
        /// <summary>
        /// [CPR_ARQUIVO_SEU_DESEJO_ID]
        /// </summary>
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeProcessado
        {
            get
            {
                {
                    var id = Id > 0 ? Id.ToString() : $"XX_ERROR_{new Random().Next(4)}_{Path.GetFileNameWithoutExtension(Nome)}";
                    return $"{id}_{DateTime.Now:yyyyMMdd}_{Path.GetFileNameWithoutExtension(Nome)}{Path.GetExtension(Nome)}";
                }
            }
            private set {  }
        }
        public int QtdeRegistros { get; set; }
        public int QtdeRejeitados { get; set; }
        public int QtdeValidos { get; set; }
        public DateTime DataInicioProcessamento { get; set; }
        public DateTime? DataFimProcessamento { get; set; }
        public DateTime DataCriacao { get; set; }
        public int TipoArquivo { get; set; }
        public string Erro { get; set; }

        public void SetNewFileName()
        {
            var id = Id > 0 ? Id.ToString() : $"XX_ERROR_{new Random().Next(4)}_{Path.GetFileNameWithoutExtension(Nome)}";
            NomeProcessado = $"{id}_{DateTime.Now:yyyyMMdd}{Path.GetExtension(Nome)}";
        }

        public TipoArquivoSeuDesejo GetTypeFileEnum()
        {
            return (TipoArquivoSeuDesejo)System.Enum.Parse(typeof(TipoArquivoSeuDesejo), TipoArquivo.ToString(), true);
        }
    }

}
