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
    [Route("api/test")]
    public class TestController : Controller
    {
        // GET api/Test
        [HttpGet]
        public IEnumerable<string> Get()
        {            
            Console.WriteLine("asd");
            return new string[] { "value1", "value2" };
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
        [Route("GetLap")]
        public Lap Post([FromBody]Lap value)
        {

            return value;
        }

    }
}