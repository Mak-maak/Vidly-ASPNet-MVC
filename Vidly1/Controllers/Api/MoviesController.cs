using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly1.DTOs;
using Vidly1.Models;
using System.Data.Entity;

namespace Vidly1.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/Customers
        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies.Include(c => c.Genre);

            if (!string.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(c => c.MovieName.Contains(query));

            var movieDtos = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        //GET api/Customer/1
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST api/Customers
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (User.IsInRole(Roles.CanManageEntities))
            {
                if (!ModelState.IsValid)
                    BadRequest();

                Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);

                _context.Movies.Add(movie);
                _context.SaveChanges();

                movieDto.Id = movie.Id;

                return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
            }

            return BadRequest();
        }

        //PUT api/Customers/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (User.IsInRole(Roles.CanManageEntities))
            {
                if (!ModelState.IsValid)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                Movie movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

                if (movieInDb == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                Mapper.Map(movieDto, movieInDb);

                _context.SaveChanges();
            }
        }

        // DELETE api/Customers/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            if (User.IsInRole(Roles.CanManageEntities))
            {
                Movie movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

                if (movieInDb == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                _context.Movies.Remove(movieInDb);
                _context.SaveChanges();
            }
        }
    }
}
