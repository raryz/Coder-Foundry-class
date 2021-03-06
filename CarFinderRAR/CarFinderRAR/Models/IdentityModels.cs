﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CarFinderRAR.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public async Task<List<string>> MakesForYear(string year)   // need to define the stored procedure on the database
        {
            var yearParam = new SqlParameter("@year", year);

            return await this.Database
                .SqlQuery<string>("MakesForYear @year", yearParam).ToListAsync();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public async Task<List<string>> GetYears()              // need to define the stored procedure on the database
        {
            return await this.Database
                .SqlQuery<string>("GetYears").ToListAsync();
        }

        public async Task<List<string>> GetMakes(int year)   
        {
            var yearParam = new SqlParameter("@year", year);

            return await this.Database
                .SqlQuery<string>("GetMakes @year", yearParam).ToListAsync();
        }

        public async Task<List<string>> GetModels(int year, string make)   // need to define the stored procedure on the database
        {
            var yearParam = new SqlParameter("@year", year);
            var makeParam = new SqlParameter("@make", make);

            return await this.Database
                .SqlQuery<string>("GetModels @year, @make", yearParam, makeParam).ToListAsync();
        }

        public async Task<List<string>> GetTrims(int year, string make, string model)
        {
            var yearParam = new SqlParameter("@year", year);
            var makeParam = new SqlParameter("@make", make);
            var modelParam = new SqlParameter("@model", model);

            return await this.Database
                .SqlQuery<string>("GetTrims @year, @make, @model", yearParam, makeParam, modelParam).ToListAsync();
        }
    }
}