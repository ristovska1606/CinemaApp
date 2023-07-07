using CinemaApp.Domain.DomainModels;
using CinemaApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CinemaApp.Web.Controllers
{
    [Route("[controller]")]
    public class MoivesController : Controller
    {

        private readonly IMovieService _movieService;
        public MoivesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: Movies
        public IActionResult Index()
        {
            return View(_movieService.GetAllMoviesAsList());
        }

        // GET: Movies/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetSpecificMovie(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(String MovieTitle, String MovieCover, DateTime ValidFrom, DateTime ValidTo, String TicketPrice)
        {
            Movie movie = null;
            if (ModelState.IsValid)
            {
                movie =  _movieService.CreateNewMovieWithParametars(MovieTitle, MovieCover,ValidFrom, ValidTo,TicketPrice);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetSpecificMovie(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _movieService.DeleteMovie(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
