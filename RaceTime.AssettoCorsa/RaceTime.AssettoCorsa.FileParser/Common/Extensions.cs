using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceTime.AssettoCorsa.FileParser.Common
{
    public static class Extensions
    {
        public static string GetDriverName(this string name)
        {
            return name.Substring(name.IndexOf('[') + 1, name.LastIndexOf('[') - 2 - name.IndexOf('[')); ;
        }

        public static string GetCarName(this string text)
        {
            text = text.Replace("Dispatching TCP message to ", "");
            return text.Substring(0, text.IndexOf('(') - 1);
        }
    }
}
