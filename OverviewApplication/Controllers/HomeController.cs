using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        public List<string> GetEndpoints()
        {
            var configEndpoints = _configuration.GetSection("Endpoints").GetChildren().Select(x => x.Value).ToList();
            return configEndpoints;
        }

        public async Task<IActionResult> Index()
        {

            var result = await _repository.GetAll(GetEndpoints());
            return View(result);
        }
    }
}
