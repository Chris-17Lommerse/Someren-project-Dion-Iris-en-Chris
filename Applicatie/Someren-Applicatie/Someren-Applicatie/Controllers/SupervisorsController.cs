using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories;
using Someren_Applicatie.Repositories.Supervisors;
using Someren_Applicatie.Repositories.Lecturers;

namespace Someren_Applicatie.Controllers
{
    [Route("Supervisors")]
    public class SupervisorsController : Controller
    {
        private readonly ISupervisorsRepository _supervisorsRepository;
        private readonly ILecturersRepository _lecturersRepository;

        public SupervisorsController(ISupervisorsRepository supervisorsRepository, ILecturersRepository lecturersRepository)
        {
            _supervisorsRepository = supervisorsRepository;
            _lecturersRepository = lecturersRepository;
        }

        [HttpGet("Index/{id?}")]
        public IActionResult Index(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Activities");
            try
            {
                List<Lecturer> supervisors = _supervisorsRepository.GetByActivityId(id);
                ViewData["ActivityId"] = id;
                ViewData["ActivityName"] = _supervisorsRepository.GetActivityName(id);
                return View(supervisors);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Activities");
            }
        }

        [HttpGet("Create/{id?}")]
        public IActionResult Create(int? id)
        {
            if (id == null)
                return NotFound();
            try
            {
                List<Lecturer> lecturers = _lecturersRepository.GetAll();
                ViewData["ActivityId"] = id;
                return View(lecturers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Supervisors", new { id = id });
            }

        }

        [HttpPost("CreatePost")]
        public IActionResult CreatePost(int docentnr, int activiteitid)
        {
            try
            {
                _supervisorsRepository.Add(docentnr, activiteitid);
                Lecturer lecturer = _lecturersRepository.GetById(docentnr);
                TempData["ConfirmMessage"] = $"{lecturer.Voornaam} {lecturer.Achternaam} toegevoegd aan activiteit als begeleider";
                return RedirectToAction("Index", "Supervisors", new { id = activiteitid });
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Create", "Supervisors", new { id = activiteitid });
            }
        }

        [HttpGet("Delete/{docentnr}/{activityid}")]
        public IActionResult Delete(int docentnr, int activityid)
        {
            if (docentnr == null || activityid == null) return NotFound();
            try
            {
                Lecturer lecturer = _lecturersRepository.GetById(docentnr);
                ViewData["ActivityId"] = activityid;
                ViewData["ActivityName"] = _supervisorsRepository.GetActivityName(activityid);
                return View(lecturer);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Supervisors", new { id = activityid });
            }
        }

        [HttpPost("DeletePost")]
        public IActionResult DeletePost(int docentnr, int activiteitId)
        {
            try
            {
                _supervisorsRepository.Delete(docentnr, activiteitId);
                Lecturer lecturer = _lecturersRepository.GetById(docentnr);
                TempData["ConfirmMessage"] = $"{lecturer.Voornaam} {lecturer.Achternaam} verwijderd van activiteit als begeleider";
                return RedirectToAction("Index", "Supervisors", new { id = activiteitId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Supervisors", new { id = activiteitId });
            }
        }




    }
}