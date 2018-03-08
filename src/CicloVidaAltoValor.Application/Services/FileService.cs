using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Interfaces.Model;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Interfaces.Services;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Model.ValueObject;
using CicloVidaAltoValor.Application.Settings;

namespace CicloVidaAltoValor.Application.Services
{
    public class FileService : IFileService
    {

        private readonly ILogger<FileService> _logger;
        private readonly IFileUserStatusService _fileUserStatusService;
        private readonly IFileProductService _fileProductService;
        private readonly IFileUserService _fileUserService;
        private readonly ICampanhaFaseRepository _campanhaFaseRepository;
        private readonly IArquivoRepository _arquivoRepository;
        private readonly CampaignSettings _campaignSettings;
        private readonly PathFilesSettings _pathFilesSettings;
        private readonly ICampanhaRepository _campanhaRepository;


        public FileService(
            ILogger<FileService> logger,
            IFileUserStatusService fileUserStatusService,
            IFileProductService fileProductService,
            ICampanhaFaseRepository campanhaFaseRepository,
            ICampanhaRepository campanhaRepository,
            IFileUserService fileUserService,
            IArquivoRepository arquivoRepository,
            IOptions<CampaignSettings> options,
            IOptions<PathFilesSettings> optionsFile)
        {
            _logger = logger;
            _fileUserStatusService = fileUserStatusService;
            _fileProductService = fileProductService;
            _fileUserService = fileUserService;
            _campanhaFaseRepository = campanhaFaseRepository;
            _campanhaRepository = campanhaRepository;
            _arquivoRepository = arquivoRepository;
            _campaignSettings = options.Value;
            _pathFilesSettings = optionsFile.Value;
            
        }


