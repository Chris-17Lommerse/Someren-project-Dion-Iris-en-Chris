using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Rooms;
using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomRepository _roomsRepository;

        public RoomsController(IRoomRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        public IActionResult Index()
        {
            List<Room> rooms =
            [
                new Room("A1-01", 8, TypeKamer.Student),
                new Room("A1-02", 1, TypeKamer.Lecture)
                ,
                ];
            return View(rooms);
        }

        // GET: RoomsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomsController/Create
        [HttpPost]
        public ActionResult Create(Room room)
        {
            try
            {
                // add user via repository
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
            if (id == null)
            {
                return NotFound();
            }

            // get user via repository
            Room? room = _roomsRepository.GetById((string)id);
            return View(room);
        }

        [HttpPost]
        // POST: RoomsController/Edit
        public ActionResult Edit(Room room)
        {
            try
            {
                // Edit user via repository
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
        public ActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get user via repository
            Room? room = _roomsRepository.GetById((string)id);
            return View(room);
        }
        [HttpPost]
        // POST: UsersController/Delete
        public ActionResult Delete(Room room)
        {
            try
            {
                // Delete user via repository
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
