using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugtrackerRAR_2.Models
{
    // create properties for items on the form
    // the controller will receive a parameter of an object of this type

    public class EmailMessage
    {
        public string subject { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string message { get; set; }
        //    public string body { get; set; }
        //    public string destination { get; set; }

    }
}