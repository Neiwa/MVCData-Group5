using MVCData_Group5.Models.ViewModels;
using MVCData_Group5.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCData_Group5.Controllers
{
    public class CustomerController : MovieDbController
    {
        // GET: Order
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                string email = GetLoggedInUser()?.Customer?.EmailAddress;
                if (email != null)
                    return RedirectToAction("Orders", new { email });
            }
            ViewBag.CustomerSL = new SelectList(db.Customers, "EmailAddress", "EmailAddress");
            return View();
        }

        public ActionResult Orders(string email)
        {
            if(email == null)
            {
                return RedirectToAction("Index");
            }

            Models.Database.Customer customer = db.Customers.FirstOrDefault(c => c.EmailAddress == email);

            if(customer == null)
            {
                return RedirectToAction("Index");
            }

            var query = customer.Orders.Select(o => new CustomerOrderViewModel
            {
                OrderDate = o.OrderDate,
                Movies = o.OrderRows.GroupBy(r => r.Movie).Select(g => new ShoppingCartMovieViewModel
                {
                    Id = g.Key.Id,
                    Title = g.Key.Title,
                    Price = g.First().Price,
                    Amount = g.Count()
                }).ToList(),
                TotalCost = o.OrderRows.Sum(r => r.Price),
                MovieCount = o.OrderRows.Count()
            });

            var model = new CustomerOrdersViewModel
            {
                Name = string.Format("{0} {1}", customer.Firstname, customer.Lastname),
                EmailAddress = customer.EmailAddress,
                Orders = query.ToList()
            };

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult MostExpensiveOrder()
        {
            var order = db.Orders.OrderByDescending(o => o.OrderRows.Sum(r => r.Price)).First();
            var model = new OrderSummaryViewModel
            {
                OrderDate = order.OrderDate,
                TotalCost = order.OrderRows.Sum(r => r.Price),
                MovieCount = order.OrderRows.Count(),
                Name = $"{order.Customer.Firstname} {order.Customer.Lastname}"
            };

            return PartialView("_SingleOrderSummaryPartial", model);
        }
    }
}