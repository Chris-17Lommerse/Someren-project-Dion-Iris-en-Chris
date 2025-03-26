using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories;
using Someren_Applicatie.Repositories.Participants;
using Someren_Applicatie.Repositories.Students;

namespace Someren_Applicatie.Controllers
{
    [Route("Participants")] //delete if no worky
    public class ParticipantsController : Controller
    {
        private readonly IParticipantsRepository _participantsRepository;
        private readonly IStudentsRepository _studentsRepository;

        public ParticipantsController(IParticipantsRepository participantsRepository, IStudentsRepository studentsRepository)
        {
            _participantsRepository = participantsRepository;
            _studentsRepository = studentsRepository;
        }

        [HttpGet("Index/{id?}")]
        public IActionResult Index(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Activities");
            try
            {
                List<Student> participants = _participantsRepository.GetByActivityId(id);
                ViewData["ActivityId"] = id;
                ViewData["ActivityName"] = _participantsRepository.GetActivityName(id);
                return View(participants);
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
                List<Student> students = _studentsRepository.GetAll();
                ViewData["ActivityId"] = id;
                return View(students);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Participants", new { id = id });
            }
        }

        [HttpPost("CreateConfirmed")]
        public IActionResult CreateConfirmed(int studentennr, int activiteitid)
        {
            try
            {
                _participantsRepository.Add(studentennr, activiteitid);
                Student student = _studentsRepository.GetById(studentennr);
                TempData["ConfirmMessage"] = $"{student.Voornaam} {student.Achternaam} toegevoegd aan activiteit als deelnemer";
                return RedirectToAction("Index", "Participants", new { id = activiteitid });
            }
            catch (SqlException ex)
            {
                TempData["ErrorMessage"] = "kies een student";
                return RedirectToAction("Create", "Participants", new { id = activiteitid });
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Create", "Participants", new { id = activiteitid });
            }
        }

        [HttpGet("Delete/{studentnr}/{activityid}")]
        public IActionResult Delete(int studentnr, int activityid)
        {
            if (studentnr == null || activityid == null) return NotFound();
            try
            {
                Student student = _studentsRepository.GetById(studentnr);
                ViewData["ActivityId"] = activityid;
                ViewData["ActivityName"] = _participantsRepository.GetActivityName(activityid);
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Participants", new { id = activityid });
            }
        }

        [HttpPost("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int StudentNr, int ActiviteitId)
        {
            try
            {
                _participantsRepository.Delete(StudentNr, ActiviteitId);
                Student student = _studentsRepository.GetById(StudentNr);
                TempData["ConfirmMessage"] = $"{student.Voornaam} {student.Achternaam} verwijderd van activiteit als deelnemer";
                return RedirectToAction("Index", "Participants", new { id = ActiviteitId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Participants", new { id = ActiviteitId });
            }
        }
    }
}
