using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly1.ViewModels;

namespace Vidly1.Models
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).ToList();

            if(User.IsInRole(Roles.CanManageEntities))
                return View("List", movies);

            return View("ReadOnlyList", movies);
        }

        [Authorize]
        public ActionResult New()
        {
            if (User.IsInRole(Roles.CanManageEntities))
            {
                IEnumerable<Genre> genres = _context.Genres.ToList();
                MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
                {
                    Genres = genres
                };

                return View("MovieForm", movieFormViewModel);
            }

            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            Movie movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (User.IsInRole(Roles.CanManageEntities))
            {
                if (!ModelState.IsValid)
                {
                    MovieFormViewModel viewModel = new MovieFormViewModel(movie)
                    {
                        Genres = _context.Genres.ToList()
                    };

                    return View("MovieForm", viewModel);
                }

                // adding new movie
                if (movie.Id == 0)
                {
                    movie.DateAdded = DateTime.Today;
                    _context.Movies.Add(movie);
                }
                else
                {
                    // updating existing movie
                    Movie movieInDb = _context.Movies.SingleOrDefault(m => m.Id == movie.Id);

                    // map props that you want to update
                    movieInDb.MovieName = movie.MovieName;
                    movieInDb.ReleaseDate = movie.ReleaseDate;
                    movieInDb.Stock = movie.Stock;
                    movie.GenreId = movie.GenreId;
                }
                _context.SaveChanges();

                return RedirectToAction("Index", "Movie");
            }

            return View("ReadOnlyList");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            if (User.IsInRole(Roles.CanManageEntities))
            {
                Movie movie = _context.Movies.SingleOrDefault(c => c.Id == id);

                IEnumerable<Genre> genres = _context.Genres.ToList();
                MovieFormViewModel movieFormViewModel = new MovieFormViewModel(movie)
                {
                    Genres = genres
                };

                return View("MovieForm", movieFormViewModel);
            }

            return View("ReadOnlyList");
        }
    }
}