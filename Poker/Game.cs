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
        public List<Card> GameDeck { get; set; }
        public int DeckCount { get; set; }
        public List<Card> Flop { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player Player3 { get; set; }
        public Player Player4 { get; set; }

        public Game()
        {
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

            this.DeckCount = 51;
        }

        public void PlayGame(List<PictureBox> flopPictureBoxes, Label player1ResultLabel, Label player2ResultLabel, Label player3ResultLabel, 
            Label player4ResultLabel, List<PictureBox> player1PictureBoxes,List<PictureBox> player2PictureBoxes, 
            List<PictureBox> player3PictureBoxes, List<PictureBox> player4PictureBoxes, Label gameResultLabel)
        {
            this.GameDeck = GenerateGameDeck.CreateDeck();
            this.GenerateFlop();
            this.DisplayFlop(flopPictureBoxes);
            this.GenerateRandomHand(this.Player1);
            this.GenerateRandomHand(this.Player2);
            this.GenerateRandomHand(this.Player3);
            this.GenerateRandomHand(this.Player4);

            this.BuildHandCombinations(this.Player1);
            this.BuildHandCombinations(this.Player2);
            this.BuildHandCombinations(this.Player3);
            this.BuildHandCombinations(this.Player4);

            this.CheckHandCombinations(this.Player1);
            this.CheckHandCombinations(this.Player2);
            this.CheckHandCombinations(this.Player3);
            this.CheckHandCombinations(this.Player4);

            this.DisplayPlayerResults(this.Player1.OutputString, this.Player1, player1ResultLabel, player1PictureBoxes);
            this.DisplayPlayerResults(this.Player2.OutputString, this.Player2, player2ResultLabel, player2PictureBoxes);
            this.DisplayPlayerResults(this.Player3.OutputString, this.Player3, player3ResultLabel, player3PictureBoxes);
            this.DisplayPlayerResults(this.Player4.OutputString, this.Player4, player4ResultLabel, player4PictureBoxes);
            this.DisplayGameResults(gameResultLabel);
        }

        public void GenerateFlop()
        {
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                int randomCard = random.Next(0, this.DeckCount - 1);

                this.Flop.Add(this.GameDeck[randomCard]);
                this.GameDeck.RemoveAt(randomCard);
                this.DeckCount--;
            }
        }

        public void DisplayFlop(List<PictureBox> flopPictureBoxes)
        {
            for (int i = 0; i < 5; i++)
            {
                flopPictureBoxes[i].ImageLocation = this.Flop[i].Image;
                flopPictureBoxes[i].SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        public void GenerateRandomHand(Player player)
        {
            Random random = new Random();

            //TAKE 5 RANDOM CARDS FROM THE DECK OF CARDS
            //USING 51 INSTEAD OF 52 FOR THE ARRAY ITERATION
            //int deckCount = 51;

            player.Hand.HandCardList = new List<Card>();

            for (int i = 0; i < 2; i++)
            {
                int randomCard = random.Next(0, this.DeckCount - 1);

                player.Hand.HandCardList.Add(GameDeck[randomCard]);
                this.GameDeck.RemoveAt(randomCard);
                this.DeckCount--;
            }
        }

        private void BuildHandCombinations(Player player)
        {
            flopCombinations(player, 0, 1, 2);
            flopCombinations(player, 0, 1, 3);
            flopCombinations(player, 0, 1, 4);

            flopCombinations(player, 0, 2, 3);
            flopCombinations(player, 0, 2, 4);

            flopCombinations(player, 1, 2, 3);
            flopCombinations(player, 1, 2, 4);

            flopCombinations(player, 2, 3, 4);
        }

        public void flopCombinations(Player player, int firstFlopCard, int secondFlopCard, int thirdFlopCard)
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
            //List<HandResult> handResultChecks = new List<HandResult>();

            foreach (Hand hand in player.HandCombinations)
            {
                this.CheckSortedHand(hand);
                //handResultChecks.Add(hand.HandResult);
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

        public void CheckSortedHand(Hand hand)
        {
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

        private void DisplayPlayerResults(string outputString, Player player, Label playerResultLabel, List<PictureBox> playerPictureBoxes)
        {
            playerResultLabel.Text = "";

            playerResultLabel.Text = player.Name + ": " + player.OutputString;

            for (int i = 0; i < 2; i++)
            {
                playerPictureBoxes[i].ImageLocation = player.Hand.HandCardList[i].Image;
                playerPictureBoxes[i].SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void DisplayGameResults(Label gameResultLabel)
        {

            List<Player> playerList = new List<Player>();
            playerList.Add(new Player() { Name = this.Player1.Name, Hand = this.Player1.Hand });
            playerList.Add(new Player() { Name = this.Player2.Name, Hand = this.Player2.Hand });
            playerList.Add(new Player() { Name = this.Player3.Name, Hand = this.Player3.Hand });
            playerList.Add(new Player() { Name = this.Player4.Name, Hand = this.Player4.Hand });

            List<Player> playersSortedByHand = playerList.OrderByDescending(player => player.Hand.HandResult).ToList<Player>();

            HandResult winningHand = playersSortedByHand[0].Hand.HandResult;

            List<Player> winningPlayers = (from player in playersSortedByHand
                         where player.Hand.HandResult.Equals(winningHand)
                         select player).ToList<Player>();

            List<Player> winningHands = winningPlayers.OrderByDescending(hand => hand.Hand.CardValue1).ToList();

            if (winningHands.Count == 1)
            {
                gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
            }

            else if (winningHands.Count == 2)
            {
                if (winningHands[0].Hand.CardValue1 > winningHands[1].Hand.CardValue1)
                {
                    gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
                }

                else if (winningHands[1].Hand.CardValue1 > winningHands[0].Hand.CardValue1)
                {
                    gameResultLabel.Text = winningHands[1].Name.ToUpper() + " is the winner!".ToUpper();
                }

                else if (winningHands[0].Hand.CardValue1 == winningHands[1].Hand.CardValue1)
                {

                    if (winningHands[0].Hand.CardValue2 > winningHands[1].Hand.CardValue2)
                    {
                        gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[1].Hand.CardValue2 > winningHands[0].Hand.CardValue2)
                    {
                        gameResultLabel.Text = winningHands[1].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[0].Hand.HandCardList.Max(o => o.CardValue) > winningHands[1].Hand.HandCardList.Max(o => o.CardValue))
                    {
                        gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[1].Hand.HandCardList.Max(o => o.CardValue) > winningHands[0].Hand.HandCardList.Max(o => o.CardValue))
                    {
                        gameResultLabel.Text = winningHands[1].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[0].Hand.HandCardList.Min(o => o.CardValue) > winningHands[1].Hand.HandCardList.Min(o => o.CardValue))
                    {
                        gameResultLabel.Text = winningHands[0].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else if (winningHands[1].Hand.HandCardList.Min(o => o.CardValue) > winningHands[0].Hand.HandCardList.Min(o => o.CardValue))
                    {
                        gameResultLabel.Text = winningHands[1].Name.ToUpper() + " is the winner!".ToUpper();
                    }

                    else
                    {
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
                gameResultLabel.Text = winningPlayer[0].Name.ToUpper() + " is the winner!".ToUpper();
            }

            else if (winningHands.Count == 4)
            {
                var winningPlayer = winningHands.OrderByDescending(player => player.Hand.HandCardList.Max(o => o.CardValue)).ToList();
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
