using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarFinderRAR.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string ModelName { get; set; }

        public string ModelTrim { get; set; }

        public string ModelYear { get; set; }

        public string BodyStyle { get; set; }

        public string DriveType { get; set; }

        public string Doors { get; set; }

        public string Seats { get; set; }

        
    }
}