using System.Collections.Generic;
using System.Security.Claims;
using CicloVidaAltoValor.Application.Enum;

namespace CicloVidaAltoValor.Application.Interfaces.Model
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        string GetUserDocument();
        int GetUserId();
        int GetCampaignId();
        string GetWallet();
        string GetWalletToLower();
        Carteira GetWalletEnum();
        string GetClaim(Claim claim);
        string GetClaimTypeValue(string type);
        Claim GetClaimValue(string type);

    }
}
