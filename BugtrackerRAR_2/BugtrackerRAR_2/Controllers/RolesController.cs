using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BugtrackerRAR_2.Models;

namespace BugtrackerRAR_2.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }
    }

    //public class UserProjectsHelper
    //{
    //private UserManager<ApplicationUser> manager =
    //        new UserManager<ApplicationUser>(
    //            new UserStore<ApplicationUser>(
    //                new ApplicationDbContext()));

        //private ApplicationDbContext db = new ApplicationDbContext();


    //    public bool IsUserInProject(string userId, int projectId){
    //    //All in one line
    //return db.Projects.Find(projectId).Users.Any(u => u.Id == userId);
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

    //public void AddUserToProject( string userId, int projectId)
    //{
    //    if (!IsOnProject(userId, projectId))
    //   var project = db.Projects.Find(projectId);
    //    projectId.Users.Add(DBNull.Users.Find(userId));
    //    db.Entry(project).State = EntityState.Modified;
    //    db.SaveChanges();
    //}

    //     public void RemoveFromProject( string userId, int projectId)
    //{
    //    if (IsOnProject(userId, projectId))
    //   var project = db.Projects.Find(projectId);
    //    projectId.Users.Remove(DBNull.Users.Find(userId));
    //    db.Entry(project).State = EntityState.Modified;
    //    db.SaveChanges();
    //}

    //public ICollection<ApplicationUser> ListUsersOnProject(int projectId){
    //    return db.Projects.Find(projectId).Users;
    //}

    // public ICollection<Project> ListProjectsForUser(string userId){
    //     return db.Users.Find(userId).Projects;
    // }
    //    public ICollection<ApplicationUser> ListUsersNotOnProject(int projectId){
    //return db.Users.Where(u => u.Projects.All(projectId=>projectId.Id != projectId)).ToList();
        //return db.Users.Except(db.Projects.Find(projectId).Users.ToList()).ToList().;
            //return DBNull.Users.include("Projects").Where(uint=>!(uint.Projects.Any(  etc   ))
    //}
}
