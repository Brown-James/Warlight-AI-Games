﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlightBot
{
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
                            Console.WriteLine(parts[2]);
                        }
                    }
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
                    else if (parts[0] == "update_map")
                    {

                    }
                    else if (parts[0] == "opponent_moves")
                    {

                    }
                    else if (parts[0] == "go")
                    {

                    }
                    else if (parts[0] == "pick_starting_region")
                    {
                        Console.WriteLine("Give me anywhere");
                    }
                    else if (parts[0] == "exit")
                    {
                        run = false;
                    }
                    line = "";
                }
        #endregion
            }
        }
    }
}