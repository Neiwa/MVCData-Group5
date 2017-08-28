using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCData_Group5.Models.ViewModels
{
    public class CheckOutOrderDetailsViewModel
    {
        public int MovieCount { get; set; }
        [DataType(DataType.Currency)]
        public double OrderTotal { get; set; }
    }
}