using Microsoft.AspNetCore.Mvc;

namespace Someren_Applicatie.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
