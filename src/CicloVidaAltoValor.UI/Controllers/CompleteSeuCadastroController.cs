using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Contracts.Home;
using Microsoft.AspNetCore.Mvc;
using CicloVidaAltoValor.Application.Interfaces.Services;
using CicloVidaAltoValor.Application.Contracts.Usuario;

namespace CicloVidaAltoValor.UI.Controllers
{
    //[Route("complete-seu-cadastro")]
    public class CompleteSeuCadastroController : BaseController
    {
        private readonly IUserService _usuarioService;

        public CompleteSeuCadastroController(IUserService userService)
        {
            _usuarioService = userService;
        }

        //// GET: /<controller>/
        //public async Task<IActionResult> Index()
        //{
        //    var response = await _usuarioService.GetUserAsync();
        //    return View(response.Content);
        //}

      

    }
}