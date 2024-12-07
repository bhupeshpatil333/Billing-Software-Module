using My_Task1.DAL;
using My_Task1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_Task1.Controllers
{
    public class BillingController : Controller
    {
        private DatabaseHelper db = new DatabaseHelper();
        // GET: Billing
        public ActionResult Index()
        {
            
            var billings = db.GetBillings();
            ViewBag.Billings = billings;
            return View(billings);
        }

        public ActionResult Add()
        {
            ViewBag.Customers = new SelectList(db.GetCustomers(), "CustomerID", "Name");
            ViewBag.Products = db.GetProducts() ?? new List<Product>();
            return View();
        }

        [HttpPost]
        public ActionResult InsertBilling(Billing billing)
        {
            if (ModelState.IsValid)
            {
                db.AddBilling(billing);
                return RedirectToAction("Index");
            }
            ViewBag.Customers = new SelectList(db.GetCustomers(), "CustomerID", "Name", billing.CustomerID);
            return View(billing);
        }

        public ActionResult Edit(int id)
        {
            var billing = db.GetBillingById(id);
            ViewBag.Customers = new SelectList(db.GetCustomers(), "CustomerID", "Name", billing.CustomerID);
            return View(billing);
        }

        [HttpPost]
        public ActionResult EditBilling(Billing billing)
        {
            if (ModelState.IsValid)
            {
                db.UpdateBilling(billing);
                return RedirectToAction("Index");
            }
            ViewBag.Customers = new SelectList(db.GetCustomers(), "CustomerID", "Name", billing.CustomerID);
            return View(billing);
        }

        public ActionResult Delete(int id)
        {
            db.DeleteBilling(id);
            return RedirectToAction("Index");
        }
    }
}