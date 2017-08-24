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
    public class CartController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected ShoppingCart ShoppingCart
        {
            get
            {
                ShoppingCart _cart = Session[DataKeys.ShoppingCart] as ShoppingCart;
                if (_cart == null)
                {
                    _cart = new ShoppingCart();
                    Session[DataKeys.ShoppingCart] = _cart;
                }
                return _cart;
            }
        }

        protected double ShoppingCartTotal
        {
            get
            {
                object total = Session[DataKeys.ShoppingCartTotal];
                if (total is double)
                {
                    return (double)total;
                }
                Session[DataKeys.ShoppingCartTotal] = 0D;
                return 0D;
            }
            set
            {
                Session[DataKeys.ShoppingCartTotal] = value;
            }
        }

        [HttpPost]
        public ActionResult AddToCart(int movieId, string returnUrl)
        {
            Movie movie = db.Movies.Find(movieId);
            if (movie != null)
            {
                ShoppingCart.Add(movieId);
                ShoppingCartTotal += movie.Price;
            }
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Movie");
            }
        }

        private double UpdateShoppingCartTotal(List<Movie> movies = null)
        {
            var movieIds = ShoppingCart.Keys.Where(k => ShoppingCart[k] > 0).ToArray();
            if (movies == null || movieIds.Except(movies.Select(m => m.Id)).Count() > 0)
            {
                movies = db.Movies.Where(m => movieIds.Contains(m.Id)).ToList();
            }

            double total = movies.Sum(m => m.Price * ShoppingCart[m.Id]);
            ShoppingCartTotal = total;
            return total;
        }

        public ActionResult Index()
        {
            var movieIds = ShoppingCart.Keys.Where(k => ShoppingCart[k] > 0).ToArray();

            IQueryable<Movie> query = db.Movies
                .Where(m => movieIds.Contains(m.Id));
            ViewBag.Query = query.ToString();

            List<Movie> movies = query.ToList();
            var model = movies.Select(m => new ShoppingCartMovieViewModel
            {
                Title = m.Title,
                Price = m.Price,
                AmountInCart = ShoppingCart[m.Id]
            });

            ShoppingCartTotal = model.Sum(vm => vm.Price * vm.AmountInCart);

            ViewBag.MovieCount = ShoppingCart.AmountItems;
            ViewBag.OrderTotal = UpdateShoppingCartTotal(movies);

            return View(model);
        }

        [HttpPost]
        public ActionResult ReduceAllByOne()
        {
            ShoppingCart.RemoveOneOfAll();

            return RedirectToAction("Index");
        }

        public ActionResult CheckOut(CheckOutViewModel model)
        {
            model.MovieCount = ShoppingCart.AmountItems;
            model.OrderTotal = UpdateShoppingCartTotal();

            return View(model);
        }

        [HttpPost]
        [ActionName("CheckOut")]
        public ActionResult CheckOutSubmit(CheckOutViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.EmailAddress == model.EmailAddress);
                if (customer != null && CheckOut(customer))
                {
                    return RedirectToAction("CheckOutComplete");
                }
            }
            model.MovieCount = ShoppingCart.AmountItems;
            model.OrderTotal = UpdateShoppingCartTotal();

            return View(model);
        }

        private bool CheckOut(Customer customer)
        {
            var movieIds = ShoppingCart.Keys.Where(k => ShoppingCart[k] > 0).ToArray();
            IQueryable<Movie> query = db.Movies.Where(m => movieIds.Contains(m.Id));

            var movies = query.ToDictionary(m => m.Id);

            double total = 0D;

            Order order = new Order();
            order.Customer = customer;
            order.OrderDate = DateTime.Now;
            order.OrderRows = new HashSet<OrderRow>();
            foreach (var key in ShoppingCart.Keys)
            {
                for (int i = 0; i < ShoppingCart[key]; i++)
                {
                    total += movies[key].Price;
                    order.OrderRows.Add(new OrderRow { Movie = movies[key], Order = order, Price = movies[key].Price });
                }
            }

            // Ensure that a movie price has not changed between displaying total to customer and actual checkout
            if (total != ShoppingCartTotal)
            {
                return false;
            }

            db.Orders.Add(order);
            db.SaveChanges();

            ShoppingCart.Clear();

            return true;
        }

        public ActionResult CheckOutNewCustomer(CheckOutNewUserViewModel model)
        {
            model.MovieCount = ShoppingCart.AmountItems;
            model.OrderTotal = UpdateShoppingCartTotal();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("CheckOutNewCustomer")]
        public ActionResult CheckOutNewCustomerSubmit(CheckOutNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    DeliveryAddress = model.DeliveryAddress,
                    DeliveryCity = model.DeliveryCity,
                    DeliveryZip = model.DeliveryZip,
                    EmailAddress = model.EmailAddress,
                    PhoneNo = model.PhoneNo
                };

                if (model.SameBillingAsDelivery)
                {
                    customer.BillingAddress = model.DeliveryAddress;
                    customer.BillingCity = model.DeliveryCity;
                    customer.BillingZip = model.DeliveryZip;
                }
                else
                {
                    customer.BillingAddress = model.BillingAddress;
                    customer.BillingCity = model.BillingCity;
                    customer.BillingZip = model.BillingZip;
                }

                if (CheckOut(customer))
                {
                    return RedirectToAction("CheckOutComplete");
                }
            }
            model.MovieCount = ShoppingCart.AmountItems;
            model.OrderTotal = UpdateShoppingCartTotal();

            return View(model);
        }

        public ActionResult CheckOutComplete()
        {

            return View();
        }
    }
}