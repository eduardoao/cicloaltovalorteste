using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Contracts.Usuario;
using CicloVidaAltoValor.Application.Contracts.UsuarioStatusFase;

namespace CicloVidaAltoValor.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResponseContract<UsuarioViewModel>> GetUserAsync();
        Task<ResponseContract<UsuarioViewModel>> RegisterUpdateAsync(UsuarioViewModel viewModel);
        Task<ResponseContract<UsuarioStatusFaseViewModel>> GetUserStatusFaseAsync();
        Task<bool> CanRedemption();

        Task<bool> CanRedemption(int usuarioId);


        Task<bool> HasUpdateAddressRegister();

    }
}
