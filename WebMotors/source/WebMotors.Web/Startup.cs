using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebMotors.Application;
using WebMotors.Core.Application;
using WebMotors.Core.Repositorios;
using WebMotors.Core.Servicos;
using WebMotors.Infra.Contexto;
using WebMotors.Infra.Repositorios;
using WebMotors.Infra.Servicos;

namespace WebMotors.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddTransient<IAnuncioApplication, AnuncioApplication>();

            services.AddDbContext<WebMotorsContexto>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("WebMotorsContextoConnection")));

            services.AddHttpClient<IMarcaServico, MarcaServico>();
            services.AddHttpClient<IModeloServico, ModeloServico>();
            services.AddHttpClient<IVersaoServico, VersaoServico>();

            services.AddScoped<WebMotorsContexto>();
            services.AddScoped<IAnuncioRepository, AnuncioRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
