using System;
using Dharma.Repository;
using CicloVidaAltoValor.Application.Enum;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [CPR_CAMPANHA_PRODUTO_FASE]
    /// </summary>
    public class CampanhaProdutoFase : IEntity
    {
        /// <summary>
        /// [CPR_CAMPANHA_PRODUTO_FASE_ID]
        /// </summary>
        public int CampanhaProdutoFaseId { get; set; }

        /// <summary>
        /// [CPR_CAMPANHA_PRODUTO_ID]
        /// </summary>
        public int CampanhaProdutoId { get; set; }

        public CampanhaProduto CampanhaProduto { get; set; }

        /// <summary>
        /// [CPR_CAMPANHA_FASE_ID]
        /// </summary>
        public int CampanhaFaseId { get; set; }

        public CampanhaFase CampanhaFase { get; set; }

        /// <summary>
        /// [CPR_ARQUIVO_ID]
        /// </summary>
        public int ArquivoId { get; set; }

        /// <summary>
        /// [PERIODO]
        /// </summary>
        public DateTime Periodo { get; set; }

        /// <summary>
        /// [FAIXA_META]
        /// </summary>
        public string FaixaMeta { get; set; }

        // [CARTEIRA]
        public string Carteira { get; set; }

        /// <summary>
        /// [CATALOGO]
        /// </summary>
        public string Catalogo { get; set; }

        /// <summary>
        /// [VOLTAGEM]
        /// </summary>
        public string Voltagem { get; set; }


        /// <summary>
        /// [URL_IMAGEM]
        /// </summary>
        public string UrlImagem { get; set; }

        /// <summary>
        /// [BASE64_IMAGEM]
        /// </summary>
        //public string Base64Imagem { get; set; }


        /// <summary>
        /// [DATA_CRIACAO]
        /// </summary>
        public DateTime DataCriacao { get; set; }

        public CatalogoSeuDesejo? GetCatalogoSeuDesejo()
        {
            if (!string.IsNullOrEmpty(Catalogo))
            {
                return (CatalogoSeuDesejo?)System.Enum.Parse(typeof(CatalogoSeuDesejo), this.Catalogo, true);
            }

            return null;
        }

        public void SetValue(CampanhaProdutoFase campanhaProdutoFase)
        {

            ArquivoId = campanhaProdutoFase.ArquivoId;
            Periodo = campanhaProdutoFase.Periodo;
            Carteira = campanhaProdutoFase.Carteira;
            Catalogo = campanhaProdutoFase.Catalogo;
            FaixaMeta = campanhaProdutoFase.FaixaMeta;
            UrlImagem = campanhaProdutoFase.UrlImagem;
            //Base64Imagem = campanhaProdutoFase.Base64Imagem;
            Voltagem = campanhaProdutoFase.Voltagem;
        }
    }
}
