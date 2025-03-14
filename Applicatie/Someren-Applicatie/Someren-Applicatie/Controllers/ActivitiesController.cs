using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Activities;
using Someren_Applicatie.Repositories.Rooms;
using System.Diagnostics;


namespace Someren_Applicatie.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivitiesRepository _activitiesRepository;

        public ActivitiesController(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }
        public IActionResult Index()
        {
            List<Activiteit> activities = _activitiesRepository.GetAll();
            return View(activities);

            
        }
        // GET: ActivitiesController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActivitiesController/Create
        [HttpPost]
        public ActionResult Create(Activiteit activiteit)
        {
            try
            {
                // add user via repository
                _activitiesRepository.Add(activiteit);

                // Go back to list (Via Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Something went wrong, go back to Index
                ViewBag.ErrorMessage = ex.Message;
                return View(activiteit);
            }
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        // GET: ActivitiesController/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get user via repository
            Activiteit? activiteit = _activitiesRepository.GetById((int)id);
            return View(activiteit);
        }

        [HttpPost]
        // POST: UsersController/Edit
        public ActionResult Edit(Activiteit activiteit)
        {
            try
            {
                // Edit user via repository
                _activitiesRepository.Update(activiteit);

                // Go back to list (Via Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Something went wrong, go back to Index
                ViewBag.ErrorMessage = ex.Message;
                return View(activiteit);
            }
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get user via repository
            Activiteit? activiteit =_activitiesRepository.GetById((int)id);
            return View(activiteit);
        }
        [HttpPost]
        // POST: UsersController/Delete
        public ActionResult Delete(Activiteit activiteit)
        {
            try
            {
                // Delete user via repository
                _activitiesRepository.Delete(activiteit);

                // Go back to list (Via Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Something went wrong, go back to Index
                ViewBag.ErrorMessage = ex.Message;
                return View(activiteit);
            }
        }
    }
}
