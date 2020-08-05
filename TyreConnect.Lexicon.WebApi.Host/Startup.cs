// <copyright file="Startup.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TyreConnect.Lexicon.ApplicationCore.Extensions;

namespace TyreConnect.Lexicon.WebApi
{
    public class Startup
    {
        private readonly string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddCors(options =>
              {
                  options.AddPolicy(
                      name: myAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins(
                              "http://localhost:4200",
                              "https://app-tyreconnect-lexicon-prod-002.azurewebsites.net")
                              .AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                      });
              });

            services.AddControllers();

            // Add Lexicon ApplicationCore Services
            services.AddLexiconApplicationCoreServices();
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

            app.UseCors(myAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
