using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Roulette
{
    public class RouletteData
    {
        public string Qualifier { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public int WinningNumber { get; set; }
    }
}
