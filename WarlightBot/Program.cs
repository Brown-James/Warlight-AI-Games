using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlightBot
{
    class Program
    {
        public static void Main()
        {
            Map map = new Map();
            map.AddSuperRegion(new SuperRegion(0, 5));
            map.AddRegion(new Region(0, 0));
            map.AddRegion(new Region(1, 0));
            map.AddRegion(new Region(2, 0));

            map.CreateNeighbours(0, 1);
            map.CreateNeighbours(0, 2);
            map.CreateNeighbours(1, 2);
            map.CreateNeighbours(1, 0);

            map.superRegions[0].AddRegion(map.Regions[0]);
            map.superRegions[0].AddRegion(map.Regions[1]);
            map.superRegions[0].AddRegion(map.Regions[2]);

            Console.WriteLine(map.superRegions[0].ToString());

            Console.ReadLine();

            //WarlightBot bot = new WarlightBot();
            //Console.WriteLine("Warlight bot running");
            //bot.Run();
            //Console.ReadLine();
        }
    }
    class WarlightBot
    {
        private string myName;
        private string opponentName;
        private int totalArmiesThisTurn;

        private Map map;

        public WarlightBot()
        {
            map = new Map();
        }

        #region Public Methods
        public void Run()
        {
            //while (true)
            {
                string line = Console.ReadLine();
                if (line == null)
                {
                    //break;
                }

                line = line.Trim();

                String[] parts = line.Split(' ');

                if (parts[0] == "settings")
                {
                    if (parts[1] == "your_bot")
                    {
                        myName = parts[2];
                    }
                    else if (parts[1] == "opponent_bot")
                    {
                        opponentName = parts[2];
                    }
                    else if (parts[1] == "starting_armies")
                    {
                        totalArmiesThisTurn = Convert.ToInt32(parts[2]);
                    }
                }
                else if (parts[0] == "setup_map")
                {
                    if (parts[1] == "super_regions")
                    {
                        for(int i = 2; i <= parts.Length - 1; i++)
                        {
                            int superRegionId = Convert.ToInt32(parts[i]);
                            i++;
                            int armiesPerTurn = Convert.ToInt32(parts[i]);
                            map.AddSuperRegion(new SuperRegion(superRegionId, armiesPerTurn));
                        }
                    }
                    else if (parts[1] == "regions")
                    {
                        for(int i = 2; i <= parts.Length - 1; i++)
                        {
                            int regionId = Convert.ToInt32(parts[i]);
                            i++;
                            int superRegionId = Convert.ToInt32(parts[i]);
                            SuperRegion regionSuperRegion = new SuperRegion();
                            foreach (SuperRegion superRegion in map.superRegions)
                            {
                                if(superRegion.Id == superRegionId)
                                {
                                    regionSuperRegion = superRegion;
                                }
                            }
                            map.AddRegion(new Region(regionId, regionSuperRegion.Id));
                        }

                    }
                    else if (parts[1] == "neighbors")
                    {
                        for(int i = 2; i <= parts.Length - 1; i++)
                        {
                            int region1Id = Convert.ToInt32(parts[i]);
                            i++;
                            String[] neighbours = parts[i].Split(',');
                            foreach(String neighbour in neighbours)
                            {
                                map.CreateNeighbours(region1Id, Convert.ToInt32(neighbour));
                            }
                        }
                    }
                    else if (parts[1] == "wastelands")
                    {

                    }
                    else if (parts[1] == "opponent_starting_regions")
                    {

                    }
                }
                else if(parts[0] == "update_map")
                {

                }
                else if(parts[0] == "opponent_moves")
                {

                }
                else if(parts[0] == "go")
                {

                }
                else if(parts[0] == "pick_starting_region")
                {

                }
            }
        #endregion
        }
    }
}
