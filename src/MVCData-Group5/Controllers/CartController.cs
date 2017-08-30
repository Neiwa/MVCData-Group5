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
    [ShoppingCartAsSessionActionFilter]
    public class CartController : MovieDbController
    {
        class ShoppingCartAsCookieActionFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var cartCookie = filterContext.HttpContext.Request.Cookies[DataKeys.ShoppingCart];
                if (cartCookie == null)
                {
                    ((CartController)filterContext.Controller).ShoppingCart = new ShoppingCart();
                }
                else
                {
                    try
                    {
                        ((CartController)filterContext.Controller).ShoppingCart = ShoppingCart.Deserialize(cartCookie.Value);
                    }
                    catch
                    {
                        ((CartController)filterContext.Controller).ShoppingCart = new ShoppingCart();
                    }
                }
            }

            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                var cookie = new HttpCookie(DataKeys.ShoppingCart, ((CartController)filterContext.Controller).ShoppingCart.Serialize());
                cookie.Expires = DateTime.Now.AddDays(7);
                filterContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }
        class ShoppingCartAsSessionActionFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var cart = ((Controller)filterContext.Controller).Session[DataKeys.ShoppingCart] as ShoppingCart;
                if(cart == null)
                {
                    cart = new ShoppingCart();
                    ((Controller)filterContext.Controller).Session[DataKeys.ShoppingCart] = cart;
                }
                ((CartController)filterContext.Controller).ShoppingCart = cart;
            }
        }

        public ShoppingCart ShoppingCart { get; set; }

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

        protected double UpdateShoppingCartTotal(IEnumerable<Movie> movies = null)
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

        [ChildActionOnly]
        public ActionResult NavBarCartDisplay()
        {
            if(ShoppingCart.AmountItems > 0 && ShoppingCartTotal == 0)
            {
                UpdateShoppingCartTotal();
            }

            ViewBag.AmountItems = ShoppingCart.AmountItems;
            ViewBag.OrderTotal = ShoppingCartTotal;

            return PartialView("_NavBarCartDisplay");
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
            else
            {
                Messages.NewDanger("Failed to add movie to shopping cart!");
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

        public ActionResult Index()
        {
            var movieIds = ShoppingCart.Keys.Where(k => ShoppingCart[k] > 0).ToArray();

            IQueryable<Movie> query = db.Movies
                .Where(m => movieIds.Contains(m.Id));
            ViewBag.Query = query.ToString();

            List<Movie> movies = query.ToList();
            var movieVMs = movies.Select(m => new ShoppingCartMovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Price = m.Price,
                Amount = ShoppingCart[m.Id]
            });

            var model = new ShoppingCartViewModel
            {
                Movies = movieVMs.ToList(),
                MovieCount = ShoppingCart.AmountItems,
                OrderTotal = UpdateShoppingCartTotal(movies)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ModifyAmount(int id, int amount)
        {
            if (ShoppingCart.Keys.Contains(id) && amount >= 0)
            {
                ShoppingCart[id] = amount;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EmptyCart()
        {
            ShoppingCart.Clear();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ReduceAllByOne()
        {
            ShoppingCart.RemoveOneOfAll();

            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult CheckOut(CheckOutViewModel model)
        {
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("CheckOut")]
        public ActionResult CheckOutSubmit(CheckOutViewModel model)
        {
            if (ShoppingCart.AmountItems == 0)
            {
                Messages.NewInfo("You can't check out an empty order.");
                return RedirectToAction("Index", model);
            }

            if (ModelState.IsValid)
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.EmailAddress == model.EmailAddress);
                if (customer == null)
                {
                    // Error handling
                    Messages.NewDanger("No details found for the specified email address!");
                }

                if (customer != null && CheckOut(customer))
                {
                    return RedirectToAction("CheckOutComplete");
                }
            }

            return RedirectToAction("Index", model);
        }

        [ChildActionOnly]
        public ActionResult CheckOutOrderDetails()
        {
            var model = new CheckOutOrderDetailsViewModel
            {
                MovieCount = ShoppingCart.AmountItems,
                OrderTotal = UpdateShoppingCartTotal()
            };

            return PartialView(model);
        }

        public ActionResult CheckOutNewCustomer()
        {
            if (ShoppingCart.AmountItems == 0)
            {
                Messages.NewInfo("You can't check out an empty order.");
                return RedirectToAction("Index");
            }

            var model = new CheckOutNewUserViewModel();
            if (Request.IsAuthenticated)
            {
                ApplicationUser user = GetLoggedInUser();
                if (user.Customer != null)
                {
                    return RedirectToAction("Index");
                }
                model.EmailAddress = user.Email;
            }
            ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("CheckOutNewCustomer")]
        public ActionResult CheckOutNewCustomerSubmit(CheckOutNewUserViewModel model)
        {
            if (ShoppingCart.AmountItems == 0)
            {
                Messages.NewInfo("You can't check out an empty order.");
                return RedirectToAction("Index");
            }

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

                if (Request.IsAuthenticated)
                {
                    ApplicationUser user = GetLoggedInUser();
                    user.Customer = customer;
                    db.SaveChanges();
                }

                if (CheckOut(customer))
                {
                    return RedirectToAction("CheckOutComplete");
                }
            }

            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CheckOutLoggedIn()
        {
            if (ShoppingCart.AmountItems == 0)
            {
                Messages.NewInfo("You can't check out an empty order.");
                return RedirectToAction("Index");
            }

            ApplicationUser user = GetLoggedInUser();
            if (user.Customer == null)
            {
                // Redirect to customer creation
                return RedirectToAction("CheckOutNewCustomer");
            }
            else
            {
                if (CheckOut(user.Customer))
                {
                    return RedirectToAction("CheckOutComplete");
                }
            }



            return RedirectToAction("Index");
        }

        public ActionResult CheckOutComplete()
        {

            return View();
        }

        private bool CheckOut(Customer customer)
        {
            // Get info about movies in the shopping cart
            var movieIds = ShoppingCart.Keys.Where(k => ShoppingCart[k] > 0).ToArray();
            IQueryable<Movie> query = db.Movies.Where(m => movieIds.Contains(m.Id));

            var movies = query.ToDictionary(m => m.Id);

            IEnumerable<int> diff = movieIds.Except(movies.Keys);
            // Not all movies where found in the database
            if (diff.Count() > 0)
            {
                foreach (var key in diff)
                {
                    ShoppingCart.Remove(key);
                }
                Messages.NewDanger("One or movies in your shopping cart is no longer available, please review your order and then Check Out again.");

                return false;
            }

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
                Messages.NewWarning("One or more movies has changed price, please confirm the new total and then Check Out again.");
                return false;
            }

            db.Orders.Add(order);
            db.SaveChanges();

            ShoppingCart.Clear();
            UpdateShoppingCartTotal();

            return true;
        }
    }
}