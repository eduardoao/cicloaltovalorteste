using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CicloVidaAltoValor.UI.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected string GetProfile()
        {
            var claim = User.FindFirst(x => x.Type == ClaimTypes.GroupSid);
            return claim == null ? "varejo" : claim.Value.ToLower();
        }

        protected bool CanMakeWish()
        {
            var value = User.FindFirstValue(ClaimTypes.PrimarySid);
            return !string.IsNullOrEmpty(value) && Convert.ToBoolean(value);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ViewBag.Profile = GetProfile();           
            return base.OnActionExecutionAsync(context, next);
        }

    }
}