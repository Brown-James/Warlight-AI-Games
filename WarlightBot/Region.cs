using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlightBot
{
    class Region
    {
        private int id;
        private List<Region> neighbours = new List<Region>();
        private SuperRegion superRegion;
        private String ownerName = "";          // name of the player who currently owns the territory
        private int armies = 0;             // the number of armies currently in the territory
        private int superRegionId;

        public Region()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SuperRegion"></param>
        public Region(int Id, int SuperRegionId)
        {
            this.id = Id;
            this.superRegionId = SuperRegionId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Neighbours"></param>
        /// <param name="SuperRegion"></param>
        /// <param name="PlayerName"></param>
        public Region(int Id, List<Region> Neighbours, SuperRegion SuperRegion, String OwnerName)
        {
            this.id = Id;
            this.neighbours = Neighbours;
            this.superRegion = SuperRegion;
            this.ownerName = OwnerName;
        }

        public int Id
        {
            get { return id; }
        }

        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }

        public List<Region> Neighbours
        {
            get { return neighbours; }
            set { neighbours = value; }
        }

        public override string ToString()
        {
            string str = "ID : " + id.ToString() + "\nOwner Name : " + ownerName + "\nArmies : " + armies.ToString() + "\nSuperRegion ID : " + superRegionId + "\nNeighbours : ";
            foreach(Region region in neighbours)
            {
                str += region.id + " ";
            }
            return str + "\n";
        }
    }
}
