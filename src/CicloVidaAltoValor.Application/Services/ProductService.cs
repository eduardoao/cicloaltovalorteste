using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Contracts.Produto;
using CicloVidaAltoValor.Application.Contracts.Resgate;
using CicloVidaAltoValor.Application.Interfaces.Model;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Interfaces.Services;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Properties;
using CicloVidaAltoValor.Application.Settings;

namespace CicloVidaAltoValor.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICampanhaProdutoRepository _campanhaProdutoRepository;
        private readonly IProdutoCampanhaRepository _produtoCampanhaRepository;
        private readonly IAuthService _authService;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioPremioRepository _usuarioPremioRepository;
        private readonly CampaignSettings _campaignSettings;

        public ProductService(
            IAuthService authService,
            IOptions<CampaignSettings> options,
            IUser user,
            IUsuarioRepository usuarioRepository,
            IUsuarioPremioRepository usuarioPremioRepository,
            IProdutoRepository produtoRepository,
            ICampanhaProdutoRepository campanhaProdutoRepository,
            IProdutoCampanhaRepository produtoCampanhaRepository,
            IMapper mapper,
            ILogger<ProductService> logger)
        {
            _produtoRepository = produtoRepository;
            _campanhaProdutoRepository = campanhaProdutoRepository;
            _produtoCampanhaRepository = produtoCampanhaRepository;
            _authService = authService;
            _logger = logger;
            _mapper = mapper;
            _user = user;
            _usuarioRepository = usuarioRepository;
            _usuarioPremioRepository = usuarioPremioRepository;
            _campaignSettings = options.Value;
        }






        public async Task<ResponseContract<ProdutoViewModel>> GetByIdAsnc(GetProductByIdRequest request)
        {
            var response = new ResponseContract<ProdutoViewModel>();

            var result = await _produtoRepository.FindAsync(request.Id);

            if (result == null)
            {
                return response;
            }

            var vm = _mapper.Map<ProdutoViewModel>(result);

            response.SetContent(vm);
            response.SetValid();

            return response;
        }



        public async Task<ResponseContract<ProdutoCampanhaViewModel>> GetProductCampaignByCampaignIdAsync(int id)
        {

            var response = new ResponseContract<ProdutoCampanhaViewModel>();
            var model = await _produtoCampanhaRepository.GetByCampaignIdAsync(id);

            if (model == null)
            {
                return response;
            }

            response.SetContent(_mapper.Map<ProdutoCampanhaViewModel>(model));
            response.SetValid();

            return response;
        }

        public async Task<ResponseContract<IEnumerable<ProdutoCampanhaViewModel>>> GetAllProductCampaignByCampaignIdAsync()
        {


            var response = new ResponseContract<IEnumerable<ProdutoCampanhaViewModel>>();
            var model = await _produtoCampanhaRepository.GetAllByCampaignIdAsync(_user.GetCampaignId(), _user.GetWallet());


            response.SetContent(_mapper.Map<IEnumerable<ProdutoCampanhaViewModel>>(model));
            response.SetValid();

            return response;
        }

        public async Task<ResponseContract<ResgateViewModel>> MakeRedemptionAsync(ResgateViewModel request)
        {
            var response = new ResponseContract<ResgateViewModel>();

            response.SetContent(request);

            try
            {
                var product = await _produtoCampanhaRepository.FindByAsync(request.ProdutoCampanhaId,request.CampanhaProdutoId, request.ProdutoId, _user.GetCampaignId(), _user.GetWallet());

                if (product == null || product.CampanhaProduto == null)
                {
                    response.AddError(Resources.ProductInvalid);
                    return response;
                }

                if (product.CampanhaProduto.Campanha == null || product.CampanhaProduto.Campanha.DesabilitaResgate.GetValueOrDefault())
                {
                    response.AddError(Resources.CampaignInvalid);
                    return response;

                }
                if (await _usuarioPremioRepository.HasPrizeAsync(_user.GetUserId()))
                {
                    response.AddError(Resources.UserAlreadyRedemption);
                    return response;
                }

                _usuarioPremioRepository.BeginTransaction();

                var usuarioPremio = new UsuarioPremio(_user.GetUserId(), request.CampanhaProdutoId,
                    Enum.UsuarioPremioStatus.AGUARDANDO_PROCESSAMENTO.GetHashCode(), DateTime.Now);
                usuarioPremio = await _usuarioPremioRepository.InsertAsync(usuarioPremio);

                _usuarioPremioRepository.Commit();
                response.SetValid();

                _logger.LogWarning($"[UsuarioId: {_user.GetUserId()}] [CPF: {_user.GetUserDocument()}] realizou o resgate - [CampanhaProduto: {request.CampanhaProdutoId}] [Produto: {request.ProdutoId}] .");

                await _authService.RefreshClaims();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                _usuarioPremioRepository.Rollback();
                response.AddError(Resources.ErrorOnMakeWish);
            }

            return response;
        }




        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
