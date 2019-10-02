using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using infodiorama.Models;

namespace infodiorama.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DbContext context = new DbContext();

            return View(context.GetRecords().ToList());
        }

        // GET: Entity/Edit/5
        [HttpPost]
        public IActionResult Edit(int? id)
        {

            DbContext context = new DbContext();

            

            if (id == null)
            {
                return NotFound();
            }


            return View(context.getEntityView());
        }

        public IActionResult Edit()
        {
            DbContext context = new DbContext();
            return View(context.getEntityView());
        }
        // GET: Entity/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /*  // GET: Entity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/



        // POST: Entity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Entity/Delete/5
        /*  public ActionResult Delete(int id)
          {
              return View();
          }

          // POST: Entity/Delete/5
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Delete(int id, IFormCollection collection)
          {
              try
              {
                  // TODO: Add delete logic here

                  return RedirectToAction(nameof(Index));
              }
              catch
              {
                  return View();
              }
          }*/
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
