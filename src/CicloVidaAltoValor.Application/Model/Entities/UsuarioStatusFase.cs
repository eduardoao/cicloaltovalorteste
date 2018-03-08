using System;
using Dharma.Repository;
using CicloVidaAltoValor.Application.Enum;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [CPR_USUARIO_STATUS_FASE]
    /// </summary>
    public class UsuarioStatusFase : IEntity
    {
        /// <summary>
        /// [CPR_USUARIO_STATUS_FASE_ID]
        /// </summary>
        //public int Id { get; set; }

        /// <summary>
        /// [CPR_ARQUIVO_ID]
        /// </summary>
        public int ArquivoId { get; set; }

        /// <summary>
        /// [CPR_CAMPANHA_FASE_ID]
        /// </summary>
        public int CampanhaFaseId { get; set; }

        public CampanhaFase CampanhaFase { get; set; }

        /// <summary>
        /// [CPR_USUARIO_ID]
        /// </summary>
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public DateTime Periodo { get; set; }
        public decimal Meta { get; set; }
        public string FaixaMeta { get; set; }
        public decimal Gasto { get; set; }
        public string GastoPercentual { get; set; }
        public bool Desafio1 { get; set; }
        public bool Desafio2 { get; set; }
        public bool Desafio3 { get; set; }
        public bool Desafio4 { get; set; }
        public bool Desafio5 { get; set; }
        public bool Desafio6 { get; set; }
        public bool Desafio7 { get; set; }

        public string Catalogo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public CatalogoSeuDesejo? GetCatalogoSeuDesejo()
        {
            if (!string.IsNullOrEmpty(Catalogo))
            {
                return (CatalogoSeuDesejo?)System.Enum.Parse(typeof(CatalogoSeuDesejo), this.Catalogo, true);
            }

            return null;

        }

        public FaixaMeta GetFaixaMeta()
        {
            /*
             
            1.500 a 3.500 - FAIXA_1
            3.501 a 6.000 - FAIXA_2
            6.001 a 10.000 - FAIXA_3
            10.001 a 15.000 - FAIXA_4
            15.001          - FAIXA_5 

             */
            return (FaixaMeta)System.Enum.Parse(typeof(FaixaMeta), this.FaixaMeta, true);
        }

        public bool ValidateMeta()
        {
            if (this.Meta >= 1.500m && Meta <= 3.500m)
            {
                //Faixa_1
                return true;
            }

            if (this.Meta >= 3.501m && Meta <= 6.000m)
            {
                //Faixa_2
                return true;
            }
            if (this.Meta >= 6.001m && Meta <= 10.000m)
            {
                //Faixa_3
                return true;
            }
            if (this.Meta >= 10.001m && Meta <= 15.000m)
            {
                //Faixa_4
                return true;
            }

            if (this.Meta >= 15.001m)
            {
                //Faixa_5
                return true;
            }
            return false;
        }

        public FaixaMeta GetFaixaMetaRule()
        {
            if (this.Meta >= 0m && Meta <= 3500m)
            {
                //Faixa_1
                return Enum.FaixaMeta.Faixa_1;
            }

            if (this.Meta >= 3501m && Meta <= 6000m)
            {
                //Faixa_2
                return Enum.FaixaMeta.Faixa_2;

            }
            if (this.Meta >= 6001m && Meta <= 10000m)
            {
                //Faixa_3
                return Enum.FaixaMeta.Faixa_3;

            }
            if (this.Meta >= 10001m && Meta <= 15000m)
            {
                //Faixa_4
                return Enum.FaixaMeta.Faixa_4;

            }

            //else if (this.Meta >= 15001m)
            //{
            //Faixa_5
            return Enum.FaixaMeta.Faixa_5;
            //}


        }

        public string GetPeriodo()
        {
            return this.Periodo.ToString("yyyy/MM");
        }

        public string GetGastoPercentual()
        {
            return $"{this.GastoPercentual}%";
        }

        public bool IsActive()
        {
            return this.Ativo;
        }


    }
}
