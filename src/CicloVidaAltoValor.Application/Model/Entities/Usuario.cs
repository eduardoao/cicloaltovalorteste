using System;
using System.Text;
using Dharma.Repository;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class Usuario : IEntity
    {
        /// <summary>
        /// CPR_USUARIO_ID
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        ///  CPR_CAMPANHA_ID
        /// </summary>
        public int CampanhaId { get; set; }

        /// <summary>
        ///  USR_USUARIO_ID
        /// </summary>
        public int? UsrUsuarioId { get; set; }

        /// <summary>
        /// DT_OPT_IN
        /// </summary>
        public DateTime DataOptin { get; set; }

        /// <summary>
        /// NIVEL
        /// </summary>
        public int? Nivel { get; set; }

        /// <summary>
        ///  NIVEL_COMPARTILHADO
        /// </summary>
        public bool? NivelCompartilhado { get; set; }


        /// <summary>
        ///  APELIDO
        /// </summary>
        public string Apelido { get; set; }

        /// <summary>
        ///  NOME
        /// </summary>
        public string Nome { get; set; }


        /// <summary>
        ///  DT_NASCIMENTO
        /// </summary>
        public DateTime? DataNascimento { get; set; }

        /// <summary>
        ///  DOCUMENTO
        /// </summary>
        public string Documento { get; set; }


        /// <summary>
        /// EMAIL
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// DESC_LOGRADOURO
        /// </summary>
        public string Logradouro { get; set; }

        /// <summary>
        /// NUMERO_LOGRADOURO
        /// </summary>
        public string NumeroLogradouro { get; set; }

        /// <summary>
        /// USR_ESTADO_UF
        /// </summary>
        public string Estado { get; set; }


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

        /// <summary>
        /// INFORMACOES_ADICIONAIS
        /// </summary>
        public string InformacoesAdicionais { get; set; }

        /// <summary>
        /// NUMERO_DDD1
        /// </summary>
        public int? DddResidencial { get; set; }

        /// <summary>
        /// NUMERO_TELEFONE1
        /// </summary>
        public string TelefoneResidencial { get; set; }


        /// <summary>
        /// NUMERO_DDD2
        /// </summary>
        public int? DddCelular { get; set; }

        /// <summary>
        /// NUMERO_TELEFONE2
        /// </summary>
        public string TelefoneCelular { get; set; }

        /// <summary>
        /// NUMERO_DDD3
        /// </summary>
        public int? DddComercial { get; set; }

        /// <summary>
        /// NUMERO_TELEFONE3
        /// </summary>
        public string TelefoneComercial { get; set; }

        /// <summary>
        /// INDICADOR_SEXO
        /// </summary>
        public char? Sexo { get; set; }

        /// <summary>
        /// DT_ULTIMA_ATUALIZACAO
        /// </summary>
        public DateTime? DataUltimaAtualizacao { get; set; }

        /// <summary>
        /// OBSERVACAO
        /// </summary>
        public string Observacao { get; set; }

        /// <summary>
        /// CPR_USUARIO_ORIGEM_ID
        /// </summary>
        public int? UsuarioOrigemId { get; set; }

        /// <summary>
        /// CARTEIRA
        /// </summary>
        public string Carteira { get; set; }

        /// <summary>
        /// DT_OPT_OUT
        /// </summary>
        public DateTime? DataOptOut { get; set; }

        public string ToFile500(int usuarioIdParceiro, int lojaId, int mecanicaId)
        {
            const string cultureDate = "yyyy-MM-dd";

            //500|1012759|4184|693|2|58750100700||0.0|2017-09-25|00:02:51|||200

            return new StringBuilder("500").Append("|")
                       .Append(usuarioIdParceiro).Append("|")
                       .Append(lojaId).Append("|")
                       .Append(mecanicaId).Append("|")
                       .Append("2").Append("|")
                       .Append(Documento).Append("|")
                       .Append(string.Empty).Append("|")
                       .Append("0.0").Append("|")
                       .Append(DateTime.Now.ToString(cultureDate)).Append("|")
                       .Append(DateTime.Now.ToString("HH:mm:ss")).Append("|")
                       .Append(string.Empty).Append("|")
                       .Append(string.Empty).Append("|")
                       .Append("100")
                       .ToString();
        }

        public void SetRegisterUpdate(Usuario viewModel)
        {
            this.Sexo = viewModel.Sexo;

            this.Logradouro = viewModel.Logradouro;
            this.NumeroLogradouro = viewModel.NumeroLogradouro;
            this.Cep = viewModel.Cep;
            this.Cidade = viewModel.Cidade;
            this.Bairro = viewModel.Bairro;
            this.Estado = viewModel.Estado;
            this.Complemento = viewModel.Complemento;

            this.TelefoneResidencial = viewModel.TelefoneResidencial;
            this.TelefoneCelular = viewModel.TelefoneCelular;
            this.TelefoneComercial = viewModel.TelefoneComercial;

            this.DddCelular = viewModel.DddCelular;
            this.DddComercial = viewModel.DddComercial;
            this.DddResidencial = viewModel.DddResidencial;

            this.Email = viewModel.Email;

            this.DataOptOut = viewModel.DataOptOut;
            
        }
    }
}
