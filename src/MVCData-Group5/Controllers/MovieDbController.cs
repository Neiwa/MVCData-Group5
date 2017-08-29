using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCData_Group5.Models;
using MVCData_Group5.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCData_Group5.Controllers
{
    public abstract class MovieDbController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected MessageContainer Messages
        {
            get
            {
                var list = Session[DataKeys.MessageContainer] as MessageContainer;
                if (list == null)
                {
                    list = new MessageContainer();
                    Session[DataKeys.MessageContainer] = list;
                }
                return list;
            }
        }

        protected UserManager<ApplicationUser> UserManager { get; set; }

        protected ApplicationUser GetLoggedInUser()
        {
            if (!Request.IsAuthenticated)
                return null;

            return UserManager.FindById(User.Identity.GetUserId());
        }

        public MovieDbController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
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