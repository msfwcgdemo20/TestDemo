using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using TestDemo.Filters;
using TestDemo.Middlewares;
using TestDemo.Infrastructure;


namespace TestDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = GetElasticLogger();
    }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CrossOrigin",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                    );
            });

            // Add EF Configuration
            services.AddEFConfiguration(Configuration);

            services.AddControllers(
               // Global Exception Filter
               options => options.Filters.Add(typeof(HttpGlobalExceptionFilter))
           ).AddNewtonsoftJson(
               joptions =>
               {
                   joptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                   joptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   joptions.SerializerSettings.Converters.Add(new StringEnumConverter());
               }
           );

            services.AddDependecy();

            DoSwaggerRegister(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/health");

            app.UseCors("CrossOrigin");

            app.UseCorrelationId();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Swagger API");
            });
            
        }
        private void DoSwaggerRegister(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // ReSharper disable once RedundantNameQTestDemoifier
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "1.0.0.0",
                    Title = "Project21July1 App - TestDemo Service API",
                    Description = "Microservice Framework Demo Project21July1 App - TestDemo Service."
                });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

    private Serilog.ILogger GetElasticLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
           .Enrich.WithProperty("Application", "TestDemo")
           .Enrich.FromLogContext()
           .Enrich.WithExceptionDetails()
           .Enrich.WithMachineName()
           .WriteTo.Console()
           .WriteTo.Debug()
           .Enrich.WithProperty("Environment", "Development")
           .WriteTo.Elasticsearch(ConfigureElasticSink())
           .CreateLogger();
    }

    private ElasticsearchSinkOptions ConfigureElasticSink()
    {
        return new ElasticsearchSinkOptions(new Uri("http://msfwdemoelk.southeastasia.cloudapp.azure.com:9200"))
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
            IndexFormat = $"{"msfwdemo"}-{"dev"}"
        };
    }
}
}

