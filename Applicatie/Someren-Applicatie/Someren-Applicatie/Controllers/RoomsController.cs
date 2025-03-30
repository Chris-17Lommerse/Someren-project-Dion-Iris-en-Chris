using Microsoft.AspNetCore.Mvc;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Rooms;
using Someren_Applicatie.Models.Enums;
using Someren_Applicatie.Repositories.Students;

namespace Someren_Applicatie.Controllers
{
    public class RoomsController : Controller
    {
        // Repository variable will be declared
        private readonly IRoomRepository _roomsRepository;
        private readonly IStudentsRepository _studentsRepository;
        // Constructor
        public RoomsController(IRoomRepository roomsRepository, IStudentsRepository studentsRepository)
        {
            _roomsRepository = roomsRepository;
            _studentsRepository = studentsRepository;
        }

        public IActionResult Index(string? searchString)
        {
            try
            {
                List<Room> rooms;
                // Get all rooms from the database and return the Index View of the Activities
                if (!string.IsNullOrEmpty(searchString) && (!searchString.Contains("Kies een optie")))
                {
                    rooms = _roomsRepository.GetByRoomSize(searchString);
                }
                else
                {
                    rooms = _roomsRepository.GetAll();
                }
                return View(rooms);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult DormitoryStudents(string? id)
        { 
            Room? room = _roomsRepository.GetById(id);
            List<Student> students = _studentsRepository.GetByRoomNumber(id) ?? new List<Student>();

            DormitoryStudentsViewModel dormitoryStudentsViewModel = new DormitoryStudentsViewModel
            {
                Room = room,
                StudentsWithARoom = students
            };

            return View(dormitoryStudentsViewModel);
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
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            try
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
            catch (Exception ex)
            {
                {
                    throw new Exception(ex.Message);
                }
            }
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
