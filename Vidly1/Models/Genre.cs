using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly1.Models
{
    public class Genre
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(100)]
        public string GenreName { get; set; }
    }
}