        /// <summary>
        /// Processa arquivo cvs para o banco de dados.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Process()
        {
            if (!CheckDirectories())
            {
                return false;
            }

            var files = GetFiles(_pathFilesSettings.ToProccess).ToList();

            var campanhaAtual = await _campanhaRepository.GetCurrentAsync(_campaignSettings.Name);

            if (campanhaAtual == null)
            {
                _logger.LogInformation($"Your wish campaign not current.");
                return false;
            }         


            foreach (var file in files)
            {

                var swatch = new Stopwatch();

                swatch.Start();

                _logger.LogInformation($"Processing to file: {file.Nome} init.");
                
                var valid = await ProcessFile(file, campanhaAtual);

                if (!valid)
                {
                    Directory.Move(file.Nome, Path.Combine(_pathFilesSettings.Reject, file.NomeProcessado));
                }
                else
                {
                    Directory.Move(file.Nome, Path.Combine(_pathFilesSettings.Proccessed, file.NomeProcessado));
                }

                _logger.LogInformation($"Processing to file: {file.NomeProcessado} finished.");

                swatch.Stop();

                _logger.LogInformation($"Process file: {file.NomeProcessado} end at: {swatch.Elapsed:hh\\:mm\\:ss}.");

            }

            // File 500
            //await _bonusService.BonusAccess(campanhaFaseAtual.CampanhaId);

            if (!files.Any())
            {
                _logger.LogInformation($"file(s) not found.");
                return true;
            }

            //await _bonusService.BonusUserRegisterUpdate(campaignSeuDesejo.CampanhaId);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="campanhaFaseAtual"></param>
        /// <returns></returns>
        //private async Task<bool> ProcessFile(Arquivo file, CampanhaFase campanhaFaseAtual)
        private async Task<bool> ProcessFile(Arquivo file, Campanha campanhaFaseAtual)
        {
            try
            {
                var contentsFile = new List<IArquivo>();
                
                switch (file.GetTypeFileEnum())
                {
                    case TipoArquivoSeuDesejo.UsuarioStatus:
                        file.DataInicioProcessamento = DateTime.Now;
                        file = await _arquivoRepository.InsertAsync(file);
                        file.QtdeValidos = await _fileUserStatusService.ProcessBulkCopyAsync(file, campanhaFaseAtual);
                        break;
                    case TipoArquivoSeuDesejo.Cadastro:
                        file.DataInicioProcessamento = DateTime.Now;
                        file = await _arquivoRepository.InsertAsync(file);
                        await _fileUserService.ProcessBulkCopyAsync(file, campanhaFaseAtual);
                        break;
                    default:
                        var validFile = await ReadFile(contentsFile, file);

                        if (!validFile)
                        {
                            _logger.LogInformation($"Invalid file. [{file.Nome}]-[{file.NomeProcessado}]");
                            return false;
                        }

                        file.DataInicioProcessamento = DateTime.Now;
                        file = await _arquivoRepository.InsertAsync(file);
                        break;
                }
                
                switch (file.GetTypeFileEnum())
                {
                    //case TipoArquivoSeuDesejo.Cadastro:
                    //    await _fileUserService.ProcessAsync(contentsFile.Cast<UsuarioArquivo>(), file, campanhaFaseAtual);
                    //    break;

                    //case TipoArquivoSeuDesejo.UsuarioStatus:
                    //    await _fileUserStatusService.ProcessAsync(contentsFile.Cast<UsuarioStatusArquivo>(), file, campanhaFaseAtual);
                    //    break;

                    //case TipoArquivoSeuDesejo.Produto:

                    //    await _fileProductService.ProcessAsync(contentsFile.Cast<ProdutoArquivo>(), file, campanhaFaseAtual);
                    //    break;
                }


                file.DataFimProcessamento = DateTime.Now;
                file.QtdeRejeitados = contentsFile.Count(x => !string.IsNullOrEmpty(x.Erro) && !x.Valido);
                if (file.QtdeValidos == 0)
                {
                    file.QtdeValidos = contentsFile.Count(x => x.Valido);
                }

                await _arquivoRepository.UpdateAsync(file);

                _logger.LogInformation($"File: {file.Nome} id: {file.Id}  valids: {file.QtdeValidos} - errors: {file.QtdeRejeitados}");
                _logger.LogInformation("Finished.");

                return true;
            }
            catch (Exception ex)
            {
                if (file.Id > 0)
                {
                    _logger.LogError(ex, $"{file.Id}  {ex.Message}");
                    file.Erro = ex.Message;
                    await _arquivoRepository.UpdateAsync(file);
                }
                else
                {
                    _logger.LogError(ex, $"{file.Nome} {ex.Message}");
                }
                _logger.LogInformation("Finished with error.");
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentsFile"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> ReadFile(ICollection<IArquivo> contentsFile, Arquivo file)
        {
            try
            {

                const char delimiter = ';';

                var columns = new Dictionary<string, int>();
                var errors = new List<string>();

                var prototype = FactoryArquivoSeuDesejo(file.GetTypeFileEnum());

                //int rows = 0;

                using (var sr = new StreamReader(File.Open(file.Nome, FileMode.Open), Encoding.UTF7))//"iso-8859-1"
                //using (var sr = new StreamReader(File.Open(file.Nome, FileMode.Open), Encoding.GetEncoding("utf-8")))//"iso-8859-1"
                //using (var sr = new StreamReader(File.Open(file.Nome, FileMode.Open), Encoding.GetEncoding("iso-8859-1")))
                {

                    while (!sr.EndOfStream)
                    {
                        var line = await sr.ReadLineAsync();


                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        var values = line.Split(delimiter);

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

                            line += delimiter + "Erros";
                            errors.Add(line);
                            continue;
                        }

                        var item = prototype.Clone();


                        foreach (var column in columns)
                        {
                            if (values.Length <= column.Value)
                                continue;

                            var value = values[column.Value];

                            if (value == null)
                                continue;

                            line = item.Set(column.Key, value, line);
                        }

                        errors.Add(line);
                        //errors.Add(item.Read(values, line));
                        contentsFile.Add(item);
                    }
                }

                if (errors.Any())
                {
                    File.Delete(file.Nome);
                    await File.WriteAllLinesAsync(file.Nome, errors, Encoding.UTF8, CancellationToken.None);
                }

                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"{file.Nome} {ex.Message}");

                return false;

            }
        }

        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// </summary>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        public static Encoding GetFileEncoding(string srcFile)
        {
            // *** Use Default of Encoding.Default (Ansi CodePage)
            Encoding enc = Encoding.Default;

            // *** Detect byte order mark if any - otherwise assume default
            byte[] buffer = new byte[5];
            FileStream file = new FileStream(srcFile, FileMode.Open);
            file.Read(buffer, 0, 5);
            file.Close();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                enc = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                enc = Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                enc = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                enc = Encoding.UTF7;

            return enc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static Encoding GetEncoding(string filename)
        {
            using (var file = new StreamReader(filename, true))
            {
                file.Read();
                return file.CurrentEncoding;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckDirectories()
        {
            if (!CheckDirectory(_pathFilesSettings.ToProccess))
            {
                _logger.LogError($"Path: {_pathFilesSettings.ToProccess} invalid.");
                return false;
            }
            if (!CheckDirectory(_pathFilesSettings.Reject))
            {
                _logger.LogError($"Path: {_pathFilesSettings.ToProccess} invalid.");
                return false;
            }
            if (!CheckDirectory(_pathFilesSettings.Proccessed))
            {
                _logger.LogError($"Path: {_pathFilesSettings.ToProccess} invalid.");
                return false;
            }

            //if (!CheckDirectory(_pathFilesSettings.File500ToProcess))
            //{
            //    _logger.LogError($"Path: {_pathFilesSettings.File500ToProcess} invalid.");
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool CheckDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }

            _logger.LogInformation($"Directory [{path}] not exist");
            _logger.LogInformation($"Create directory [{path}]");

            try
            {
                Directory.CreateDirectory(path);
                _logger.LogInformation($"Directory [{path}] created with success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error on create directory [{path}]");
                return false;
            }

            return true;

        }

        /// <summary>
        ///  Busca os arquivos na pasta informada.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private IEnumerable<Arquivo> GetFiles(string path)
        {
            return Directory.EnumerateFiles(path).Where(x => CheckFileName(x))
                .Select(x => new Arquivo(x, GetTipoArquvoSeuDesejoEnum(x).GetValueOrDefault().GetHashCode())).OrderByDescending(x => x.TipoArquivo).ToList();

        }

        /// <summary>
        ///  Verifica se o nome do arquivo é válido.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool CheckFileName(string fileName)
        {
            return GetTipoArquvoSeuDesejoEnum(fileName) != null;
        }

        /// <summary>
        ///  Obtém o tipo de arquivo (enum) de acordo com a regra do nome do arquivo.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static TipoArquivoSeuDesejo? GetTipoArquvoSeuDesejoEnum(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            if (!new FileInfo(fileName).Extension.ToLower().Contains("csv"))
                return null;

            if (fileName.ToUpper().Contains("PRODUTO"))
            {
                return TipoArquivoSeuDesejo.Produto;
            }

            if (fileName.ToUpper().Contains("STATUS"))
            {
                return TipoArquivoSeuDesejo.UsuarioStatus;
            }
            if (fileName.ToUpper().Contains("CADASTRO"))
            {
                return TipoArquivoSeuDesejo.Cadastro;
            }

            return null;

        }

        /// <summary>
        ///  Obtém a instância do objeto de acordo com o tipo de arquivo.
        /// </summary>
        /// <param name="typeFile"></param>
        /// <returns></returns>
        private static IArquivo FactoryArquivoSeuDesejo(TipoArquivoSeuDesejo typeFile)
        {

            switch (typeFile)
            {
                case TipoArquivoSeuDesejo.Cadastro:
                    return new UsuarioArquivo();
                case TipoArquivoSeuDesejo.UsuarioStatus:
                    return new UsuarioStatusArquivo();
                case TipoArquivoSeuDesejo.Produto:
                    return new ProdutoArquivo();
                default:
                    return null;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
