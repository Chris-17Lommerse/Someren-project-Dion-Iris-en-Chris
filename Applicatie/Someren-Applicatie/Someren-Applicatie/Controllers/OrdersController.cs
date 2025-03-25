using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Drinks;
using Someren_Applicatie.Repositories.Orders;
using Someren_Applicatie.Repositories.Rooms;
using Someren_Applicatie.Repositories.Students;

namespace Someren_Applicatie.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly IDrinksRepository _drinksRepository;
        public OrdersController(IOrdersRepository ordersRepository, IDrinksRepository drinksRepository, IStudentsRepository studentsRepository)
        {
            _ordersRepository = ordersRepository;
            _studentsRepository = studentsRepository;
            _drinksRepository = drinksRepository;
        }

        public IActionResult Index()
        {
            List<Order> orders = _ordersRepository.GetAll();
            return View(orders);
        }

        [HttpGet]
        public IActionResult AddOrder()
        {
            ViewBag.Students = new SelectList(_studentsRepository.GetAll(), "StudentNr", "Voornaam");
            ViewBag.Drinks = new SelectList(_drinksRepository.GetAll(), "DrankId", "DrankNaam");
            return View(new Order());
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            try
            {
                _ordersRepository.Add(order);

                return RedirectToAction("Details", new { id1 = order.DrankId, id2 = order.StudentNr });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(order);
            }
        }

        [HttpGet]
        public IActionResult Details(int? studentNr, int? drankId)
        {
            try
            {
                if (studentNr == null || drankId == null)
                {
                    return NotFound();
                }

                Order? order = _ordersRepository.GetById((int)studentNr, (int)drankId);
                return View(order);
            } 
            catch(Exception)
            {
                throw new Exception("Cannot load the order from the database");
            }
        }
    }
}
