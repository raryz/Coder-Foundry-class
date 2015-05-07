using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugtrackerRAR_2.Models
{
    public class DashBrdViewModel
    {
        public int projectId { get; set; }
        public string projectName { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

    }
}