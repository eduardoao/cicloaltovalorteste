using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Test.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace CicloVidaAltoValor.Application.Test.Repositories
{
    public class ProdutoCampanhaTest : IClassFixture<DataBaseFixture>
    {
        private readonly IProdutoCampanhaRepository repository;
        private const int campanhaId = 37;

        public ProdutoCampanhaTest(DataBaseFixture dataBaseFixture)
        {
            repository = dataBaseFixture.ServiceProvider.GetRequiredService<IProdutoCampanhaRepository>();
        }

        [Fact]
        public void TestGetById()
        {
            var model = repository.FindAsync(1).Result;

            Assert.Null(model);
        }

        [Fact]
        public void Test_GetProdutoCampanhaById_Sucess()
        {
            //var model = repository.GetProdutoCampanha(campanhaId).Result;
            //var produtoCampanha = ((List<ProdutoCampanha>)model).Find(p => p.CampanhaProdutoId == 3412);
            //Assert.Equal(3412, produtoCampanha.CampanhaProdutoId);
        }

             

        
    }
}
