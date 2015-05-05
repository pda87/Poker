using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Player
    {
        public Hand Hand { get; set; }
        public string OutputString { get; set; }

        public Player()
        {
            this.Hand = new Hand();
        }
    }
}
