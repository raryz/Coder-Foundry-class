using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugtrackerRAR_2.Models.Helpers
{
    public class UserTicketsHelper
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        //public ApplicationUser ListDeveloperOnTicket(int ticketId)
        //{
        //    var role = db.Roles.First(r => r.Name == "Developer");

        //    //role should contain one role record with the id and the name.

        //    return db.Users.Where(u => u.Id.Any(p => p.Id == projectId))
        //        .Intersect(db.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id))).ToList();

        //}

        //Chunked
        //var project = db.Projects.Find(projectId);
        //var user = db.Users.Find(userId);
        //var userList = project.Users;
        //if (userList.Contains(user))
        //{
        //    return true;
        //}
        //return false;
        //}
    }

        
}

