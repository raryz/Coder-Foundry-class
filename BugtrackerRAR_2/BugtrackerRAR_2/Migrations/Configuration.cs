namespace BugtrackerRAR_2.Migrations
{
    using BugtrackerRAR_2.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugtrackerRAR_2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }



        protected override void Seed(BugtrackerRAR_2.Models.ApplicationDbContext context)
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

            if (!context.Users.Any(r => r.Email == "guestadmin@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "guestadmin@coderfoundry.com",
                    Email = "guestadmin@coderfoundry.com",
                    FirstName = "Guest",
                    LastName = "Lastname",
                    DisplayName = "Guest Lastname"
                }, "Vbn456!");

            }

            var userId = userManager.FindByEmail("guestadmin@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Admin");

            if (!context.Users.Any(r => r.Email == "jerrydev@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jerrydev@coderfoundry.com",
                    Email = "jerrydev@coderfoundry.com",
                    FirstName = "Jerry",
                    LastName = "Dev",
                    DisplayName = "Jerry Dev"
                }, "Abc123!");

            }

            //var userId = userManager.FindByEmail("jerrydev@coderfoundry.com").Id;
            //userManager.AddToRole(userId, "Developer");

            //var userId = userManager.FindByEmail("maryproj@coderfoundry.com").Id;
            //userManager.AddToRole(userId, "Project Manager");

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
