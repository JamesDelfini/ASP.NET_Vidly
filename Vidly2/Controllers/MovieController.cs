using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using Vidly2.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly2.Controllers
{
    public class MovieController : Controller
    {
        public ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movie
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }
        public ActionResult Show(int? id)
        {
            var movies = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movies == null)
                HttpNotFound();

            return View(movies);
        }
        public ActionResult Create()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MoviesFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movies = _context.Movies.Single(m => m.Id == id);
            var viewModels = new MoviesFormViewModel(movies)
            {
                Id = movies.Id,
                Title = movies.Title,
                ReleaseDate = movies.ReleaseDate,
                Stock = movies.Stock,
                GenreId = movies.GenreId,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movies movies)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MoviesFormViewModel(movies)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            movies.DatePublish = DateTime.Now;
            if (movies.Id == 0)
                _context.Movies.Add(movies);
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movies.Id);
                movieInDb.Title = movies.Title;
                movieInDb.ReleaseDate = movies.ReleaseDate;
                movieInDb.DatePublish = movies.DatePublish;
                movieInDb.GenreId = movies.GenreId;
                movieInDb.Stock = movies.Stock;
            }

            // Run it on Debug Mode
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Movie");
        }
    }
}