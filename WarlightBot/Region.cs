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
        private List<Region> neighbours;
        private SuperRegion superRegion;
        private String playerName;          // name of the player who currently owns the territory
        private int armies;                 // the number of armies currently in the territory

        public Region(int Id, List<Region> Neighbours, SuperRegion SuperRegion, String PlayerName)
        {
            this.id = Id;
            this.neighbours = Neighbours;
            this.superRegion = SuperRegion;
            this.playerName = PlayerName;
        }

        public string PlayerName
        {
            get { return playerName; }
        }
    }
}
