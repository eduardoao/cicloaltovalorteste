using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class CepTotalMap : DommelEntityMap<CepTotal>
    {
        /// <summary>
        /// 
        /// </summary>
        public CepTotalMap()
        {
            ToTable("CEP_TOTAL");
            Map(p => p.Cep).ToColumn("CEP").IsKey();
            Map(p => p.Endereco).ToColumn("ENDERECO");
            Map(p => p.Bairro).ToColumn("BAIRRO");
            Map(p => p.Cidade).ToColumn("CIDADE");
            Map(p => p.Estado).ToColumn("ESTADO");
            Map(p => p.Complemento).ToColumn("COMPLEMENTO");
            Map(p => p.EnderecoAbreviado).ToColumn("ENDERECO_ABREVIADO");
            Map(p => p.EnderecoTipo).ToColumn("ENDERECO_TIPO");
        }
    }
}