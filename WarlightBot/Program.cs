using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlightBot
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class WarlightBot
    {
        private string myName;
        private string opponentName;
        private int totalArmiesThisTurn;
        public WarlightBot()
        {

        }

        #region Public Methods
        public void Run()
        {
            while(true)
            {
                string line = Console.ReadLine();
                if (line == null)
                {
                    break;
                }

                line = line.Trim();

                String[] parts = line.Split(' ');
                
                if (parts[0] == "settings" )
                {
                    if(parts[1] == "your_bot")
                    {
                        myName = parts[2];
                    }
                    else if(parts[1] == "opponent_bot")
                    {
                        opponentName = parts[2];
                    }
                    else if(parts[1] == "starting_armies")
                    {
                        totalArmiesThisTurn = Convert.ToInt32(parts[2]);
                    }
                }
            }
        }
        #endregion
    }
}
