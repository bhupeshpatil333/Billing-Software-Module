using My_Task1.DAL;
using My_Task1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_Task1.Controllers
{
    public class CustomerController : Controller
    {
        private DatabaseHelper db = new DatabaseHelper();
        // GET: Customer
        public ActionResult Index()
        {
            var customers = db.GetCustomers();
            return View(customers);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.AddCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customer Edit
        public ActionResult Edit(int id)
        {
            var customer = db.GetCustomerById(id);
            return View(customer);
        }

        // POST: Customer Edit
        [HttpPost]
        public ActionResult EditByID(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customer Delete
        public ActionResult Delete(int id)
        {
            db.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}