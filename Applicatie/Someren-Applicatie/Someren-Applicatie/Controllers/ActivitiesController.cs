using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Someren_Applicatie.Controllers
{
    public class ActivitiesController : Controller
    {
        public IActionResult Index()
        {
            List<Activity> activities =
                [
                new Activity(1,"Test", new DateTime(2025, 3, 23)),
                new Activity(2, "Test2", new DateTime(2025, 4, 1))
                ,
                ];

            //pass the list to the View
            return View(activities);
        }
    }
}
