using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Interfaces.Model;

namespace CicloVidaAltoValor.Application.Model.ValueObject
{
    public class UsuarioSeuDesejo : IUser
    {
        private readonly IHttpContextAccessor _accessor;


        public UsuarioSeuDesejo(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }


        public string GetUserDocument()
        {
            return _accessor.HttpContext.User.FindFirst(ClaimTypes.UserData).Value;
        }

        public int GetUserId()
        {
            return Convert.ToInt32(_accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }


        public Carteira GetWalletEnum()
        {
            return (Carteira)System.Enum.Parse(typeof(Carteira), _accessor.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value, true);
        }

        public string GetWallet()
        {
            return _accessor.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;
        }

        public string GetClaim(Claim claim)
        {
            return _accessor.HttpContext.User.FindFirst(x => x == claim).Value;
        }

        public string GetClaimTypeValue(string type)
        {
            return _accessor.HttpContext.User.FindFirst(type).Value;
        }

        public Claim GetClaimValue(string type)
        {
            return _accessor.HttpContext.User.FindFirst(type);
        }

        public int GetCampaignId()
        {
            return Convert.ToInt32(_accessor.HttpContext.User.FindFirst(ClaimTypes.System).Value);
        }

        public string GetWalletToLower()
        {
            return _accessor.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value.ToLower();
        }
    }
}
