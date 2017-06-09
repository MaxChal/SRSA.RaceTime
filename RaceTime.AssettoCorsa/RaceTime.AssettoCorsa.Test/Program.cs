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
            var parser = new AssettoCorsaFileParser();
            parser.StartFileParser();

            AcServerPluginManager pluginManager = new AcServerPluginManager();
            pluginManager.LoadInfoFromServerConfig();
            pluginManager.AddPlugin(new RaceTimeACPlugin());
            pluginManager.LoadPluginsFromAppConfig();
            pluginManager.Connect();

            Console.ReadLine();
        }


    }
}
