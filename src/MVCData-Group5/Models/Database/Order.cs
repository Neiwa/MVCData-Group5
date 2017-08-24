using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCData_Group5.Models.Database
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderRow> OrderRows { get; set; }
    }
}