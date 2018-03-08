using System;
using CicloVidaAltoValor.Application.Interfaces.Services;
using CicloVidaAltoValor.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CicloVidaAltoValor.Application.Contracts.Login;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Extensions;
using CicloVidaAltoValor.Application.Interfaces.Model;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Properties;

namespace CicloVidaAltoValor.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IUser _user;
        private readonly IUsuarioAcessoRepository _usuarioAcessoRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly CampaignSettings _campaignSettings;
        private readonly ICampanhaRepository _campanhaRepository;


        public AuthService(
                IUserService userService,
                IUser user,
                IUsuarioAcessoRepository usuarioAcessoRepository,
                ICampanhaRepository campanhaRepository,
                IOptions<CampaignSettings> options,
                IHttpContextAccessor contextAccessor,
                IUsuarioRepository usuarioRepository,

                ILogger<AuthenticationService> logger)
        {
            _userService = userService;
            _user = user;
            _usuarioAcessoRepository = usuarioAcessoRepository;
            _contextAccessor = contextAccessor;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _campaignSettings = options.Value;
            _campanhaRepository = campanhaRepository;


        }

        public async Task<ResponseContract<LoginViewModel>> LoginAsync(LoginViewModel viewModel)
        {
            var response = new ResponseContract<LoginViewModel>();

            try
            {

                var campanha = await _campanhaRepository.GetCurrentAsync(_campaignSettings.Name);
                if (campanha == null)
                {
                    response.AddError(Resources.CampaignInvalid);
                    return response;
                }

                var usuario = await _usuarioRepository.GetByBirthDateAndDocumentAndCampaign(campanha.CampanhaId, viewModel.Cpf.RemoveFormatacao(), viewModel.DataNascimento);

                if (usuario == null)
                {
                    response.AddError(Resources.LoginInvalid);
                    return response;
                }

                await GenerateClaimsAsync(usuario, campanha);

                _logger.LogWarning($"[UsuarioId: {usuario.UsuarioId}] [CPF: {usuario.Documento}] realizou login com sucesso.");

                await SetAccessControl(usuario);

                response.SetValid();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.AddError(Resources.ErrorOnLogin);

            }

            return response;

        }




        public async Task LogoutAsync()
        {
            try
            {
                await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _logger.LogWarning($"[UsuarioId: {_user.GetUserId()}] [CPF: {_user.GetUserDocument()}] realizou logout.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        public async Task RefreshClaims()
        {

            var campanhaId = _user.GetCampaignId();
            var usuarioId = _user.GetUserId();
            var documento = _user.GetClaimTypeValue(ClaimTypes.UserData);
            var nome = _user.GetClaimTypeValue(ClaimTypes.GivenName);
            var carteira = _user.GetClaimTypeValue(ClaimTypes.GroupSid);
            var dataNascimento = _user.GetClaimTypeValue(ClaimTypes.DateOfBirth);

            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()),
                new Claim(ClaimTypes.UserData, documento),
                new Claim(ClaimTypes.GivenName, nome),
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.System, campanhaId.ToString()),
                new Claim(ClaimTypes.GroupSid, carteira),
                new Claim(ClaimTypes.DateOfBirth, dataNascimento),
                new Claim(ClaimTypes.IsPersistent, "false"),
                new Claim(ClaimTypes.PrimarySid, (await _userService.CanRedemption(usuarioId)).ToString() ),
                new Claim(ClaimTypes.Authentication, CookieAuthenticationDefaults.AuthenticationScheme)
            };
            var identity = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity);
        }

        private async Task GenerateClaimsAsync(Usuario usuario, Campanha campanha)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.UserData, usuario.Documento),
                new Claim(ClaimTypes.GivenName, usuario.Nome),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.System, campanha.CampanhaId.ToString()),
                new Claim(ClaimTypes.GroupSid, usuario.Carteira),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.GetValueOrDefault().ToString("d")),
                new Claim(ClaimTypes.IsPersistent, "false"),
                new Claim(ClaimTypes.PrimarySid, (await _userService.CanRedemption(usuario.UsuarioId)).ToString() ),
                new Claim(ClaimTypes.Authentication, CookieAuthenticationDefaults.AuthenticationScheme)
            };


            var identity = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity);
        }

        private async Task SetAccessControl(Usuario usuario)
        {

            var usuarioAcesso = new UsuarioAcesso
            {
                UsuarioId = usuario.UsuarioId,
                DataCriacao = DateTime.Now
            };

            await _usuarioAcessoRepository.InsertAsync(usuarioAcesso);
            _logger.LogWarning($"[UsuarioId: {usuario.UsuarioId}] [CPF: {usuario.Documento}] incluido no controle de acesso.");
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
