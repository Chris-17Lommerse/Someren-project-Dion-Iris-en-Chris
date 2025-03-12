using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Someren_Applicatie.Controllers
{
    public class ActivitiesController : Controller
    {
        public IActionResult Index()
        {
            DateTime dt2 = new DateTime(2015, 12, 31);

            List<Activity> activities =
                [
                new Activity("test"),
                new Activity("test2")
                
                ];

            //pass the list to the View
            return View(activities);
        }
    }
}
