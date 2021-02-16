using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestPackages.Models;
using Microsoft.Extensions.Options;


namespace QuestPackages
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            // Adding the settings 
            services.Configure<HttpRequestSettings>(Configuration.GetSection(nameof(HttpRequestSettings)));
            services.AddSingleton<IHttpRequestSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<HttpRequestSettings>>().Value);


            services.Configure<PackageDatabaseSettings>(Configuration.GetSection(nameof(PackageDatabaseSettings)));
            services.AddSingleton<IPackageDatabaseSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<PackageDatabaseSettings>>().Value);



            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
