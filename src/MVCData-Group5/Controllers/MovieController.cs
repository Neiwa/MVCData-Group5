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
    public class MovieController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Movie
        public ActionResult Index(int length = 10, int page = 1)
        {
            // Decrement page for easier calculations, but not less than 0
            page = --page < 0 ? 0 : page;
            // Ensure length is positive
            length = length > 0 ? length : 10;
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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}