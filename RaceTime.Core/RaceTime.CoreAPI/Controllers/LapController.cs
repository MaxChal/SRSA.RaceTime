using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceTime.Common.Models;
using RaceTime.CoreAPI.Hubs;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace RaceTime.CoreAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Lap")]
    public class LapController : ApiHubController<TestHub>
    {
        RaceTimeContext db = new RaceTimeContext();

        public LapController(IConnectionManager signalRConnectionManager)
           : base(signalRConnectionManager)
        {

        }


        // POST api/lap/addlap
        [HttpPost]
        [Route("AddLap")]
        public int AddLap([FromBody]Lap value)
        {
            SendLapAsync(value);
            db.Laps.Add(value);
            
            return db.SaveChanges();
        }

        public async Task SendLapAsync(Lap lap)
        {
            await Clients.All.Test(lap);
        }
    }
}