using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCData_Group5.Models.ViewModels
{
    public class CreateMovieViewModel
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Director { get; set; }

        [Required]
        [Range(1900, 9999)]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [DataType(DataType.ImageUrl)]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}