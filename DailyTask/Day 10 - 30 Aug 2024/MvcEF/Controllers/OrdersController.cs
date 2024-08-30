using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcEF.Models;

namespace MvcEF.Controllers
{
    public class OrdersController : Controller
    {
        private readonly CustomerDbContext _context;

        public OrdersController(CustomerDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Orders.Include(c => c.Customer).ToList());
        }

        public IActionResult Details(int id)
        {
            Order or = _context.Orders.Include(c => c.Customer).FirstOrDefault(o => o.OrderID == id) ?? new Order();

            return View(or);
        }

        public IActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(_context.Customers, "CustomerID", "CustomerName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ProductName,OrderID,CustomerID")] Order order)
        {
            try
            {
                ViewBag.CutomerID = new SelectList(_context.Customers, "CustomerID", "CustomerName", order.CustomerID);
                _context.Add(order);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            
        }

        public ActionResult Delete(int id)
        {
            Order or = _context.Orders.Include(c => c.Customer).FirstOrDefault(o => o.OrderID == id) ?? new Order();

            return View(or);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Order order)
        {
            _context.Remove(id);
            _context.SaveChanges();
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.CutomerID = new SelectList(_context.Customers, "CustomerID", "CustomerName");
            Order or = _context.Orders.Include(c => c.Customer).FirstOrDefault(o => o.OrderID == id) ?? new Order();

            return View(or);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, Order order)
        {
            ViewBag.CutomerID = new SelectList(_context.Customers, "CustomerID", "CustomerName", order.CustomerID);
            _context.Update(order);
            _context.SaveChanges();
            return View();
        }




    }
}
