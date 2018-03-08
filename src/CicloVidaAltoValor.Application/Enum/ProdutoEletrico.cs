namespace CicloVidaAltoValor.Application.Enum
{
    public static class ProdutoEletrico
    {
        public static string V110 = "110V";
        public static string V220 = "220V";

        public static bool Exist(string value)
        {
            return V110 == value.ToUpper() || V220 == value.ToUpper();
        }

        public static string Get(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            if (V110 == value.ToUpper())
            {
                return V110;
            }
            return V220 == value.ToLower() ? V220 : null;
        }

        public static bool Is110V(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return V110 == value.ToUpper();
        }

        public static bool Is220V(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return V220 == value.ToUpper();
        }

    }
}
