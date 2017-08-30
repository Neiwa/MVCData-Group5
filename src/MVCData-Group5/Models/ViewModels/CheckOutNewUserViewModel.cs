using System.ComponentModel.DataAnnotations;
using MVCData_Group5.Models.Validators;

namespace MVCData_Group5.Models.ViewModels
{
    public class CheckOutNewUserViewModel
    {
        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Use same Billing Address as Delivery Addess")]
        public bool SameBillingAsDelivery { get; set; }

        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }
        [Required]
        [Display(Name = "Delivery City")]
        public string DeliveryCity { get; set; }

        [DataType(DataType.PostalCode)]
        [Required]
        [Display(Name = "Delivery Zip")]
        public string DeliveryZip { get; set; }

        [RequiredIfFalse("SameBillingAsDelivery")]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [RequiredIfFalse("SameBillingAsDelivery")]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; }

        [DataType(DataType.PostalCode)]
        [RequiredIfFalse("SameBillingAsDelivery")]
        [Display(Name = "Billing Zip")]
        public string BillingZip { get; set; }

        [Phone]
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
    }
}