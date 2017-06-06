using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaceTime.AssettoCorsa.FileParser;

namespace RaceTime.AssettoCorsa.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new AssettoCorsaFileParser();
            parser.StartFileParser();

            Console.ReadLine();
        }
    }
}
