using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;
using System.Threading.Tasks;
namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface ICepTotalRepository : ISQLRepository<CepTotal>
    {
        Task<CepTotal> FindByZipCodeAsync(string zipcode);
    }
}
