using System.Linq;

namespace CicloVidaAltoValor.Application.Contracts.Login
{
    public class LoginAdminViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string GetName()
        {
            if (!Username.Contains("."))
            {
                return Username;
            }
            var arr = Username.Split('.');
            return $"{arr[0].First().ToString().ToUpper()}{string.Join("", arr[0].Skip(1))} {arr[1].First().ToString().ToUpper()}{string.Join("", arr[1].Skip(1))}";
        }
    }
}
