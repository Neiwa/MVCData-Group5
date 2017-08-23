using MVCData_Group5.Models;
using MVCData_Group5.Models.Database;
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
            // Decrement page for easier calculations
            page--;
            var model = db.Movies.OrderBy(m => m.Id).Skip(length * page).Take(length);

            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "Title,Director,ReleaseYear,Price,ImageUrl")]Movie movie)
        {
            if(ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();

                TempData[TempDataKeys.MovieAdded] = movie.Title;

                return RedirectToAction("Add");
            }

            return View(movie);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}