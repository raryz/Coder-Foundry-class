using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Personal_Website_2.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;

namespace Personal_Website_2.Controllers
{
    
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(db.Posts.OrderByDescending(p => p.Created).ToPagedList(page ?? 1, 3));
        //    return View(db.Posts.ToPagedList(pageNumber, pageSize));

          //  return View(db.Posts.OrderByDescending(p => p.Created).Take(3).ToList());
        }

        // GET: Posts
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminIndex()
        {
            return View(await db.Posts.ToListAsync());
        }

        // GET: Posts/Details/5
        [AllowAnonymous]
        
        public ActionResult Details(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.FirstOrDefault(p => p.Slug == Slug);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }


        // GET: Posts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Created,Body,Title,Published")] Post post, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(post.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid title.");
                    return View(post);
                }
                if (db.Posts.Any(p => p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(post);
                }
                else
                {
                    post.Created = System.DateTimeOffset.Now;
                    post.Slug = Slug;

                    db.Posts.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }


        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Created,Updated,Title,Body,MediaURL,Slug")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                post.Updated = System.DateTimeOffset.Now;           // added 4-14-2015
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Comment commentMessage, string slug)
        {
            if (ModelState.IsValid)
            {
                var msgBody = commentMessage.Body;
                if (string.IsNullOrWhiteSpace(msgBody))
                {
                    ModelState.AddModelError("Title", "Invalid comment.");
                    return View();
                }
                else
                {
                    commentMessage.Created = System.DateTimeOffset.Now;
                    //commentMessage.Updated = System.DateTimeOffset.Now;   // remove later if allow nulls
                    commentMessage.UpdatedReason = " ";
                    commentMessage.AuthorId = User.Identity.GetUserId();

                    db.Comments.Add(commentMessage);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { slug=slug });
                }
            }
            return View();
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<ActionResult> EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        
                // POST: Comments/Edit/5
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                [Authorize(Roles = "Moderator,Admin")]
                public async Task<ActionResult> EditComment([Bind(Include = "Id,PostId,AuthorId,Created,Updated,UpdatedReason,Body")] Comment comment)
                {
                    if (ModelState.IsValid)
                    {
                        Post post = db.Posts.Find(comment.PostId);
                        db.Entry(comment).State = EntityState.Modified;
                        comment.Updated = System.DateTimeOffset.Now;           // added 4-14-2015
                        await db.SaveChangesAsync();
                        
                        return RedirectToAction("Details", new { slug = post.Slug });
                    }
                    return View(comment);
                }

                // GET: Comments/Delete/5
                [Authorize(Roles = "Moderator,Admin")]
                public async Task<ActionResult> DeleteComment(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Comment comment = await db.Comments.FindAsync(id);
                    if (comment == null)
                    {
                        return HttpNotFound();
                    }
                    return View(comment);
                }

                // GET: Comments/Delete/5
                [Authorize(Roles = "Moderator,Admin")]
                [HttpPost]
                public async Task<ActionResult> DeleteComment(int id)
                {
                    
                    Comment comment = await db.Comments.FindAsync(id);
                    Post post =  db.Posts.Find(comment.PostId); 
                    if (comment == null)
                    {
                        return HttpNotFound();
                    }
                    db.Comments.Remove(comment);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Details", new { slug = post.Slug });
                   
                }

                

                // GET: Search
                
                [HttpPost]
                public ActionResult DetailSearch(string findtext)
                {

                    var found = (db.Posts.Where(p => p.Title.Contains(findtext)))
                          .Union(db.Posts.Where(p => p.Slug.Contains(findtext)))
                          .Union(db.Posts.Where(p => p.Body.Contains(findtext)))
                          .Union(db.Posts.Where(p => p.Comments.Any(c => c.Body.Contains(findtext)))).ToPagedList(1, 3);

                   
                  return View(found);  
                    
                }   
        
      /*
                [Authorize(Roles = "Admin")]
                [Authorize(Roles = "Moderator")]
                [HttpPost]
                public async Task<ActionResult> CommentIndex(int PostId, string slug)
                {
                         
             

                     return View(await db.Comments.ToListAsync(PostId));


                    // Task<comment> commentList = await db.Comments.Find(PostId);
                    // return RedirectToAction("CommentIndex", new { slug = slug });
                
            
                }*/
                /*
                        [Authorize(Roles = "Admin")]
                        [Authorize(Roles = "Moderator")]
                        [HttpPost]
                        public async Task<ActionResult> DeleteComment(int PostId, string slug)
                        {

                            Comment comment =  db.Comments.FindAsync(PostId);
                            db.Comments.Remove(comment);
                            await db.SaveChangesAsync();
            
                            return RedirectToAction("Details", new { slug = slug });
                        }
         
                        [Authorize(Roles = "Admin")]
                        [Authorize(Roles = "Moderator")]
                        [HttpPost]
                        public async Task<ActionResult> EditComment(int PostId, string slug, comment Comment)
                        {

                            Comment commentDisplay =  db.Comments.FindAsync(Comment.Id);
                    
                            return RedirectToAction("EditCommentScreen", new { slug = slug });

                        }
           
                 * [HttpPost]
                [ValidateAntiForgeryToken]
                [Authorize(Roles = "Admin")]
                public async Task<ActionResult> Edit([Bind(Include = "Id,Created,Updated,Title,Body,MediaURL,Slug")] Post post)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(post).State = EntityState.Modified;
                        post.Updated = System.DateTimeOffset.Now;           // added 4-14-2015
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    return View(post);
                }
                     */
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
