using My_Task1.DAL;
using My_Task1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_Task1.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseHelper db = new DatabaseHelper();
        public ActionResult Dashboard()
        {
            var totalSales = db.GetTotalSales(); 
            var totalRevenue = db.GetTotalRevenue(); 

            var dashboardViewModel = new DashboardViewModel
            {
                TotalSales = totalSales,
                TotalRevenue = totalRevenue
            };

            return View(dashboardViewModel);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}