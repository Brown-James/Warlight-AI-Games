using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlightBot
{
    class SuperRegion
    {
        private List<Region> subRegions;
        private int armiesPerTurn;
        private int id;

        public SuperRegion()
        {
            this.subRegions = new List<Region>();
        }
        
        // Returns the owner of the SuperRegion as a string
        public string OwnedBy()
        {
            string ownedBy = subRegions[0].PlayerName;
            foreach(Region region in subRegions)
            {
                if (region.PlayerName != ownedBy)
                {
                    return null;
                }
            }
            return ownedBy;
        }

        public void AddRegion(Region region)
        {
            if (!subRegions.Contains(region))
            {
                subRegions.Add(region);
            }
        }
    }
}
