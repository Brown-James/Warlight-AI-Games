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

        public int Armies
        {
            get { return armies; }
            set { armies = value; }
        }

        public List<Region> Neighbours
        {
            get { return neighbours; }
            set { neighbours = value; }
        }

        /// <summary>
        /// Returns the IDs of bordering enemy regions if there is one. Other wise returns -1
        /// </summary>
        /// <param name="enemyName"></param>
        /// <returns></returns>
        public List<Region> EnemyBorder(string enemyName)
        {
            List<Region> enemyRegions = new List<Region>();
            foreach(Region neighbour in neighbours)
            {
                if(neighbour.OwnerName == enemyName)
                {
                    enemyRegions.Add(neighbour);
                }
            }
            return enemyRegions;
        }

        public List<Region> WastelandBorder()
        {
            List<Region> wastelands = new List<Region>();
            foreach(Region neighbour in neighbours)
            {
                if(neighbour.OwnerName == "neutral")
                {
                    wastelands.Add(neighbour);
                }
            }

            return wastelands;
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
