using Microsoft.AspNetCore.Mvc;

namespace Someren_Applicatie.Controllers
{
    public class ParticipantsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
