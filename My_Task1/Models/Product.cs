using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My_Task1.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductBrand { get; set; }
        public string ProductSupplier { get; set; }
        public int OldStock { get; set; }
        public string ProductCategory { get; set; }

        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get => _totalAmount;
            set => _totalAmount = value; // Allow setting the value
        }
    }
}