using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaceTime.AssettoCorsa.FileParser;
using acPlugins4net;
using RaceTime.AssettoCorsa.ServerPlugin;
using System.Threading;

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
            parser.FileAlive = true;
            Thread thread = new Thread(new ParameterizedThreadStart(StartFileParser));
            thread.Start(parser);
                        
            Console.ReadLine();
            parser.FileAlive = false;
            Console.ReadLine();
        }

        private static void StartFileParser(object parser)
        {
            var fileParser = (AssettoCorsaFileParser)parser;
            fileParser.StartFileParser();
        }
    }
}
