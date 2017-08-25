using MVCData_Group5.Models;
using MVCData_Group5.Models.Database;
using MVCData_Group5.Models.ViewModels;
using MVCData_Group5.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCData_Group5.Controllers
{
    public class MovieController : MovieDbController
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: Movie
        public ActionResult List(int length = 9, int page = 1)
        {
            // Decrement page for easier calculations, but not less than 0
            page = --page < 0 ? 0 : page;
            // Ensure length is positive
            length = length > 0 ? length : 9;

            ViewBag.Pages = (db.Movies.Count() + length - 1) / length;
            ViewBag.CurrentPage = page + 1;
            ViewBag.Length = length;

            var model = db.Movies.OrderBy(m => m.Id).Skip(length * page).Take(length).Select(m => new DisplayMovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Price = m.Price,
                ReleaseYear = m.ReleaseYear,
                ImageUrl = m.ImageUrl
            });

            return View(model.ToList());
        }

        [ChildActionOnly]
        public ActionResult MovieEntry(DisplayMovieViewModel model)
        {
            ViewBag.ReturnUrl = Request.Url.PathAndQuery;

            return PartialView("_movieRowPartial", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CreateMovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                Movie m = new Movie
                {
                    Title = movie.Title,
                    Director = movie.Director,
                    ReleaseYear = movie.ReleaseYear,
                    Price = movie.Price,
                    ImageUrl = movie.ImageUrl
                };
                db.Movies.Add(m);
                db.SaveChanges();

                TempData[DataKeys.MovieAdded] = movie.Title;

                return RedirectToAction("Add");
            }

            return View(movie);
        }

        public ActionResult PopularMovies(int count = 5)
        {
            count = count < 1 ? 1 : count;

            var model = db.Movies.OrderByDescending(m => m.OrderRows.Count()).Take(count).Select(m => new DisplayMovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Price = m.Price,
                ReleaseYear = m.ReleaseYear,
                ImageUrl = m.ImageUrl
            });

            return PartialView("MultipleMovies", model.ToList());
        }

        public ActionResult NewestMovies(int count = 5)
        {
            count = count < 1 ? 1 : count;

            var model = db.Movies.OrderByDescending(m => m.ReleaseYear).Take(count).Select(m => new DisplayMovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Price = m.Price,
                ReleaseYear = m.ReleaseYear,
                ImageUrl = m.ImageUrl
            });

            return PartialView("MultipleMovies", model.ToList());
        }

        public ActionResult OldestMovies(int count = 5)
        {
            count = count < 1 ? 1 : count;

            var model = db.Movies.OrderBy(m => m.ReleaseYear).Take(count).Select(m => new DisplayMovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Price = m.Price,
                ReleaseYear = m.ReleaseYear,
                ImageUrl = m.ImageUrl
            });

            return PartialView("MultipleMovies", model.ToList());
        }

        public ActionResult CheapestMovies(int count = 5)
        {
            count = count < 1 ? 1 : count;

            var model = db.Movies.OrderBy(m => m.Price).Take(count).Select(m => new DisplayMovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Price = m.Price,
                ReleaseYear = m.ReleaseYear,
                ImageUrl = m.ImageUrl
            });

            return PartialView("MultipleMovies", model.ToList());
        }
    }
}