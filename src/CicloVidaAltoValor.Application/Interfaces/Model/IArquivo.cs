namespace CicloVidaAltoValor.Application.Interfaces.Model
{
    public interface IArquivo
    {
        string Erro { get; set; }
        bool Valido { get; set; }

        IArquivo Clone();

        void Read(string[] lines);
        string Set(string key, object value, string line);


    }
}
