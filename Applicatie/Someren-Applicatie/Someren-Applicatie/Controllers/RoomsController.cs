using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Rooms;
using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Controllers
{
    public class RoomsController : Controller
    {
        // Repository variable will be declared
        private readonly IRoomRepository _roomsRepository;

        // Constructor
        public RoomsController(IRoomRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        public IActionResult Index()
        {
            // Get all rooms from the database and return the Index View of the Activities
            List<Room> rooms = _roomsRepository.GetAll();
            return View(rooms);
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
        public ActionResult Create(Room room)
        {
            try
            {
                // add room via repository
                _roomsRepository.Add(room);

                // Go back to list (Via Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Something went wrong, go back to Index
                ViewBag.ErrorMessage = ex.Message;
                return View(room);
            }
        }

        // GET: RoomsController/Edit/5
        [HttpGet]
        public ActionResult Edit(string? id)
        {
            // 404 Not Found error
            if (id == null)
            {
                return NotFound();
            }

            // get room via repository
            Room? room = _roomsRepository.GetById((string)id);
            return View(room);
        }

        [HttpPost]
        // POST: RoomsController/Edit
        public ActionResult Edit(Room room)
        {
            try
            {
                // Edit room via repository
                _roomsRepository.Update(room);

                // Go back to list (Via Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Something went wrong, go back to Index
                ViewBag.ErrorMessage = ex.Message;
                return View(room);
            }
        }
        [HttpGet]
        // GET: RoomsController/Delete
        public ActionResult Delete(string? id)
        {
            // 404 Not Found error
            if (id == null)
            {
                return NotFound();
            }

            // get room via repository
            Room? room = _roomsRepository.GetById((string)id);
            return View(room);
        }
        [HttpPost]
        // POST: RoomsController/Delete
        public ActionResult Delete(Room room)
        {
            try
            {
                // Delete room via repository
                _roomsRepository.Delete(room);

                // Go back to list (Via Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Something went wrong, go back to Index
                ViewBag.ErrorMessage = ex.Message;
                return View(room);
            }
        }
    }
}
