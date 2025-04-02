using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Drinks;
using Someren_Applicatie.Repositories.Rooms;

namespace Someren_Applicatie.Controllers
{
    public class DrinksController : Controller
    {
        // Repository variable will be declared
        private readonly IDrinksRepository _drinksRepository;

        // Constructor
        public DrinksController(IDrinksRepository drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }

        public IActionResult Index()
        {
            try
            {
                List<Drink> drinks = _drinksRepository.GetAll();
                return View(drinks);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: RoomsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            // Returns a create form to add a room
            return View();
        }

        // POST: RoomsController/Create
        [HttpPost]
        public ActionResult Create(Drink drink)
        {
            try
            {
                // add room via repository
                _drinksRepository.Add(drink);

                // Go back to list (Via Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Something went wrong, go back to Index
                ViewBag.ErrorMessage = ex.Message;
                return View(drink);
            }
        }

        // GET: RoomsController/Edit/5
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

                // get room via repository
                Drink? drink = _drinksRepository.GetById((int)id);
                return View(drink);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        // POST: RoomsController/Edit
        public ActionResult Edit(Drink drink)
        {
            try
            {
                // Edit room via repository
                _drinksRepository.Update(drink);

                // Go back to list (Via Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Something went wrong, go back to Index
                ViewBag.ErrorMessage = ex.Message;
                return View(drink);
            }
        }
        [HttpGet]
        // GET: RoomsController/Delete
        public ActionResult Delete(int? id)
        {
            try
            {
                // 404 Not Found error
                if (id == null)
                {
                    return NotFound();
                }

                // get room via repository
                Drink? drink = _drinksRepository.GetById((int)id);
                return View(drink);
            }
            catch (Exception ex)
            {
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        [HttpPost]
        // POST: RoomsController/Delete
        public ActionResult Delete(Drink drink)
        {
            try
            {
                // Delete room via repository
                _drinksRepository.Delete(drink);

                // Go back to list (Via Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Something went wrong, go back to Index
                ViewBag.ErrorMessage = ex.Message;
                return View(drink);
            }
        }
    }
}

