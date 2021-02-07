using System;
using System.Text.Json;
using BigDataAssigment.Api.ApiClients;
using BigDataAssigment.Api.Configuration;
using BigDataAssigment.Api.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using BigDataAssigment.Api.Repositories;
using BigDataAssigment.Api.Caching;
using BigDataAssigment.Api.SignalR;

namespace BigDataAssigment.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo {
                         Title = "BigDataAssigment.Api",
                         Version = "v1" ,
                         Description="BigDataTechnology Developer Assigment ",
                         Contact= new OpenApiContact {
                             Name= "BigData Technology",
                             Url= new Uri("https://bigdatateknoloji.com")
                         }
                     });
            });
            services.AddLogging(opt=> {
                opt.AddConsole();
            });

            services.AddMvcCore().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; 
            });

            services.AddDbContext<ForecastDbContext>(opt=> opt.UseSqlServer(Configuration.GetConnectionString("ForecastDBConnection"),
                                                                    sqlserverOptions=> { sqlserverOptions.EnableRetryOnFailure(); }));
             
            services.AddResponseCaching(options=> {

                options.UseCaseSensitivePaths = true;
                options.MaximumBodySize = 1024;

            });

            services.AddSignalR();

            services.AddSingleton<AppMemoryCache>();
            services.AddSingleton<IConfigSettings, ConfigSettings>();
            services.AddSingleton<ForecastHub>();

            services.AddScoped<IMemoryCacheService, MemoryCacheService>();
            services.AddScoped<ILocationIQApiWrapper, LocationIQApiWrapper>();
            services.AddScoped<IDarkSkyApiWrapper, DarkSkyApiWrapper>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IForecastRepository, ForecastRepository>();
            services.AddScoped(_ => AutoMapperConfiguration.GetMapper());
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BigDataAssigment.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ForecastHub>("/forecasthub");
            });
            app.UseResponseCaching();


        }

        
    }
}
