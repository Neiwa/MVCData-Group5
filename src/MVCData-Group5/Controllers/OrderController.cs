using MVCData_Group5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCData_Group5.Controllers
{
    public class OrderController : MovieDbController
    {
        // GET: Order
        public ActionResult Index()
        {
            ViewBag.CustomerSL = new SelectList(db.Customers, "EmailAddress", "EmailAddress");
            return View();
        }

        [ChildActionOnly]
        public ActionResult View(CustomerOrderViewModel model)
        {

            return PartialView(model);
        }

        public ActionResult ViewForCustomer(string email)
        {
            if(email == null)
            {
                RedirectToAction("Index");
            }

            Models.Database.Customer customer = db.Customers.FirstOrDefault(c => c.EmailAddress == email);
            var query = customer.Orders.Select(o => new CustomerOrderViewModel
            {
                OrderDate = o.OrderDate,
                Movies = o.OrderRows.GroupBy(r => r.Movie).Select(g => new ShoppingCartMovieViewModel
                {
                    Id = g.Key.Id,
                    Title = g.Key.Title,
                    Price = g.Key.Price,
                    Amount = g.Count()
                }).ToList(),
                TotalCost = o.OrderRows.Sum(r => r.Price)
            });

            var model = new CustomerOrdersViewModel
            {
                Name = string.Format("{0} {1}", customer.Firstname, customer.Lastname),
                EmailAddress = customer.EmailAddress,
                Orders = query.ToList()
            };

            return View(model);
        }

        public ActionResult MostExpensiveOrder()
        {
            var order = db.Orders.OrderByDescending(o => o.OrderRows.Sum(r => r.Price)).First();
            var model = new CustomerOrderViewModel
            {
                OrderDate = order.OrderDate,
                TotalCost = order.OrderRows.Sum(r => r.Price),
                Movies = order.OrderRows.GroupBy(r => r.Movie).Select(g => new ShoppingCartMovieViewModel
                {
                    Id = g.Key.Id,
                    Title = g.Key.Title,
                    Price = g.Key.Price,
                    Amount = g.Count()
                }).ToList()
            };

            return PartialView("View", model);
        }
    }
}