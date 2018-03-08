using System;
using System.Data;
using System.Globalization;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Extensions;
using CicloVidaAltoValor.Application.Interfaces.Model;

namespace CicloVidaAltoValor.Application.Model.ValueObject
{
    public class UsuarioStatusArquivo : IArquivo
    {
        public int UsuarioId { get; set; }
        public string Cpf { get; set; }
        public DateTime Periodo { get; set; }
        public decimal Meta { get; set; }
        public string FaixaMeta { get; set; }
        public decimal Gasto { get; set; }
        public string GastoPercentual { get; set; }
        public bool Desafio1 { get; set; }
        public bool Desafio2 { get; set; }
        public bool Desafio3 { get; set; }
        public bool Desafio4 { get; set; }
        public bool Desafio5 { get; set; }
        public bool Desafio6 { get; set; }
        public bool Desafio7 { get; set; }
        public string Catalogo { get; set; }
        public string Erro { get; set; }
        public bool Valido { get; set; }

        public IArquivo Clone()
        {
            return (UsuarioStatusArquivo)MemberwiseClone();
        }


        public void Read(string[] lines)
        {
            try
            {
               
                this.Cpf = lines[0];

                if (!this.Cpf.IsCpf())
                {
                    this.Erro = "CPF inválido. ";
                }

                DateTime dateCheck;
                if (DateTime.TryParseExact(lines[1], "yyyy/MM", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateCheck))
                {
                    Periodo = dateCheck;
                }
                else
                {
                    this.Erro += "Período inválido. ";
                }

                decimal meta;
                if (decimal.TryParse(lines[2], out meta))
                {
                    Meta = meta;
                }
                else
                {
                    this.Erro += "Meta inválida. ";
                }


                FaixaMeta = lines[3];


                decimal gasto;
                if (decimal.TryParse(lines[4], out gasto))
                {
                    Gasto = gasto;
                }
                else
                {
                    this.Erro += "Gasto inválido. ";
                }

                GastoPercentual = lines[5];
                //int gastoPercentual;
                //if (int.TryParse(string.Join("", lines[5].Where(char.IsNumber)), out gastoPercentual))
                //{
                //    GastoPercentual = gastoPercentual;
                //}
                //else
                //{
                //    this.Erro += "GastoPercentual inválido. ";
                //}

                var desafio1 = CheckChallenge(lines[6]);
                if (desafio1.HasValue)
                {
                    Desafio1 = desafio1.GetValueOrDefault();
                }
                else
                {
                    this.Erro += "Desafio1 inválido. ";
                }


                var desafio2 = CheckChallenge(lines[7]);
                if (desafio2.HasValue)
                {
                    Desafio2 = desafio2.GetValueOrDefault();
                }
                else
                {
                    this.Erro += "Desafio2 inválido. ";
                }

                var desafio3 = CheckChallenge(lines[8]);
                if (desafio3.HasValue)
                {
                    Desafio3 = desafio3.GetValueOrDefault();
                }
                else
                {
                    this.Erro += "Desafio3 inválido. ";
                }
                var desafio4 = CheckChallenge(lines[9]);
                if (desafio4.HasValue)
                {
                    Desafio4 = desafio4.GetValueOrDefault();
                }
                else
                {
                    this.Erro += "Desafio4 inválido. ";
                }
                var desafio5 = CheckChallenge(lines[10]);
                if (desafio5.HasValue)
                {
                    Desafio5 = desafio5.GetValueOrDefault();
                }
                else
                {
                    this.Erro += "Desafio5 inválido. ";
                }


                var desafio6 = CheckChallenge(lines[11]);
                if (desafio6.HasValue)
                {
                    Desafio6 = desafio6.GetValueOrDefault();
                }
                else
                {
                    this.Erro += "Desafio6 inválido. ";
                }
                if (string.IsNullOrEmpty(lines[12]?.ToString()) || lines[12].ToString().Contains("Null"))
                {
                    Catalogo = null;
                }
                else
                {
                    CatalogoSeuDesejo catalogoSeuDesejo;
                    if (System.Enum.TryParse(lines[12].ToString(), true, out catalogoSeuDesejo))
                    {
                        Catalogo = catalogoSeuDesejo.ToString();
                    }
                    else
                    {
                        Catalogo = null;
                    }
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

        }

        public string Set(string key, object value, string line)
        {
            key = key.ToUpper();

            try
            {
                switch (key)
                {

                    case "CPF":
                        this.Cpf = value?.ToString();

                        if (!this.Cpf.IsCpf())
                        {
                            this.Erro = "CPF inválido. ";
                            line += ";CPF inválido";
                        }

                        break;

                    case "PERIODO":
                        DateTime dateCheck;
                        if (DateTime.TryParseExact(value.ToString(), "yyyy/MM", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateCheck))
                        {
                            Periodo = dateCheck;
                        }
                        else
                        {
                            this.Erro += "Período inválido. ";
                            line += ";Período inválido";
                        }
                        break;

                    case "META":

                        decimal meta;
                        if (decimal.TryParse(value.ToString(), out meta))
                        {
                            if (this.Meta < 0m)
                            {
                                line += ";Meta inválida";
                                this.Erro += "Meta inválida. ";
                            }
                            else
                            {
                                Meta = meta;
                            }
                        }
                        else
                        {
                            line += ";Meta inválida";
                            this.Erro += "Meta inválida. ";
                        }
                        break;
                    case "FAIXA_META":

                        //FaixaMeta = value.ToString();
                        if (this.Meta >= 0m && Meta <= 3500m)
                        //if (this.Meta >= 1.500m && Meta <= 3.500m)
                        {
                            //Faixa_1
                            FaixaMeta = Enum.FaixaMeta.Faixa_1.ToString();
                        }

                        else if (this.Meta >= 3501m && Meta <= 6000m)
                        {
                            //Faixa_2
                            FaixaMeta = Enum.FaixaMeta.Faixa_2.ToString();

                        }
                        else if (this.Meta >= 6001m && Meta <= 10000m)
                        {
                            //Faixa_3
                            FaixaMeta = Enum.FaixaMeta.Faixa_3.ToString();

                        }
                        else if (this.Meta >= 10001m && Meta <= 15000m)
                        {
                            //Faixa_4
                            FaixaMeta = Enum.FaixaMeta.Faixa_4.ToString();

                        }

                        else if (this.Meta >= 15001m)
                        {
                            //Faixa_5
                            FaixaMeta = Enum.FaixaMeta.Faixa_5.ToString();

                        }
                        else
                        {
                            line += ";Faixa_Meta inválida";
                            this.Erro += "Faixa_Meta inválida. ";
                        }


                        break;
                    case "GASTO":
                        decimal gasto;
                        if (decimal.TryParse(value.ToString(), out gasto))
                        {
                            Gasto = gasto;
                        }
                        else
                        {
                            line += ";Gasto inválido";
                            this.Erro += "Gasto inválido. ";
                        }
                        break;
                    case "GASTO_PERC":
                        GastoPercentual = value.ToString();
                        //int gastoPercentual;
                        //if (int.TryParse(string.Join("", value.ToString().Where(char.IsNumber)), out gastoPercentual))
                        //{
                        //    GastoPercentual = gastoPercentual;
                        //}
                        //else
                        //{
                        //    line += ";GastoPercentual inválido";
                        //    this.Erro += "GastoPercentual inválido. ";
                        //}
                        break;
                    case "DESAFIO_1":
                        var desafio1 = CheckChallenge(value.ToString());
                        if (desafio1.HasValue)
                        {
                            Desafio1 = desafio1.GetValueOrDefault();
                        }
                        else
                        {
                            line += ";Desafio1 inválido";
                            this.Erro += "Desafio1 inválido. ";
                        }
                        break;
                    case "DESAFIO_2":
                        var desafio2 = CheckChallenge(value.ToString());
                        if (desafio2.HasValue)
                        {
                            Desafio2 = desafio2.GetValueOrDefault();
                        }
                        else
                        {
                            line += ";Desafio2 inválido";
                            this.Erro += "Desafio2 inválido. ";
                        }
                        break;
                    case "DESAFIO_3":
                        var desafio3 = CheckChallenge(value.ToString());
                        if (desafio3.HasValue)
                        {
                            Desafio3 = desafio3.GetValueOrDefault();
                        }
                        else
                        {
                            line += ";Desafio3 inválido";
                            this.Erro += "Desafio3 inválido. ";
                        }




                        break;
                    case "DESAFIO_4":
                        var desafio4 = CheckChallenge(value.ToString());
                        if (desafio4.HasValue)
                        {
                            Desafio4 = desafio4.GetValueOrDefault();
                        }
                        else
                        {
                            line += ";Desafio4 inválido";
                            this.Erro += "Desafio4 inválido. ";
                        }
                        break;
                    case "DESAFIO_5":
                        var desafio5 = CheckChallenge(value.ToString());
                        if (desafio5.HasValue)
                        {
                            Desafio5 = desafio5.GetValueOrDefault();
                        }
                        else
                        {
                            line += ";Desafio5 inválido";
                            this.Erro += "Desafio5 inválido. ";
                        }
                        break;
                    case "DESAFIO_6":
                        var desafio6 = CheckChallenge(value.ToString());
                        if (desafio6.HasValue)
                        {
                            Desafio6 = desafio6.GetValueOrDefault();
                        }
                        else
                        {
                            line += ";Desafio6 inválido";
                            this.Erro += "Desafio6 inválido. ";
                        }
                        break;
                    case "DESAFIO_7":
                        var desafio7 = CheckChallenge(value.ToString());
                        if (desafio7.HasValue)
                        {
                            Desafio7 = desafio7.GetValueOrDefault();
                        }
                        else
                        {
                            line += ";Desafio7 inválido";
                            this.Erro += "Desafio7 inválido. ";
                        }
                        break;
                    case "LAMPADA":
                    case "CATALOGO":
                        if (string.IsNullOrEmpty(value?.ToString()) || value.ToString().Contains("Null"))
                        {
                            Catalogo = null;
                        }
                        else
                        {
                            CatalogoSeuDesejo catalogoSeuDesejo;
                            if (System.Enum.TryParse(value.ToString(), true, out catalogoSeuDesejo))
                            {
                                Catalogo = catalogoSeuDesejo.ToString();
                            }
                            else
                            {
                                Catalogo = null;
                                //line += ";Catálogo inválido";
                                //this.Erro += "Catálogo inválido. ";
                            }
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

        private bool? CheckChallenge(string value)
        {
            switch (value)
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "N":
                    return false;
                case "S":
                    return true;
            }

            return null;
        }

        public DataRow ToDataRow(int fileId, DataRow dr)
        {
            
            dr["PERIODO"] = Periodo;

            dr["CPR_ARQUIVO_ID"] = fileId;
            dr["CPR_USUARIO_CPF"] = Cpf;

            dr["META"] = Meta;
            dr["FAIXA_META"] = FaixaMeta;
            dr["GASTO"] = Gasto;
            dr["GASTO_PERCENTUAL"] = GastoPercentual;
            dr["DESAFIO_1"] = Desafio1;
            dr["DESAFIO_2"] = Desafio2;
            dr["DESAFIO_3"] = Desafio3;
            dr["DESAFIO_4"] = Desafio4;
            dr["DESAFIO_5"] = Desafio5;
            dr["DESAFIO_6"] = Desafio6;
            dr["DESAFIO_7"] = false;
            dr["CATALOGO"] = Catalogo;

            return dr;
        }
    }
}
