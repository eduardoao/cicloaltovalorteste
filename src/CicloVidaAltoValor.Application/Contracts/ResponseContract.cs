using System.Collections.Generic;

namespace CicloVidaAltoValor.Application.Contracts
{
    public class ResponseContract<T>
    {
        public ResponseContract()
        {

        }

        public ResponseContract(bool valid, List<string> errors)
        {
            Valid = valid;
            Errors = errors;
        }

        public ResponseContract(bool valid)
        {
            Valid = valid;
        }

        public ResponseContract(bool valid, T content)
        {
            Valid = valid;
            Content = content;
        }

        
        public bool Valid { get; private set; }
        public T Content { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void AddError(string item)
        {
            Errors.Add(item);
        }

        public void SetValid()
        {
            this.Valid = true;
        }

        public void SetContent(T content)
        {
            this.Content = content;
        }
    }
}