using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using AutoMapper;
using CicloVidaAltoValor.Application.Contracts.CepTotal;
using CicloVidaAltoValor.Application.Contracts.Desejos;
using CicloVidaAltoValor.Application.Contracts.Login;
using CicloVidaAltoValor.Application.Contracts.Usuario;
using CicloVidaAltoValor.Application.Interfaces.Model;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Interfaces.Services;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Model.ValueObject;
using CicloVidaAltoValor.Application.ProfileMappings;
using CicloVidaAltoValor.Application.Repository.Contexts;
using CicloVidaAltoValor.Application.Repository.Mappings;
using CicloVidaAltoValor.Application.Repository.Repositories;
using CicloVidaAltoValor.Application.Services;
using CicloVidaAltoValor.Application.Settings;
using CicloVidaAltoValor.Application.Validators;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dharma.Common.Results;
using Dharma.Repository.SQL;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CicloVidaAltoValor.Application
{
    public static class CicloAltoValorConfiguration

    {
        /// <summary>
        /// Web
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServicesWeb(this IServiceCollection services, IConfiguration configuration)
        {

            Common(services, configuration);

            // Context
            services.AddScoped<IConnectionContext, ConnectionContext>();
            var connStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

            services.AddScoped(typeof(DotzAppContext), x => new DotzAppContext(new SqlConnection(connStrings.DotzApp)));
            services.AddScoped(typeof(DotzSystemContext), x => new DotzSystemContext(new SqlConnection(connStrings.DotzSystem)));
            services.AddScoped(typeof(DotzCepContext), x => new DotzCepContext(new SqlConnection(connStrings.DotzCep)));


            // Repositories
            services.AddScoped<IProdutoCampanhaRepository, ProdutoCampanhaRepository>();
            services.AddScoped<ICampanhaRepository, CampanhaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICampanhaProdutoRepository, CampanhaProdutoRepository>();
            services.AddScoped<ICampanhaTipoRepository, CampanhaTipoRepository>();
            services.AddScoped<IUsuarioStatusFaseRepository, UsuarioStatusFaseRepository>();
            services.AddScoped<IArquivoRepository, ArquivoRepository>();
            services.AddScoped<IAplicacaoParametroRepository, AplicacaoParametroRepository>();
            services.AddScoped<IAplicacaoRepository, AplicacaoRepository>();
            services.AddScoped<ICampanhaUsuarioAtualizacaoCadastroRepository, CampanhaUsuarioAtualizacaoCadastroRepository>();
            services.AddScoped<ICampanhaFaseRepository, CampanhaFaseRepository>();
            services.AddScoped<ICampanhaFaseUsuarioAcessoRepository, CampanhaFaseUsuarioAcessoRepository>();
            services.AddScoped<IUsuarioComplementoRepository, UsuarioComplementoRepository>();
            services.AddScoped<ICampanhaProdutoFaseRepository, CampanhaProdutoFaseRepository>();
            services.AddScoped<IUsuarioPremioFaseRepository, UsuarioPremioFaseRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoEstadoRepository, ProdutoEstadoRepository>();
            services.AddScoped<IUsuarioPremioRepository, UsuarioPremioRepository>();
            services.AddScoped<ICampanhaPrevisaoFaturaRepository, CampanhaPrevisaoFaturaRepository>();
            services.AddScoped<ICepTotalRepository, CepTotalRepository>();
            services.AddScoped<IUsuarioAcessoRepository, UsuarioAcessoRepository>();
            services.AddScoped<IUsuarioStatusRepository, UsuarioStatusRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileProductService, FileProductService>();
            services.AddScoped<IFileUserService, FileUserService>();
            services.AddScoped<IFileUserStatusService, FileUserStatusService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICampaignProductFaseService, CampaignProductFaseService>();
            services.AddScoped<ICepTotalService, CepTotalService>();
            services.AddScoped<ICampaignService, CampaignService>();



            //Admin
            services.AddScoped<JwtSecurityTokenHandler>();

            services.AddScoped<IDataSerializer<AuthenticationTicket>, TicketSerializer>();
        }


        /// <summary>
        ///  Wokers and tests
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Common(configuration);

            // Context
            services.AddTransient<IConnectionContext, ConnectionContext>();
            var connStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

            services.AddTransient(typeof(DotzAppContext), x => new DotzAppContext(new SqlConnection(connStrings.DotzApp)));
            services.AddTransient(typeof(DotzSystemContext), x => new DotzSystemContext(new SqlConnection(connStrings.DotzSystem)));
            services.AddTransient(typeof(DotzCepContext), x => new DotzCepContext(new SqlConnection(connStrings.DotzCep)));

            //Repositories
            services.AddTransient<IProdutoCampanhaRepository, ProdutoCampanhaRepository>();
            services.AddTransient<ICampanhaRepository, CampanhaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ICampanhaProdutoRepository, CampanhaProdutoRepository>();
            services.AddTransient<ICampanhaTipoRepository, CampanhaTipoRepository>();
            services.AddTransient<IUsuarioStatusFaseRepository, UsuarioStatusFaseRepository>();
            services.AddTransient<IArquivoRepository, ArquivoRepository>();
            services.AddTransient<IAplicacaoParametroRepository, AplicacaoParametroRepository>();
            services.AddTransient<IAplicacaoRepository, AplicacaoRepository>();
            services.AddTransient<ICampanhaUsuarioAtualizacaoCadastroRepository, CampanhaUsuarioAtualizacaoCadastroRepository>();
            services.AddTransient<ICampanhaFaseRepository, CampanhaFaseRepository>();
            services.AddTransient<ICampanhaFaseUsuarioAcessoRepository, CampanhaFaseUsuarioAcessoRepository>();
            services.AddTransient<IUsuarioComplementoRepository, UsuarioComplementoRepository>();
            services.AddTransient<ICampanhaProdutoFaseRepository, CampanhaProdutoFaseRepository>();
            services.AddTransient<IUsuarioPremioFaseRepository, UsuarioPremioFaseRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IProdutoEstadoRepository, ProdutoEstadoRepository>();
            services.AddTransient<IUsuarioPremioRepository, UsuarioPremioRepository>();
            services.AddTransient<ICampanhaPrevisaoFaturaRepository, CampanhaPrevisaoFaturaRepository>();
            services.AddTransient<ICepTotalRepository, CepTotalRepository>();
            services.AddTransient<IUsuarioAcessoRepository, UsuarioAcessoRepository>();
            services.AddTransient<IUsuarioStatusRepository, UsuarioStatusRepository>();


            // Services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IFileProductService, FileProductService>();
            services.AddTransient<IFileUserService, FileUserService>();
            services.AddTransient<IFileUserStatusService, FileUserStatusService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICampaignProductFaseService, CampaignProductFaseService>();
            services.AddTransient<ICepTotalService, CepTotalService>();
            services.AddTransient<ICampaignService, CampaignService>();





        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void Common(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddOptions();
            services.AddLogging(x =>
            {
                x.AddFile(configuration.GetSection("Logging"));
            });

            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.Configure<PathFilesSettings>(configuration.GetSection(nameof(PathFilesSettings)));
            services.Configure<CampaignSettings>(configuration.GetSection(nameof(CampaignSettings)));
            services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
            
            


            InitalizeMappers();

            services.AddSingleton(Mapper.Instance);


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, UsuarioSeuDesejo>();


            // Cache
            //services.AddMemoryCache();
            //services.AddScoped<CacheInterceptor>();
            //services.AddSingleton<ICacheManager, RuntimeCache>();

            services.AddScoped(_ => new ErrorBuilder(typeof(Properties.Resources)));

            //Validator
            services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
            services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();
            services.AddTransient<IValidator<UsuarioViewModel>, UsuarioViewModelValidator>();
            services.AddTransient<IValidator<Usuario>, UsuarioValidator>();
            services.AddTransient<IValidator<DesejoProdutoViewModel>, DesejoProdutoViewModelValidator>();
            services.AddTransient<IValidator<LoginAdminViewModel>, LoginAdminViewModelValidator>();
            services.AddTransient<IValidator<CepTotalRequest>, CepTotalRequestValidator>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureRedisKeyStore(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            var redisSettings = configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();

            if (redisSettings.Use)
            {
                var redis = ConnectionMultiplexer.Connect($"{redisSettings.Host}:{redisSettings.Port}");

                services.AddDataProtection(options =>
                {
                    options.ApplicationDiscriminator = appSettings.ApplicationName;

                })
                .SetApplicationName(appSettings.ApplicationName)
                .SetDefaultKeyLifetime(TimeSpan.FromDays(365 * 2))
                .PersistKeysToRedis(redis, $"{appSettings.ApplicationName}-DataProtection-Keys");
            }
            else
            {
                services.AddDataProtection(options =>
                {
                    options.ApplicationDiscriminator = appSettings.ApplicationName;

                })
                .SetDefaultKeyLifetime(TimeSpan.FromDays(365 * 2))
                .SetApplicationName(appSettings.ApplicationName);
                //.PersistKeysToFileSystem(new DirectoryInfo(@""));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private static void InitalizeMappers()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMissingTypeMaps = true;
                cfg.EnableNullPropagationForQueryMapping = true;

                cfg.AddProfile<ProdutoProfile>();
                cfg.AddProfile<UsuarioProfile>();
                cfg.AddProfile<UsuarioStatusFaseProfile>();

                cfg.AddProfiles(Assembly.GetEntryAssembly());
            });

            FluentMapper.Initialize(cfg =>
            {
                cfg.AddMap(new ProdutoEstadoMap());
                cfg.AddMap(new UsuarioPremioFaseMap());
                cfg.AddMap(new CampanhaProdutoFaseMap());
                cfg.AddMap(new ProdutoMap());
                cfg.AddMap(new UsuarioComplementoMap());
                cfg.AddMap(new CampanhaFaseUsuarioAcessoMap());
                cfg.AddMap(new CampanhaUsuarioAtualizacaoCadastroMap());
                cfg.AddMap(new CampanhaFaseMap());
                cfg.AddMap(new UsuarioStatusFaseMap());
                cfg.AddMap(new ArquivoMap());
                cfg.AddMap(new UsuarioExtratoCupomMap());
                cfg.AddMap(new UsuarioExtratoMap());
                cfg.AddMap(new UsuarioPremioHistoricoStatusMap());
                cfg.AddMap(new UsuarioPremioStatusMap());
                cfg.AddMap(new UsuarioPremioMap());
                cfg.AddMap(new CampanhaPrevisaoFaturaMap());
                cfg.AddMap(new CampanhaTipoMap());
                cfg.AddMap(new CampanhaProdutoMap());
                cfg.AddMap(new UsuarioMap());
                cfg.AddMap(new CampanhaMap());
                cfg.AddMap(new AplicacaoMap());
                cfg.AddMap(new AplicacaoParametroMap());
                cfg.AddMap(new ProdutoFornecedorMap());
                cfg.AddMap(new ProdutoDetalheMap());
                cfg.AddMap(new CepTotalMap());
                cfg.AddMap(new UsuarioAcessoMap());
                cfg.AddMap(new ProdutoCampanhaMap());
                cfg.AddMap(new UsuarioStatusMap());

                cfg.ForDommel();
            });

            Dapper.SqlMapper.AddTypeMap(typeof(string), System.Data.DbType.AnsiString);
        }
    }
}
