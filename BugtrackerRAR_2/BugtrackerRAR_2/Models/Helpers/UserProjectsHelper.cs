using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugtrackerRAR_2.Models.Helpers
{
    public class UserProjectsHelper
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        public bool IsUserInProject(string userId, int projectId)
        {
            //All in one line
            //return db.Projects.Find(projectId).Users.Any(u => u.Id == userId);  <- might work but was not passing projectId correctly

            // had to reverse the order of projectId and pr.Id in the following code for some reason, was
            // getting an error about unable to return a boolean for pr.ID

            if (db.Projects.Include(p => p.Users).FirstOrDefault(pr => pr.Id == projectId ).Users.Any(u => u.Id == userId))
            {
                return true;
            }
            return false;
        }

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


        public bool IsUserNotInProject(string userId, int projectId)
        {
            return !db.Projects.Find(projectId).Users.Any(u => u.Id == userId);
        }

        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsUserInProject(userId, projectId))
            {
                var project = db.Projects.Find(projectId);       //(Good Example)
                project.Users.Add(db.Users.Find(userId));
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void RemoveUserFromProject(string userId, int projectId)
        {
            if (IsUserInProject(userId, projectId))
            {
                var project = db.Projects.Find(projectId);
                project.Users.Remove(db.Users.Find(userId));
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public ICollection<Project> ListProjectsForUser(string userId)
        {
            return db.Users.Find(userId).Projects;
        }

        public ICollection<ApplicationUser> ListUsersOnProject(int projectId)
        {
            
           return db.Projects.Include(p => p.Users).FirstOrDefault(pr => pr.Id == projectId).Users;
        }
                  

        public ICollection<ApplicationUser> ListUsersNotOnProject(int projectId)
        {
            //Efficient
            return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();

        }
    }
}
   
