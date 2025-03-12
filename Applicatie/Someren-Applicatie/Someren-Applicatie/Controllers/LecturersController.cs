using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;

namespace Someren_Applicatie.Controllers
{
    public class LecturersController : Controller
    {
        public IActionResult Index()
        {
            //create a list of hardcoded test lecturers
            List<Lecturer> lecturers =
                [
                new Lecturer(1,"John", "Doe", "06-12345678", 45, 478),
                new Lecturer(2, "Jane", "Doe",  "06-87654321", 32, 221)
                ,
                ];

            //pass the list to the View
            return View(lecturers);
        }
    }
}
