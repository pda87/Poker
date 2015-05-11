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
        public int BankBalance { get; set; }
        public int Bet { get; set; }
        public Outcome WinOrLose { get; set; }
        public bool Bankrupt { get; set; }


        public Player()
        {
            this.Hand = new Hand();
            this.HandCombinations = new List<Hand>();
            this.BankBalance = 100;
            this.Bankrupt = false;
        }

        public enum Outcome
        {
            Lose,
            Win
        }
    }
}
