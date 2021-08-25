using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OverviewApplication.Models;
using OverviewApplication.Services;

namespace OverviewApplication.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IRepository m_repository;

        public HomeController(IRepository repository)
        {
            m_repository = repository;
        }

        public List<string> endpoints = new List<string>()
        {
            "service1",
            "service2",
            "service3"
        };

        public async Task<IActionResult> Index()
        {

            var result = await m_repository.GetAll(endpoints);
            return View(result);
        }
    }
}
