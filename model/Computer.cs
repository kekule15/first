using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace first.model
{
    public class Computer
    {

        public string Motherboard { get; set; } = "";
        public string VideoCard { get; set; } = "";

        public bool HasLTE { get; set; }

        public bool HasWifi { get; set; }
        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }



    }
}