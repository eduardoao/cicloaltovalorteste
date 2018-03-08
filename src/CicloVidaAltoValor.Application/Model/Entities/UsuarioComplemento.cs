using System;
using Dharma.Repository;
using CicloVidaAltoValor.Application.Enum;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    /// <summary>
    /// [CPR_USUARIO_COMPLEMENTO]
    /// </summary>
    public class UsuarioComplemento : IEntity
    {

        /// <summary>
        /// [CPR_USUARIO_ID] 
        /// </summary>
        public int UsuarioId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Usuario Usuario { get; set; }

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


        //public CartaoBin CartaoBin { get; set; }

        /// <summary>
        /// [DATA_CRIACAO]
        /// </summary>
        public DateTime DataCriacao { get; set; }


        public FaixaMeta GetFaixaMeta()
        {
            var meta = Convert.ToDecimal(Valor);

            if (meta >= 0m && meta <= 3500m)
            {
                //Faixa_1
                return Enum.FaixaMeta.Faixa_1;
            }

            if (meta >= 3501m && meta <= 6000m)
            {
                //Faixa_2
                return Enum.FaixaMeta.Faixa_2;

            }
            if (meta >= 6001m && meta <= 10000m)
            {
                //Faixa_3
                return Enum.FaixaMeta.Faixa_3;

            }
            if (meta >= 10001m && meta <= 15000m)
            {
                //Faixa_4
                return Enum.FaixaMeta.Faixa_4;

            }

            if (meta >= 15001m)
            {
                //Faixa_5
                return Enum.FaixaMeta.Faixa_5;
            }

            return Enum.FaixaMeta.Faixa_5;
        }
    }
}
