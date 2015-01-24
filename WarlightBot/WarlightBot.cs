using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WarlightBot
{
    class WarlightBot
    {
        #region Fields

        private string myName;
        private string opponentName;
        private int totalArmiesThisTurn = 5;

        private Map map;

        private Stopwatch timer = new Stopwatch();

        #endregion

        #region Constructors

        public WarlightBot()
        {
            map = new Map();
        }

        #endregion

        #region Public Methods
        public void Run()
        {
            bool run = true;
            while (run == true)
            {
                string line = Console.ReadLine();

                if (line == null)
                {
                    break;
                }

                line = line.Trim();

                String[] parts = line.Split(' ');

                #region Settings
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
                    else if (parts[1] == "starting_regions")
                    {

                    }
                }
                #endregion
                #region Setup_map
                else if (parts[0] == "setup_map")
                {
                    if (parts[1] == "super_regions")
                    {
                        for (int i = 2; i <= parts.Length - 1; i++)
                        {
                            int superRegionId = Convert.ToInt32(parts[i]);
                            i++;
                            int armiesPerTurn = Convert.ToInt32(parts[i]);
                            map.AddSuperRegion(new SuperRegion(superRegionId, armiesPerTurn));
                        }
                    }
                    else if (parts[1] == "regions")
                    {
                        for (int i = 2; i <= parts.Length - 1; i++)
                        {
                            int regionId = Convert.ToInt32(parts[i]);
                            i++;
                            int superRegionId = Convert.ToInt32(parts[i]);
                            SuperRegion regionSuperRegion = new SuperRegion();
                            foreach (SuperRegion superRegion in map.superRegions)
                            {
                                if (superRegion.Id == superRegionId)
                                {
                                    regionSuperRegion = superRegion;
                                }
                            }
                            map.AddRegion(new Region(regionId, regionSuperRegion.Id));
                        }

                    }
                    else if (parts[1] == "neighbors")
                    {
                        for (int i = 2; i <= parts.Length - 1; i++)
                        {
                            int region1Id = Convert.ToInt32(parts[i]);
                            i++;
                            String[] neighbours = parts[i].Split(',');
                            foreach (String neighbour in neighbours)
                            {
                                map.CreateNeighbours(region1Id, Convert.ToInt32(neighbour));
                            }
                        }
                    }
                    else if (parts[1] == "wastelands")
                    {
                        for (int i = 2; i <= parts.Length - 1; i++)
                        {
                            foreach (Region region in map.Regions)
                            {
                                if (region.Id == Convert.ToInt32(parts[i]))
                                {
                                    region.OwnerName = "neutral";
                                }
                            }
                        }
                    }
                    else if (parts[1] == "opponent_starting_regions")
                    {

                    }
                }
                #endregion
                #region Update_map
                else if (parts[0] == "update_map")
                {
                    for (int i = 1; i <= parts.Length - 1; i++)
                    {
                        int regionId = Convert.ToInt32(parts[i]);
                        i++;
                        String owner = parts[i];
                        i++;
                        int armies = Convert.ToInt32(parts[i]);
                        foreach (Region region in map.Regions)
                        {
                            if (region.Id == regionId)
                            {
                                region.OwnerName = owner;
                                region.Armies = armies;
                            }
                        }

                    }
                }
                #endregion
                #region Opponent_moves
                else if (parts[0] == "opponent_moves")
                {

                }
                #endregion
                #region Go
                else if (parts[0] == "go")
                {
                    if (parts[1] == "place_armies")
                    {
                        string output = PlaceArmies(map);
                        Console.WriteLine(output);
                    }
                    else if (parts[1] == "attack/transfer")
                    {
                        string output = AttackTransfer(map);
                        Console.WriteLine(output);
                    }
                }
                #endregion
                #region Pick_starting_region
                else if (parts[0] == "pick_starting_region")
                {
                    Console.WriteLine(parts[2].ToString());
                }
                #endregion
                #region Exit
                else if (parts[0] == "exit")
                {
                    run = false;
                }
                #endregion
            }
        #endregion

        }

        /// <summary>
        /// Deploys armies, giving preference to any territories currently bordered by an enemy.
        /// Will deploy armies evenly if not bordered by an enemy.
        /// </summary>
        /// <param name="map">The current map</param>
        /// <returns>String to output in the form "myName place_armies regionId noOfArmies"</returns>
        private string PlaceArmies(Map map)
        {
            string output = "";
            List<Region> regionsToDeploy = new List<Region>();
            Dictionary<int, int> armyDictionary = new Dictionary<int, int>();

            // Set up a list of all regions which are also bordered by an enemy territory so that they
            // can be given preference when deploying armies.
            foreach (Region region in map.OwnedRegions(myName))
            {
                if (region.EnemyBorder(opponentName).Count > 0)
                {
                    regionsToDeploy.Add(region);
                }
            }
            
            // If not bordered by any enemies, populate the list with all owned territories so that we can
            // deploy across all of them easily
            if (regionsToDeploy.Count == 0)
            {
                regionsToDeploy = map.OwnedRegions(myName);
            }

            int count = 0;

            while (totalArmiesThisTurn > 0)
            {
                // Add a new key to the dictionary is required, otherwise increment by 1
                if (!armyDictionary.ContainsKey(regionsToDeploy[count].Id))
                {
                    armyDictionary.Add(regionsToDeploy[count].Id, 1);
                }
                else
                {
                    armyDictionary[regionsToDeploy[count].Id] += 1;
                }

                // Loop back to the beginning if required
                if (count + 1 >= regionsToDeploy.Count)
                {
                    count = 0;
                }
                else { count += 1; }

                totalArmiesThisTurn -= 1;
            }
        
            foreach (KeyValuePair<int, int> deploy in armyDictionary)
            {
                output += myName + " place_armies " + deploy.Key + " " + deploy.Value + ",";
            }
        
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private string AttackTransfer(Map map)
        {
            string output = "";

            foreach(Region region in map.regions)
            {
                if(region.Armies > 2)
                {
                    List<Region> enemyBorders = new List<Region>();
                    enemyBorders = region.EnemyBorder(opponentName);

                    List<Region> wastelandBorders = new List<Region>();
                    wastelandBorders = region.WastelandBorder();

                    if (enemyBorders.Count > 0)
                    {
                        foreach (Region enemy in enemyBorders)
                        {
                            if (region.Armies > 1.5 * enemy.Armies)
                            {
                                output += myName + " attack/transfer " + region.Id + " " + enemy.Id + " " + (region.Armies - 1).ToString() + ",";
                                region.Armies = 1;
                            }
                        }
                    }
                    else if (wastelandBorders.Count > 0)
                    {
                        foreach (Region neighbor in wastelandBorders)
                        {
                            if (region.Armies > 1.5 * neighbor.Armies)
                            {
                                output += myName + " attack/transfer " + region.Id + " " + neighbor.Id + " " + (region.Armies - 1).ToString() + ",";
                                region.Armies = 1;
                            }
                        }
                    }
                    else
                    {
                        foreach(Region neighbor in region.Neighbours)
                        {
                            if(region.Armies > neighbor.Armies)
                            {
                                output += myName + " attack/transfer " + region.Id + " " + neighbor.Id + " " + (region.Armies - neighbor.Armies).ToString() + ",";
                                region.Armies -= (region.Armies - neighbor.Armies);
                            }
                        }
                    }
                    
                }
            }

            return output;
        }
    }
}
