namespace BugTrackerRAR.Migrations
{
    using BugTrackerRAR.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTrackerRAR.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTrackerRAR.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(r => r.Email == "admin@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "admin@coderfoundry.com",
                    Email = "admin@coderfoundry.com",
                    FirstName = "Bob",
                    LastName = "Ryzowicz",
                    DisplayName = "Bob Ryzowicz"
                }, "Abc123!");

            }

            //var userId = userManager.FindByEmail("admin@coderfoundry.com").Id;
            //userManager.AddToRole(userId, "Admin");

            if (!context.Users.Any(r => r.Email == "johnsubj@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "johnsub@coderfoundry.com",
                    Email = "johnsub@coderfoundry.com",
                    FirstName = "John",
                    LastName = "Submitter",
                    DisplayName = "John Submitter"
                }, "Abc123!");

            }

            var userId = userManager.FindByEmail("johnsub@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Submitter");

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
        }
    }
}
