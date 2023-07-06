using CinemaApp.Domain.DomainModels;
using CinemaApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
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

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, MovieTitle, MovieCover, ValidFrom, ValidTo, TicketPrice")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieService.CreateNewMovie(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
    }
}
