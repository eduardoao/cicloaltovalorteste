using System.Linq;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Contracts.CepTotal;
using CicloVidaAltoValor.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CicloVidaAltoValor.UI.Controllers
{
    [Route("cep")]
    public class CepController : BaseController
    {
        private readonly ICepTotalService _cepTotalService;

        public CepController(ICepTotalService cepTotalService)
        {
            _cepTotalService = cepTotalService;
        }

        [Route("{Cep}")]
        public async Task<IActionResult> Index(CepTotalRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return Json(new { valid = false, errors });
            }

            var response = await _cepTotalService.FindByZipCodeAsync(request);

            return Json(response);
        }
    }
}