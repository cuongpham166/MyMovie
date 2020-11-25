using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using MyMovie.Data;
using MyMovie.Models;

namespace MyMovie.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GenreController(ApplicationDbContext db)
        {
            _db = db;
        }

        /*public IActionResult Index()
        {
            IEnumerable<Genre> genreList = _db.Genre;
            return View(genreList);
        }*/

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_db.Genre.ToDataSourceResult(request));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre obj)
        {
            if (ModelState.IsValid)
            {
                _db.Genre.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, Models.Genre genre)
        {
            if (genre != null)
            {
                _db.Remove(genre);
                _db.SaveChanges();
            }
            return Json(new[] { genre }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public IActionResult Edit([DataSourceRequest] DataSourceRequest request, Models.Genre genre)
        {
            if (genre != null && ModelState.IsValid)
            {
                _db.Genre.Update(genre);
                _db.SaveChanges();
            }
            return Json(new[] { genre }.ToDataSourceResult(request, ModelState));
        }
    }
}