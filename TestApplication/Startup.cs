using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TestApplication.Diagnostics;

namespace TestApplication
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
            services.AddControllersWithViews();
            services.AddHealthChecksBuilder();
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
                endpoints.MapHealthChecks("/service1", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("Service 1"),
                    ResponseWriter = JsonWriter.WriteResponse
                });
                endpoints.MapHealthChecks("/service2", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("Service 2"),
                    ResponseWriter = JsonWriter.WriteResponse
                });
                endpoints.MapHealthChecks("/service3", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("Service 3"),
                    ResponseWriter = JsonWriter.WriteResponse
                });

            });
        }
    }
}
