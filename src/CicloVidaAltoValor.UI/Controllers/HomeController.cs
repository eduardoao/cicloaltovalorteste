using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Contracts;
using CicloVidaAltoValor.Application.Contracts.Error;
using CicloVidaAltoValor.Application.Contracts.Home;
using CicloVidaAltoValor.Application.Contracts.Produto;
using CicloVidaAltoValor.Application.Contracts.Resgate;
using CicloVidaAltoValor.Application.Contracts.Usuario;
using CicloVidaAltoValor.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CicloVidaAltoValor.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService _usuarioService;
        private readonly IProductService _productService;
        private readonly ICampaignService _campaignService;

        public HomeController(IUserService userService, IProductService productService, ICampaignService campaignService)
        {
            _usuarioService = userService;
            _productService = productService;
            _campaignService = campaignService;
        }

        private async Task<HomeViewModel> GetModelAsync()
        {
            return new HomeViewModel
            {
                Usuario = (await _usuarioService.GetUserAsync()).Content,
                PodeResgatar = await _usuarioService.CanRedemption(),
                Produtos = (await _productService.GetAllProductCampaignByCampaignIdAsync()).Content,
                CompletouCadastro = await _usuarioService.HasUpdateAddressRegister(),
                Campanha = (await _campaignService.GetByIdAsync()).Content

            };
        }

        [Route("home")]
        public async Task<IActionResult> Index()
        {
            return View(await GetModelAsync());
        }

        [Route("home/cadastro")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(UsuarioViewModel viewModel)
        {
            var vm = await GetModelAsync();


            if (!ModelState.IsValid)
            {
                return View("Index", vm);
            }

            var response = await _usuarioService.RegisterUpdateAsync(viewModel);

            if (!response.Valid)
            {
                response.Errors.ForEach(e => ModelState.AddModelError("", e));
                return View("Index", vm);
            }

            ViewBag.Mensagem = response.Valid;
            vm.Usuario = response.Content;


            return View("Index", vm);
        }

        [HttpPost("home/questao-confirma")]
        public ActionResult QuestaoConfirma([FromBody]ResgateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return PartialView("_QuestaoConfirma", viewModel);
        }

        [Route("home/resgate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Resgate(ResgateViewModel viewModel)
        {
            HomeViewModel response;

            try
            {
                if (ModelState.IsValid)
                {
                    var responseWish = await _productService.MakeRedemptionAsync(viewModel);

                    if (responseWish.Valid)
                    {
                        @ViewBag.Sucesso = responseWish.Valid;
                    }
                    else
                    {
                        responseWish.Errors.ForEach(x => ModelState.AddModelError("", x));
                    }
                }
            }
            finally
            {
                response = await GetModelAsync();
            }

            return View("Index", response);

        }


        [HttpGet("Error/404")]
        [AllowAnonymous]
        public IActionResult Error404()
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            if (HttpContext.Request.Path.HasValue)
            {
                @ViewBag.RequestedUrl = GetRequestUrlPath();
            }
            else
            {
                @ViewBag.RequestedUrl = "";
            }
            @ViewBag.ReferrerUrl = "";
            return View();
        }

        //[Route("error/500")]
        [Route("Error/{code:int?}")]
        [AllowAnonymous]
        public IActionResult Error(int? code)
        {

            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Uri GetRequestUrlPath()
        {
            return new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{((Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame)HttpContext.Request.HttpContext.Features).RawTarget}");
        }

        /*
        // GET: /<controller>/
        public async Task<IActionResult> CompletarCadastro()
        {
            var response = await _usuarioService.GetUserAsync();
            return View(response.Content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompletarCadastro(UsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var response = await _usuarioService.RegisterUpdateAsync(viewModel);

            if (!ModelState.IsValid)
            {
                response.Errors.ForEach(e => ModelState.AddModelError("", e));
                return View(viewModel);
            }

            ViewBag.Mensagem = response.Valid;

            return View(response.Content);
        }
        */

    }
}
