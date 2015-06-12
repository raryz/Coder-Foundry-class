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

        [HttpGet]                             // optional use of HttpGet since the stored procedure
        [Route("GetMakes")]                   // starts with the name with "Get"
        public async Task<List<string>> GetMakes(int year)
        {

            return await db.GetMakes(year);
        }

        [HttpGet]                             // optional use of HttpGet since the stored procedure
        [Route("GetModels")]                   // starts with the name with "Get"
        public async Task<List<string>> GetModels(int year, string make)
        {

            return await db.GetModels(year, make);
        }

        [HttpGet]                             // optional use of HttpGet since the stored procedure
        [Route("GetTrims")]                   // starts with the name with "Get"
        public async Task<List<string>> GetModels(int year, string make, string model)
        {

            return await db.GetTrims(year, make, model);
        }
    }
}
