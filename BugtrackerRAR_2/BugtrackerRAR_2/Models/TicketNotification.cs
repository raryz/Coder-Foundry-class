using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugtrackerRAR_2.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }

        public string UserId { get; set; }

    }
}