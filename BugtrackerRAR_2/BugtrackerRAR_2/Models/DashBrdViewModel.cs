using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugtrackerRAR_2.Models
{
    public class DashBrdViewModel
    {
        public DashBrdViewModel()
        {
            projInfo = new List<ProjectInfo>();
            devInfo = new List<DevInfo>();
        }
        public List<ProjectInfo> projInfo {get; set;}
        public List<DevInfo> devInfo { get; set; }

    }

    public class DevInfo
    {
        public string DevName { get; set; }
        public int NumTickets { get; set; }
    }

    public class ProjectInfo
    {
        public string ProjectName { get; set; }
        public int NumTickets { get; set; }
    }

}