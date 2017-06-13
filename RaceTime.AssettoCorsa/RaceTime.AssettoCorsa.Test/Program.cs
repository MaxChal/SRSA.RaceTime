using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaceTime.AssettoCorsa.FileParser;
using acPlugins4net;
using RaceTime.AssettoCorsa.ServerPlugin;

namespace RaceTime.AssettoCorsa.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RaceTimeACPlugin raceTimePlugin = new RaceTimeACPlugin();

            AcServerPluginManager pluginManager = new AcServerPluginManager();
            pluginManager.LoadInfoFromServerConfig();
            pluginManager.AddPlugin(raceTimePlugin);
            pluginManager.LoadPluginsFromAppConfig();
            pluginManager.Connect();

            var parser = new AssettoCorsaFileParser(raceTimePlugin);
            parser.StartFileParser();
            
            Console.ReadLine();
        }


    }
}
