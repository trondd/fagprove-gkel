using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OverviewApplication.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var result = HomeController.EndPoints;
            return View(result);
        }

        //HTTP Get and POST methods for Delete function
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var find = HomeController.EndPoints.Find(i => i.Id == id);
            if (find == null)
            {
                return NotFound();
            }

            return View(find);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = HomeController.EndPoints.Find(i => i.Id == id);
            HomeController.EndPoints.Remove(result);
            return RedirectToAction("Index");
        }


        //HTTP Get and POST methods for Edit function
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var find = HomeController.EndPoints.Find(i => i.Id == id);
            if (find == null)
            {
                return NotFound();
            }

            return View(find);
        }

        [HttpPost]
        public ActionResult Edit(int id, Models.Endpoint endpointData)
        {
            var oldValue = HomeController.EndPoints.Find(i => i.Id == id);
            oldValue.Uri = endpointData.Uri;
            oldValue.Description = endpointData.Description;
            oldValue.Name = endpointData.Name;
            oldValue.Id = endpointData.Id;

            return RedirectToAction("Index");
        }


        //HTTP Get and POST methods for Create/Add new function
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Endpoint endpointData)
        {
            var id = HomeController.EndPoints.Count();
            endpointData.Id = id;

            HomeController.EndPoints.Add(endpointData);
            return RedirectToAction("Index");
        }
    }
}
