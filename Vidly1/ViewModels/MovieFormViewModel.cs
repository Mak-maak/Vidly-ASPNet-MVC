using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly1.Models;

namespace Vidly1.ViewModels
{
    public class MovieFormViewModel
    {
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            MovieName = movie.MovieName;
            ReleaseDate = movie.ReleaseDate;
            Stock = movie.Stock;
            GenreId = movie.GenreId;
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Required]
        [Range(1, 20)]
        public int? Stock { get; set; }

        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }
    }
}