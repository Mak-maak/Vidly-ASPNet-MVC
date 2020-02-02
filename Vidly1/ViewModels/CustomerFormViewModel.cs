using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly1.Models;
using Vidly1.Validations;

namespace Vidly1.ViewModels
{
    public class CustomerFormViewModel
    {
        public CustomerFormViewModel(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            Birthdate = customer.Birthdate;
            MembershipTypeId = customer.MembershipTypeId;

        }

        public CustomerFormViewModel()
        {
            Id = 0;
        }

        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        [CheckAgeMin18]
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }        

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Customer" : "New Customer";
            }
        }
    }
}