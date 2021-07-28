using MediatR;
using MFoundation.Core.Aggregates;
using MFoundation.Core.Common.Serializer;
using MFoundation.Core.Messaging.Commands;
using MFoundation.Core.Messaging.Events;
using MFoundation.Core.Messaging.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TestDemo
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddDependecy(this IServiceCollection services)
        {
            services.AddDomainCoreDependecy();

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(CreateModel).GetTypeInfo().Assembly);
            services.AddCustomHealthCheck();

            return services;
        }

        private static IServiceCollection AddDomainCoreDependecy(this IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();
            //  services.AddScoped<IDomainRepository, DomainRepository>();
            //  services.AddScoped<IDomainEventSerializer, JsonDomainEventSerializer>();
            return services;
        }

        private static IServiceCollection AddCustomHealthCheck(this IServiceCollection services)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }
    }
}
