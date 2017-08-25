namespace MVCData_Group5.Migrations
{
    using MVCData_Group5.Models.Database;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCData_Group5.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVCData_Group5.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Movies.AddOrUpdate(m => m.Title,
                new Movie { Title = "Interstellar", Director = "Christopher Nolan", ReleaseYear = 2014, ImageUrl = "https://images-na.ssl-images-amazon.com/images/M/MV5BMjIxNTU4MzY4MF5BMl5BanBnXkFtZTgwMzM4ODI3MjE@._V1_UX182_CR0,0,182,268_AL_.jpg", Price = 179 },
                new Movie { Title = "The Wolf of Wall Street", Director = "Martin Scorcese", ReleaseYear = 2013, Price = 119 },
                new Movie { Title = "Pulp Fiction", Director = "Quentin Tarantino", ReleaseYear = 1994, ImageUrl = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTkxMTA5OTAzMl5BMl5BanBnXkFtZTgwNjA5MDc3NjE@._V1_UX182_CR0,0,182,268_AL_.jpg", Price = 49 },
                new Movie { Title = "Hobbit: Battle of the five armies", Director = "Peter Jackson", ReleaseYear = 2014, Price = 179 },
                new Movie { Title = "The Godfather", Director = "Francis Ford Coppola", ReleaseYear = 1972, ImageUrl = "https://images-na.ssl-images-amazon.com/images/M/MV5BZTRmNjQ1ZDYtNDgzMy00OGE0LWE4N2YtNTkzNWQ5ZDhlNGJmL2ltYWdlL2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_UY268_CR3,0,182,268_AL_.jpg", Price = 59 },
                new Movie { Title = "The Dark Knight", Director = "Christopher Nolan", ReleaseYear = 2008, ImageUrl = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_UX182_CR0,0,182,268_AL_.jpg", Price = 129 },
                new Movie { Title = "12 Angry Men", Director = "Sidney Lumet", ReleaseYear = 1957, ImageUrl = "https://images-na.ssl-images-amazon.com/images/M/MV5BODQwOTc5MDM2N15BMl5BanBnXkFtZTcwODQxNTEzNA@@._V1_UX182_CR0,0,182,268_AL_.jpg", Price = 29 },
                new Movie { Title = "Schindler's List", Director = "Steven Spielberg", ReleaseYear = 1993, ImageUrl = "https://images-na.ssl-images-amazon.com/images/M/MV5BNDE4OTMxMTctNmRhYy00NWE2LTg3YzItYTk3M2UwOTU5Njg4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_UX182_CR0,0,182,268_AL_.jpg", Price = 59 },
                new Movie { Title = "The Good, the Bad and the Ugly", Director = "Sergio Leone", ReleaseYear = 1966, ImageUrl = "https://images-na.ssl-images-amazon.com/images/M/MV5BOTQ5NDI3MTI4MF5BMl5BanBnXkFtZTgwNDQ4ODE5MDE@._V1_UX182_CR0,0,182,268_AL_.jpg", Price = 19 },
                new Movie { Title = "Fight Club", Director = "David Fincher", ReleaseYear = 1999, ImageUrl = "https://images-na.ssl-images-amazon.com/images/M/MV5BZGY5Y2RjMmItNDg5Yy00NjUwLThjMTEtNDc2OGUzNTBiYmM1XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_UX182_CR0,0,182,268_AL_.jpg", Price = 199 },
                new Movie { Title = "Forrest Gump", Director = "Robert Zemeckis", ReleaseYear = 1994, ImageUrl = "https://images-na.ssl-images-amazon.com/images/M/MV5BYThjM2MwZGMtMzg3Ny00NGRkLWE4M2EtYTBiNWMzOTY0YTI4XkEyXkFqcGdeQXVyNDYyMDk5MTU@._V1_UY268_CR10,0,182,268_AL_.jpg", Price = 153 }
                );
        }
    }
}
