using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using MyMovie.Data;
using MyMovie.Models;
using MyMovie.ViewModel;

namespace MyMovie.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var genres = _db.Genre.ToList();
            ViewData["genres"] = genres;
            ViewData["defaultGenre"] = genres.First();
            return View();
        }

        public IActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_db.Movie.ToDataSourceResult(request));
        }

        // ViewModel Variante
        /*public IActionResult Create()
        {
            MovieGenreViewModel myModel = new MovieGenreViewModel();
            myModel.Genre = _db.Genre;
            myModel.Movie = _db.Movie;
            return View(myModel);
        }*/

        public IActionResult Create()
        {
            return View();
        }

        public JsonResult GetGenres()
        {
            return Json(_db.Genre, new Newtonsoft.Json.JsonSerializerSettings());
        }

        /*[HttpPost]
        public IActionResult Create(Movie obj){
            if (ModelState.IsValid)
            {
                _db.Movie.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }*/

        [HttpPost]
        public IActionResult Create (MovieGenreVM moviegenreVM)
        {
            if (ModelState.IsValid)
            {
                Movie movie = new Movie();
                movie.ImageUrl = moviegenreVM.Movie.ImageUrl;
                movie.Overview = moviegenreVM.Movie.Overview;
                movie.Rating = moviegenreVM.Movie.Rating;
                movie.ReleaseDate = moviegenreVM.Movie.ReleaseDate;
                movie.Score = moviegenreVM.Movie.Score;
                movie.Title = moviegenreVM.Movie.Title;
                _db.Movie.Add(movie);
                _db.SaveChanges();

                foreach (var genreID in moviegenreVM.selectedGenres)
                {
                    MovieGenre movieGenre = new MovieGenre();
                    movieGenre.GenreId = genreID;
                    movieGenre.MovieId = movie.Id;
                    _db.MovieGenre.Add(movieGenre);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moviegenreVM);
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, Models.Movie movie)
        {
            if (movie != null)
            {
                _db.Remove(movie);
                _db.SaveChanges();
            }
            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public IActionResult Edit([DataSourceRequest] DataSourceRequest request, Models.Movie movie)
        {
            if (movie != null && ModelState.IsValid)
            {
                _db.Movie.Update(movie);
                _db.SaveChanges();
            }
            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }
        
        public IActionResult Detail(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Movie.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
    }
}