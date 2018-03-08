using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Interfaces.Services;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Model.ValueObject;
using CicloVidaAltoValor.Application.Settings;

namespace CicloVidaAltoValor.Application.Services
{
    public class FileUserService : IFileUserService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioComplementoRepository _usuarioComplementoRepository;
        private readonly ILogger<FileUserService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly ConnectionStrings _connectionStrings;

        public FileUserService(
            IUsuarioRepository usuarioRepository,
            IUsuarioComplementoRepository usuarioComplementoRepository,
            ILogger<FileUserService> logger,
            IServiceProvider serviceProvider,
            IOptions<ConnectionStrings> options,
            IMapper mapper)


        {
            _usuarioRepository = usuarioRepository;
            _usuarioComplementoRepository = usuarioComplementoRepository;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
            _connectionStrings = options.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        /// <param name="file"></param>
        /// <param name="campaign"></param>
        /// <param name="campanhaFaseAtual"></param>
        /// <returns></returns>
        public async Task ProcessAsync(IEnumerable<UsuarioArquivo> users, Arquivo file, CampanhaFase campanhaFaseAtual)
        {

            foreach (var item in users.Where(x => x.Valido))
            {

                var usuario = await _usuarioRepository.GetByCampaignAndDocumentAsync(campanhaFaseAtual.CampanhaId, item.Documento);
                if (usuario != null)
                {
                    _logger.LogError($"Usuário já existe ou já cadastrado para campanha: CPF: [{item.Documento}] - Campanha: [{campanhaFaseAtual.CampanhaId}]");
                    continue;
                }

                if (!campanhaFaseAtual.IsFase1())
                {
                    continue;
                }

                usuario = _mapper.Map<Usuario>(item);
                usuario.CampanhaId = campanhaFaseAtual.CampanhaId;

                usuario = await _usuarioRepository.InsertAsync(usuario);

                foreach (var complemento in item.UsuarioComplemento)
                {
                    var usuarioComplemento = _mapper.Map<UsuarioComplemento>(complemento);
                    usuarioComplemento.UsuarioId = usuario.UsuarioId;
                    usuarioComplemento = await _usuarioComplementoRepository.InsertAsync(usuarioComplemento);

                }

            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task DropTempTableAsync()
        {
            using (var conn = new SqlConnection(_connectionStrings.DotzApp))
            {
                conn.Open();
                var comm = conn.CreateCommand();
                const string sql =
                    @" IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'BB_TEMP_USUARIO_CADASTRO')
                                        BEGIN
                                          DROP TABLE BB_TEMP_USUARIO_CADASTRO
                                        END  ;";
                comm.CommandText = sql;
                await comm.ExecuteNonQueryAsync();
                conn.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private async Task CreateTempTableAsync(DataTable dataTable)
        {
            using (var conn = new SqlConnection(_connectionStrings.DotzApp))
            {
                conn.Open();
                var comm = conn.CreateCommand();


                //const string sql = @"CREATE TABLE [dbo].[BB_TEMP_USUARIO_CADASTRO](
                //                 [CPR_USUARIO_ID] [int] NULL,
                //                 [CPR_CAMPANHA_ID] [int] NOT NULL,
                //                 [USR_USUARIO_ID] [int] NULL,
                //                 [DT_OPT_IN] [datetime] NOT NULL,
                //                 [NIVEL] [smallint] NULL,
                //                 [NIVEL_COMPARTILHADO] [bit] NULL,
                //                 [APELIDO] [varchar](15) NULL,
                //                 [NOME] [varchar](100) NULL,
                //                 [DT_NASCIMENTO] [datetime] NULL,
                //                 [DOCUMENTO] [varchar](20) NULL,
                //                 [EMAIL] [varchar](128) NULL,
                //                 [DESC_LOGRADOURO] [varchar](300) NULL,
                //                 [NUMERO_LOGRADOURO] [varchar](30) NULL,
                //                 [USR_ESTADO_UF] [char](2) NULL,
                //                 [NUMERO_CEP] [varchar](9) NULL,
                //                 [NOME_CIDADE] [varchar](100) NULL,
                //                 [NOME_BAIRRO] [varchar](100) NULL,
                //                 [DESC_COMPLEMENTO] [varchar](300) NULL,
                //                 [INFORMACOES_ADICIONAIS] [varchar](1000) NULL,
                //                 [NUMERO_DDD1] [int] NULL,
                //                 [NUMERO_TELEFONE1] [varchar](10) NULL,
                //                 [NUMERO_DDD2] [int] NULL,
                //                 [NUMERO_TELEFONE2] [varchar](10) NULL,
                //                 [NUMERO_DDD3] [int] NULL,
                //                 [NUMERO_TELEFONE3] [varchar](10) NULL,
                //                 [INDICADOR_SEXO] [char](1) NULL,
                //                 [DT_ULTIMA_ATUALIZACAO] [datetime] NULL,
                //                 [OBSERVACAO] [varchar](300) NULL,
                //                 [CPR_USUARIO_ORIGEM_ID] [int] NULL,
                //                 [CARTEIRA] [varchar](20) NULL,
                //                 [DT_OPT_OUT] [datetime] NULL,                                    

                //                    --COMPLEMENTO
                //                    [PLST1] VARCHAR(120) NULL,
                //                    [PLST2] VARCHAR(120) NULL,
                //                    [PLST3] VARCHAR(120) NULL,
                //                    [PLST4] VARCHAR(120) NULL,
                //                    [PLST5] VARCHAR(120) NULL,
                //                    [PLST6] VARCHAR(120) NULL,
                //                    [PLST7] VARCHAR(120) NULL,
                //                    [PLST8] VARCHAR(120) NULL,
                //                    [PLST9] VARCHAR(120) NULL,
                //                    [PLST10] VARCHAR(120) NULL,
                //                    [PLST11] VARCHAR(120) NULL,
                //                    [PLST12] VARCHAR(120) NULL,
                //                    [PLST13] VARCHAR(120) NULL,
                //                    [PLST14] VARCHAR(120) NULL,
                //                    [PLST15] VARCHAR(120) NULL,
                //                    [PLST16] VARCHAR(120) NULL,
                //                    [PLST17] VARCHAR(120) NULL,
                //                    [PLST18] VARCHAR(120) NULL,
                //                    [META1] VARCHAR(120) NULL,
                //                    [META2] VARCHAR(120) NULL,
                //                    [META3] VARCHAR(120) NULL
                //                    );";


                const string sql = @"CREATE TABLE [dbo].[BB_TEMP_USUARIO_CADASTRO](
	                                [CPR_USUARIO_ID] [int] NULL,
	                                [CPR_CAMPANHA_ID] [int] NOT NULL,
	                                [USR_USUARIO_ID] [int] NULL,
	                                [DT_OPT_IN] [datetime] NOT NULL,
	                                [NIVEL] [smallint] NULL,
	                                [NIVEL_COMPARTILHADO] [bit] NULL,
	                                [APELIDO] [varchar](15) NULL,
	                                [NOME] [varchar](100) NULL,
	                                [DT_NASCIMENTO] [datetime] NULL,
	                                [DOCUMENTO] [varchar](20) NULL,
	                                [EMAIL] [varchar](128) NULL,
	                                [DESC_LOGRADOURO] [varchar](300) NULL,
	                                [NUMERO_LOGRADOURO] [varchar](30) NULL,
	                                [USR_ESTADO_UF] [char](2) NULL,
	                                [NUMERO_CEP] [varchar](9) NULL,
	                                [NOME_CIDADE] [varchar](100) NULL,
	                                [NOME_BAIRRO] [varchar](100) NULL,
	                                [DESC_COMPLEMENTO] [varchar](300) NULL,
	                                [INFORMACOES_ADICIONAIS] [varchar](1000) NULL,
	                                [NUMERO_DDD1] [int] NULL,
	                                [NUMERO_TELEFONE1] [varchar](10) NULL,
	                                [NUMERO_DDD2] [int] NULL,
	                                [NUMERO_TELEFONE2] [varchar](10) NULL,
	                                [NUMERO_DDD3] [int] NULL,
	                                [NUMERO_TELEFONE3] [varchar](10) NULL,
	                                [INDICADOR_SEXO] [char](1) NULL,
	                                [DT_ULTIMA_ATUALIZACAO] [datetime] NULL,
	                                [OBSERVACAO] [varchar](300) NULL,
	                                [CPR_USUARIO_ORIGEM_ID] [int] NULL,
	                                [CARTEIRA] [varchar](20) NULL,
	                                [DT_OPT_OUT] [datetime] NULL      
                                    );";

                comm.CommandText = sql;

                await comm.ExecuteNonQueryAsync();

                comm.CommandText = "select * from BB_TEMP_USUARIO_CADASTRO where 1 = 2";
                var dtp = new SqlDataAdapter(comm);
                dtp.Fill(dataTable);
                conn.Close();
            }
        }

        private DataTable GetTableStructure()
        {
            using (var conn = new SqlConnection(_connectionStrings.DotzApp))
            {
                conn.Open();
                var comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM BB_TEMP_USUARIO_CADASTRO WHERE 1 = 2";
                var dtp = new SqlDataAdapter(comm);
                var table = new DataTable();
                dtp.Fill(table);
                return table;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="campaignSeuDesejo"></param>
        /// <returns></returns>
        public async Task<int> ProcessBulkCopyAsync(Arquivo file, CampanhaFase campanhaFase)
        {
            await DropTempTableAsync();

            int result;


            using (var dataTable = new DataTable())
            {
                await CreateTempTableAsync(dataTable);
                _logger.LogInformation("Read file and send records.");
                result = await ProcessFileAsync(file, dataTable, campanhaFase);
            }

            _logger.LogInformation("Merge User Status.");
            await _usuarioRepository.FixUserAndComplement();
            await DropTempTableAsync();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="campanhaAtual"></param>
        /// <returns></returns>
        public async Task<int> ProcessBulkCopyAsync(Arquivo file, Campanha campanhaAtual)
        {
            await DropTempTableAsync();

            int result;

            using (var dataTable = new DataTable())
            {
                await CreateTempTableAsync(dataTable);
                _logger.LogInformation("Read file and send records.");
                result = await ProcessFileAsync(file, dataTable, campanhaAtual);
            }

            _logger.LogInformation("Merge User Status.");
            await _usuarioRepository.FixUserAndComplement();
            await DropTempTableAsync();

            return result;

        }


        private async Task<int> ProcessFileAsync(Arquivo file, DataTable dataTable, CampanhaFase campanhaFase)
        {
            var elapsed = new Stopwatch();
            elapsed.Start();
            var iLine = 0;
            var batchsize = 0;
            var rows = 0;
            const char delimiter = ';';
            var columns = new Dictionary<string, int>();



            using (var bk = new SqlBulkCopy(_connectionStrings.DotzApp)
            {
                DestinationTableName = "BB_TEMP_USUARIO_CADASTRO",
                BulkCopyTimeout = 0,
                BatchSize = 100000
            })
            using (var stream = new StreamReader(file.Nome))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                  

                    
                    //var columnsTable = dataTable.Columns;


                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var values = line.Split(delimiter);

                    /*
                    if (columns.Count == 0)
                    {
                        for (var i = 0; i < values.Length; i++)
                        {
                            var value = values[i];
                            if (!columns.ContainsKey(value))
                            {
                                columns.Add(value, i);
                            }
                        }
                        continue;
                    }

                    var item = new UsuarioArquivo();


                    foreach (var column in columns)
                    {
                        if (values.Length <= column.Value)
                            continue;

                        var value = values[column.Value];

                        if (value == null)
                            continue;

                        line = item.Set(column.Key, value, line);
                    }

                    if (!item.Valido)
                    {
                        _logger.LogError($"Item inválido: {JsonConvert.SerializeObject(item, Formatting.Indented)}");
                        continue;
                    }
                    */


                    // Com validação direta

                    iLine++;
                    if (iLine == 1) continue;

                    var item = new UsuarioArquivo();

                    item.Read(values);

                    if (!item.Valido)
                    {
                        _logger.LogError($"Item inválido: {JsonConvert.SerializeObject(item, Formatting.Indented)}");
                        continue;
                    }
                    

                    var usuario = _mapper.Map<Usuario>(item);
                    usuario.CampanhaId = campanhaFase.CampanhaId;

                    var dr = dataTable.NewRow();

                    dr["CPR_USUARIO_ID"] = usuario.UsuarioId;
                    dr["CPR_CAMPANHA_ID"] = usuario.CampanhaId;
                    dr["USR_USUARIO_ID"] = (object)usuario.UsrUsuarioId ?? DBNull.Value;
                    dr["DT_OPT_IN"] = DateTime.Now;
                    dr["NIVEL"] = DBNull.Value;
                    dr["NIVEL_COMPARTILHADO"] = DBNull.Value;
                    dr["APELIDO"] = DBNull.Value;
                    dr["NOME"] = usuario.Nome;
                    dr["DT_NASCIMENTO"] = usuario.DataNascimento;
                    dr["DOCUMENTO"] = usuario.Documento;
                    dr["EMAIL"] = DBNull.Value;
                    dr["DESC_LOGRADOURO"] = (object)usuario.Logradouro ?? DBNull.Value;
                    dr["NUMERO_LOGRADOURO"] = (object)usuario.NumeroLogradouro ?? DBNull.Value;
                    dr["USR_ESTADO_UF"] = (object)usuario.Estado ?? DBNull.Value;
                    dr["NUMERO_CEP"] = (object)usuario.Cep ?? DBNull.Value;
                    dr["NOME_CIDADE"] = (object)usuario.Cidade ?? DBNull.Value;
                    dr["NOME_BAIRRO"] = (object)usuario.Bairro ?? DBNull.Value;
                    dr["DESC_COMPLEMENTO"] = (object)usuario.Complemento ?? DBNull.Value;
                    dr["INFORMACOES_ADICIONAIS"] = (object)usuario.InformacoesAdicionais ?? DBNull.Value;
                    dr["NUMERO_DDD1"] = (object)usuario.DddResidencial ?? DBNull.Value;
                    dr["NUMERO_TELEFONE1"] = (object)usuario.TelefoneResidencial ?? DBNull.Value;
                    dr["NUMERO_DDD2"] = (object)usuario.DddCelular ?? DBNull.Value;
                    dr["NUMERO_TELEFONE2"] = (object)usuario.TelefoneCelular ?? DBNull.Value;
                    dr["NUMERO_DDD3"] = (object)usuario.DddComercial ?? DBNull.Value;
                    dr["NUMERO_TELEFONE3"] = (object)usuario.TelefoneComercial ?? DBNull.Value;
                    dr["INDICADOR_SEXO"] = usuario.Sexo;
                    dr["DT_ULTIMA_ATUALIZACAO"] = DateTime.Now;
                    dr["OBSERVACAO"] = DBNull.Value;
                    dr["CPR_USUARIO_ORIGEM_ID"] = DBNull.Value;
                    dr["CARTEIRA"] = (object)usuario.Carteira ?? DBNull.Value;
                    dr["DT_OPT_OUT"] = DateTime.Now;

                    foreach (var complemento in item.UsuarioComplemento)
                    {
                        dr[complemento.Nome] = (object)complemento.Valor ?? DBNull.Value;
                    }


                    


                    batchsize += 1;
                    rows += 1;
                    dataTable.Rows.Add(dr);
                    if (batchsize != 100000)
                    {
                        continue;
                    }
                    await bk.WriteToServerAsync(dataTable);
                    dataTable.Rows.Clear();
                    batchsize = 0;
                }

                bk.WriteToServer(dataTable);
                dataTable.Rows.Clear();
            }

            elapsed.Stop();

            _logger.LogInformation($"{rows} records imported in {elapsed.Elapsed:hh\\:mm\\:ss} seconds.");


            return rows;
        }


        private async Task<int> ProcessFileAsync(Arquivo file, DataTable dataTable, Campanha campanhaFase)
        {
            var elapsed = new Stopwatch();
            elapsed.Start();
            var iLine = 0;
            var batchsize = 0;
            var rows = 0;
            const char delimiter = ';';
            var columns = new Dictionary<string, int>();



            using (var bk = new SqlBulkCopy(_connectionStrings.DotzApp)
            {
                DestinationTableName = "BB_TEMP_USUARIO_CADASTRO",
                BulkCopyTimeout = 0,
                BatchSize = 100000
            })

            using (var stream = new StreamReader(file.Nome))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var values = line.Split(delimiter);


                    iLine++;
                    if (iLine == 1) continue;

                    var item = new UsuarioArquivo();

                    item.Read(values);

                    if (!item.Valido)
                    {
                        _logger.LogError($"Item inválido: {JsonConvert.SerializeObject(item, Formatting.Indented)}");
                        continue;
                    }


                    var usuario = _mapper.Map<Usuario>(item);
                    usuario.CampanhaId = campanhaFase.CampanhaId;

                    var dr = dataTable.NewRow();

                    dr["CPR_USUARIO_ID"] = usuario.UsuarioId;
                    dr["CPR_CAMPANHA_ID"] = usuario.CampanhaId;
                    dr["USR_USUARIO_ID"] = (object)usuario.UsrUsuarioId ?? DBNull.Value;
                    dr["DT_OPT_IN"] = DateTime.Now;
                    dr["NIVEL"] = DBNull.Value;
                    dr["NIVEL_COMPARTILHADO"] = DBNull.Value;
                    dr["APELIDO"] = DBNull.Value;
                    dr["NOME"] = usuario.Nome;
                    dr["DT_NASCIMENTO"] = usuario.DataNascimento;
                    dr["DOCUMENTO"] = usuario.Documento;
                    dr["EMAIL"] = DBNull.Value;
                    dr["DESC_LOGRADOURO"] = (object)usuario.Logradouro ?? DBNull.Value;
                    dr["NUMERO_LOGRADOURO"] = (object)usuario.NumeroLogradouro ?? DBNull.Value;
                    dr["USR_ESTADO_UF"] = (object)usuario.Estado ?? DBNull.Value;
                    dr["NUMERO_CEP"] = (object)usuario.Cep ?? DBNull.Value;
                    dr["NOME_CIDADE"] = (object)usuario.Cidade ?? DBNull.Value;
                    dr["NOME_BAIRRO"] = (object)usuario.Bairro ?? DBNull.Value;
                    dr["DESC_COMPLEMENTO"] = (object)usuario.Complemento ?? DBNull.Value;
                    dr["INFORMACOES_ADICIONAIS"] = (object)usuario.InformacoesAdicionais ?? DBNull.Value;
                    dr["NUMERO_DDD1"] = (object)usuario.DddResidencial ?? DBNull.Value;
                    dr["NUMERO_TELEFONE1"] = (object)usuario.TelefoneResidencial ?? DBNull.Value;
                    dr["NUMERO_DDD2"] = (object)usuario.DddCelular ?? DBNull.Value;
                    dr["NUMERO_TELEFONE2"] = (object)usuario.TelefoneCelular ?? DBNull.Value;
                    dr["NUMERO_DDD3"] = (object)usuario.DddComercial ?? DBNull.Value;
                    dr["NUMERO_TELEFONE3"] = (object)usuario.TelefoneComercial ?? DBNull.Value;
                    dr["INDICADOR_SEXO"] = usuario.Sexo;
                    dr["DT_ULTIMA_ATUALIZACAO"] = DateTime.Now;
                    dr["OBSERVACAO"] = DBNull.Value;
                    dr["CPR_USUARIO_ORIGEM_ID"] = DBNull.Value;
                    dr["CARTEIRA"] = (object)usuario.Carteira ?? DBNull.Value;
                    dr["DT_OPT_OUT"] = DateTime.Now;

                    foreach (var complemento in item.UsuarioComplemento)
                    {
                        dr[complemento.Nome] = (object)complemento.Valor ?? DBNull.Value;
                    }

                    batchsize += 1;
                    rows += 1;
                    dataTable.Rows.Add(dr);
                    if (batchsize != 100000)
                    {
                        continue;
                    }
                    await bk.WriteToServerAsync(dataTable);
                    dataTable.Rows.Clear();
                    batchsize = 0;
                }

                bk.WriteToServer(dataTable);
                dataTable.Rows.Clear();
            }

            elapsed.Stop();

            _logger.LogInformation($"{rows} records imported in {elapsed.Elapsed:hh\\:mm\\:ss} seconds.");


            return rows;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

      
    }
}
