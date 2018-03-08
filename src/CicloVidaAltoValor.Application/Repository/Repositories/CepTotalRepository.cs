using Dapper;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;
using System.Threading.Tasks;
namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class CepTotalRepository : SQLRepository<CepTotal>, ICepTotalRepository
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="context">DotzCepContext context</param>
        public CepTotalRepository(DotzCepContext context)
            : base(context)
        {
        }
        /// <summary>
        /// Obtém endereço por ZipCode.
        /// </summary>
        /// <param name="zipcode">string zipcode</param>
        /// <returns></returns>
        public Task<CepTotal> FindByZipCodeAsync(string zipcode)
        {
            var p = new { nome = new DbString { Value = zipcode, Length = 20, IsAnsi = true } };


            return Connection.SqlConnection.QueryFirstOrDefaultAsync<CepTotal>("SELECT * FROM [CEP_TOTAL] (NOLOCK) WHERE [CEP] = @zipcode;", p);
        }
    }
}