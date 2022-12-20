using meuprojeto_web.Extensions;
using meuprojeto_web.Helpers;
using meuprojeto_web.Proxies;
using meuprojeto_web.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace meuprojeto_web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            Environment = environment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var environmentUtil = new EnvironmentUtil(Environment.EnvironmentName);

            services
               .AddHttpContextAccessor()
               .AddHttpClient();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient(typeof(DatatablesHelper));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddTransient(x => new MinhaApiProxy(environmentUtil.ObterAmbiente().ObterSigla(), x.GetService<IHttpClientFactory>()));

            services.AddSession();

            services.AddMvc();

            var builder = services.AddControllersWithViews();

            if (environmentUtil.ObterAmbiente() != TipoAmbiente.Producao)
            {
                // Posibilita que ao atualizar um arquivo *.cshtml, a alteração seja refletida sem que seja necessário recompilar o projeto(https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-compilation?view=aspnetcore-3.1)
                builder.AddRazorRuntimeCompilation();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Define o middleware para interceptar exceptions não tratadas
            app.UseExceptionHandler($"/feedback/{(int)HttpStatusCode.InternalServerError}");

            // Customiza as páginas de erro
            app.UseStatusCodePagesWithReExecute("/feedback/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
