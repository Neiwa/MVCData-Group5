using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCData_Group5.Models.Database
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Director { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual ICollection<OrderRow> OrderRows { get; set; }
    }
}