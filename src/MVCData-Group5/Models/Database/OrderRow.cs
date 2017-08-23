using System.ComponentModel.DataAnnotations;

namespace MVCData_Group5.Models.Database
{
    public class OrderRow
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Movie Movie { get; set; }
    }
}