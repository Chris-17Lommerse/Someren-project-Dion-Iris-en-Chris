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
                List<Drink> drinks =
                [
                    new Drink(1, "Cola", 50, false, 1),
                    new Drink(2, "Wijn", 40, true, 2)
                    ];
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
        //        [HttpPost]
        //        public ActionResult Create(Room room)
        //        {
        //            try
        //            {
        //                // add room via repository
        //                _roomsRepository.Add(room);

        //                // Go back to list (Via Index)
        //                return RedirectToAction("Index");
        //            }
        //            catch (Exception ex)
        //            {
        //                // Something went wrong, go back to Index
        //                ViewBag.ErrorMessage = ex.Message;
        //                return View(room);
        //            }
        //        }

        //        // GET: RoomsController/Edit/5
        //        [HttpGet]
        //        public ActionResult Edit(string? id)
        //        {
        //            try
        //            {
        //                // 404 Not Found error
        //                if (id == null)
        //                {
        //                    return NotFound();
        //                }

        //                // get room via repository
        //                Room? room = _roomsRepository.GetById((string)id);
        //                return View(room);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //        }

        //        [HttpPost]
        //        // POST: RoomsController/Edit
        //        public ActionResult Edit(Room room)
        //        {
        //            try
        //            {
        //                // Edit room via repository
        //                _roomsRepository.Update(room);

        //                // Go back to list (Via Index)
        //                return RedirectToAction("Index");
        //            }
        //            catch (Exception ex)
        //            {
        //                // Something went wrong, go back to Index
        //                ViewBag.ErrorMessage = ex.Message;
        //                return View(room);
        //            }
        //        }
        //        [HttpGet]
        //        // GET: RoomsController/Delete
        //        public ActionResult Delete(string? id)
        //        {
        //            try
        //            {
        //                // 404 Not Found error
        //                if (id == null)
        //                {
        //                    return NotFound();
        //                }

        //                // get room via repository
        //                Room? room = _roomsRepository.GetById((string)id);
        //                return View(room);
        //            }
        //            catch (Exception ex)
        //            {
        //                {
        //                    throw new Exception(ex.Message);
        //                }
        //            }
        //        }
        //        [HttpPost]
        //        // POST: RoomsController/Delete
        //        public ActionResult Delete(Room room)
        //        {
        //            try
        //            {
        //                // Delete room via repository
        //                _roomsRepository.Delete(room);

        //                // Go back to list (Via Index)
        //                return RedirectToAction("Index");
        //            }
        //            catch (Exception ex)
        //            {
        //                // Something went wrong, go back to Index
        //                ViewBag.ErrorMessage = ex.Message;
        //                return View(room);
        //            }
        //        }
        //    }
    }
}
