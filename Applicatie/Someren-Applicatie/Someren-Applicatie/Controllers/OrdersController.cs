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
        public OrdersController(IOrdersRepository ordersRepository, IDrinksRepository drinksRepository, IStudentsRepository studentsRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            List<Order> orders = _ordersRepository.GetAll();
            return View(orders);
        }

        [HttpGet]
        public IActionResult AddOrder()
        {
            DrinkOrderViewModel drinkOrderViewModel = new DrinkOrderViewModel();
            ViewBag.Students = new SelectList(drinkOrderViewModel.Students);
            ViewBag.Drinks = new SelectList(drinkOrderViewModel.Drinks);
            return View(new DrinkOrderViewModel());
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {

            try
            {
                _ordersRepository.Add(order);

                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(order);
            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                Order? order = _ordersRepository.GetById((int)id);
                return View(order);
            } 
            catch(Exception)
            {
                throw new Exception("Cannot load the order from the database");
            }
        }
    }
}
