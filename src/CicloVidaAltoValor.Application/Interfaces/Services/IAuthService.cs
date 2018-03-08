using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Contracts.Login;
using CicloVidaAltoValor.Application.Contracts;


namespace CicloVidaAltoValor.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ResponseContract<LoginViewModel>> LoginAsync(LoginViewModel viewModel);

        Task LogoutAsync();

        Task RefreshClaims();

    }
}
