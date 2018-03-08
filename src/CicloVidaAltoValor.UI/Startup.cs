using System;
using System.IO.Compression;
using System.Threading;
using CicloVidaAltoValor.Application;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CicloVidaAltoValor.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddDetection()
                .AddDevice();

            services.AddMvc()
                .AddFluentValidation()
                .AddJsonOptions(x =>
                {
                    x.SerializerSettings.Formatting = Formatting.Indented;
                    x.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    x.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    x.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    x.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.ConfigureServicesWeb(Configuration);

            services.ConfigureRedisKeyStore(Configuration);

            services.AddAntiforgery();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = CookieAuthenticationDefaults.CookiePrefix + $"CicloVidaAltoValorBBCookie";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                options.Cookie.Expiration = TimeSpan.FromMinutes(50);
                options.LoginPath = new PathString("/login");
                options.LogoutPath = new PathString("/logout");
                options.ReturnUrlParameter = "returnUrl";
            });


            const string culture = "pt-BR";
            var cultureBr = new System.Globalization.CultureInfo(culture);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureBr;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureBr;
            Thread.CurrentThread.CurrentCulture = cultureBr;
            Thread.CurrentThread.CurrentUICulture = cultureBr;

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseResponseCompression();

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            // app.UseRewriter(new RewriteOptions().AddRedirectToHttps());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");


            });
        }
    }
}
