using TestDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Infrastructure
{
    public static class EFInstaller
    {
        public static IServiceCollection AddEFConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestDemoContext>(options =>
            {

                options.UseSqlServer(configuration.GetConnectionString("defaultConnectString"));

            });

            services.AddScoped<ITestDemoModelRepository, TestDemoModelRepository>();
            services.AddScoped<IReadTestDemoModelRepository, TestDemoModelRepository>();
            return services;
        }
    }
}
