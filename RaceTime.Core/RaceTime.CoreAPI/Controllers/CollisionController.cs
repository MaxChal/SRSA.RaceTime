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
    [Route("api/Collision")]
    public class CollisionController : ApiHubController<AssettoCorsaHub>
    {
        RaceTimeContext db = new RaceTimeContext();
        
        
        public CollisionController(IConnectionManager signalRConnectionManager)
           : base(signalRConnectionManager)
        {

        }

        // POST api/session/addcollision
        [HttpPost]
        [Route("AddCollision")]
        public Collision AddCollision([FromBody]Collision value)
        {
            SendCollisionAsync(value);

            db.Collisions.Add(value);
            db.SaveChanges();
            return value;
        }

        public async Task SendCollisionAsync(Collision collision)
        {
            await Clients.All.NewCollision(collision);
        }
    }
}