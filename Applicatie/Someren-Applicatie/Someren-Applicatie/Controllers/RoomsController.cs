using Microsoft.AspNetCore.Mvc;

namespace Someren_Applicatie.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
