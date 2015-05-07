using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Player
    {
        public string Name { get; set; }
        public Hand Hand { get; set; }
        public List<Hand> HandCombinations { get; set; }
        public string OutputString { get; set; }

        public Player()
        {
            this.Hand = new Hand();
            this.HandCombinations = new List<Hand>();
        }
    }
}
