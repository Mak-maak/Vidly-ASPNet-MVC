using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly1.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string MovieName { get; set; }

        public int GenreId { get; set; }

        public GenreDto Genre { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

    }
}