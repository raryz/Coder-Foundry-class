using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugtrackerRAR_2.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Threading.Tasks;

namespace BugtrackerRAR_2.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        [Authorize]
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }

        // GET: Tickets  with Barnie Template
        [Authorize]
        public ActionResult IndexB()
        {
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }

        // GET: Tickets  with Barnie Template
        // list of projects assigned to project managers or developers
        [Authorize]
        public ActionResult IndexBp()
        {
            var UserId = User.Identity.GetUserId();
            var tickets = db.Tickets.Where(t => t.Project.Users.Any(u => u.Id == UserId));
            
            return View(tickets.ToList());
        }
        
             // GET: Tickets  with Barnie Template
        [Authorize]
        public ActionResult IndexBd()
        {
            var UserId = User.Identity.GetUserId();
            var tickets = db.Tickets.Where(t => t.AssignedUserId == UserId);
            
            return View(tickets.ToList());
        }

       
         [Authorize]
        public ActionResult IndexBs()
        {
            var UserId = User.Identity.GetUserId();
            var tickets = db.Tickets.Where(t => t.OwnerUserId == UserId);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize]
        public ActionResult Create()
        {

            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPriorityID = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusID = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeID = new SelectList(db.TicketTypes, "Id", "Name");

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeID,TicketPriorityID,TicketStatusID,OwnerUserId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.OwnerUserId = User.Identity.GetUserId();
                ticket.Created = System.DateTimeOffset.Now;
                ticket.TicketStatusID = 1;                       // 1 = unassigned

                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityID = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityID);
            // ViewBag.TicketStatusID = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusID);
            //
            ViewBag.TicketTypeID = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeID);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityID = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityID);
            ViewBag.TicketStatusID = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusID);
            ViewBag.TicketTypeID = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeID);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeID,TicketPriorityID,TicketStatusID,OwnerUserId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                ticket.Updated = System.DateTimeOffset.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityID = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityID);
            ViewBag.TicketStatusID = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusID);
            ViewBag.TicketTypeID = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeID);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(TicketComment commentMessage, int TicketId)
        {
            if (ModelState.IsValid)
            {
                var msgBody = commentMessage.Comment;
                if (string.IsNullOrWhiteSpace(msgBody))
                {
                    ModelState.AddModelError("Title", "Invalid comment.");
                    return View();
                }
                else
                {
                    commentMessage.Created = System.DateTimeOffset.Now;
                    //commentMessage.Updated = System.DateTimeOffset.Now;   // not defined in TicketComment
                    //commentMessage.UpdatedReason = " ";
                    commentMessage.UserId = User.Identity.GetUserId();

                    db.TicketComments.Add(commentMessage);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = TicketId });
                }
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAttachment(HttpPostedFileBase fileUpload, int TicketId, TicketAttachment ticketattachment)
        {
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                // check the file name to make sure
                // it's an image type
                var ext = Path.GetExtension(fileUpload.FileName);
                if (ext != ".png" && ext != ".jpg" && ext != ".txt")
                    // otherwise, throw and error
                    ModelState.AddModelError("image", "Invalid format.");
            }

            if (ModelState.IsValid)
            {
                var msgDescription = ticketattachment.Description;
                if (string.IsNullOrWhiteSpace(msgDescription))
                {
                    ModelState.AddModelError("Title", "Invalid description.");
                    return View();
                }

                // relative server path
                var filePath = "/images/";
                //path on physical drive on server
                var absPath = Server.MapPath("~" + filePath);
                //media url for relative path
                ticketattachment.FileUrl = filePath + fileUpload.FileName;
                //save image
                fileUpload.SaveAs(Path.Combine(absPath, fileUpload.FileName));

                db.Entry(ticketattachment).State = EntityState.Modified;
                ticketattachment.Created = System.DateTimeOffset.Now;           // 
                ticketattachment.FilePath = filePath;
                ticketattachment.UserId = User.Identity.GetUserId();

                db.TicketAttachments.Add(ticketattachment);
                await db.SaveChangesAsync();

                return RedirectToAction("Details", new { id = TicketId });

            }    // end if model state is valid

            return View();    // if invalid format for image

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
