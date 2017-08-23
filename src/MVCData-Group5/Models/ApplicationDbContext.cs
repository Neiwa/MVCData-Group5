using Microsoft.AspNet.Identity.EntityFramework;
using MVCData_Group5.Models.Database;
using System.Data.Entity;

namespace MVCData_Group5.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}