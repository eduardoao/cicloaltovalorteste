using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Contracts.Login;
using CicloVidaAltoValor.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CicloVidaAltoValor.UI.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("login")]
        [Route("/")]
        [AllowAnonymous]
        public IActionResult Index(string returnUrl)
        {

            ViewData["returnUrl"] = returnUrl;
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                if (!TempData.ContainsKey("Errors"))
                {
                    return View();
                }

                var errors = (string[])TempData.Peek("Errors");
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View();
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel viewModel, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var response = await _authService.LoginAsync(viewModel);

            if (!response.Valid)
            {
                response.Errors.ForEach(x => ModelState.AddModelError("", x));
                return View();
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");

        }


        [Route("logout")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            await _authService.LogoutAsync();
            return RedirectToAction("Index");
        }
    }
}
