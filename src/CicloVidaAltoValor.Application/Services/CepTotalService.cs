using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Contracts.CepTotal;
using CicloVidaAltoValor.Application.Extensions;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Interfaces.Services;
namespace CicloVidaAltoValor.Application.Services
{
    public class CepTotalService : ICepTotalService
    {
        public CepTotalService(ICepTotalRepository cepTotalRepository, IMapper mapper, ILogger<CepTotalService> logger)
        {
            _cepTotalRepository = cepTotalRepository;
            _mapper = mapper;
            _logger = logger;
        }
        private readonly ICepTotalRepository _cepTotalRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CepTotalService> _logger;
        public async Task<ResponseContract<CepTotalViewModel>> FindByZipCodeAsync(CepTotalRequest request)
        {
            var response = new ResponseContract<CepTotalViewModel>();
            try
            {
                var vm = _mapper.Map<CepTotalViewModel>(await _cepTotalRepository.FindByZipCodeAsync(request.Cep.JustNumbers()));
                if (vm == null)
                {
                    return response;
                }
                response.SetContent(vm);
                response.SetValid();
                return response;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return response;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}