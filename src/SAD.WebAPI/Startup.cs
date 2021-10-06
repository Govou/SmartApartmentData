using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;
using SAD.Infrastructure.DataAccess.Repository;
using SAD.Infrastructure.Services.IndexingServices;
using SAD.Infrastructure.Services.SearchServices;

namespace SAD.WebAPI
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

            var uri = new Uri(Configuration.GetValue<string>("AWSElasticSearchUrl"));
            var settings = new ConnectionSettings(uri)
                .BasicAuthentication(Configuration.GetValue<string>("AWSElasticSearchUsername"), Configuration.GetValue<string>("AWSElasticSearchPassword"));

            services.AddSingleton<IElasticClient>(new ElasticClient(settings));
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<IManagementRepository, ManagementRepository>();
            services.AddSingleton<IPropertiesRepository, PropertiesRepository>();
            services.AddScoped<IManagementIndexingService, ManagementIndexingService>();
            services.AddScoped<IPropertyIndexingService, PropertyIndexingService>();
            services.AddScoped<IManagementSearchService, ManagementSearchService>();
            services.AddScoped<IPropertySearchService, PropertySearchService>();

            services.AddSwaggerDocument(x =>
            {
                x.Title = "Smart Apartment Data API";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseOpenApi();

            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
