using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OverviewApplication.Models;

namespace OverviewApplication.Services
{
    public class Repository : IRepository
    {
        private readonly ITypedHttpClient _client;

        public Repository(ITypedHttpClient client)
        {
            _client = client;
        }

        //Creates a new list of all the health results from all services listed in the endpoints list
        public async Task<List<HealthResultContent>> GetAll(List<Endpoint> endpoints)
        {
            var list = new List<HealthResultContent>();

            var httpClient = _client.GetClient();

            foreach (var endpoint in endpoints)
            {
                var response = await httpClient.GetAsync(endpoint.Uri);
                var resultJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<HealthResultContent>(resultJson);
                result.service = endpoint.Name;
                result.HealthEndpoint = endpoint.Uri;

                result.Id = endpoint.Id;

                list.Add(result);
            }
            
            return list;
        }
    }
}
