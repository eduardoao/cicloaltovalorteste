using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class Aplicacao : IEntity
    {
        /// <summary>
        /// SYS_APLICACAO_ID
        /// </summary>
        public string AplicacaoId { get; set; }


        /// <summary>
        /// NOME
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// DESCRICAO
        /// </summary>
        public string Descricao { get; set; }


        /// <summary>
        /// ATIVO
        /// </summary>
        public bool Ativo { get; set; }


        /// <summary>
        /// DESC_NOME_ASSEMBLY
        /// </summary>
        public string NomeAssembly { get; set; }


        /// <summary>
        /// ORIGEM_CADASTRO_ID
        /// </summary>
        public int OrigemCadastroId { get; set; }

    }
}
