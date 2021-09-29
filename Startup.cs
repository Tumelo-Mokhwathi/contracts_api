using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using contracts_api.Infrastructure;
using contracts_api.Models;
using contracts_api.Services;
using contracts_api.Services.Interface;

namespace contracts_api
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
            services.AddControllers();
            InitialiseMvc(services);
            InitialiseServices(services);
            InitialseDatabase(services);
            ConfigureSWagger(services);
        }

        private void InitialiseServices(IServiceCollection services)
        {
            services.AddTransient<IContractsService, ContractsService>();
        }

        private void InitialseDatabase(IServiceCollection services)
        {
            services.AddDbContext<ContractsDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("ApplicationDb")));
        }

        private void InitialiseMvc(IServiceCollection services)
        {
           // services.AddControllers().AddNewtonsoftJson(options =>
           // options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           //);

            services
                .AddControllers(o => o.Conventions.Add(new ApiExplorerIgnores()))
                .AddNewtonsoftJson(
                    opts => opts
                    .SerializerSettings
                    .NullValueHandling = NullValueHandling.Ignore);
        }

        private void ConfigureSWagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                "v1",
                new OpenApiInfo { Title = "Contracts API", Version = "1" });
            });
        }
 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
