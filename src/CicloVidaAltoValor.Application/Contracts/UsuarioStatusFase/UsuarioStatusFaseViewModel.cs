using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CicloVidaAltoValor.Application.Contracts.Campanha;
using CicloVidaAltoValor.Application.Contracts.Usuario;
using CicloVidaAltoValor.Application.Enum;

namespace CicloVidaAltoValor.Application.Contracts.UsuarioStatusFase
{
    public class UsuarioStatusFaseViewModel
    {
        /// <summary>
        /// [CPR_USUARIO_STATUS_FASE_ID]
        /// </summary>
        //public int Id { get; set; }


        /// <summary>
        /// [CPR_CAMPANHA_FASE_ID]
        /// </summary>
        public int CampanhaFaseId { get; set; }


        /// <summary>
        /// [CPR_ARQUIVO_ID]
        /// </summary>
        public int ArquivoId { get; set; }

        /// <summary>
        /// [CPR_USUARIO_ID]
        /// </summary>
        public int UsuarioId { get; set; }

        public UsuarioViewModel Usuario { get; set; }

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

        public bool PossuiFase { get; set; }

      

        public bool HasCatalog()
        {
            return !string.IsNullOrEmpty(Catalogo);
        }

        [Display(Name = @"Data da última atualização")]
        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string GetClassDesafio1()
        {
            return this.Desafio1 ? "fa-check" : "fa-circle-thin";
        }

        public string GetClassDesafio2()
        {
            if (this.Desafio1)
            {
                return this.Desafio2 ? "fa-check" : "fa-circle-thin";
            }
            else
            {
                return this.Desafio2 ? "fa-circle-thin incomplete" : "fa-circle-thin";
            }
        }

        public string GetClassDesafio3()
        {
            if (this.Desafio1)
            {
                return this.Desafio3 ? "fa-check" : "fa-circle-thin";
            }
            else
            {
                return this.Desafio3 ? "fa-circle-thin incomplete" : "fa-circle-thin";
            }
        }

        public string GetClassDesafio4()
        {
            if (this.Desafio1)
            {
                return this.Desafio4 ? "fa-check" : "fa-circle-thin";
            }
            else
            {
                return this.Desafio4 ? "fa-circle-thin incomplete" : "fa-circle-thin";
            }
        }

        public string GetClassDesafio5()
        {
            if (this.Desafio1)
            {
                return this.Desafio5 ? "fa-check" : "fa-circle-thin";
            }
            else
            {
                return this.Desafio5 ? "fa-circle-thin incomplete" : "fa-circle-thin";
            }
        }

        public string GetClassDesafio6()
        {
            if (this.Desafio1)
            {
                return this.Desafio6 ? "fa-check" : "fa-circle-thin";
            }
            else
            {
                return this.Desafio6 ? "fa-circle-thin incomplete" : "fa-circle-thin";
            }
        }

        public string GetClassDesafio7()
        {
            if (this.Desafio1)
            {
                return this.Desafio7 ? "fa-check" : "fa-circle-thin";
            }
            else
            {
                return this.Desafio7 ? "fa-circle-thin incomplete" : "fa-circle-thin";
            }
        }

        public CatalogoSeuDesejo? GetCatalogoSeuDesejo()
        {
            if (!string.IsNullOrEmpty(Catalogo))
            {
                return (CatalogoSeuDesejo?)System.Enum.Parse(typeof(CatalogoSeuDesejo), this.Catalogo, true);
            }

            return null;
        }

        public string GetPeriodo()
        {
            return this.Periodo.ToString("yyyy/MM");
        }

        public bool IsActive()
        {
            return this.Ativo;
        }

        public string GetMeta()
        {
            return this.Meta.ToString("N0");
        }
      
        public string GetGasto()
        {
            return this.Gasto.ToString("N0");
        }

        public string GetDataAtualizacaoText()
        {
            var challengeText = "";

            if (DataCriacao != DateTime.MinValue && DataCriacao != default(DateTime))
            {
                challengeText = $"Seus desafios foram atualizados no dia {DataCriacao:dd/MM/yyyy}, dados atualizados toda sexta-feira.";
            }

            return challengeText;
            
        }
    }
}
