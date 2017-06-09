using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceTime.Common.Models;

namespace RaceTime.CoreAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Lap")]
    public class LapController : Controller
    {
        RaceTimeContext db = new RaceTimeContext();
        // POST api/lap/addlap
        [HttpPost]
        [Route("AddLap")]
        public int AddLap([FromBody]Lap value)
        {
            db.Laps.Add(value);
            
            return db.SaveChanges();
        }
    }
}