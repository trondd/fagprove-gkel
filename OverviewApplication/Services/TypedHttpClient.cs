using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OverviewApplication.Services
{
    public class TypedHttpClient : ITypedHttpClient
    {
        private readonly HttpClient _client;

        public TypedHttpClient(HttpClient client)
        {
            //client.BaseAddress = new Uri("https://localhost:44351/");
            _client = client;
        }
        public HttpClient GetClient()
        {
            return _client;
        }
    }
}
