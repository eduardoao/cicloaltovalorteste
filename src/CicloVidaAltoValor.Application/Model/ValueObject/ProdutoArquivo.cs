using System;
using System.Globalization;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Interfaces.Model;

namespace CicloVidaAltoValor.Application.Model.ValueObject
{
    public class ProdutoArquivo : IArquivo
    {
        public string Carteira { get; set; }
        public string FaixaMeta { get; set; }
        public DateTime Periodo { get; set; }
        public string Catalogo { get; set; }
        public string UrlImagem { get; set; }
        public string Voltagem { get; set; }

        public int ProdutoId { get; set; }
        public int Pid { get; set; }
        public int LojaId { get; set; }
        
        public string Erro { get; set; }
        public bool Valido { get; set; }

        public IArquivo Clone()
        {
            return (ProdutoArquivo)MemberwiseClone();
        }

        public void Read(string[] lines)
        {
            throw new NotImplementedException();
        }


        public string Set(string key, object value, string line)
        {
            key = key.ToUpper();

            try
            {
                switch (key)
                {
                    case "CARTEIRA":
                    case "PERFIL":
                        Carteira carteira;
                        if (System.Enum.TryParse(value.ToString(), true, out carteira))
                        {
                            Carteira = carteira.ToString().ToUpper();
                        }
                        else
                        {
                            line += ";Perfil inválido";
                            this.Erro += "Perfil inválido. ";
                            //Perfil = null;
                        }
                        break;

                    case "FAIXA_META":
                    case "FAIXAMETA":

                        FaixaMeta faixaMeta;
                        if (System.Enum.TryParse(value.ToString(), true, out faixaMeta))
                        {
                            FaixaMeta = faixaMeta.ToString();
                        }
                        else
                        {
                            line += ";FAIXA_META inválida";
                            this.Erro += "FAIXA_META inválida. ";
                            //Perfil = null;
                        }
                        //FaixaMeta = value?.ToString();
                        break;

                    case "MÊS":
                    case "MES":
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

                    case "CATÁLOGO":
                    case "CATALOGO":
                        CatalogoSeuDesejo catalogoSeuDesejo;
                        if (System.Enum.TryParse(value.ToString(), true, out catalogoSeuDesejo))
                        {
                            Catalogo = catalogoSeuDesejo.ToString();
                        }
                        else
                        {
                            //Catalogo = null;
                            line += ";Catalogo inválido";
                            this.Erro += "Catalogo inválido. ";
                        }

                        break;

                    case "CODIGO_PRODUTO":
                    case "CODIGOPRODUTO":
                        int produtoId;
                        if (int.TryParse(value?.ToString(), out produtoId))
                            ProdutoId = produtoId;
                        break;

                    case "PRODUTO_ELETRICO":
                    case "PRODUTOELETRICO":


                        if (Enum.ProdutoEletrico.Exist(value?.ToString()))
                        {
                            Voltagem = value?.ToString();
                        }
                        else
                        {
                            Voltagem = null;
                            //line += ";ProdutoEletrico inválido";
                            //this.Erro += "ProdutoEletrico inválido. ";
                        }

                        break;

                    case "PID":
                    case "MECANICA":
                        int mecanicaId;
                        if (int.TryParse(value?.ToString(), out mecanicaId))
                            Pid = mecanicaId;
                        break;

                    case "URL_IMAGEM":
                    case "IMAGEM":

                        if (!string.IsNullOrWhiteSpace(value?.ToString()) && !value.ToString().ToUpper().Contains("SEM IMAGEM"))
                        {
                            UrlImagem = value?.ToString();
                        }

                        break;

                    case "LOJA":
                    case "LOJA_ID":
                    case "LOJAID":
                        int lojaId;
                        if (int.TryParse(value?.ToString(), out lojaId))
                            LojaId = lojaId;
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
    }
}
