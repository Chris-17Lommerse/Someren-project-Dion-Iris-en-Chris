using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Drinks;
using Someren_Applicatie.Repositories.Orders;
using Someren_Applicatie.Repositories.Rooms;
using Someren_Applicatie.Repositories.Students;

namespace Someren_Applicatie.Controllers
{
    public class OrdersController : Controller
    {
        // Interfaces are defined here
        private readonly IOrdersRepository _ordersRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly IDrinksRepository _drinksRepository;

        // Constructor
        public OrdersController(IOrdersRepository ordersRepository, IDrinksRepository drinksRepository, IStudentsRepository studentsRepository)
        {
            _ordersRepository = ordersRepository;
            _studentsRepository = studentsRepository;
            _drinksRepository = drinksRepository;
        }

        // Index method
        public IActionResult Index()
        {
            try
            {
                List<Order> orders = _ordersRepository.GetAll();
                return View(orders);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // AddOrder method
        [HttpGet]
        public IActionResult AddOrder()
        {
            try
            {
                List<Student> students = _studentsRepository.GetAll();
                List<Drink> drinks = _drinksRepository.GetAll();

                DrinkOrderViewModel drinkOrderViewModel = new DrinkOrderViewModel(students, drinks);
                return View(drinkOrderViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST method for AddOrder 
        [HttpPost]
        public IActionResult AddOrder(DrinkOrderViewModel drinkOrderViewModel)
        {
            try
            {
                Order order = new Order()
                {
                    StudentNr = drinkOrderViewModel.SelectedStudentNr,
                    DrankId = drinkOrderViewModel.SelectedDrankId,
                    Aantal = drinkOrderViewModel.Aantal
                };

                _ordersRepository.Add(order);
                Student? student = _studentsRepository.GetById(order.StudentNr);
                order.StudentNaam = student?.Voornaam;
                Drink? drink = _drinksRepository.GetById(order.DrankId);
                order.DrankNaam = drink.DrankNaam.ToLower();
                TempData["ConfirmMessage"] = $"{order.StudentNaam} heeft {order.Aantal} {order.DrankNaam} besteld";

                DrinkOrderViewModel viewModel = new DrinkOrderViewModel
                {
                    Students = _studentsRepository.GetAll(),
                    Drinks = _drinksRepository.GetAll()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                drinkOrderViewModel.Students = _studentsRepository.GetAll();
                drinkOrderViewModel.Drinks = _drinksRepository.GetAll();
                return View(drinkOrderViewModel);
            }
        }

        // Details GET method
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
