using System.ComponentModel.DataAnnotations;

namespace MVCData_Group5.Models.ViewModels
{
    public class CheckOutViewModel
    {
        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }
}