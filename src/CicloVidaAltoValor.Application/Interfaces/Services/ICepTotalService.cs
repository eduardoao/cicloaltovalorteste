using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Contracts.CepTotal;
using System.Threading.Tasks;
namespace CicloVidaAltoValor.Application.Interfaces.Services
{
    public interface ICepTotalService
    {
        Task<ResponseContract<CepTotalViewModel>> FindByZipCodeAsync(CepTotalRequest request);
    }
}
