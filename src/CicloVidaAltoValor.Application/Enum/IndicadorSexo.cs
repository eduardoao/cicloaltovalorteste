namespace CicloVidaAltoValor.Application.Enum
{
    public static class IndicadorSexo
    {
        public static char M { get; } = 'M';
        public static char F { get; } = 'F';

        public static bool Exist(char value)
        {
            return M.ToString() == value.ToString().ToUpper() || F.ToString() == value.ToString().ToUpper();
        }
    }
}
