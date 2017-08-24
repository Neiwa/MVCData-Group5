using System.ComponentModel.DataAnnotations;

namespace MVCData_Group5.Models.ViewModels
{
    public class CheckOutViewModel
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        public int MovieCount { get; set; }

        [DataType(DataType.Currency)]
        public double OrderTotal { get; set; }
    }
}