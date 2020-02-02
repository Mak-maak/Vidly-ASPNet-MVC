using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly1.Models;

namespace Vidly1.DTOs
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}