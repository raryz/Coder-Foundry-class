using CarFinderRAR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CarFinderRAR.Controllers
{
    [RoutePrefix("api/Cars")]
    public class CarController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public class SelectedOptions
        {
            public int year {get; set;}
            public string make { get; set; }
            public string model { get; set; }
            public string trim { get; set; }
        }

        [HttpGet]                                 // have to use HttpGet because the stored procedure
        [Route("MakesForYear")]                   // does not start with the name with "Get"
        public async Task<List<string>> MakesForYear(string year)
        {

            return await db.MakesForYear(year);
        }

        [HttpGet]                             // optional use of HttpGet since the stored procedure
        [Route("GetYears")]                   // starts with the name with "Get"
        public async Task<List<string>> GetYears()
        {

            return await db.GetYears();
        }

        [HttpPost, HttpGet]                             // optional use of HttpGet since the stored procedure
        [Route("GetMakes")]                   // starts with the name with "Get"
        public async Task<List<string>> GetMakes([FromUri] SelectedOptions selected)
        {

            return await db.GetMakes(selected.year);
        }

        [HttpPost]                             // optional use of HttpGet since the stored procedure
        [Route("GetModels")]                   // starts with the name with "Get"
        public async Task<List<string>> GetModels(SelectedOptions selected)
        {

            return await db.GetModels(selected.year, selected.make);
        }

        [HttpPost]                             // optional use of HttpGet since the stored procedure
        [Route("GetTrims")]                   // starts with the name with "Get"
        public async Task<List<string>> GetTrims(SelectedOptions selected)
        {

            return await db.GetTrims(selected.year, selected.make, selected.model);
        }
    }
}
