namespace Personal_Website_2.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Personal_Website_2.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Personal_Website_2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Personal_Website_2.Models.ApplicationDbContext context)
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
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r=>r.Name == "Admin")){
                roleManager.Create(new IdentityRole { Name = "Admin"});
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if(!context.Users.Any(r=>r.Email == "admin@coderfoundry.com")){
                userManager.Create(new ApplicationUser{
                    UserName = "admin@coderfoundry.com",
                    Email = "admin@coderfoundry.com",
                    FirstName = "Bob",
                    LastName = "Ryzowicz",
                    DisplayName = "Bob Ryzowicz"
                }, "Abc123!");

                }

            var userId = userManager.FindByEmail("admin@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Admin");

            if (!context.Users.Any(r => r.Email == "lreaves@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "lreaves@coderfoundry.com",
                    Email = "lreaves@coderfoundry.com",
                }, "Password-1");

            }

            userId = userManager.FindByEmail("lreaves@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");

            if (!context.Users.Any(r => r.Email == "bdavis@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "bdavis@coderfoundry.com",
                    Email = "bdavis@coderfoundry.com",
                }, "Password-1");

            }

             userId = userManager.FindByEmail("bdavis@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");

            if (!context.Users.Any(r => r.Email == "ajensen@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ajensen@coderfoundry.com",
                    Email = "ajensen@coderfoundry.com",
                }, "Password-1");

            }

            userId = userManager.FindByEmail("ajensen@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");

            if (!context.Users.Any(r => r.Email == "tjones@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "tjones@coderfoundry.com",
                    Email = "tjones@coderfoundry.com",
                }, "Password-1");

            }

            userId = userManager.FindByEmail("tjones@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");

            if (!context.Users.Any(r => r.Email == "tparrish@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "tparrish@coderfoundry.com",
                    Email = "tparrish@coderfoundry.com",
                }, "Password-1");

            }

            userId = userManager.FindByEmail("tparrish@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");


          }   // end Seed
        }
    }

