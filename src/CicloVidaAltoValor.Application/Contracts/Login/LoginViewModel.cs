using System;
using System.ComponentModel.DataAnnotations;


namespace CicloVidaAltoValor.Application.Contracts.Login
{
    public class LoginViewModel
    {
        [Display(Name = "CPF")]
        public string Cpf { get; set; }


        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }
    }



}
