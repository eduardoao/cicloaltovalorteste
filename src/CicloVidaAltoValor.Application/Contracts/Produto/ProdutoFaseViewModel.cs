using System;
using System.Globalization;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Extensions;


namespace CicloVidaAltoValor.Application.Contracts.Produto
{
    public class ProdutoFaseViewModel
    {
        public ProdutoFaseViewModel()
        {

        }

        public ProdutoFaseViewModel(int produtoId, int campanhaProdutoId, string nome, string urlImagem)
        {
            ProdutoId = produtoId;
            CampanhaProdutoId = campanhaProdutoId;
            Nome = nome;
            UrlImagem = urlImagem;
        }

        public int CampanhaProdutoFaseId { get; set; }
        public int ProdutoId { get; set; }
        public int CampanhaProdutoId { get; set; }
        public string Nome { get; set; }
        public string UrlImagem { get; set; }
        //public string Base64Imagem { get; set; }
        public string Voltagem { get; set; }
        public bool Eletrico { get; set; }
        public string Carteira { get; set; }
        public string FaixaMeta { get; set; }

        public DateTime Periodo { get; set; }

        public DateTime DataCriacaoResgate { get; set; }


        /// <summary>
        /// [CPR_CAMPANHA_FASE_ID]
        /// </summary>
        public int CampanhaFaseId { get; set; }

        //public CampanhaFaseViewModel CampanhaFase { get; set; }

        public string Catalogo { get; set; }

        public bool PodeDesejar { get; set; }

        public string GetByName()
        {
            return this.Nome.RemoveVolts();
        }

        public bool IsCurrentPeriod()
        {
            return DateTime.Now.Month == this.Periodo.Month && DateTime.Now.Year == this.Periodo.Year;
        }

        public bool IsCurrentPeriod(DateTime period)
        {
            return period.Month == this.Periodo.Month && period.Year == this.Periodo.Year;
        }

        //public bool CanWish(CatalogoSeuDesejo catalogoSeuDesejoUsuario, CampanhaFaseViewModel campanhaFase)
        //{

        //    var valid = false;


        //    switch (catalogoSeuDesejoUsuario)
        //    {
        //        case CatalogoSeuDesejo.Bronze:
        //            valid = catalogoSeuDesejoUsuario == GetCatalogoSeuDesejo();
        //            break;
        //        case CatalogoSeuDesejo.Prata:
        //            valid = catalogoSeuDesejoUsuario == GetCatalogoSeuDesejo() || GetCatalogoSeuDesejo() == CatalogoSeuDesejo.Bronze;
        //            break;
        //        case CatalogoSeuDesejo.Ouro:
        //            valid = catalogoSeuDesejoUsuario == GetCatalogoSeuDesejo() || GetCatalogoSeuDesejo() == CatalogoSeuDesejo.Bronze || GetCatalogoSeuDesejo() == CatalogoSeuDesejo.Prata;
        //            break;
        //        default:
        //            return false;
        //    }


        //    return this.CampanhaFaseId == campanhaFase.CampanhaFaseId && valid;
        //}

        public CatalogoSeuDesejo GetCatalogoSeuDesejo()
        {
            return (CatalogoSeuDesejo)System.Enum.Parse(typeof(CatalogoSeuDesejo), this.Catalogo, true);
        }



        /// <summary>
        /// Usado para desejos já realizados.
        /// </summary>
        /// <returns></returns>
        public string GetMonthName()
        {
            var currentMonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(Periodo.Month);

            return $"{currentMonthName[0].ToString().ToUpper()}{currentMonthName.Substring(1)}";
        }


        public string GetNameLowerCase()
        {
            return this.Nome.ToLower();
        }

        public string GetNameUpperCase()
        {
            return this.Nome.ToUpper();
        }



        public string GetCatalogLowerCase()
        {
            return this.Catalogo.ToLower();
        }

        public string GetCatalogUpperCase()
        {
            return this.Catalogo.ToUpper();
        }








    }





}
