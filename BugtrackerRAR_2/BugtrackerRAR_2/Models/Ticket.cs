using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugtrackerRAR_2.Models
{
    public class Ticket
    {

         public Ticket()
        {  // when create a Ticket object initialize the following so that they are not null
            this.TicketAttachments = new HashSet<TicketAttachment>();
            this.TicketComments = new HashSet<TicketComment>();
            this.TicketHistories = new HashSet<TicketHistory>();
            this.TicketNotifications = new HashSet<TicketNotification>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public Nullable<DateTimeOffset> Updated { get; set; }
        public int ProjectId { get; set; }
        public int TicketTypeID { get; set; }
        public int TicketPriorityID { get; set; }
        public int TicketStatusID { get; set; }
        public string OwnerUserId { get; set; }
        public string AssignedUserId { get; set; }

        public virtual Project Project { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
        public virtual ApplicationUser AssignedUser { get; set; }

        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }
    }
}