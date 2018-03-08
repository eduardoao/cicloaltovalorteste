using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class CampanhaPrevisaoFatura : IEntity
    {
        /// <summary>
        /// CPR_CAMPANHA_PREVISAO_FATURA_ID
        /// </summary>
        public int CampanhaPrevisaoFaturaId { get; set; }

        /// <summary>
        /// CPR_CAMPANHA_ID
        /// </summary>
        public int CampanhaId { get; set; }

        /// <summary>
        /// DATA_PREVISAO_GERACAO_FATURA
        /// </summary>
        public DateTime DataPrevisaoGeracaoFatura { get; set; }

        /// <summary>
        /// DATA_REAL_GERACAO_FATURA
        /// </summary>
        public DateTime? DataRealGeracaoFatura { get; set; }

        /// <summary>
        /// DATA_CRIACAO
        /// </summary>
        public DateTime DataCriacao { get; set; }
    }
}
