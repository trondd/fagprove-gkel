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
        public Task<List<HealthContent>> HealthData
        {
            get
            {
                var result = _repository.GetAll(GetEndpoints());
                return result;
            }
        }
        

        public async Task<IActionResult> Index()
        {

            var result = await HealthData;
            return View(result);
        }

        public async Task<IActionResult> Admin()
        {
            var result = EndPoints;
            return View(result);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var find = EndPoints.Find(i => i.Id == id);
            if (find == null)
            {
                return NotFound();
            }

            return View(find);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = EndPoints.Find(i => i.Id == id);
            EndPoints.Remove(result);
            return RedirectToAction("Admin");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var find = EndPoints.Find(i => i.Id == id);
            if (find == null)
            {
                return NotFound();
            }

            return View(find);
        }

        [HttpPost]
        public ActionResult Edit(int id, Endpoint endpointData)
        {
            var oldValue = EndPoints.Find(i => i.Id == id);
                oldValue.Uri = endpointData.Uri;
                oldValue.Description = endpointData.Description;
                oldValue.Name = endpointData.Name;
                oldValue.Id = endpointData.Id;

                return RedirectToAction("Admin");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Endpoint endpointData)
        {
            var id = EndPoints.Count();
            endpointData.Id = id;

            EndPoints.Add(endpointData);
            return RedirectToAction("Admin");
        }
    }
}
