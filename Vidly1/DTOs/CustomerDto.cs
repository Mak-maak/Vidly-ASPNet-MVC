using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly1.Validations;

namespace Vidly1.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        public bool IsSubscribedToNewsletter { get; set; }
        
        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        //[CheckAgeMin18]
        public DateTime? Birthdate { get; set; }
    }
}