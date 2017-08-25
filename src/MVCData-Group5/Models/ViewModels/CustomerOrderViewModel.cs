using System;
using System.Collections.Generic;
using MVCData_Group5.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MVCData_Group5.Models.ViewModels
{
    public class CustomerOrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public ICollection<ShoppingCartMovieViewModel> Movies { get; set; }
        [DataType(DataType.Currency)]
        public double TotalCost { get; set; }
    }
}