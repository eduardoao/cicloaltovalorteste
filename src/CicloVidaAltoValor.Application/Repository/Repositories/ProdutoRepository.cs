using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class ProdutoRepository : SQLRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DotzAppContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> GetAllVoltsById(int produtoId)
        {
            const string sql = @"SELECT 
                                        P.* 
                                FROM WMS_PRODUTO (NOLOCK) P 
								INNER JOIN  WMS_PRODUTO_EQUIVALENCIA (NOLOCK) EQ ON  P.PRODUTO_ID IN (EQ.PRODUTO_ID_PRINCIPAL,EQ.PRODUTO_ID_EQUIVALENTE) AND EQ.PRODUTO_ID_PRINCIPAL <> EQ.PRODUTO_ID_EQUIVALENTE
								WHERE 
                                     EQ.PRODUTO_ID_PRINCIPAL = @produtoId";

            return await this.Connection.SqlConnection.QueryAsync<Produto>(sql, new { produtoId });
        }

        public async Task<bool> IsVoltAsync(int produtoId)
        {
            const int detalheProdutoVolt = 1010;
            const string sql = @"SELECT 
                                        1 
                                FROM WMS_PRODUTO (NOLOCK)
                                WHERE 
                                        PRODUTO_ID = @produtoId AND 
                                        PRODUTO_ID IN (
                                                     SELECT 
                                                            PRODUTO_ID_EQUIVALENTE 
                                                    FROM WMS_PRODUTO_EQUIVALENCIA (NOLOCK)
                                                    WHERE 
                                                        PRODUTO_ID_EQUIVALENTE IN (
                                                                SELECT 
                                                                    PRODUTO_ID 
                                                                FROM WMS_PRODUTO_DETALHE (NOLOCK)
                                                                WHERE 
                                                                    TIPO_PRODUTO_DETALHE_ID = @detalheProdutoVolt
                                                                                )
                                                      )";

            return await this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { produtoId, detalheProdutoVolt });
        }
    }
}
