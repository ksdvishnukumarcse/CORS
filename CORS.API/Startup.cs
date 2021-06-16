using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace CORS.API
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
            services.AddCors(options =>
            {
                options.AddPolicy(PolicyConstants.GetVerbsOnly, corsBuilder =>
                 {
                     corsBuilder.WithMethods(new string[] { "GET" });
                 });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(PolicyConstants.SpecificHeader, corsBuilder =>
                {
                    corsBuilder.WithHeaders(new string[] { "my-custom-header" });
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(PolicyConstants.AllowAll, corsBuilder =>
                {
                    corsBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors(PolicyConstants.GetVerbsOnly);
            //app.UseCors(PolicyConstants.SpecificHeader);
            //app.UseCors(PolicyConstants.AllowAll);
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
