using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlightBot
{
    class Map
    {
        public List<Region> regions = new List<Region>();
        public List<SuperRegion> superRegions;

        public List<Region> Regions
        {
            get { return regions; }
        }

        public List<SuperRegion> SuperRegions
        {
            get { return superRegions; }
        }

        public Map()
        {
            this.superRegions = new List<SuperRegion>();
        }

        public void AddSuperRegion(SuperRegion superRegion)
        {
            if (!superRegions.Contains(superRegion))
            {
                superRegions.Add(superRegion);
            }
        }

        public void AddRegion(Region region)
        {
            if (!regions.Contains(region))
            {
                regions.Add(region);
            }
        }

        public void CreateNeighbours(int id1, int id2)
        {
            int regionsPosition1 = -1;
            int regionsPosition2 = -1;

            for (int i = 0; i <= regions.Count() - 1; i++)
            {
                if (regions[i].Id == id1)
                {
                    regionsPosition1 = i;
                }
                else if(regions[i].Id == id2)
                {
                    regionsPosition2 = i;
                }
            }

            // Create the links
            regions[regionsPosition1].Neighbours.Add(regions[regionsPosition2]);
            regions[regionsPosition2].Neighbours.Add(regions[regionsPosition1]);

        }
    }
}
