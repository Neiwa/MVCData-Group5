using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCData_Group5.Models.ViewModels
{
    public class OrderSummaryViewModel
    {
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total price of order")]
        public double TotalCost { get; set; }
        [Display(Name = "Amount of movies")]
        public int MovieCount { get; set; }
        public string Name { get; set; }
    }
}