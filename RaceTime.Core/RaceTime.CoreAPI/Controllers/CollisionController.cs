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
    [Route("api/Collision")]
    public class CollisionController : Controller
    {
        RaceTimeContext db = new RaceTimeContext();
        // POST api/session/addcollision
        [HttpPost]
        [Route("AddCollision")]
        public Collision AddSession([FromBody]Collision value)
        {
            db.Collisions.Add(value);
            db.SaveChanges();
            return value;
        }
    }
}