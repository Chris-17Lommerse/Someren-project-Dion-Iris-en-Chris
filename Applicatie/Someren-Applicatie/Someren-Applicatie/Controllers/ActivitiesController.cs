using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using System.Diagnostics;


namespace Someren_Applicatie.Controllers
{
    public class ActivitiesController : Controller
    {
        public IActionResult Index()
        {
            List<Activiteit> activities =
                [
                new Activiteit(1,"Test", new DateTime(2025, 5, 15,16,0,0), new DateTime(2025, 5, 15, 18, 0, 0)),
                new Activiteit(2,"Test2", new DateTime(2025,6, 10,14,30,0), new DateTime(2025, 6, 10, 18, 30, 30))
                
                ,
                ];

            //pass the list to the View
            return View(activities);

            
        }
        // GET: ActivitiesController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActivitiesController/Create

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
