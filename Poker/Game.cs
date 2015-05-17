using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
    class Game
    {
        public bool FirstClick { get; set; }
        public List<Card> GameDeck { get; set; }
        public int DeckCount { get; set; }
        public List<Card> Flop { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player Player3 { get; set; }
        public Player Player4 { get; set; }
        public List<Player> PlayerList { get; set; }
        public int Pot { get; set; }
        public bool EndGame { get; set; }
        public bool ResetGame { get; set; }
        public bool GameOver { get; set; }
        public int RoundNumber { get; set; }
        public BetManager BetManager { get; set; }

        public Game()
        {
            this.FirstClick = true;
            this.GameDeck = new List<Card>();
            this.Flop = new List<Card>();

            this.Player1 = new Player();
            this.Player1.Name = "Player 1";
            this.Player2 = new Player();
            this.Player2.Name = "Player 2";
            this.Player3 = new Player();
            this.Player3.Name = "Player 3";
            this.Player4 = new Player();
            this.Player4.Name = "Player 4";

            this.PlayerList = new List<Player>()
            {
                this.Player1,
                this.Player2,
                this.Player3,
                this.Player4
            };

            this.DeckCount = 51;

            this.EndGame = false;
            this.RoundNumber = 1;



            this.BetManager = new BetManager(this.PlayerList);
        }

        public void displayPlayerBankBalances(Label player1BankBalance, Label player2BankBalance, Label player3BankBalance, Label player4BankBalance)
        {
            player1BankBalance.Text = "Player Balance: " + String.Format("{0:C}", this.Player1.BankBalance);
            player1BankBalance.Refresh();
            player2BankBalance.Text = "Player Balance: " + String.Format("{0:C}", this.Player2.BankBalance);
            player2BankBalance.Refresh();
            player3BankBalance.Text = "Player Balance: " + String.Format("{0:C}", this.Player3.BankBalance);
            player3BankBalance.Refresh();
            player4BankBalance.Text = "Player Balance: " + String.Format("{0:C}", this.Player4.BankBalance);
            player4BankBalance.Refresh();
        }

        public void PlayGame(List<PictureBox> flopPictureBoxes, Label player1ResultLabel, Label player2ResultLabel, Label player3ResultLabel,
            Label player4ResultLabel, Label player1BankBalance, Label player2BankBalance, Label player3BankBalance, Label player4BankBalance,
            List<PictureBox> player1PictureBoxes, List<PictureBox> player2PictureBoxes, List<PictureBox> player3PictureBoxes,
            List<PictureBox> player4PictureBoxes, Label gameResultLabel, Label potLabel)
        {

            if (this.ResetGame)
            {
                this.checkBankruptcy();

                this.DisplayGameResults(gameResultLabel);

                this.EndGame = false;
                this.ResetGame = false;
                this.Flop.Clear();
                gameResultLabel.Text = "CLICK PLAY...";

                if (this.Player1.Bankrupt)
                {
                    this.GameOver = true;
                }

            }

            else if (this.EndGame)
            {
                if (!this.Player2.Bankrupt)
                {
                    this.DisplayPlayerResults(this.Player2.OutputString, this.Player2, player2ResultLabel, player2BankBalance, player2PictureBoxes);
                }

                if (!this.Player3.Bankrupt)
                {
                    this.DisplayPlayerResults(this.Player3.OutputString, this.Player3, player3ResultLabel, player3BankBalance, player3PictureBoxes);
                }

                if (!this.Player4.Bankrupt)
                {
                    this.DisplayPlayerResults(this.Player4.OutputString, this.Player4, player4ResultLabel, player4BankBalance, player4PictureBoxes);
                }

                this.DisplayGameResults(gameResultLabel);
                this.ResetGame = true;

                if (this.Player1.Bankrupt)
                {
                    this.GameOver = true;
                }

                this.RoundNumber++;
                this.BetManager.PlayOrder++;

                if (this.BetManager.PlayOrder == 4)
                {
                    this.BetManager.PlayOrder = 0;
                }

            }

            #region flop5
            if (this.Flop.Count == 5)
            {
                this.EndGame = true;

                displayPlayerBankBalances(player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance);

                if (this.Player1.Bankrupt)
                {
                    this.GameOver = true;
                }
            }
            #endregion

            #region flop4
            else if (this.Flop.Count == 4)
            {
                this.GenerateFlop(1);

                flopPictureBoxes[4].ImageLocation = this.Flop[4].Image;
                flopPictureBoxes[4].SizeMode = PictureBoxSizeMode.AutoSize;

                displayPlayerBankBalances(player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance);

                foreach (Player player in this.PlayerList)
                {
                    if (player.Bankrupt)
                    {
                        continue;
                    }

                    this.flopCombinations(player, 0, 1, 4);
                    this.flopCombinations(player, 1, 2, 4);
                    this.flopCombinations(player, 2, 3, 4);
                    this.CheckHandCombinations(player);
                }

                this.DisplayPlayerResults(this.Player1.OutputString, this.Player1, player1ResultLabel, player1BankBalance, player1PictureBoxes);
                this.EndGame = true;

            }
            #endregion

            #region flop3
            else if (this.Flop.Count == 3)
            {
                this.GenerateFlop(1);

                flopPictureBoxes[3].ImageLocation = this.Flop[3].Image;
                flopPictureBoxes[3].SizeMode = PictureBoxSizeMode.AutoSize;

                displayPlayerBankBalances(player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance);

                foreach (Player player in this.PlayerList)
                {
                    if (player.Bankrupt)
                    {
                        continue;
                    }

                    this.flopCombinations(player, 0, 1, 3);
                    this.flopCombinations(player, 1, 2, 3);
                    this.CheckHandCombinations(player);
                }

                this.DisplayPlayerResults(this.Player1.OutputString, this.Player1, player1ResultLabel, player1BankBalance, player1PictureBoxes);
            }
            #endregion

            #region flop0

            else if (this.Flop.Count == 0)
            {

                foreach (PictureBox pictureBox in player2PictureBoxes)
                {
                    pictureBox.ImageLocation = @"CardImages/CardBacks/blue.png";
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                foreach (PictureBox pictureBox in player3PictureBoxes)
                {
                    pictureBox.ImageLocation = @"CardImages/CardBacks/blue.png";
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                foreach (PictureBox pictureBox in player4PictureBoxes)
                {
                    pictureBox.ImageLocation = @"CardImages/CardBacks/blue.png";
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                foreach (PictureBox pictureBox in flopPictureBoxes)
                {
                    pictureBox.ImageLocation = @"CardImages/CardBacks/blue.png";
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                this.ClearGameVariables();
                this.GameDeck = GenerateGameDeck.CreateDeck();
                this.DeckCount = this.GameDeck.Count();
                this.GenerateFlop(3);
                this.DisplayFlop(flopPictureBoxes);

                this.PlayerList.ForEach(player => player.Hand.GenerateRandomHand(this, player));

                displayPlayerBankBalances(player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance);

                foreach (Player player in this.PlayerList)
                {
                    if (player.Bankrupt)
                    {
                        continue;
                    }

                    this.flopCombinations(player, 0, 1, 2);
                    this.CheckHandCombinations(player);
                }

                this.DisplayPlayerResults(this.Player1.OutputString, this.Player1, player1ResultLabel, player1BankBalance, player1PictureBoxes);
            }
            #endregion
        }

        private void checkBankruptcy()
        {
            foreach (Player player in this.PlayerList)
            {
                if (player.BankBalance == 0)
                {
                    player.Bankrupt = true;
                }
            }
        }

        public void Payout(Label gameResultLabel)
        {
            var winningPlayers = (from player in this.PlayerList
                                  where player.WinOrLose.Equals(Player.Outcome.Win)
                                  select player).ToList();

            if (winningPlayers.Count == 1)
            {
                winningPlayers[0].BankBalance += this.Pot;
            }

            else if (winningPlayers.Count == 2)
            {
                var splitPot = this.Pot % 2;

                if (splitPot != 0)
                {
                    this.Pot--;
                }

                winningPlayers[0].BankBalance += this.Pot / 2;
                winningPlayers[1].BankBalance += this.Pot / 2;
            }

            else if (winningPlayers.Count == 3)
            {
                //ToDo
            }

            else if (winningPlayers.Count == 4)
            {
                //ToDo
            }

            else
            {
                //gameResultLabel.Text = "UNHANDLED CASE";
            }

            this.PlayerList.ForEach(player => player.WinOrLose = Player.Outcome.Lose);
            this.checkBankruptcy();

        }

        private void ClearGameVariables()
        {
            foreach (Player player in this.PlayerList)
            {
                player.OutputString = "";
                player.Hand.CardValue1 = 0;
                player.Hand.CardValue2 = 0;
                player.Hand.HandCardList.Clear();
                player.Hand.SortedHand.Clear();
                player.Hand.OutputString = "";
                player.Hand.HandResult = HandResult.HighCard;
                player.HandCombinations.Clear();
                player.WinOrLose = Player.Outcome.Lose;
            }

            //this.Pot = 0;
            this.Flop.Clear();
            this.GameDeck.Clear();
        }

        private void GenerateFlop(int flopSize)
        {
            Random random = new Random();

            for (int i = 0; i < flopSize; i++)
            {
                int randomCard = random.Next(0, this.DeckCount - 1);

                this.Flop.Add(this.GameDeck[randomCard]);
                this.GameDeck.RemoveAt(randomCard);
                this.DeckCount--;
            }
        }

        private void DisplayFlop(List<PictureBox> flopPictureBoxes)
        {
            for (int i = 0; i < 3; i++)
            {
                flopPictureBoxes[i].ImageLocation = this.Flop[i].Image;
                flopPictureBoxes[i].SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void flopCombinations(Player player, int firstFlopCard, int secondFlopCard, int thirdFlopCard)
        {
            List<Card> handBuilder = new List<Card>();

            handBuilder.Add(player.Hand.HandCardList[0]);
            handBuilder.Add(player.Hand.HandCardList[1]);

            handBuilder.Add(this.Flop[firstFlopCard]);
            handBuilder.Add(this.Flop[secondFlopCard]);
            handBuilder.Add(this.Flop[thirdFlopCard]);

            player.HandCombinations.Add(new Hand()
            {
                SortedHand = handBuilder.OrderBy(o => o.CardValue).ToList<Card>(),
                CardValue1 = player.Hand.CardValue1,
                CardValue2 = player.Hand.CardValue2,
                OutputString = player.OutputString
            });


        }

        private void CheckHandCombinations(Player player)
        {
            foreach (Hand hand in player.HandCombinations)
            {
                this.CheckSortedHand(hand);
            }

            var maxHand = player.HandCombinations.OrderByDescending(o => o.HandResult);

            foreach (var item in maxHand)
            {
                player.Hand.SortedHand = item.SortedHand;
                player.Hand.HandResult = item.HandResult;
                player.Hand.CardValue1 = item.CardValue1;
                player.Hand.CardValue2 = item.CardValue2;
                player.OutputString = item.OutputString;
                break;
            }
        }

        private void CheckSortedHand(Hand hand)
        {
            hand.OutputString = "";
            string firstValue = "";
            string secondValue = "";

            //STRAIGHT FLUSH CASE
            if (checkStraightFlushCase(hand))
            {
                //YOU HAVE A STRAIGHT FLUSH
                hand.HandResult = HandResult.StraightFlush;
            }

            //FOUR OF A KIND CASE
            else if (checkFourOfAKindCase(hand))
            {
                //YOU HAVE FOUR OF A KIND
                hand.HandResult = HandResult.FourOfAKind;
                firstValue = ": " + hand.CardValue1.ToString();
            }

            //FULL HOUSE CASE
            else if (checkFullHouseCase(hand))
            {
                //YOU HAVE A FULL HOUSE
                hand.HandResult = HandResult.FullHouse;
            }

            //FLUSH CASE
            else if (checkFlushCase(hand))
            {
                //YOU HAVE A FLUSH
                hand.HandResult = HandResult.Flush;
            }

              //STRAIGHT CASE
            else if (checkStraightCase(hand))
            {
                //YOU HAVE A STRAIGHT
                hand.HandResult = HandResult.Straight;
            }

              //THREE OF A KIND
            else if (checkThreeOfAKindCase(hand))
            {
                //YOU HAVE THREE OF A KIND
                hand.HandResult = HandResult.ThreeOfAKind;
                firstValue = ": " + hand.CardValue1.ToString();

            }

              //TWO PAIRS
            else if (checkIfTwoPairsCase(hand))
            {
                //YOU HAVE TWO PAIRS
                hand.HandResult = HandResult.TwoPairs;
                firstValue = ": a pair of " + hand.CardValue1.ToString() + "s";
                secondValue = "and a pair of " + hand.CardValue2.ToString() + "s";
            }

              //PAIR
            else if (checkPairCase(hand))
            {
                //YOU HAVE A PAIR
                hand.HandResult = HandResult.Pair;
                firstValue = " of " + hand.CardValue1.ToString() + "s";
            }

            //IF NO OTHER HANDS, THE HIGH CARD IS THE CATCH ALL
            else
            {
                //YOU HAVE A HIGH CARD
                hand.HandResult = HandResult.HighCard;
                hand.CardValue1 = hand.SortedHand[4].CardValue;
                firstValue = ": " + hand.SortedHand[4].Name;
            }

            hand.OutputString += hand.HandResult.ToString() + firstValue + " " + secondValue;

            if (hand.OutputString.Contains("Sixs"))
            {
                hand.OutputString = hand.OutputString.Replace("Sixs", "Sixes");
            }

        }

        private void DisplayPlayerResults(string outputString, Player player, Label playerResultLabel, Label playerBankBalance, List<PictureBox> playerPictureBoxes)
        {
            playerResultLabel.Text = "";

            playerResultLabel.Text = player.OutputString;

            for (int i = 0; i < 2; i++)
            {
                playerPictureBoxes[i].ImageLocation = player.Hand.HandCardList[i].Image;
                playerPictureBoxes[i].SizeMode = PictureBoxSizeMode.AutoSize;
            }

            playerBankBalance.Text = "Player Balance: " + String.Format("{0:C}", player.BankBalance);
        }

        //THIS HAS THE player.Outcome = Outcome.Win/player.Outcome = Outcome.Lose SECTIONS IN IT
        private void DisplayGameResults(Label gameResultLabel)
        {

            List<Player> playersSortedByHand = this.PlayerList.OrderByDescending(player => player.Hand.HandResult).ToList<Player>();

            HandResult winningHand = playersSortedByHand[0].Hand.HandResult;

            List<Player> winningPlayers = (from player in playersSortedByHand
                                           where player.Hand.HandResult.Equals(winningHand)
                                           select player).ToList<Player>();

            List<Player> winningHands = winningPlayers.OrderByDescending(hand => hand.Hand.CardValue1).ToList();

            if (winningHands.Count == 1)
            {
                winningHands[0].WinOrLose = Player.Outcome.Win;
                gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
            }

            else if (winningHands.Count == 2)
            {
                if (winningHands[0].Hand.CardValue1 > winningHands[1].Hand.CardValue1)
                {
                    winningHands[0].WinOrLose = Player.Outcome.Win;
                    gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
                }

                else if (winningHands[1].Hand.CardValue1 > winningHands[0].Hand.CardValue1)
                {
                    winningHands[1].WinOrLose = Player.Outcome.Win;
                    gameResultLabel.Text = winningHands[1].Name.ToUpper() + " is the winner!".ToUpper();
                }

                else if (winningHands[0].Hand.CardValue1 == winningHands[1].Hand.CardValue1)
                {

                    if (winningHands[0].Hand.CardValue2 > winningHands[1].Hand.CardValue2)
                    {
                        winningHands[0].WinOrLose = Player.Outcome.Win;
                        gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[1].Hand.CardValue2 > winningHands[0].Hand.CardValue2)
                    {
                        winningHands[1].WinOrLose = Player.Outcome.Win;
                        gameResultLabel.Text = winningHands[1].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[0].Hand.HandCardList.Max(o => o.CardValue) > winningHands[1].Hand.HandCardList.Max(o => o.CardValue))
                    {
                        winningHands[0].WinOrLose = Player.Outcome.Win;
                        gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[1].Hand.HandCardList.Max(o => o.CardValue) > winningHands[0].Hand.HandCardList.Max(o => o.CardValue))
                    {
                        winningHands[1].WinOrLose = Player.Outcome.Win;
                        gameResultLabel.Text = winningHands[1].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[0].Hand.HandCardList.Min(o => o.CardValue) > winningHands[1].Hand.HandCardList.Min(o => o.CardValue))
                    {
                        winningHands[0].WinOrLose = Player.Outcome.Win;
                        gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[1].Hand.HandCardList.Min(o => o.CardValue) > winningHands[0].Hand.HandCardList.Min(o => o.CardValue))
                    {
                        winningHands[1].WinOrLose = Player.Outcome.Win;
                        gameResultLabel.Text = winningHands[1].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else
                    {
                        winningHands[0].WinOrLose = Player.Outcome.Win;
                        winningHands[1].WinOrLose = Player.Outcome.Win;
                        gameResultLabel.Text = "DRAW: " + winningHands[0].Name + "/" + winningHands[1].Name;
                    }

                }

                else
                {
                    gameResultLabel.Text = "UNHANDLED CASE";
                }
            }

            else if (winningHands.Count == 3)
            {
                var winningPlayer = winningHands.OrderByDescending(player => player.Hand.HandCardList.Max(o => o.CardValue)).ToList();
                winningHands[0].WinOrLose = Player.Outcome.Win;
                gameResultLabel.Text = winningPlayer[0].Name.ToUpper() + " is the winner!".ToUpper();
            }

            else if (winningHands.Count == 4)
            {
                var winningPlayer = winningHands.OrderByDescending(player => player.Hand.HandCardList.Max(o => o.CardValue)).ToList();
                winningHands[0].WinOrLose = Player.Outcome.Win;
                gameResultLabel.Text = winningPlayer[0].Name.ToUpper() + " is the winner!".ToUpper();
            }

            else
            {
                gameResultLabel.Text = "UNHANDLED CASE";
            }
        }

        #region individualhandchecks

        private bool checkStraightFlushCase(Hand hand)
        {
            bool checkStraightFlush = false;

            List<Suit> suitList = new List<Suit>() { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };

            foreach (Suit suit in suitList)
            {
                if (hand.SortedHand.Count(o => o.Suit.Equals(suit)) == 5
                  && (hand.SortedHand[1].CardValue == hand.SortedHand[0].CardValue + 1)
                  && (hand.SortedHand[2].CardValue == hand.SortedHand[1].CardValue + 1)
                  && (hand.SortedHand[3].CardValue == hand.SortedHand[2].CardValue + 1)
                  && (hand.SortedHand[4].CardValue == hand.SortedHand[3].CardValue + 1))
                {
                    checkStraightFlush = true;
                    break;
                }

                else
                {
                    checkStraightFlush = false;
                }

            }
            return checkStraightFlush;
        }

        private bool checkFourOfAKindCase(Hand hand)
        {
            bool checkFourOfAKind = false;

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            foreach (CardValue cardValue in cardValueList)
            {
                if (hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 4)
                {
                    checkFourOfAKind = true;
                    hand.CardValue1 = cardValue;
                    break;
                }
            }
            return checkFourOfAKind;
        }

        private bool checkFullHouseCase(Hand hand)
        {

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            bool checkFullHouse = false;

            bool threeOfAKind = false;
            bool twoOfAKind = false;

            foreach (CardValue cardValue in cardValueList)
            {
                if (hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 3)
                {
                    threeOfAKind = true;
                    cardValueList.Remove(cardValue);
                    hand.CardValue1 = cardValue;
                    break;
                }
            }

            if (threeOfAKind)
            {
                foreach (CardValue cardValue in cardValueList)
                {
                    if (hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 2)
                    {
                        twoOfAKind = true;
                        hand.CardValue2 = cardValue;
                        break;
                    }
                }
            }

            if (threeOfAKind && twoOfAKind)
            {
                checkFullHouse = true;
            }

            else
            {
                checkFullHouse = false;
            }

            return checkFullHouse;
        }

        private bool checkStraightCase(Hand hand)
        {
            bool checkStraight = false;

            if ((hand.SortedHand[1].CardValue == hand.SortedHand[0].CardValue + 1)
                && (hand.SortedHand[2].CardValue == hand.SortedHand[1].CardValue + 1)
                  && (hand.SortedHand[3].CardValue == hand.SortedHand[2].CardValue + 1)
                  && (hand.SortedHand[4].CardValue == hand.SortedHand[3].CardValue + 1))
            {
                checkStraight = true;
            }
            return checkStraight;
        }

        private bool checkFlushCase(Hand hand)
        {
            bool checkFlush = false;

            List<Suit> suitList = new List<Suit>() { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };

            foreach (Suit suit in suitList)
            {
                if ((hand.SortedHand.Count(o => o.Suit.Equals(suit)) == 5))
                {
                    checkFlush = true;
                    break;
                }
            }
            return checkFlush;
        }

        private bool checkThreeOfAKindCase(Hand hand)
        {
            bool checkThreeOfAKind = false;

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            foreach (CardValue cardValue in cardValueList)
            {
                if (hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 3)
                {
                    checkThreeOfAKind = true;
                    hand.CardValue1 = cardValue;
                    break;
                }
            }

            return checkThreeOfAKind;
        }

        private bool checkIfTwoPairsCase(Hand hand)
        {
            bool checkTwoPairs = false;

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            bool firstPair = false;
            bool secondPair = false;

            foreach (CardValue cardValue in cardValueList)
            {
                if (hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 2)
                {
                    firstPair = true;
                    cardValueList.Remove(cardValue);
                    hand.CardValue1 = cardValue;
                    break;
                }
            }

            foreach (CardValue cardValue in cardValueList)
            {
                if (hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 2)
                {
                    secondPair = true;
                    hand.CardValue2 = cardValue;
                    break;
                }
            }

            if (firstPair && secondPair)
            {
                checkTwoPairs = true;
            }

            else
            {
                checkTwoPairs = false;
            }

            return checkTwoPairs;
        }

        private bool checkPairCase(Hand hand)
        {
            bool checkPair = false;

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            foreach (CardValue cardValue in cardValueList)
            {
                if (hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 2)
                {
                    checkPair = true;
                    hand.CardValue1 = cardValue;
                    break;
                }
            }
            return checkPair;
        }

        #endregion
    }
}
