using CicloVidaAltoValor.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Interfaces.Model;

namespace CicloVidaAltoValor.Application.Model.ValueObject
{
    public class UsuarioArquivo : IArquivo
    {
        public string Complemento { get; set; }
        public string Documento { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Logradouro { get; set; }
        public string Nome { get; set; }
        public string NumeroLogradouro { get; set; }
        public char Sexo { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public int? DddResidencial { get; set; }
        public string TelefoneResidencial { get; set; }
        public int? DddCelular { get; set; }
        public string TelefoneCelular { get; set; }
        public int? DddComercial { get; set; }
        public string TelefoneComercial { get; set; }
        public string Email { get; set; }
        public string Carteira { get; set; }
        public List<UsuarioComplementoArquivo> UsuarioComplemento { get; private set; } = new List<UsuarioComplementoArquivo>();


        public IArquivo Clone()
        {
            var item = (UsuarioArquivo)MemberwiseClone();
            item.UsuarioComplemento = new List<UsuarioComplementoArquivo>(item.UsuarioComplemento.Select(x => x.Clone()).Cast<UsuarioComplementoArquivo>());
            return item;
        }




        public string Set(string key, object value, string line)
        {
            key = key.ToUpper();

            try
            {
                switch (key)
                {
                    case "CPF":
                        if (value.ToString().IsCpf())
                        {
                            this.Documento = value?.ToString();
                        }
                        else
                        {
                            this.Erro = "CPF inválido. ";
                            line += ";CPF inválido";
                        }
                        break;

                    case "NOME":
                        if (!string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            Nome = value?.ToString();
                        }
                        else
                        {
                            this.Erro = "NOME inválido. ";
                            line += ";NOME inválido";
                        }
                        break;

                    case "NASCIMENTO":
                    case "DT_NASCIMENTO":
                    case "DT_NSC":
                    case "DT NSC":
                        DateTime dateCheck;
                        if (DateTime.TryParseExact(value.ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture,
                            DateTimeStyles.None, out dateCheck))
                        {
                            DataNascimento = dateCheck;
                        }
                        else
                        {
                            this.Erro += "Data de Nascimento inválida. ";
                            line += ";Data de Nascimento inválida";
                        }
                        break;

                    case "SEXO":
                        if (!string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            var sexo = Convert.ToChar(value.ToString());
                            if (IndicadorSexo.Exist(sexo))
                            {
                                Sexo = sexo;
                            }
                            else
                            {
                                this.Erro += "Sexo inválido. ";
                                line += ";Sexo inválido. ";
                            }
                        }
                        else
                        {
                            Sexo = IndicadorSexo.M;
                        }

                        break;

                    case "ENDERECO":
                    case "ENDEREÇO":
                        Logradouro = value?.ToString();
                        break;

                    case "NUMERO":
                    case "NÚMERO":
                        NumeroLogradouro = value?.ToString();
                        break;

                    case "COMPLEMENTO":
                        Complemento = value?.ToString();
                        break;

                    case "BAIRRO":
                        Bairro = value?.ToString();
                        break;

                    case "MUNICIPIO":
                    case "CIDADE":
                        Cidade = value?.ToString();
                        break;

                    case "UF":
                        if (!string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            if (value.ToString().IsState())
                            {
                                Estado = value?.ToString().ToUpper();
                            }
                            else
                            {
                                this.Erro += "Estado inválido. ";
                                line += ";Estado inválido. ";
                            }
                        }
                        break;

                    case "CEP":

                        if (!string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            if (value.ToString().IsPostalCode())
                            {
                                Cep = value?.ToString();
                            }
                            else
                            {
                                this.Erro += "CEP inválido. ";
                                line += ";CEP inválido. ";
                            }
                        }
                        break;

                    case "DDD FIXO":
                    case "DDD RES":
                    case "DDD RES.":
                    case "DDD RESIDENCIAL":
                        if (value is string && !string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            int dddres;
                            if (int.TryParse(value.ToString(), out dddres))
                            {
                                if (dddres != 0)
                                    DddResidencial = dddres;
                            }
                            else
                            {
                                this.Erro += "DddResidencial inválido. ";
                                line += ";DddResidencial inválido. ";
                            }

                        }
                        //else
                        //    DddResidencial = ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture);
                        break;

                    case "TEL RES":
                    case "TEL RES.":
                    case "TEL.RES":
                    case "TEL. RES":
                    case "TEL. RESIDENCIAL":
                    case "TELEFONE RES":
                    case "TELEFONE RES.":
                    case "TELEFONE RESIDENCIAL":
                    case "FIXO":
                        if (value is string && !string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            int telRes;
                            if (int.TryParse(value.ToString(), out telRes))
                            {
                                if (telRes != 0)
                                    TelefoneResidencial = telRes.ToString();
                            }
                            else
                            {
                                this.Erro += "TelefoneResidencial inválido. ";
                                line += ";TelefoneResidencial inválido. ";
                            }
                        }
                        break;

                    case "DDD CEL":
                    case "DDD CELULAR":
                    case "DDD CEL.":
                        if (value is string && !string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            int dddCel;
                            if (int.TryParse(value.ToString(), out dddCel))
                            {
                                if (dddCel != 0)
                                    DddCelular = dddCel;
                            }
                            else
                            {
                                this.Erro += "DddCelular inválido. ";
                                line += ";DddCelular inválido. ";
                            }

                        }
                        //else
                        //    DddCelular = ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture);
                        break;
                    case "TEL CEL":
                    case "TEL CEL.":
                    case "TEL.CEL":
                    case "TEL. CEL":
                    case "TEL. CEL.":
                    case "TEL. CELULAR":
                    case "TELEFONE CEL":
                    case "TELEFONE CEL.":
                    case "TELEFONE CELULAR":
                    case "CEL":
                        if (!string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            int telCel;
                            if (int.TryParse(value.ToString(), out telCel))
                            {
                                if (telCel != 0)
                                    TelefoneCelular = telCel.ToString();
                            }
                            else
                            {
                                this.Erro += "TelefoneCelular inválido. ";
                                line += ";TelefoneCelular inválido. ";
                            }
                        }
                        break;

                    case "DDD COM":
                    case "DDD COMERCIAL":
                    case "DDD COM.":
                        if (value is string && !string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            DddComercial = int.Parse(value.ToString());
                            int dddCom;
                            if (int.TryParse(value.ToString(), out dddCom))
                            {
                                if (dddCom != 0)
                                    DddComercial = dddCom;
                            }
                            else
                            {
                                this.Erro += "DddComercial inválido. ";
                                line += ";DddComercial inválido. ";
                            }
                        }
                        //else
                        //DddComercial = ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture);
                        break;

                    case "TEL COM.":
                    case "TEL.COM":
                    case "TEL. COM":
                    case "TEL. COM.":
                    case "TEL. COMERCIAL":
                    case "TELEFONE COM":
                    case "TELEFONE COM.":
                    case "TELEFONE COMERCIAL":
                        if (!string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            TelefoneComercial = value?.ToString();
                        }
                        break;

                    case "EMAIL":
                    case "E-MAIL":
                        if (!string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            Email = value?.ToString();
                        }
                        break;
                    case "TIP_CARTEIRA":
                    case "CARTEIRA":
                        if (!string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            Carteira carteira;
                            if (System.Enum.TryParse(value.ToString(), true, out carteira))
                            {
                                Carteira = carteira.ToString();
                            }
                            else
                            {
                                line += ";Carteira inválida";
                                this.Erro += "Carteira inválida. ";
                                //Perfil = null;
                            }
                        }
                        else
                        {
                            this.Erro += "Carteira inválida. ";
                            line += ";Carteira inválida. ";
                        }
                        break;
                    default:
                        if (key.StartsWith("PLST") && !string.IsNullOrWhiteSpace(value.ToString()))
                        {
                            UsuarioComplemento.Add(new UsuarioComplementoArquivo(TipoComplemento.Cartao, key, value?.ToString()));
                        }

                        if (key.StartsWith("META") && !string.IsNullOrWhiteSpace(value.ToString()))
                        {
                            UsuarioComplemento.Add(new UsuarioComplementoArquivo(TipoComplemento.Meta, key, value?.ToString()));
                        }

                        break;
                }



                if (string.IsNullOrWhiteSpace(Erro))
                {
                    Valido = true;
                }
            }
            catch (Exception ex)
            {

                Erro += $"{ex.Message} ";
                Valido = false;
            }

            return line;
        }

        public string Erro { get; set; }
        public bool Valido { get; set; }





        public void Read(string[] lines)
        {
            try
            {


                this.Documento = lines[0];

                if (!this.Documento.IsCpf())
                {
                    this.Erro = "CPF inválido. ";
                }

                if (!string.IsNullOrWhiteSpace(lines[1]?.ToString()))
                {
                    Nome = lines[1]?.ToString();
                }
                else
                {
                    this.Erro = "NOME inválido. ";

                }

                DateTime dateCheck;
                if (DateTime.TryParseExact(lines[2], "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateCheck))
                {
                    DataNascimento = dateCheck;
                }
                else
                {
                    this.Erro += "Data de Nascimento inválida. ";
                }

                if (!string.IsNullOrWhiteSpace(lines[3]?.ToString()))
                {
                    var sexo = Convert.ToChar(lines[3].ToString());
                    if (IndicadorSexo.Exist(sexo))
                    {
                        Sexo = sexo;
                    }
                    else
                    {
                        this.Erro += "Sexo inválido. ";
                    }
                }
                else
                {
                    Sexo = IndicadorSexo.M;
                }

                Cep = lines[4];

                //if (lines[4].IsPostalCode())
                //{
                //    Cep = lines[4];
                //}
                //else
                //{
                //    this.Erro += "CEP inválido. ";
                //}


                Logradouro = lines[5];
                NumeroLogradouro = lines[6];
                Bairro = lines[7];
                Cidade = lines[8];
                Estado = lines[9];

                //if (lines[8].IsState())
                //{
                //    Estado = lines[8];
                //}
                //else
                //{

                //    this.Erro += "Estado inválido. ";
                //}

                if (!string.IsNullOrWhiteSpace(lines[10]))
                {
                    int ddRes;
                    if (int.TryParse(lines[10], out ddRes))
                    {
                        DddResidencial = ddRes;
                    }

                }

                TelefoneResidencial = lines[11];

                if (!string.IsNullOrWhiteSpace(lines[12]))
                {
                    int ddCel;
                    if (int.TryParse(lines[12], out ddCel))
                    {
                        DddCelular = ddCel;
                    }
                }

                TelefoneCelular = lines[13];

                if (!string.IsNullOrWhiteSpace(lines[14]))
                {
                    Carteira carteira;
                    if (System.Enum.TryParse(lines[14], true, out carteira))
                    {
                        Carteira = carteira.ToString();
                    }
                    else
                    {
                        this.Erro += "Carteira inválida. ";
                    }
                }
                else
                {
                    this.Erro += "Carteira inválida. ";
                }


                //var totalPlasticos = 18;
                //var indexPlasticos = 15;

                //for (var i = 1; i <= totalPlasticos; i++)
                //{
                //    var plastico = lines[indexPlasticos++];
                //    if (!string.IsNullOrEmpty(plastico))
                //    {
                //        UsuarioComplemento.Add(new UsuarioComplementoArquivo(TipoComplemento.Cartao, $"PLST{i}", plastico));
                //    }
                //}

                //var indexMeta = 33;
                //var totalMeta = 3;

                //for (var i = 1; i <= totalMeta; i++)
                //{
                //    var meta = lines[indexMeta++];
                //    if (!string.IsNullOrEmpty(meta))
                //    {
                //        UsuarioComplemento.Add(new UsuarioComplementoArquivo(TipoComplemento.Meta, $"Meta{i}", meta));
                //    }
                //}

                if (string.IsNullOrWhiteSpace(Erro))
                    Valido = true;

            }
            catch (Exception ex)
            {
                Erro += $"{ex.Message} ";

                if (lines.Length != 37)
                {
                    this.Erro += " Tamanho da linha do arquivo inválida. ";
                }
                Valido = false;
            }

        }
    }
}
