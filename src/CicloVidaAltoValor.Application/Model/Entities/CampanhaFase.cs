using System;
using Dharma.Repository;
using CicloVidaAltoValor.Application.Enum;


namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [CPR_CAMPANHA_FASE]
    /// </summary>
    public class CampanhaFase : IEntity
    {
        /// <summary>
        /// [CPR_CAMPANHA_FASE_ID]
        /// </summary>
        public int CampanhaFaseId { get; set; }



        /// <summary>
        /// [CPR_CAMPANHA_ID]
        /// </summary>
        public int CampanhaId { get; set; }

        public Campanha Campanha { get; set; }

        public int Fase { get; set; }

        /// <summary>
        /// [PERIODO]
        /// </summary>
        public DateTime Periodo { get; set; }

        /// <summary>
        /// [DATA_INICIO_RESGATE]
        /// </summary>
        public DateTime? DataInicioResgate { get; set; }

        /// <summary>
        /// [DATA_FIM_RESGATE]
        /// </summary>

        public DateTime? DataFimResgate { get; set; }


        /// <summary>
        /// [DATA_CRIACAO]
        /// </summary>
        public DateTime DataCriacao { get; set; }

        public bool IsFase1()
        {
            return Fase == CampanhaSeuDesejoFase.Fase1.GetHashCode();
        }

        public bool IsFase2()
        {
            return Fase == CampanhaSeuDesejoFase.Fase2.GetHashCode();
        }

        public bool IsFase3()
        {
            return Fase == CampanhaSeuDesejoFase.Fase3.GetHashCode();
        }

        public CampanhaSeuDesejoFase GetFaseEnum()
        {
            return (CampanhaSeuDesejoFase)System.Enum.Parse(typeof(CampanhaSeuDesejoFase), this.Fase.ToString(), true);
        }

        public bool IsCurrentPeriod(DateTime period)
        {
            return period.Month == this.Periodo.Month && period.Year == this.Periodo.Year;
        }

        public string GetMetaName()
        {
            var nome = "";
            if (IsFase1())
            {
                nome = "META1";
            }
            if (IsFase2())
            {
                nome = "META2";
            }
            if (IsFase3())
            {
                nome = "META3";
            }

            return nome;
        }
    }
}
