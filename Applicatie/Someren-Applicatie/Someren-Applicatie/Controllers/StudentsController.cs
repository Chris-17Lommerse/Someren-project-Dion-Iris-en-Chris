using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Students;

namespace Someren_Applicatie.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository _studentsRepository;
        public StudentsController(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }


        public IActionResult Index()
        {
            List<Student> students = _studentsRepository.GetAll();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            try
            {
                _studentsRepository.Add(student);
                //Back to index page
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(student);
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            Student? student = _studentsRepository.GetById((int)id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            try
            {
                _studentsRepository.Update(student);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(student);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Student? student = _studentsRepository.GetById((int)id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(Student student)
        {
            try
            {
                _studentsRepository.Delete(student);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(student);
            }
        }
    }
}
