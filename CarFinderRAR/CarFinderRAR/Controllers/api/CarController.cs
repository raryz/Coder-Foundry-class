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
    }
}
