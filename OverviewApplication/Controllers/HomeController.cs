using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders.Testing;
using OverviewApplication.Models;
using OverviewApplication.Services;

namespace OverviewApplication.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;
        

        public HomeController(IRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        
        public static List<Endpoint> EndPoints { get; set; } = new List<Endpoint>();
        public List<Endpoint> GetEndpoints()
        {
            if (!EndPoints.Any())
            {
                var id = 0;
                var debug = _configuration.GetSection("Endpoints2").GetChildren();
                foreach (var i in debug)
                {
                    var endpoint = i.Get<Endpoint>();
                    endpoint.Id = id++;
                    EndPoints.Add(endpoint);
                }
                return EndPoints;
            }
            else
            {
                return EndPoints;
            }
        }

        public async Task<List<ServiceItem>> GetServiceItems()
        {
            var healthResults = await _repository.GetAll(GetEndpoints());
            var list = new List<ServiceItem>();

            foreach (var item in healthResults)
            {
                list.Add(new ServiceItem{endpoint = item.HealthEndpoint, status = item.status, serviceName = item.service, id = item.Id});
            }

            return list;
        }

        public async Task<List<HealthItem>> GetHealthResults(int id)
        {
            var healthResults = await _repository.GetAll(GetEndpoints());
            var list = new List<HealthItem>();
            var find = healthResults.Find(i => i.Id == id);

            foreach (var result in find.results)
            {
                list.Add(result);
            }

            return list;
        }

        public async Task<IActionResult> Index()
        {

            var result = await GetServiceItems();
            return View(result);
        }


        //Provide JSON endpoints for datatables to get data from
        public async Task<IActionResult> ServicesJson()
        {
            var result = await GetServiceItems();
            return new JsonResult(result);
        }

        public async Task<IActionResult> HealthJson(int id)
        {
            var result = await GetHealthResults(id);
            return new JsonResult(result);
        }
    }
}
