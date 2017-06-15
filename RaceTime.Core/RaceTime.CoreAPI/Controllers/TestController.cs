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
    [Route("api/test")]
    public class TestController : ApiHubController<AssettoCorsaHub>
    {
        RaceTimeContext db = new RaceTimeContext();

        public TestController(IConnectionManager signalRConnectionManager)
           : base(signalRConnectionManager)
        {

        }


        // GET api/Test
        [HttpGet]
        public async Task Get()
        {            
            Console.WriteLine("asd");
            await Clients.All.NewLap(new Lap());
        }

        // GET api/Test/action
        [HttpGet]     
        [Route("action")]
        public IEnumerable<string> actions()
        {
            Console.WriteLine("asd");
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        [Route("AddLap")]
        public Lap Post([FromBody]Lap value)
        {
            db.Laps.Add(new Lap() {
                LapId = Guid.NewGuid().ToString(),
                LapLength = 1,
                DBTimestamp = DateTime.Now
            });

            db.SaveChanges();
            return db.Laps.LastOrDefault() ;
        }

    }
}