namespace CicloVidaAltoValor.Application.Contracts.CepTotal
{
    public class CepTotalViewModel
    {
        /// <summary>
        /// Numero do CEP do endereço.
        /// </summary>
        public string Cep { get; set; }
        /// <summary>
        /// Descrição do logradouro do endereço.
        /// </summary>
        public string Endereco { get; set; }
        /// <summary>
        /// Nome do Bairro.
        /// </summary>
        public string Bairro { get; set; }
        /// <summary>
        ///  Nome da cidade.
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Código do estado.
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        ///  Complemento do endereço.
        /// </summary>
        public string Complemento { get; set; }
        /// <summary>
        ///  Endereço abreviado.
        /// </summary>
        public string EnderecoAbreviado { get; set; }
        /// <summary>
        /// Tipo de endereço.
        /// </summary>
        public string EnderecoTipo { get; set; }
    }
}
