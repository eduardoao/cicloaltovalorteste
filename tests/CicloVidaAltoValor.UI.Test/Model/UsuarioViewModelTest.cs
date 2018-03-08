using AutoMapper;
using CicloVidaAltoValor.Application.Contracts.Usuario;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Test.Fixtures;
using CicloVidaAltoValor.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using Xunit;

namespace CicloVidaAltoValor.Application.Test.Model
{
    public class UsuarioViewModelTest : IClassFixture<DataBaseFixture>
    {
        private IUsuarioRepository repository;
        private IMapper mapper;

        public UsuarioViewModelTest(DataBaseFixture fixture)
        {
            repository = fixture.ServiceProvider.GetService<IUsuarioRepository>();
            mapper = fixture.ServiceProvider.GetService<IMapper>();
        }

       
        [Fact]
        public void Test_ViewModel_Validator_Usuario_Sucess()
        {
            var validator = new UsuarioViewModelValidator();
            var usuario = repository.FindAsync(6695773).Result;

            Assert.NotNull(usuario);

            var vm = mapper.Map<UsuarioViewModel>(usuario);            
            vm.TelefoneComercial = "(11) 9194-8217";
            vm.TelefoneResidencial = "(11) 9994-8217";
            vm.TelefoneCelular = "(11) 94994-8217"; 
            vm.Cep = "08255-000";

            var response = validator.Validate(vm);

            Console.WriteLine(JsonConvert.SerializeObject(response.Errors, Formatting.Indented));

            Assert.True(response.IsValid);         

        }

        //[Fact]
        public void TestNumber()
        {
            const string culture = "pt-BR";
            var cultureBr = new System.Globalization.CultureInfo(culture);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureBr;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureBr;

            decimal d = 5000m;

            Console.WriteLine(d.ToString("N2"));
            Console.WriteLine(d.ToString("N0"));
            Console.WriteLine(d.ToString("F0"));
            Console.WriteLine(d.ToString("F2"));
            Console.WriteLine(String.Format(d % 1 == 0 ? "{0:N0}" : "{0:N2}", d));
        }
    }
}
