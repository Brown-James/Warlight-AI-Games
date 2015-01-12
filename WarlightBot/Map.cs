using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlightBot
{
    class Map
    {
        public List<Region> regions;
        public List<SuperRegion> superRegions;

        public Map()
        {
            this.regions = new List<Region>();
            this.superRegions = new List<SuperRegion>();
        }
    }
}
