using System;
using System.Collections.Generic;
using System.Text;

namespace RaceTime.Common.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public string CarName { get; set; }
       // public List<Competitor> Drivers { get; set; }
        public int LapsCompleted { get; set; }

        public Car()
        {
          
        }
    }
}
