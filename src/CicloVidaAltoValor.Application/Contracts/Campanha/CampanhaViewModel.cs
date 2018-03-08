using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CicloVidaAltoValor.Application.Contracts.Parceiro;

namespace CicloVidaAltoValor.Application.Contracts.Campanha
{
    public class CampanhaViewModel
    {
        /// <summary>
        ///  CPR_CAMPANHA_ID
        /// </summary>
        public int CampanhaId { get; set; }

        /// <summary>
        ///  NOME
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        ///  DT_INICIO
        /// </summary>
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data Inicial")]
        public DateTime DataInicio { get; set; }

        /// <summary>
        ///  DT_FIM
        /// </summary>
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data Final")]
        public DateTime DataFim { get; set; }


        /// <summary>
        ///  ATIVO
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        ///  CPR_CAMPANHA_TIPO_ID
        /// </summary>
        public int? CampanhaTipoId { get; set; }


        /// <summary>
        ///  USUARIO_ID_PARCEIRO
        /// </summary>
        public int? UsuarioIdParceiro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ParceiroViewModel Parceiro { get; set; }

     

        public string GetPartnerName()
        {
            return Parceiro != null ? Parceiro.Nome : string.Empty;
        }

        /// <summary>
        ///  GERAR_PREVISAO_FATURA_AUTOMATICO
        /// </summary>
        [Display(Name = @"Gerar Previsão Fatura Automático")]
        public bool? GerarPrevisaoFauturaAutomatico { get; set; }


        /// <summary>
        ///  GERAR_PREVISAO_FATURA_REALIZADO
        /// </summary>
        [Display(Name = @"Gerar Previsão Fatura Realizado")]
        public bool? GerarPrevisaoFaturaRealizado { get; set; }

        /// <summary>
        /// DESABILITA_RESGATE
        /// </summary>
        [Display(Name = @"Desabilitar Resgate")]
        public bool? DesabilitaResgate { get; set; }

        public bool GetGerarPrevisaoFauturaAutomatico()
        {
            return GerarPrevisaoFauturaAutomatico.GetValueOrDefault();
        }

        public bool GetGerarPrevisaoFaturaRealizado()
        {
            return GerarPrevisaoFaturaRealizado.GetValueOrDefault();
        }

        public bool GetDesabilitaResgate()
        {
            return this.DesabilitaResgate.GetValueOrDefault();
        }

    }
}
