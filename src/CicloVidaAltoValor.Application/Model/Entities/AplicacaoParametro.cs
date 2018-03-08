using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class AplicacaoParametro : IEntity
    {
        public int AplicacaoParametroId { get; set; }

        public string AplicacaoId { get; set; }

        public string ChaveParametro { get; set; }

        public bool Ativo { get; set; }

        public string Valor { get; set; }

        public string Observacao { get; set; }

        public int AplicacaoParametroTipoDadosId { get; set; }

        public int AplicacaoParametroCategoriaId { get; set; }

        public int OrigemCadastroId { get; set; }

    }
}
