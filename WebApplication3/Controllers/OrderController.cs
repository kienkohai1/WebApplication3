using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: Order/Index
        public IActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }

        // POST: Order/Create
        [HttpPost]
        public IActionResult Create(ServiceOrder order)
        {
            if (ModelState.IsValid)
            {
                _orderService.Add(order);
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Order/UpdateStatus
        [HttpPost]
        public IActionResult UpdateStatus(int id, OrderStatus status)
        {
            _orderService.UpdateStatus(id, status);
            return RedirectToAction(nameof(Index));
        }

    }
}