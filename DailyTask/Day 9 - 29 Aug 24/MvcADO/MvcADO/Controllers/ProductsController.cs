using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcADO.DataAccess;
using MvcADO.Models;

namespace MvcADO.Controllers
{
    public class ProductsController : Controller
    {
        ProductDataAccess pda = new ProductDataAccess();
        // GET: ProductsController
        public ActionResult Index()
        {
            IEnumerable<Product> products = pda.Retrieve();
            return View(products);  
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            Product product = pda.Search(id);
            return View(product);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product pt)
        {
            try
            {
                pda.Insert(pt);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = pda.Search(id);
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product pt)
        {
            try
            {
                pda.Update(pt);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = pda.Search(id);
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                pda.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
