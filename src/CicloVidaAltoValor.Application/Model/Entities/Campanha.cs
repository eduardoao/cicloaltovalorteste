using System;
using System.Collections.Generic;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// CPR_CAMPANHA
    /// </summary>
    public class Campanha : IEntity
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
        public DateTime DataInicio { get; set; }

        /// <summary>
        ///  DT_FIM
        /// </summary>
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
        /// 
        /// </summary>

        public CampanhaTipo CampanhaTipo { get; set; }


        /// <summary>
        ///  USUARIO_ID_PARCEIRO
        /// </summary>
        public int? UsuarioIdParceiro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Parceiro Parceiro { get; set; }

        /// <summary>
        ///  GERAR_PREVISAO_FATURA_AUTOMATICO
        /// </summary>
        public bool? GerarPrevisaoFauturaAutomatico { get; set; }


        /// <summary>
        ///  GERAR_PREVISAO_FATURA_REALIZADO
        /// </summary>
        public bool? GerarPrevisaoFaturaRealizado { get; set; }

        /// <summary>
        /// DESABILITA_RESGATE
        /// </summary>
        public bool? DesabilitaResgate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CampanhaFase> Fases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CampanhaPrevisaoFatura> PrevisaoFaturas { get; set; }
        

    }
}
