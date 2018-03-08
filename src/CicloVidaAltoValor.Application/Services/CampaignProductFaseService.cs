using AutoMapper;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Interfaces.Services;
using System;

namespace CicloVidaAltoValor.Application.Services
{
    public class CampaignProductFaseService : ICampaignProductFaseService
    {
        private readonly IMapper _mapper;
        private readonly ICampanhaProdutoFaseRepository _produtoFaseRepository;
        private readonly ICampanhaFaseRepository _campanhaFaseRepository;

        public CampaignProductFaseService(IMapper mapper, ICampanhaProdutoFaseRepository produtoFaseRepository, ICampanhaFaseRepository campanhaFaseRepository)
        {
            _mapper = mapper;
            _produtoFaseRepository = produtoFaseRepository;
            _campanhaFaseRepository = campanhaFaseRepository;
        }

      

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
