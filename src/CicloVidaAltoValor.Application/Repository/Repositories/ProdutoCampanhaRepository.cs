using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;
using Dapper;
using Dharma.Repository.SQL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ProdutoCampanhaRepository : SQLRepository<ProdutoCampanha>, IProdutoCampanhaRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ProdutoCampanhaRepository(DotzAppContext context) : base(context)
        {
        }



        public async Task<IEnumerable<ProdutoCampanha>> GetAllByCampaignIdAsync(int campanhaId, string carteira)
        {
            const string sql = @"SELECT 
                                        * 
                                 FROM CPR_PRODUTO_CAMPANHA (NOLOCK) A
                                 INNER JOIN CPR_CAMPANHA_PRODUTO (NOLOCK) B ON A.CPR_CAMPANHA_PRODUTO_ID  = B.CPR_CAMPANHA_PRODUTO_ID
                                 INNER JOIN WMS_PRODUTO (NOLOCK) C ON B.WMS_PRODUTO_ID = C.PRODUTO_ID
                                 WHERE  B.CPR_CAMPANHA_ID = @campanhaId 
                                        AND A.CARTEIRA = @carteira
                                        AND A.ATIVO = 1 ";
            var p = new { carteira = new DbString { Value = carteira, Length = 60, IsAnsi = true }, campanhaId };


            return (await this.Connection.SqlConnection.QueryAsync<ProdutoCampanha, CampanhaProduto, Produto, ProdutoCampanha>(sql, (produtoCampanha, campanhaProduto, produto) =>
            {
                produtoCampanha.CampanhaProduto = campanhaProduto;
                produtoCampanha.CampanhaProduto.Produto = produto;
                return produtoCampanha;
            }, splitOn: "CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID", param: p));
        }

        public async Task<ProdutoCampanha> GetByCampaignIdAsync(int campanhaId)
        {
            const string sql = @"SELECT 
                                        * 
                                 FROM CPR_PRODUTO_CAMPANHA (NOLOCK) A
                                 INNER JOIN CPR_CAMPANHA_PRODUTO (NOLOCK) B ON A.CPR_CAMPANHA_PRODUTO_ID  = B.CPR_CAMPANHA_PRODUTO_ID
                                 INNER JOIN WMS_PRODUTO (NOLOCK) C ON B.WMS_PRODUTO_ID = C.PRODUTO_ID
                                 INNER JOIN CPR_CAMPANHA (NOLOCK) D ON B.CPR_CAMPANHA_ID = D.CPR_CAMPANHA_ID          
                                 INNER JOIN CPR_CAMPANHA_TIPO (NOLOCK) E ON D.CPR_CAMPANHA_TIPO_ID = E.CPR_CAMPANHA_TIPO_ID
                                 LEFT JOIN DBS_PARCEIRO F (NOLOCK) ON D.USUARIO_ID_PARCEIRO = F.USUARIO_ID_PARCEIRO
                                 WHERE  B.CPR_CAMPANHA_ID = @campanhaId AND A.ATIVO = 1 ";

            return (await this.Connection.SqlConnection.QueryAsync<ProdutoCampanha, CampanhaProduto, Produto, Campanha, CampanhaTipo, Parceiro, ProdutoCampanha>(sql,
                (produtoCampanha, campanhaProduto, produto, campanha, campanhaTipo, parceiro) =>
                {
                    campanha.CampanhaTipo = campanhaTipo;
                    campanha.Parceiro = parceiro;
                    produtoCampanha.CampanhaProduto = campanhaProduto;
                    produtoCampanha.CampanhaProduto.Produto = produto;
                    produtoCampanha.CampanhaProduto.Campanha = campanha;
                    return produtoCampanha;
                }, splitOn: "CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID,CPR_CAMPANHA_ID,CPR_CAMPANHA_TIPO_ID, USUARIO_ID_PARCEIRO", param: new { campanhaId })).FirstOrDefault();
        }

        public async Task<ProdutoCampanha> FindByAsync(int produtoCampanhaId, int campanhaProdutoId, int produtoId, int campanhaId, string carteira)
        {
            const string sql = @"SELECT 
                                        * 
                                 FROM CPR_PRODUTO_CAMPANHA (NOLOCK) A
                                 INNER JOIN CPR_CAMPANHA_PRODUTO (NOLOCK) B ON A.CPR_CAMPANHA_PRODUTO_ID  = B.CPR_CAMPANHA_PRODUTO_ID
                                 INNER JOIN WMS_PRODUTO (NOLOCK) C ON B.WMS_PRODUTO_ID = C.PRODUTO_ID
                                 INNER JOIN CPR_CAMPANHA (NOLOCK) D ON B.CPR_CAMPANHA_ID = D.CPR_CAMPANHA_ID      
                                 WHERE  
                                        A.CPR_PRODUTO_CAMPANHA_ID = @produtoCampanhaId
                                        AND A.CPR_CAMPANHA_PRODUTO_ID = @campanhaProdutoId
                                        AND C.PRODUTO_ID = @produtoId
                                        AND B.CPR_CAMPANHA_ID = @campanhaId
                                        AND A.CARTEIRA = @carteira
                                        AND A.ATIVO = 1 ";

            var p = new { carteira = new DbString { Value = carteira, Length = 60, IsAnsi = true }, produtoCampanhaId, campanhaProdutoId, produtoId, campanhaId };
            return (await this.Connection.SqlConnection.QueryAsync<ProdutoCampanha, CampanhaProduto, Produto, Campanha, ProdutoCampanha>(sql,
                (produtoCampanha, campanhaProduto, produto, campanha) =>
                {

                    produtoCampanha.CampanhaProduto = campanhaProduto;
                    produtoCampanha.CampanhaProduto.Produto = produto;
                    produtoCampanha.CampanhaProduto.Campanha = campanha;
                    return produtoCampanha;
                }, splitOn: "CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID,CPR_CAMPANHA_ID", param: p)).FirstOrDefault();
        }
    }
}
