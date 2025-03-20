using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Activities;
using Someren_Applicatie.Repositories.Rooms;
using System.Diagnostics;


namespace Someren_Applicatie.Controllers
{
    public class ActivitiesController : Controller
    {
        // Repository variable will be declared
        private readonly IActivitiesRepository _activitiesRepository;

        // Constructor for the ActivitiesController
        public ActivitiesController(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }






  //      public IActionResult Index()
  //      {
   //         try
   //         {
                // Get sll activities from the database and return the Index View with all activities
   //             List<Activiteit> activities = _activitiesRepository.GetAll();
  //              return View(activities);
  //          } catch (Exception ex)
  //          {
  //              throw new Exception(ex.Message);
  //          }
  //      }





        public ActionResult Index(string? searchString)
        {
            List<Activiteit> activiteit;

            if (!string.IsNullOrEmpty(searchString))
            {
                activiteit = _activitiesRepository.GetByName(searchString);
            }
            else
            {
                activiteit = _activitiesRepository.GetAll();
            }

            return View(activiteit);
        }

        // GET: ActivitiesController/Create
        [HttpGet]
        public IActionResult Create()
        {
            // returns a create form to add an activity
            return View();
        }

        // POST: ActivitiesController/Create
        [HttpPost]
        public ActionResult Create(Activiteit activiteit)
        {
            try
            {
                // add activity via repository
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
            // returns a update forn to edit an activity
            return View();
        }

        // GET: ActivitiesController/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                // 404 Not Found error
                if (id == null)
                {
                    return NotFound();
                }

                // get activity via repository
                Activiteit? activiteit = _activitiesRepository.GetById((int)id);
                return View(activiteit);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        // POST: ActivitiesController/Edit
        public ActionResult Edit(Activiteit activiteit)
        {
            try
            {
                // Edit activity via repository
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
        // GET: ActivitiesController/Delete
        public ActionResult Delete(int? id)
        {
            try
            {
                // 404 Not Found error
                if (id == null)
                {
                    return NotFound();
                }

                // get activity via repository
                Activiteit? activiteit = _activitiesRepository.GetById((int)id);
                return View(activiteit);
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        // POST: ActivitiesController/Delete
        public ActionResult Delete(Activiteit activiteit)
        {
            try
            {
                // Delete activity via repository
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
