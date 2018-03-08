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
using CicloVidaAltoValor.Application.Extensions;

namespace CicloVidaAltoValor.Application.Services
{
    public class FileUserStatusService : IFileUserStatusService
    {
        private readonly ILogger<FileUserStatusService> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioStatusFaseRepository _usuarioStatusFaseRepository;
        private readonly IUsuarioStatusRepository _usuarioStatus;
        private readonly ICampanhaFaseRepository _campanhaFaseRepository;
        private readonly ICampanhaFaseUsuarioAcessoRepository _campanhaFaseUsuarioAcessoRepository;
        private readonly ConnectionStrings _connectionStrings;


        public FileUserStatusService(
            ILogger<FileUserStatusService> logger,
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            IUsuarioStatusFaseRepository usuarioStatusFaseRepository,
            IUsuarioStatusRepository usuarioStatus,
            ICampanhaFaseRepository campanhaFaseRepository,
            ICampanhaFaseUsuarioAcessoRepository campanhaFaseUsuarioAcessoRepository,
            IOptions<ConnectionStrings> options)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _usuarioStatusFaseRepository = usuarioStatusFaseRepository;
            _campanhaFaseRepository = campanhaFaseRepository;
            _campanhaFaseUsuarioAcessoRepository = campanhaFaseUsuarioAcessoRepository;
            _connectionStrings = options.Value;
            _usuarioStatus = usuarioStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usersStatus"></param>
        /// <param name="file"></param>
        /// <param name="campaign"></param>
        /// <param name="campaignSeuDesejo"></param>
        /// <returns></returns>
        public async Task ProcessAsync(IEnumerable<UsuarioStatusArquivo> usersStatus, Arquivo file, CampanhaFase campaignSeuDesejo)
        {
            var file500Dict = new Dictionary<string, CampanhaFaseUsuarioAcesso>();

            await ProcessUserStatus(usersStatus, file, campaignSeuDesejo, file500Dict);

            /*
            if (file500Dict.Any())
            {
                var valid = await _bonusService.BonusFaseAccess(file500Dict.Keys);
                if (valid)
                {
                    _logger.LogInformation($"File 500 generated with success.");
                }
                else
                {
                    foreach (var usuarioAcesso in file500Dict)
                    {
                        file500Dict[usuarioAcesso.Key].Bonificado = false;
                        await _campanhaFaseUsuarioAcessoRepository.UpdateAsync(file500Dict[usuarioAcesso.Key]);
                    }

                    _logger.LogInformation($"Error on generate file 500.");
                }
            }

           */


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usersStatus"></param>
        /// <param name="file"></param>
        /// <param name="campaign"></param>
        /// <param name="campanhaFaseAtual"></param>
        /// <param name="file500CpfList"></param>
        /// <returns></returns>
        private async Task ProcessUserStatus(IEnumerable<UsuarioStatusArquivo> usersStatus, Arquivo file, CampanhaFase campanhaFaseAtual, IDictionary<string, CampanhaFaseUsuarioAcesso> file500Dict)
        {
            foreach (var item in usersStatus.Where(x => x.Valido))
            {

                try
                {
                    var usuario = await _usuarioRepository.GetByCampaignAndDocumentAsync(campanhaFaseAtual.CampanhaId, item.Cpf);
                    if (usuario == null)
                    {
                        _logger.LogError($"Usuário não encontrado para campanha: CPF: [{item.Cpf}] - Campanha: [{campanhaFaseAtual.CampanhaId}]");
                        continue;
                    }

                    item.UsuarioId = usuario.UsuarioId;
                    var usuarioStatus = _mapper.Map<UsuarioStatusFase>(item);
                    usuarioStatus.ArquivoId = file.Id;

                    if (campanhaFaseAtual.IsCurrentPeriod(item.Periodo))
                    {
                        usuarioStatus.CampanhaFaseId = campanhaFaseAtual.CampanhaFaseId;
                    }
                    else
                    {
                        var campanhaFasePeriodo = await _campanhaFaseRepository.GetByPeriodAsync(item.Periodo);
                        if (campanhaFasePeriodo == null)
                        {
                            _logger.LogError($"Período não encontrado: {item.Periodo:yyyy/MM} - item: {JsonConvert.SerializeObject(item)}");
                            continue;
                        }
                        usuarioStatus.CampanhaFaseId = campanhaFasePeriodo.CampanhaFaseId;
                    }

                    var lastUserStatus = await _usuarioStatusFaseRepository.GetByCampaignFaseIdAndUserIdActiveAsync(usuarioStatus.CampanhaFaseId, usuarioStatus.UsuarioId);

                    if (lastUserStatus != null)
                    {
                        lastUserStatus.Ativo = false;
                        await _usuarioStatusFaseRepository.UpdateAsync(lastUserStatus);
                    }

                    usuarioStatus.Ativo = true;
                    usuarioStatus = await _usuarioStatusFaseRepository.InsertAsync(usuarioStatus);


                    if (!campanhaFaseAtual.IsFase1())
                    {
                        continue;
                    }

                    var exist = await _campanhaFaseUsuarioAcessoRepository.ExistAsync(campanhaFaseAtual.CampanhaFaseId, usuarioStatus.UsuarioId);

                    if (exist)
                    {
                        continue;
                    }

                    var campaignUserFase = new CampanhaFaseUsuarioAcesso
                    {
                        UsuarioId = usuarioStatus.UsuarioId,
                        CampanhaFaseId = campanhaFaseAtual.CampanhaFaseId,
                        Bonificado = true
                    };

                    //campaignUserFase = await _campanhaFaseUsuarioAcessoRepository.InsertAsync(campaignUserFase);

                    file500Dict.Add(usuario.Documento, campaignUserFase);
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, ex.Message);
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
                    @" IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'BB_TEMP_USUARIO_STATUS')
                                        BEGIN
                                          DROP TABLE BB_TEMP_USUARIO_STATUS
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

                const string sql = @"CREATE TABLE BB_TEMP_USUARIO_STATUS (
	                            [CPR_ARQUIVO_ID] [int] NOT NULL,
                                [CPR_USUARIO_CPF] [varchar](20) NOT NULL,
	                            [CPR_USUARIO_ID] [int] NULL,
	                            [CPR_CAMPANHA_FASE_ID] [int] NULL,
	                            [PERIODO] [datetime] NOT NULL,
	                            [META] [decimal](18, 2) NOT NULL,
	                            [FAIXA_META] [varchar](80) NOT NULL,
	                            [GASTO] [decimal](18, 2) NOT NULL,
	                            [GASTO_PERCENTUAL] [varchar](10) NULL,
	                            [DESAFIO_1] [bit] NOT NULL,
	                            [DESAFIO_2] [bit] NOT NULL,
	                            [DESAFIO_3] [bit] NOT NULL,
	                            [DESAFIO_4] [bit] NOT NULL,
	                            [DESAFIO_5] [bit] NOT NULL,
	                            [DESAFIO_6] [bit] NOT NULL,
	                            [DESAFIO_7] [bit] NOT NULL,
	                            [CATALOGO] [varchar](80) NULL)";

                comm.CommandText = sql;

                await comm.ExecuteNonQueryAsync();

                comm.CommandText = "select * from BB_TEMP_USUARIO_STATUS where 1 = 2";
                var dtp = new SqlDataAdapter(comm);
                dtp.Fill(dataTable);
                conn.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="campaignSeuDesejo"></param>
        /// <returns></returns>
        public async Task<int> ProcessBulkCopyAsync(Arquivo file, CampanhaFase campaignSeuDesejo)
        {
            await DropTempTableAsync();
            int result;

            using (var dataTable = new DataTable())
            {
                await CreateTempTableAsync(dataTable);
                _logger.LogInformation("Read file and send records.");
                result = await ProcessFileAsync(file, dataTable);
            }

            _logger.LogInformation("Merge User Status.");
            await _usuarioStatusFaseRepository.FixUserStatusAsync(campaignSeuDesejo.CampanhaId);
            await DropTempTableAsync();

            return result;
        }

        private async Task<int> ProcessFileAsync(Arquivo file, DataTable dataTable)
        {
            var elapsed = new Stopwatch();
            elapsed.Start();
            var iLine = 0;
            var batchsize = 0;
            var rows = 0;
            using (var bk = new SqlBulkCopy(_connectionStrings.DotzApp)
            {
                DestinationTableName = "BB_TEMP_USUARIO_STATUS",
                BulkCopyTimeout = 0,
                BatchSize = 100000
            })
            using (var stream = new StreamReader(file.Nome))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    iLine++;
                    if (iLine == 1) continue;

                    var values = line.Split(';');

                    var dr = dataTable.NewRow();

                    // Sem validação
                    var cpf = values[0];
                    var periodo = values[1];
                    var meta = decimal.Parse(values[2]);
                    var faixaMeta = values[3];
                    var gasto = decimal.Parse(values[4]);
                    var gastoPerc = values[5];
                    var desafio1 = values[6] == "1" || values[6] == "S";
                    var desafio2 = values[7] == "1" || values[7] == "S"; ;
                    var desafio3 = values[8] == "1" || values[8] == "S"; ;
                    var desafio4 = values[9] == "1" || values[9] == "S"; ;
                    var desafio5 = values[10] == "1" || values[10] == "S"; ;
                    var desafio6 = values[11] == "1" || values[11] == "S"; ;
                    var desafio7 = values[12] == "1" || values[12] == "S"; ;
                    var lampada = string.Equals(values[13], "Null", StringComparison.CurrentCultureIgnoreCase) ? (object)DBNull.Value : values[13];
                    //var lampada = values[12] == "Null" ? (object)DBNull.Value : values[12];

                    //var columns = dataTable.Columns;

                    dr["PERIODO"] = new DateTime(int.Parse(periodo.Substring(0, 4)),
                        int.Parse(periodo.Substring(5, 2)), 1);

                    dr["CPR_ARQUIVO_ID"] = file.Id;
                    dr["CPR_USUARIO_CPF"] = cpf;

                    dr["META"] = meta;
                    dr["FAIXA_META"] = faixaMeta;
                    dr["GASTO"] = gasto;
                    dr["GASTO_PERCENTUAL"] = gastoPerc;
                    dr["DESAFIO_1"] = desafio1;
                    dr["DESAFIO_2"] = desafio2;
                    dr["DESAFIO_3"] = desafio3;
                    dr["DESAFIO_4"] = desafio4;
                    dr["DESAFIO_5"] = desafio5;
                    dr["DESAFIO_6"] = desafio6;
                    dr["DESAFIO_7"] = desafio7;
                    //dr["DESAFIO_7"] = false;
                    dr["CATALOGO"] = lampada;

                    /*
                    // Com validação
                    var model = new UsuarioStatusArquivo();

                    model.Read(values);

                    if (!model.Valido)
                    {
                        continue;
                    }
                    dr = model.ToDataRow(file.Id, dr);
                    */
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


        public async Task<int> ProcessBulkCopyAsync(Arquivo file, Campanha campanha)
        {
            var elapsed = new Stopwatch();
            elapsed.Start();
            var iLine = 0;

            using (var stream = new StreamReader(file.Nome))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    iLine++;
                    if (iLine == 1) continue;

                    var values = line.Split(';');

                    var cpf = StringExtensions.IsCpf(values[0].ToString());
                    if (cpf)
                    {
                        var usuario = await GetUserByCampaingAsync(campanha.CampanhaId, values[0]);
                        if (usuario.UsuarioId != 0)
                        {
                            var usuarioStatus = GetIdUserByCampaing(usuario.UsuarioId);
                            if (!usuarioStatus)
                            {
                                var usuariostatus = new UsuarioStatus { UsuarioId = usuario.UsuarioId, Trocar = true};
                                await _usuarioStatus.InsertAsync(usuariostatus);
                            }
                        }
                    }
                }

                elapsed.Stop();
                _logger.LogInformation($"{iLine} records imported in {elapsed.Elapsed:hh\\:mm\\:ss} seconds.");
                return iLine;
            }
        }

        private void ProcessFileAsync(Arquivo file)
        {
            throw new NotImplementedException();
        }

        private async Task<Usuario> GetUserByCampaingAsync(int campanhaId, string cpf)
        {
            var usuario = await _usuarioRepository.GetByCampaignAndDocumentAsync(campanhaId, cpf);
            if (usuario == null)
            {
                _logger.LogError($"Usuário não encontrado para campanha: CPF: [{cpf}] - Campanha: [{campanhaId}]");
            }
            return usuario;
        }

        private bool GetIdUserByCampaing(int userId)
        {
            var usuariostatus = _usuarioStatus.GetByUserIdStatusActiveAsync(userId);
            if (usuariostatus.Result == null)
                return false;
            return true;
        }
    }
}
