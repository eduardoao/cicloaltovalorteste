using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Contracts.Campanha;
using CicloVidaAltoValor.Application.Contracts.Usuario;
using CicloVidaAltoValor.Application.Contracts.UsuarioComplemento;
using CicloVidaAltoValor.Application.Contracts.UsuarioStatusFase;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Interfaces.Model;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Interfaces.Services;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Properties;

namespace CicloVidaAltoValor.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUser _user;
        private readonly IUsuarioPremioFaseRepository _usuarioPremioFaseRepository;

        private readonly ICampanhaFaseRepository _campanhaFaseRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioComplementoRepository _usuarioComplementoRepository;
        private readonly IUsuarioPremioRepository _usuarioPremioRepository;
        private readonly IUsuarioStatusRepository _usuarioStatusRepository;
        private readonly ICampanhaUsuarioAtualizacaoCadastroRepository _enderecoAlteracaoRepository;
        private readonly IUsuarioStatusFaseRepository _usuarioStatusFaseRepository;


        public UserService(IUser user,
            IUsuarioPremioFaseRepository usuarioPremioFaseRepository,
            ICampanhaFaseRepository campanhaFaseRepository,
            IUsuarioStatusFaseRepository usuarioStatusFaseRepository,
            ILogger<UserService> logger,
            IUsuarioRepository usuarioRepository,
            IUsuarioComplementoRepository usuarioComplementoRepository,
            IUsuarioPremioRepository usuarioPremioRepository,
            IUsuarioStatusRepository usuarioStatusRepository,
            ICampanhaUsuarioAtualizacaoCadastroRepository enderecoAlteracaoRepository,
            IMapper mapper)
        {
            _user = user;
            _usuarioPremioFaseRepository = usuarioPremioFaseRepository;

            _campanhaFaseRepository = campanhaFaseRepository;
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _usuarioComplementoRepository = usuarioComplementoRepository;
            _usuarioPremioRepository = usuarioPremioRepository;
            _usuarioStatusRepository = usuarioStatusRepository;
            _usuarioStatusFaseRepository = usuarioStatusFaseRepository;
            _enderecoAlteracaoRepository = enderecoAlteracaoRepository;
            
            _mapper = mapper;
        }


        public async Task<ResponseContract<UsuarioViewModel>> GetUserAsync()
        {
            var response = new ResponseContract<UsuarioViewModel>();

            try
            {
                var model = await _usuarioRepository.FindAsync(_user.GetUserId());

                response.SetContent(_mapper.Map<UsuarioViewModel>(model));
                response.SetValid();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return response;
        }

        public async Task<ResponseContract<UsuarioViewModel>> RegisterUpdateAsync(UsuarioViewModel viewModel)
        {
            var response = new ResponseContract<UsuarioViewModel>();

            try
            {
                viewModel.RemoveFormatacao();

                var model = await _usuarioRepository.FindAsync(_user.GetUserId());

                model.SetRegisterUpdate(_mapper.Map<Usuario>(viewModel));

                var valid = await _usuarioRepository.UpdateAsync(model);
                if (!valid)
                {
                    response.AddError(Resources.ErrorOnRegisterUpdate);
                    return response;
                }

                var exist = await _enderecoAlteracaoRepository.ExistAsync(model.CampanhaId, model.UsuarioId);

                if (!exist)
                {
                    var enderecoAlteracao = new CampanhaUsuarioAtualizacaoCadastro
                    {
                        UsuarioId = model.UsuarioId,
                        CampanhaId = model.CampanhaId
                    };

                    await _enderecoAlteracaoRepository.InsertAsync(enderecoAlteracao);

                    _logger.LogWarning($"[UsuarioId: {_user.GetUserDocument()}] [CPF: {_user.GetUserDocument()}] incluido na alteração de endereço pela primeira vez.");
                }

                _logger.LogWarning($"[UsuarioId: {_user.GetUserDocument()}] [CPF: {_user.GetUserDocument()}] realizou alteração no cadastro.");
                response.SetContent(_mapper.Map<UsuarioViewModel>(model));
                response.SetValid();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.AddError(Resources.ErrorOnRegisterUpdate);
            }

            return response;

        }

        public async Task<ResponseContract<UsuarioStatusFaseViewModel>> GetUserStatusFaseAsync()
        {
            var response = new ResponseContract<UsuarioStatusFaseViewModel>(true, new UsuarioStatusFaseViewModel());

            try
            {
                var vm = new UsuarioStatusFaseViewModel();

                var campanhaFase = await _campanhaFaseRepository.GetCurrentAsync(_user.GetCampaignId());

                if (campanhaFase == null)
                {
                    campanhaFase = await _campanhaFaseRepository.GetLastFaseAsync(_user.GetCampaignId());
                }

                //vm.CampanhaFase = _mapper.Map<CampanhaFaseViewModel>(campanhaFase);

                var model = await _usuarioStatusFaseRepository.GetByUserIdAndCampaignAndCampaignFaseActiveAsync(_user.GetUserId(), _user.GetCampaignId(), campanhaFase.CampanhaFaseId);

                if (model != null)
                {
                    vm = _mapper.Map<UsuarioStatusFaseViewModel>(model);
                    vm.PossuiFase = true;
                }
                else
                {
                    var vmComplemento = await _usuarioComplementoRepository.GetByUserIdAndTypeComplementAndNameAsync(_user.GetUserId(), TipoComplemento.Meta, campanhaFase.GetMetaName());

                    if (vmComplemento != null)
                    {
                        var meta = _mapper.Map<UsuarioComplementoViewModel>(vmComplemento);

                        if (meta != null)
                        {
                            vm.Meta = Convert.ToDecimal(meta.Valor);
                        }
                    }
                }

                response.SetContent(vm);
                response.SetValid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return response;

        }

        public async Task<bool> CanRedemption()
        {
            return !await _usuarioPremioRepository
                .HasPrizeAsync(_user.GetUserId()) && await _usuarioStatusRepository.CanPrizeAsync(_user.GetUserId());
        }

        public async Task<bool> CanRedemption(int usuarioId)
        {
            return !await _usuarioPremioRepository
                .HasPrizeAsync(usuarioId) && await _usuarioStatusRepository.CanPrizeAsync(usuarioId);
        }


        public async Task<bool> HasUpdateAddressRegister()
        {
            return await _usuarioRepository.HasUpdateAddressRegister(_user.GetUserId());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
