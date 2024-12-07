using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace My_Task1.Models
{
    public class Billing
    {
        public int BillingID { get; set; }
        [Required(ErrorMessage = "Customer is required")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Billing date is required")]
        [DataType(DataType.Date)]
        public DateTime? BillingDate { get; set; }

        public string CustomerName { get; set; }
    }
}