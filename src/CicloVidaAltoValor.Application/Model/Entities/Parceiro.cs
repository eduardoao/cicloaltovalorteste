using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class Parceiro : IEntity
    {
        /// <summary>
        /// USUARIO_ID_PARCEIRO
        /// </summary>
        public int UsuarioIdParceiro { get; set; }

        public bool Ativo { get; set; }
        public int? BusinessId { get; set; }
        public string Nome { get; set; }
        public int ParceiroCategoriaId { get; set; }
        public string NomeFantasia { get; set; }
        public string ResponsavelMkt { get; set; }
        public string ResponsavelGerente { get; set; }
        public int ParceiroNegocioId { get; set; }
        public bool? PareiroPosPago { get; set; }

        public int? DiaFechamento { get; set; }

        public int? ModeloCobrancaId { get; set; }

        public bool? ValidarFuncionarioPos { get; set; }
        public bool? ControlarEstorno { get; set; }

        public int? MecanicaIdDefaultBonus { get; set; }
        public int? MecanicaIdDefaultBase { get; set; }
        public bool? FlagExigeTransacaoParaCadastro { get; set; }
        public int? MecanicaIdExigeTransacaoParaCadastro { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public bool FlagCriarFaturaAutomatica { get; set; }
        public string ResponsavelPraca { get; set; }

        public int? WmsFornecedorId { get; set; }

        public string Cnae { get; set; }

    }
}
