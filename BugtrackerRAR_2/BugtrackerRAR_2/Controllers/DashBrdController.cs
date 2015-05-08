using BugtrackerRAR_2.Models;
using BugtrackerRAR_2.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugtrackerRAR_2.Controllers
{
    public class DashBrdController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DashBrd
        public ActionResult MainAdmin()
        {
            var model = new DashBrdViewModel();
            var projects = db.Projects.Include("Tickets").OrderByDescending(p => p.Tickets.Count).Take(5);
            foreach (var p in projects)
            {
                model.projInfo.Add(new ProjectInfo { ProjectName = p.Name, NumTickets = p.Tickets.Count });
            }
            var helper = new UserRolesHelper();
            var devs = helper.UsersInRole("Developer").OrderByDescending(u => u.TicketsAssigned.Count).Take(3);
            foreach(var d in devs){
               model.devInfo.Add(new DevInfo { DevName = d.DisplayName, NumTickets = d.TicketsAssigned.Count });
            }
            return View(model);
        }
    }
}