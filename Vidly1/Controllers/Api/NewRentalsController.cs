using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly1.DTOs;
using Vidly1.Models;

namespace Vidly1.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRentalDto rentalDto)
        {
            var customer = _context.Customers.Single(
                c => c.Id == rentalDto.CustomerId);

            var movies = _context.Movies.Where(
                m => rentalDto.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.AvailableStock == 0)
                    return BadRequest("Movie is not available.");

                movie.AvailableStock--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();

            return Ok();
        }
    }
}
