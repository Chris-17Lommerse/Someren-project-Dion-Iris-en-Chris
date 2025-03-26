using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories;
using Someren_Applicatie.Repositories.Participants;
using Someren_Applicatie.Repositories.Students;

namespace Someren_Applicatie.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly IParticipantsRepository _participantsRepository;
        private readonly IStudentsRepository _studentsRepository;

        public ParticipantsController(IParticipantsRepository participantsRepository, IStudentsRepository studentsRepository)
        {
            _participantsRepository = participantsRepository;
            _studentsRepository = studentsRepository;
        }

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
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index", "Activities");
            }
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Activities");
            try
            {
                List<Student> students = _studentsRepository.GetAll();
                ViewData["ActivityId"] = id;
                return View(students);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index", "Activities");
            }
        }

        [HttpPost]
        public IActionResult Create(int studentennr, int activiteitid)
        {
            try
            {
                _participantsRepository.Add(studentennr, activiteitid);
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(string joinedId)
        {
            //joinedId always gives null so idk how it work, the address just works it gives /Participants/Delete/15,1
            if (joinedId == null) return NotFound();
            try
            {
                string[] splitIds = joinedId.Split(',');
                int studentnr = int.Parse(splitIds[0]);
                int activityId = int.Parse(splitIds[1]);
                Student student = _studentsRepository.GetById(studentnr);
                ViewData["ActivityId"] = activityId;
                ViewData["ActivityName"] = _participantsRepository.GetActivityName(activityId);
                return View(student);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index", "Activities");
            }
        }

        [HttpPost]
        public IActionResult Delete(int StudentNr, int ActiviteitId)
        {
            try
            {
                _participantsRepository.Delete(StudentNr, ActiviteitId);
                return RedirectToAction("Index", "Activities");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index", "Activities");
            }
        }
    }
}
