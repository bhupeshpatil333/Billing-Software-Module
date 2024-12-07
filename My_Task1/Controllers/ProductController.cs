using My_Task1.DAL;
using My_Task1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_Task1.Controllers
{
    public class ProductController : Controller
    {
        private DatabaseHelper db = new DatabaseHelper();
        // GET: Product
        public ActionResult Index()
        {
            var products = db.GetProducts();
            return View(products);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var product = db.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            db.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}