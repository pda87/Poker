using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class BetManager
    {
        public List<List<Player>> BettingCombinations { get; set; }
        public int PlayOrder { get; set; }
        public StringBuilder BetOutput { get; set; }
        public Random RandomNumber { get; set; }


        public BetManager(List<Player> playerList)
        {
            this.BettingCombinations = new List<List<Player>>();
            this.BettingCombinations.Add(playerList);
            this.BettingCombinations.Add(new List<Player>() { playerList[1], playerList[2], playerList[3], playerList[0] });
            this.BettingCombinations.Add(new List<Player>() { playerList[2], playerList[3], playerList[0], playerList[1] });
            this.BettingCombinations.Add(new List<Player>() { playerList[3], playerList[0], playerList[1], playerList[2] });

            this.PlayOrder = 0;

            this.BetOutput = new StringBuilder();

            this.RandomNumber = new Random();
        }

        public void CalculateBet(Player player)
        {
            if (player.HandCombinations.Exists(hand => hand.HandResult.Equals(HandResult.StraightFlush)))
            {
                player.Bet = this.RandomNumber.Next(1, player.BankBalance);
            }

            else if (player.HandCombinations.Exists(hand => hand.HandResult.Equals(HandResult.FourOfAKind))
                && player.BankBalance >= 4)
            {
                player.Bet = this.RandomNumber.Next(1, player.BankBalance)/2;
            }

            else if (player.HandCombinations.Exists(hand => hand.HandResult.Equals(HandResult.FullHouse))
                && player.BankBalance >= 8)
            {
                player.Bet = this.RandomNumber.Next(1, player.BankBalance)/4;
            }

            else if (player.HandCombinations.Exists(hand => hand.HandResult.Equals(HandResult.Flush))
                && player.BankBalance >= 8)
            {
                player.Bet = this.RandomNumber.Next(8, player.BankBalance);
            }

            else if (player.HandCombinations.Exists(hand => hand.HandResult.Equals(HandResult.Straight))
                && player.BankBalance >= 10)
            {
                player.Bet = this.RandomNumber.Next(10, player.BankBalance);
            }

            else if (player.HandCombinations.Exists(hand => hand.HandResult.Equals(HandResult.HighCard))
                && player.BankBalance >= 6)
            {
                player.Bet = this.RandomNumber.Next(1, player.BankBalance)/3;
            }

            else
            {
                player.Bet = this.RandomNumber.Next(1, player.BankBalance);
            }

        }


    }
}
