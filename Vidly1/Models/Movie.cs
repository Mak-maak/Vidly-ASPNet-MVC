using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly1.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string MovieName { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "The value of stock should be between 1 and 20.")]
        public int Stock { get; set; }

        public int AvailableStock { get; set; }

        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}