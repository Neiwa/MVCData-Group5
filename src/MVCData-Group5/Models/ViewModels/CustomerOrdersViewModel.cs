using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCData_Group5.Models.ViewModels
{
    public class CustomerOrdersViewModel
    {
        public string Name { get;  set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get;  set; }

        public ICollection<CustomerOrderViewModel> Orders { get;  set; }
    }
}