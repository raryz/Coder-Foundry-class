using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugtrackerRAR_2.Models;
using BugtrackerRAR_2.Models.Helpers;

namespace BugtrackerRAR_2.Controllers
{
    public class ProjectUsersController : Controller
    {
        private UserProjectsHelper helper = new UserProjectsHelper();
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        // GET: AssignUsers
        public ActionResult AssignUser(int id)     //Project ID passed
        {
            var model = new ProjectUsersViewModel();
            var project = db.Projects.Find(id);

            model.projectId = project.Id;
            model.projectName = project.Name;
            model.Users = new MultiSelectList(helper.ListUsersNotOnProject(id).OrderBy(u => u.DisplayName), "Id", "DisplayName"); 
            return View(model);
        }

          // GET: AssignUsers
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignUser(ProjectUsersViewModel model)    
        {
            if (ModelState.IsValid)
            {   // received model and a string of user ids
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        helper.AddUserToProject(id, model.projectId);
                    }
                    return RedirectToAction("Index", "Projects");
                }
                else
                {
                    // send error message back to view
                }
            }
            return View(model);
        }

        // GET: UnassignUsers
        public ActionResult UnassignUser(int id)     //Project ID passed
        {
            var model = new ProjectUsersViewModel();
            var project = db.Projects.Find(id);

            model.projectId = project.Id;
            model.projectName = project.Name;
            var templist = helper.ListUsersOnProject(id);
            var orderedlist = templist.OrderBy(u => u.DisplayName);
            model.Users = new MultiSelectList(orderedlist, "Id", "DisplayName");
            return View(model);
        }

        // GET: UnassignUsers
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult UnassignUser(ProjectUsersViewModel model)
        {
            if (ModelState.IsValid)
            {   // received model and a string of user ids
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        helper.RemoveUserFromProject(id, model.projectId);
                    }
                    return RedirectToAction("Index", "Projects");
                }
                else
                {
                    // send error message back to view
                }
            }
            return View(model);
        }
    }
}