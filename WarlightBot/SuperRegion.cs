using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlightBot
{
    class SuperRegion
    {
        private List<Region> subRegions = new List<Region>();
        private int armiesPerTurn;          // the bonus armies earned per turn from this super region
        private int id;

        public int Id
        {
            get { return id; }
        }

        public SuperRegion()
        {

        }

        public SuperRegion(int Id, int ArmiesPerTurn)
        {
            this.id = Id;
            this.armiesPerTurn = ArmiesPerTurn;
        }

        public SuperRegion(int Id, List<Region> SubRegions, int ArmiesPerTurn)
        {
            this.id = Id;
            this.armiesPerTurn = ArmiesPerTurn;
            this.subRegions = new List<Region>();
        }
        
        // Returns the owner of the SuperRegion as a string
        public string OwnedBy()
        {
            string ownedBy = subRegions[0].OwnerName;
            foreach(Region region in subRegions)
            {
                if (region.OwnerName != ownedBy)
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

        public override string ToString()
        {
            string str = "SuperRegion ID : " + id.ToString() + "\nArmiesPerTurn : " + armiesPerTurn.ToString() + "\n\n";
            foreach(Region region in subRegions)
            {
                str += region.ToString() + "\n";
            }
            return str;
        }
    }
}
