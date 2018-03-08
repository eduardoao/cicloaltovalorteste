using System;
using System.Threading.Tasks;
using AutoMapper;
using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Contracts.Campanha;
using CicloVidaAltoValor.Application.Interfaces.Model;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CicloVidaAltoValor.Application.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampanhaRepository _campanhaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CampaignService> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser _user;

        public CampaignService(
            ICampanhaRepository campanhaRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUser user,
            ILogger<CampaignService> logger)
        {
            _campanhaRepository = campanhaRepository;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _user = user;
        }

      

        public async Task<ResponseContract<CampanhaViewModel>> GetByIdAsync()
        {
            var response = new ResponseContract<CampanhaViewModel>();
            var model = await _campanhaRepository.GetByIdAsync(_user.GetCampaignId());
            response.SetContent(_mapper.Map<CampanhaViewModel>(model));
            response.SetValid();
            return response;
        }
       

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
