using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void bettingRound(Game game, Label potLabel, 
            Label betOutputLabel, Label player1BankBalance, Label player2BankBalance, Label player3BankBalance, Label player4BankBalance)
        {
            this.implementBets(game);
            this.potManager(game, potLabel);
            betOutputLabel.Text = this.BetOutput.ToString();
            game.displayPlayerBankBalances(player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance);
        }

        private void potManager(Game game, Label potLabel)
        {
            foreach (Player player in game.PlayerList)
            {
                if (player == game.Player1)
                {
                    continue;
                }

                player.BankBalance -= player.Bet;

                if (player.BankBalance <= 0)
                {
                    player.BankBalance = 0;
                }
            }

            foreach (Player player in game.PlayerList)
            {
                if (player.Bankrupt)
                {
                    continue;
                }
                game.Pot += player.Bet;
            }

            potLabel.Text = String.Format("{0:C}", game.Pot);
            potLabel.Refresh();

        }

        private void implementBets(Game game)
        {
            List<Player> computerPlayers = new List<Player>() { game.Player2, game.Player3, game.Player4 };

            foreach (Player player in computerPlayers)
            {
                if (player.BankBalance > 0)
                {
                    this.CalculateBet(player);
                }

                else if (player.BankBalance == 0)
                {
                    player.Bet = 0;
                }
            }

            this.BetOutput.Clear();

            this.BetOutput.AppendLine("BETS:");
            this.BetOutput.AppendLine("GAME: " + game.RoundNumber);

            if (!game.Player2.Bankrupt)
            {
                this.BetOutput.AppendLine("Player 2: " + String.Format("{0:C}", game.Player2.Bet).ToUpper());
            }

            if (!game.Player3.Bankrupt)
            {
                this.BetOutput.AppendLine("Player 3: " + String.Format("{0:C}", game.Player3.Bet).ToUpper());
            }

            if (!game.Player4.Bankrupt)
            {
                this.BetOutput.AppendLine("Player 4 : " + String.Format("{0:C}", game.Player4.Bet).ToUpper());
            }
        }

        private void CalculateBet(Player player)
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

            if (player.Bet == 0)
            {
                player.Bet = 1;
            }

        }
    }
}
