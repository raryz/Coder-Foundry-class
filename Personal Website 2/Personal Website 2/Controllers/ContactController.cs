using Microsoft.AspNet.Identity;
using Personal_Website_2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Personal_Website_2.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public async Task<ActionResult> SendMail(EmailMessage contactMessage)
        {
            if (ModelState.IsValid){
                var destination = ConfigurationManager.AppSettings["ContactEmail"];
                var subject = contactMessage.subject;

                var body = "You have received a contact form from " + contactMessage.name + " (" + contactMessage.email + ") " + "with the contents of \n\n" + contactMessage.message;
                
                var mailMessage = new IdentityMessage();

                mailMessage.Destination = destination;
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                await new EmailService().SendAsync(mailMessage);
                TempData["MessageSent"] = "";
                return RedirectToAction("Contact", "Home");
            }
            //error 
            TempData["MessageSent"] = "";
            return RedirectToAction("Contact", "Home");
        }
    }
}

// var mailMessage = new IdentityMessage( subject = subject, destination = destination, body = body );