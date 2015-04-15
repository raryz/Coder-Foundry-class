using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult> SendMail([Bind(Include = "Subject, Destination, Body")])
        {
            if (ModelState.IsValid){
               var mailMessage = new IdentityMessage(Destination, Subject, Body);
            return View();
            }
        }
    }
}

public class EmailService : IIdentityMessageService
{
    public Task SendAsync(IdentityMessage message)
    var mailMessage = new MailMessage( IdentityMessage.destination, IdentityMessage.subject, IdentityMessage.Body);

    mailMessage.IsBodyHtml = true;

    var client = new SmtpClient();

    client.SendAsync(mailMessage);
}
