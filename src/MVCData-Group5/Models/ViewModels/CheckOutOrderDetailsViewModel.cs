using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCData_Group5.Models.ViewModels
{
    public class CheckOutOrderDetailsViewModel
    {
        [Display(Name = "Amount of movies in cart")]
        public int MovieCount { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total cost of order")]
        public double OrderTotal { get; set; }
    }
}