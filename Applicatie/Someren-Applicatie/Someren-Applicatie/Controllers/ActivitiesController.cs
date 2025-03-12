using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Someren_Applicatie.Controllers
{
    public class ActivitiesController : Controller
    {
        public IActionResult Index()
        {
            

            //pass the list to the View
            return View();
        }
    }
}
