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

        // DrinksController/Index
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

        // GET: DrinksController/Create
        [HttpGet]
        public ActionResult Create()
        {
            // Returns a create form to add a drink
            return View();
        }

        // POST: DrinksController/Create
        [HttpPost]
        public ActionResult Create(Drink drink)
        {
            try
            {
                // add drink via repository
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

        // GET: DrinksController/Edit/5
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

                // get drink via repository
                Drink? drink = _drinksRepository.GetById((int)id);
                return View(drink);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        // POST: DrinksController/Edit
        public ActionResult Edit(Drink drink)
        {
            try
            {
                // Edit drink via repository
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
        // GET: DrinksController/Delete
        public ActionResult Delete(int? id)
        {
            try
            {
                // 404 Not Found error
                if (id == null)
                {
                    return NotFound();
                }

                // get drink via repository
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
        // POST: DrinksController/Delete
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

