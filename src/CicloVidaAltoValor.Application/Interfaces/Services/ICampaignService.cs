using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Contracts.Campanha;

namespace CicloVidaAltoValor.Application.Interfaces.Services
{
    public interface ICampaignService
    {
        Task<ResponseContract<CampanhaViewModel>> GetByIdAsync();
    }
}
