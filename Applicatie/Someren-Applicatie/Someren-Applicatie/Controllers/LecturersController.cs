using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Lecturers;

namespace Someren_Applicatie.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ILecturersRepository _lecturersRepository;

        public LecturersController(ILecturersRepository lecturersRepository)
        {
            _lecturersRepository = lecturersRepository;
        }

        public ActionResult Index(string? searchString)
        {
            List<Lecturer> lecturers;

            if (!string.IsNullOrEmpty(searchString))
            {
                lecturers = _lecturersRepository.GetByLastName(searchString);
            }
            else
            {
                lecturers = _lecturersRepository.GetAll();
            }

            return View(lecturers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Lecturer lecturer)
        {
            try
            {
                _lecturersRepository.Add(lecturer);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(lecturer);
            }
        }

        
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Lecturer ? lecturer = _lecturersRepository.GetById((int)id);
            return View(lecturer);
        }

        
        [HttpPost]
        public ActionResult Edit(Lecturer lecturer)
        {
            try
            {
                _lecturersRepository.Update(lecturer);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(lecturer);
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lecturer? lecturer = _lecturersRepository.GetById((int)id);
            return View(lecturer);
        }

        [HttpPost]
        public ActionResult Delete(Lecturer lecturer)
        {
            try
            {
                _lecturersRepository.Delete(lecturer);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(lecturer);
            }
        }

        
    }
}
