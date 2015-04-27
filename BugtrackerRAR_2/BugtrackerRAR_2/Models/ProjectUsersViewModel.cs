using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BugtrackerRAR_2.Models
{
    public class ProjectUsersViewModel
    {
        public int projectId {get; set;}
        public string projectName {get; set;}

        [Display(Name = "Available Users")]
        public System.Web.Mvc.MultiSelectList Users { get; set; }  //populates list box
        public string[] SelectedUsers { get; set; }      // selected items goes into string array in this case they are ids
    }
}