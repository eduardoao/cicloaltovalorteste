using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Interfaces.Services;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Model.ValueObject;

namespace CicloVidaAltoValor.Application.Services
{
    public class FileProductService : IFileProductService
    {
        private readonly ICampanhaProdutoRepository _campanhaProdutoRepository;
        private readonly ICampanhaProdutoFaseRepository _campanhaProdutoFaseRepository;
        private readonly ICampanhaFaseRepository _campanhaFaseRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FileProductService> _logger;

        public FileProductService(
            ICampanhaProdutoRepository campanhaProdutoRepository,
            ICampanhaProdutoFaseRepository campanhaProdutoFaseRepository,
            ICampanhaFaseRepository campanhaFaseRepository,
            IProdutoRepository produtoRepository,
            IMapper mapper,
            ILogger<FileProductService> logger)
        {
            _campanhaProdutoRepository = campanhaProdutoRepository;
            _campanhaProdutoFaseRepository = campanhaProdutoFaseRepository;
            _campanhaFaseRepository = campanhaFaseRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task ProcessAsync(IEnumerable<ProdutoArquivo> produtosArquivo, Arquivo file, CampanhaFase campanhaFaseAtual)
        {

            foreach (var item in produtosArquivo.Where(x => x.Valido))
            {

                try
                {
                    var produto = await _produtoRepository.FindAsync(item.ProdutoId);

                    if (produto == null)
                    {
                        _logger.LogError($"Produto não encontrado: {item.ProdutoId}");
                        continue;
                    }

                    if (!string.IsNullOrEmpty(item.Voltagem))
                    {
                        var volt = await _produtoRepository.IsVoltAsync(item.ProdutoId);
                        if (!volt)
                        {
                            _logger.LogError($"Produto elétrico inválido. {item.ProdutoId}");
                            continue;
                        }
                    }

                    var campanhaProdutoFase = _mapper.Map<CampanhaProdutoFase>(item);

                    campanhaProdutoFase.ArquivoId = file.Id;

                    if (campanhaFaseAtual.IsCurrentPeriod(item.Periodo))
                    {
                        campanhaProdutoFase.CampanhaFaseId = campanhaFaseAtual.CampanhaFaseId;
                    }
                    else
                    {
                        var camapnhaFasePeriodo = await _campanhaFaseRepository.GetByPeriodAsync(item.Periodo);

                        if (camapnhaFasePeriodo == null)
                        {
                            _logger.LogError($"Período informado não encontrado: {item.Periodo:yyyy/MM} - item: {JsonConvert.SerializeObject(item)}");
                            continue;
                        }

                        campanhaProdutoFase.CampanhaFaseId = camapnhaFasePeriodo.CampanhaFaseId;
                    }

                    var campanhaProduto = await _campanhaProdutoRepository.FindByCampaignAndProductIdAsync(campanhaFaseAtual.CampanhaId, item.ProdutoId);

                    if (campanhaProduto == null)
                    {
                        campanhaProduto = new CampanhaProduto
                        {
                            CampanhaId = campanhaFaseAtual.CampanhaId,
                            ProdutoId = item.ProdutoId,
                            LojaId = item.LojaId,
                            MecanicaId = item.Pid,
                            PremioPadrao = false
                        };

                        campanhaProduto = await _campanhaProdutoRepository.InsertAsync(campanhaProduto);
                    }
                    else
                    {

                        if (campanhaProduto.MadeChanges(item))
                        {
                            await _campanhaProdutoRepository.UpdateAsync(campanhaProduto);
                        }
                    }

                    campanhaProdutoFase.CampanhaProdutoId = campanhaProduto.CampanhaProdutoId;

                    var checkExist = await _campanhaProdutoFaseRepository.FindByKeysAsync(campanhaProduto.CampanhaProdutoId,
                        campanhaProdutoFase.CampanhaFaseId, campanhaProdutoFase.FaixaMeta, campanhaProdutoFase.Carteira, campanhaProdutoFase.Catalogo);

                    if (checkExist != null)
                    {
                        checkExist.SetValue(campanhaProdutoFase);
                        await _campanhaProdutoFaseRepository.UpdateAsync(checkExist);
                        _logger.LogInformation("Produto atualizado com sucesso.");

                    }
                    else
                    {
                        campanhaProdutoFase = await _campanhaProdutoFaseRepository.InsertAsync(campanhaProdutoFase);
                        _logger.LogInformation("Produto inserido com sucesso.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    _logger.LogError(JsonConvert.SerializeObject(item, Formatting.Indented));
                }

            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task ProcessAsync(IEnumerable<ProdutoArquivo> enumerable, Arquivo file, Campanha campanhaFaseAtual)
        {
            throw new NotImplementedException();
        }
    }
}
