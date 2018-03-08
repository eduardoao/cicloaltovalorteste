using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CicloVidaAltoValor.UI.Controllers
{
    [Route("como-funciona")]
    public class ComoFuncionaController : BaseController
    {
        private readonly IProductService _productService;
        
        public ComoFuncionaController(IProductService productService)
        {
            _productService = productService;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var response = await _productService.GetAllCardsAndCurrentProducts();
        //    return View(response.Content);
        //}
    }
}