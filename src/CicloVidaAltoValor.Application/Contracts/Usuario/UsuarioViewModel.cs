using System;
using System.ComponentModel.DataAnnotations;
using CicloVidaAltoValor.Application.Extensions;

namespace CicloVidaAltoValor.Application.Contracts.Usuario
{
    public class UsuarioViewModel
    {

        /// <summary>
        ///  NOME
        /// </summary>
        public string Nome { get; set; }


        /// <summary>
        ///  DT_NASCIMENTO
        /// </summary>
        public DateTime? DataNascimento { get; set; }

        [Display(Name = @"CPF")]
        /// <summary>
        ///  DOCUMENTO
        /// </summary>
        public string Documento { get; set; }

        [Display(Name = @"E-mail")]
        /// <summary>
        /// EMAIL
        /// </summary>
        public string Email { get; set; }

        [Display(Name = @"Endereço")]
        /// <summary>
        /// DESC_LOGRADOURO
        /// </summary>
        public string Logradouro { get; set; }

        /// <summary>
        /// NUMERO_LOGRADOURO
        /// </summary>
        [Display(Name = @"Número")]
        public string NumeroLogradouro { get; set; }

        /// <summary>
        /// USR_ESTADO_UF
        /// </summary>
        public string Estado { get; set; }

        [Display(Name = @"CEP")]
        /// <summary>
        /// NUMERO_CEP
        /// </summary>
        public string Cep { get; set; }

        /// <summary>
        /// NOME_CIDADE
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        /// NOME_BAIRRO
        /// </summary>
        public string Bairro { get; set; }


        /// <summary>
        /// DESC_COMPLEMENTO
        /// </summary>
        public string Complemento { get; set; }



        [Display(Name = @"Telefone Residencial")]
        /// <summary>
        /// NUMERO_TELEFONE1
        /// </summary>
        public string TelefoneResidencial { get; set; }



        [Display(Name = @"Telefone Celular")]
        /// <summary>
        /// NUMERO_TELEFONE2
        /// </summary>
        public string TelefoneCelular { get; set; }


        [Display(Name = @"Telefone Comercial")]
        /// <summary>
        /// NUMERO_TELEFONE3
        /// </summary>
        public string TelefoneComercial { get; set; }

        /// <summary>
        /// INDICADOR_SEXO
        /// </summary>
        public char? Sexo { get; set; }

        /// <summary>
        /// DT_OPT_OUT
        /// </summary>
        public bool Aceite { get; set; }

        /// <summary>
        /// CARTEIRA
        /// </summary>
        public string Carteira { get; set; }

        public void RemoveFormatacao()
        {
            this.Documento = this.Documento.JustNumbers();
            this.TelefoneCelular = this.TelefoneCelular.JustNumbers();
            this.TelefoneResidencial = this.TelefoneResidencial.JustNumbers();
            this.TelefoneComercial = this.TelefoneComercial.JustNumbers();
            this.Cep = this.Cep.JustNumbers();

        }
    }
}
