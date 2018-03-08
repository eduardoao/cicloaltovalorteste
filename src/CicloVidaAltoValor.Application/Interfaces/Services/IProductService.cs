using System.Collections.Generic;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Contracts.Produto;
using CicloVidaAltoValor.Application.Contracts.Resgate;

namespace CicloVidaAltoValor.Application.Interfaces.Services
{
    public interface IProductService
    {
       

        Task<ResponseContract<ProdutoViewModel>> GetByIdAsnc(GetProductByIdRequest request);
        Task<ResponseContract<ProdutoCampanhaViewModel>> GetProductCampaignByCampaignIdAsync(int id);
        Task<ResponseContract<IEnumerable<ProdutoCampanhaViewModel>>> GetAllProductCampaignByCampaignIdAsync();

        Task<ResponseContract<ResgateViewModel>> MakeRedemptionAsync(ResgateViewModel request);
    }
}
