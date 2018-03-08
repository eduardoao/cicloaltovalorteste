using System;
using System.Threading.Tasks;
using Dapper;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;
using Dharma.Repository.SQL;
using Dommel;
using System.Data.SqlClient;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class UsuarioRepository : SQLRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DotzAppContext context) : base(context)
        {
        }

        public async Task<Usuario> GetByCampaignAndDocumentAsync(int campanhaId, string documento)
        {
            const string sql = @"SELECT * FROM CPR_USUARIO (NOLOCK) WHERE CPR_CAMPANHA_ID = @campanhaId AND DOCUMENTO = @documento;";
            var p = new { campanhaId, documento = new DbString() { Value = documento, Length = 20, IsAnsi = true } };
            return await this.Connection.SqlConnection.QueryFirstOrDefaultAsync<Usuario>(sql, p);
        }

        public async Task<bool> UpdateAsync(Usuario entity)
        {
            entity.DataUltimaAtualizacao = DateTime.Now;
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<Usuario> InsertAsync(Usuario entity)
        {
            entity.DataUltimaAtualizacao = DateTime.Now;
            entity.DataOptin = DateTime.Now;

            entity.UsuarioId = Convert.ToInt32(await Connection.SqlConnection.InsertAsync(entity));

            return entity;
        }

        public async Task<Usuario> GetByBirthDateAndDocumentAndCampaign(int campanhaId, string documento, DateTime dataNascimento)
        {
            var p = new
            {
                documento = new DbString() { Value = documento, Length = 20, IsAnsi = true },
                dataNascimento,
                campanhaId
            };

            const string sql = @"SELECT * FROM CPR_USUARIO (NOLOCK) WHERE CPR_CAMPANHA_ID = @campanhaId AND DOCUMENTO = @documento AND DT_NASCIMENTO = @dataNascimento;";

            return await this.Connection.SqlConnection.QueryFirstOrDefaultAsync<Usuario>(sql, p);
        }

        public async Task<Usuario> GetByDocumentAndCampaign(int campanhaId, string documento)
        {
            var p = new
            {
                documento = new DbString() { Value = documento, Length = 20, IsAnsi = true },
                campanhaId
            };

            const string sql = @"SELECT * FROM CPR_USUARIO (NOLOCK) WHERE CPR_CAMPANHA_ID = @campanhaId AND DOCUMENTO = @documento;";

            return await this.Connection.SqlConnection.QueryFirstOrDefaultAsync<Usuario>(sql, p);
        }

        public async Task<bool> HasUpdateAddressRegister(int userId)
        {
            const string sql = @"SELECT 
                                        * 
                                 FROM CPR_USUARIO
                                 WHERE CPR_USUARIO_ID = @userId 
                                   AND ISNULL(DESC_LOGRADOURO,'') <> ''
		                           AND ISNULL(NUMERO_LOGRADOURO,'') <> ''
		                           AND ISNULL(USR_ESTADO_UF,'') <> ''
		                           AND ISNULL(NUMERO_CEP,'') <> ''
		                           AND ISNULL(NOME_CIDADE,'') <> ''
		                           AND ISNULL(NOME_BAIRRO,'') <> '' ";

            return await this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { userId });
        }

        public async Task FixUserAndComplement()
        {
            using (var conn = new SqlConnection(this.Connection.SqlConnection.ConnectionString))
            {
                var comm = conn.CreateCommand();


                conn.Open();


                comm.CommandTimeout = 0;
                // acertando o campo cpr_usuario_id
                comm.CommandText =
                    "UPDATE A SET A.CPR_USUARIO_ID = B.CPR_USUARIO_ID FROM BB_TEMP_USUARIO_CADASTRO A, CPR_USUARIO B WHERE A.DOCUMENTO = B.DOCUMENTO AND B.CPR_CAMPANHA_ID = A.CPR_CAMPANHA_ID";
                await comm.ExecuteNonQueryAsync();


                /*
                comm.CommandTimeout = 0;
                // atualiza os registros caso existam
                comm.CommandText = @"MERGE CPR_USUARIO AS T
                                     USING BB_TEMP_USUARIO_CADASTRO AS S ON (T.CPR_USUARIO_ID = S.CPR_USUARIO_ID AND T.CPR_CAMPANHA_ID = S.CPR_CAMPANHA_ID)
                                     WHEN MATCHED THEN 
                                                  UPDATE SET 
                                                        T.NOME = S.NOME;"; //......


                await comm.ExecuteNonQueryAsync();
                */

                comm.CommandTimeout = 0;

                // incluindo os registros
                comm.CommandText = @"MERGE CPR_USUARIO AS T
                                     USING BB_TEMP_USUARIO_CADASTRO AS S ON (T.CPR_USUARIO_ID = S.CPR_USUARIO_ID AND T.CPR_CAMPANHA_ID = S.CPR_CAMPANHA_ID)
                                    WHEN NOT MATCHED BY TARGET AND CPR_USUARIO_ID IS NOT NULL
                                        THEN INSERT (
                                        CPR_CAMPANHA_ID
                                        ,USR_USUARIO_ID
                                        ,DT_OPT_IN
                                        ,NIVEL
                                        ,NIVEL_COMPARTILHADO
                                        ,APELIDO
                                        ,NOME
                                        ,DT_NASCIMENTO
                                        ,DOCUMENTO
                                        ,EMAIL
                                        ,DESC_LOGRADOURO
                                        ,NUMERO_LOGRADOURO
                                        ,USR_ESTADO_UF
                                        ,NUMERO_CEP
                                        ,NOME_CIDADE
                                        ,NOME_BAIRRO
                                        ,DESC_COMPLEMENTO
                                        ,INFORMACOES_ADICIONAIS
                                        ,NUMERO_DDD1
                                        ,NUMERO_TELEFONE1
                                        ,NUMERO_DDD2
                                        ,NUMERO_TELEFONE2
                                        ,NUMERO_DDD3
                                        ,NUMERO_TELEFONE3
                                        ,INDICADOR_SEXO
                                        ,DT_ULTIMA_ATUALIZACAO
                                        ,OBSERVACAO
                                        ,CPR_USUARIO_ORIGEM_ID
                                        ,CARTEIRA
                                        ,DT_OPT_OUT) 
                                        VALUES 
                                        (S.CPR_CAMPANHA_ID
                                        ,S.USR_USUARIO_ID
                                        ,S.DT_OPT_IN
                                        ,S.NIVEL
                                        ,S.NIVEL_COMPARTILHADO
                                        ,S.APELIDO
                                        ,S.NOME
                                        ,S.DT_NASCIMENTO
                                        ,S.DOCUMENTO
                                        ,S.EMAIL
                                        ,S.DESC_LOGRADOURO
                                        ,S.NUMERO_LOGRADOURO
                                        ,S.USR_ESTADO_UF
                                        ,S.NUMERO_CEP
                                        ,S.NOME_CIDADE
                                        ,S.NOME_BAIRRO
                                        ,S.DESC_COMPLEMENTO
                                        ,S.INFORMACOES_ADICIONAIS
                                        ,S.NUMERO_DDD1
                                        ,S.NUMERO_TELEFONE1
                                        ,S.NUMERO_DDD2
                                        ,S.NUMERO_TELEFONE2
                                        ,S.NUMERO_DDD3
                                        ,S.NUMERO_TELEFONE3
                                        ,S.INDICADOR_SEXO
                                        ,S.DT_ULTIMA_ATUALIZACAO
                                        ,S.OBSERVACAO
                                        ,S.CPR_USUARIO_ORIGEM_ID
                                        ,S.CARTEIRA
                                        ,S.DT_OPT_OUT);";

                await comm.ExecuteNonQueryAsync();

                comm.CommandTimeout = 0;

                // acertando o campo cpr_usuario_id
                comm.CommandText =
                    "UPDATE A SET A.CPR_USUARIO_ID = B.CPR_USUARIO_ID FROM BB_TEMP_USUARIO_CADASTRO A, CPR_USUARIO B WHERE A.DOCUMENTO = B.DOCUMENTO AND B.CPR_CAMPANHA_ID = A.CPR_CAMPANHA_ID AND A.CPR_USUARIO_ID IS NULL";
                await comm.ExecuteNonQueryAsync();


                //remove todos os complementos
                //comm.CommandTimeout = 0;

                //comm.CommandText = @"MERGE CPR_USUARIO_COMPLEMENTO AS T
                //                    USING BB_TEMP_USUARIO_CADASTRO AS S ON (T.CPR_USUARIO_ID = S.CPR_USUARIO_ID)
                //                    WHEN MATCHED  THEN DELETE ;";


                await comm.ExecuteNonQueryAsync();


                //adiciona os novos complementos
            //    comm.CommandTimeout = 0;

            //    comm.CommandText = @"INSERT INTO CPR_USUARIO_COMPLEMENTO (CPR_USUARIO_ID, NOME, VALOR, TIPO_COMPLEMENTO_ID, DATA_CRIACAO)
												//SELECT CPR_USUARIO_ID, UPPER(NOME)[NOME], UPPER(VALOR)[VALOR], CASE CHARINDEX('META', NOME) WHEN 0 THEN 2 ELSE 1 END [TIPO_COMPLEMENTO_ID], GETDATE()
										  //      FROM 
												//	(SELECT  S.CPR_USUARIO_ID, PLST1, PLST2, PLST3, PLST4, PLST5, PLST6, PLST7, PLST8, PLST9, PLST10, PLST11, PLST12, PLST13, PLST14, PLST15,  PLST16, PLST17, PLST18, META1, META2, META3 
												//	 FROM BB_TEMP_USUARIO_CADASTRO S 
												//	 INNER JOIN CPR_USUARIO U ON S.CPR_USUARIO_ID = U.CPR_USUARIO_ID
												//	 WHERE S.CPR_USUARIO_ID IS NOT NULL
												//			) P UNPIVOT 
												//			( VALOR FOR  NOME IN (PLST1, PLST2, PLST3, PLST4, PLST5, PLST6, PLST7, PLST8, PLST9, PLST10, PLST11, PLST12, PLST13,  PLST14, PLST15,  PLST16, PLST17, PLST18, META1, META2, META3)) 
												//						AS X;";

            //    await comm.ExecuteNonQueryAsync();
            }
        }
    }

}
