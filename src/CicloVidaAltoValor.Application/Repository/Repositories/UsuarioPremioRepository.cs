using System;
using System.Threading.Tasks;
using Dharma.Repository.SQL;
using Dommel;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;
using System.Collections.Generic;
using Dapper;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class UsuarioPremioRepository : SQLRepository<UsuarioPremio>, IUsuarioPremioRepository
    {
        public UsuarioPremioRepository(DotzAppContext context) : base(context)
        {
        }

        public async Task<bool> UpdateAsync(UsuarioPremio entity)
        {
            entity.DataAtualizacao = DateTime.Now;
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<UsuarioPremio> InsertAsync(UsuarioPremio entity)
        {

            entity.UsuarioId = Convert.ToInt32(await Connection.SqlConnection.InsertAsync(entity, this.Connection.CurrentTransaction));

            return entity;
        }

        public async Task<IEnumerable<UsuarioPremio>> GetRedemptions(int campanhaId, DateTime? dataInicio, DateTime? dataFim)
        {
            dataFim = dataFim?.AddDays(1).AddSeconds(-1);

            const string sql = @"SELECT * 
                                FROM [DotzApp]..[CPR_USUARIO_PREMIO] A (NOLOCK) 
		                        INNER JOIN [DotzApp]..[CPR_USUARIO] B (NOLOCK) ON A.CPR_USUARIO_ID = B.CPR_USUARIO_ID
			                        AND B.CPR_CAMPANHA_ID = @campanhaId
                                    AND (DH_PREMIO_SELECIONADO >= @dataInicio OR @dataInicio IS NULL)
                                    AND (DH_PREMIO_SELECIONADO <= @dataFim OR @dataFim IS NULL)
		                        INNER JOIN [DotzApp]..[CPR_CAMPANHA_PRODUTO] C (NOLOCK) ON A.CPR_CAMPANHA_PRODUTO_ID = C.CPR_CAMPANHA_PRODUTO_ID
		                        INNER JOIN [DotzApp]..[WMS_PRODUTO] D (NOLOCK) ON C.WMS_PRODUTO_ID = D.PRODUTO_ID
		                        INNER JOIN [DotzApp]..[WMS_PRODUTO_FORNECEDOR] E (NOLOCK) ON D.PRODUTO_ID = E.PRODUTO_ID
                                ORDER BY B.NOME";

            return await Connection.SqlConnection.QueryAsync<UsuarioPremio, Usuario, CampanhaProduto, Produto, ProdutoFornecedor, UsuarioPremio>(sql, map:
                (premio, usuario, campanhaProduto, produto, produtoFornecedor) =>
                {
                    premio.Usuario = usuario;
                    premio.CampanhaProduto = campanhaProduto;
                    premio.CampanhaProduto.Produto = produto;
                    premio.CampanhaProduto.Produto.ProdutoFornecedor = produtoFornecedor;

                    return premio;
                },
                splitOn: "CPR_USUARIO_PREMIO_ID,CPR_USUARIO_ID,CPR_CAMPANHA_PRODUTO_ID,WMS_PRODUTO_ID,PRODUTO_ID",
                param: new { campanhaId, dataInicio, dataFim });
        }

        public async Task<bool> HasPrizeAsync(int userId)
        {
            const string sql = @"SELECT 
                                        * 
                                 FROM CPR_USUARIO_PREMIO A (NOLOCK)
                                 INNER JOIN CPR_USUARIO B (NOLOCK) ON A.CPR_USUARIO_ID = B.CPR_USUARIO_ID
                                 WHERE B.CPR_USUARIO_ID = @userId";

            return await this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { userId });
        }
    }
}
