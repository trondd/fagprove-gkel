using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TestApplication.Diagnostics
{
    public static class HealthCheckBuilderService
    {
        public static IHealthChecksBuilder AddHealthChecksBuilder(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();

            //Add dummy health checks with random results for 3 different services
            for (int i = 0; i < 5; i++)
            {
                var hResult = new RandomHealthResult().RandomHealthCheckResult();
                var name = "Service1.HealthCheck";
                name += i.ToString();
                builder.AddCheck(name, () => hResult , tags:new []{"Service 1"});
            }

            for (int i = 0; i < 5; i++)
            {
                var hResult = new RandomHealthResult().RandomHealthCheckResult();
                var name = "Service2.HealthCheck";
                name += i.ToString();
                builder.AddCheck(name, () => hResult, tags: new[] { "Service 2" });
            }

            for (int i = 0; i < 5; i++)
            {
                var hResult = new RandomHealthResult().RandomHealthCheckResult();
                var name = "Service3.HealthCheck";
                name += i.ToString();
                builder.AddCheck(name, () => hResult, tags: new[] { "Service 3" });
            }

            return builder;
        }
    }
}
