using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Rooms;

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

            List<Room> rooms = _roomsRepository.GetAll();
            return View(rooms);
        }
        // GET: RoomsController/Create
        [HttpGet]
        public IActionResult Create()
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
                return View(room);
            }
        }

        // GET: UsersController/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get user via repository
            Room? user = _roomsRepository.GetById((int)id);
            return View(user);
        }
        [HttpPost]
        // POST: UsersController/Edit
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
                return View(room);
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
            Room? room = _roomsRepository.GetById((int)id);
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
                return View(room);
            }
        }
    }
}
