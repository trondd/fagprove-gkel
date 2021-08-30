using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TestApplication.Diagnostics
{
    public class RandomHealthResult : IHealthCheck
    {
        private readonly Random _random = new Random();

        public int RandomNumber()
        {
            return _random.Next(1, 4);
        }

        public HealthCheckResult RandomHealthCheckResult()
        {
            var rInt = RandomNumber();

            switch (rInt)
            {
                case 1:
                    return HealthCheckResult.Healthy("HealthCheck is ok!");
                case 2:
                    return HealthCheckResult.Degraded("HealthCheck Is Degraded, check logs for more information");
                case 3:
                    return HealthCheckResult.Unhealthy("Service unavailable");
                default:
                    return HealthCheckResult.Unhealthy("HealthCheck Is degraded, couldn't parse random number gen");
            }
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                return RandomHealthCheckResult();
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy("Failed getting a random result", e);
            }
        }

    }
}
