using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCData_Group5.Models.Database
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(250)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(250)]
        public string BillingAddress { get; set; }

        [Required]
        [MaxLength(20)]
        [DataType(DataType.PostalCode)]
        public string BillingZip { get; set; }

        [Required]
        [MaxLength(250)]
        public string BillingCity { get; set; }

        [Required]
        [MaxLength(250)]
        public string DeliveryAddress { get; set; }

        [Required]
        [MaxLength(20)]
        [DataType(DataType.PostalCode)]
        public string DeliveryZip { get; set; }

        [Required]
        [MaxLength(250)]
        public string DeliveryCity { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}