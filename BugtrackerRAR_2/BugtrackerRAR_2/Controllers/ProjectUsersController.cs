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
        [Authorize(Roles = "Admin,Project Manager")]
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

        // GET: AssignUsers Project Managers
        public ActionResult AssignPm(int id)     //Project ID passed
        {
            var model = new ProjectUsersViewModel();
            var project = db.Projects.Find(id);

            model.projectId = project.Id;
            model.projectName = project.Name;
            model.Users = new MultiSelectList(helper.ListProjectMgrsNotOnProject(id).OrderBy(u => u.DisplayName), "Id", "DisplayName");
            return View(model);
        }

        // GET: AssignUsers Project Managers
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignPm(ProjectUsersViewModel model)
        {
            if (ModelState.IsValid)
            {   // received model and a string of user ids
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        helper.AddUserToProject(id, model.projectId);
                    }
                    return RedirectToAction("IndexBp", "Projects");
                }
                else
                {
                    // send error message back to view
                }
            }
            return View(model);
        }

        // GET: AssignUsers  Developers
        public ActionResult AssignDev(int id)     //Project ID passed
        {
            var model = new ProjectUsersViewModel();
            var project = db.Projects.Find(id);

            model.projectId = project.Id;
            model.projectName = project.Name;
            model.Users = new MultiSelectList(helper.ListDevelopersNotOnProject(id).OrderBy(u => u.DisplayName), "Id", "DisplayName");
            return View(model);
        }

        // GET: AssignUsers  Developers
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Project Manager")]
        public ActionResult AssignDev(ProjectUsersViewModel model)
        {
            if (ModelState.IsValid)
            {   // received model and a string of user ids
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        helper.AddUserToProject(id, model.projectId);
                    }
                    
                    return RedirectToAction("IndexBp", "Projects");
                    
                }                                                     
                else                                    
                {
                    // send error message back to view
                }
            }
            return View(model);
        }

        // GET: AssignUsers  Developers by Project Manager
        public ActionResult AssignDev2(int id)     //Project ID passed
        {
            var model = new ProjectUsersViewModel();
            var project = db.Projects.Find(id);

            model.projectId = project.Id;
            model.projectName = project.Name;
            model.Users = new MultiSelectList(helper.ListDevelopersNotOnProject(id).OrderBy(u => u.DisplayName), "Id", "DisplayName");
            return View(model);
        }

        // GET: AssignUsers  Developers by Project Manager
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Project Manager")]
        public ActionResult AssignDev2(ProjectUsersViewModel model)
        {
            if (ModelState.IsValid)
            {   // received model and a string of user ids
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        helper.AddUserToProject(id, model.projectId);
                    }
                    
                    return RedirectToAction("IndexBp2", "Projects");  
                }                                                     
                else
                {
                    // send error message back to view
                }
            }
            return View(model);
        }

        // GET: UnAssignUsers  Developers by Project Manager
        public ActionResult UnAssignDev(int id)     //Project ID passed
        {
            var model = new ProjectUsersViewModel();
            var project = db.Projects.Find(id);

            model.projectId = project.Id;
            model.projectName = project.Name;
            model.Users = new MultiSelectList(helper.ListDevelopersOnProject(id).OrderBy(u => u.DisplayName), "Id", "DisplayName");
            return View(model);
        }

        // GET: UnAssignUsers  Developers by Project Manager
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Project Manager")]
        public ActionResult UnAssignDev(ProjectUsersViewModel model)
        {
            if (ModelState.IsValid)
            {   // received model and a string of user ids
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        helper.RemoveUserFromProject(id, model.projectId);
                    }

                    return RedirectToAction("IndexBp2", "Projects");
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
        [Authorize(Roles = "Admin,Project Manager")]
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
                    return RedirectToAction("IndexBp", "Projects");
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