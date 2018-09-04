using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly2.Models;

namespace Vidly2.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IEnumerable<Movies> GetMovies()
        {
            return _context.Movies.ToList();
        }

        // GET /api/movies/1
        public Movies GetMovies(int id)
        {
            var movies = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movies == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return movies;
        }

        [HttpPost]
        public Movies CreateMovies(Movies movies)
        {
            movies.DatePublish = DateTime.Now;

            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Movies.Add(movies);
            _context.SaveChanges();

            return movies;
        }

        // PUT /api/movies
        [HttpPut]
        public void UpdateMovies(int id, Movies movies)
        {
            movies.DatePublish = DateTime.Now;

            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var moviesInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (moviesInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            moviesInDb.Title = movies.Title;
            moviesInDb.GenreId = movies.GenreId;
            moviesInDb.ReleaseDate = movies.ReleaseDate;
            moviesInDb.DatePublish = movies.DatePublish;
            moviesInDb.Stock = movies.Stock;

            _context.SaveChanges();
        }

        // DELETE /api/movies/1
        [HttpDelete]
        public void DeleteMovies(int id)
        {
            var movies = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movies == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movies);
            _context.SaveChanges();
        }
    }
}
