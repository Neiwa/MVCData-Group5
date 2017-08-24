using System.ComponentModel.DataAnnotations;

namespace MVCData_Group5.Models.ViewModels
{
    public class NewUserViewModel
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public bool SameBillingAsDelivery { get; set; }

        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public string DeliveryCity { get; set; }

        [DataType(DataType.PostalCode)]
        [Required]
        public string DeliveryZip { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }

        [DataType(DataType.PostalCode)]
        public string BillingZip { get; set; }

        [Phone]
        [Required]
        public string PhoneNo { get; set; }
    }
}