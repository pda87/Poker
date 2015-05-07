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

        public Game()
        {
            this.GameDeck = new List<Card>();
            this.Flop = new List<Card>();
            this.Player1 = new Player();
            this.Player1.Name = "Player 1";
            this.Player2 = new Player();
            this.Player2.Name = "Player 2";
            this.DeckCount = 51;
        }

        public void PlayGame(List<PictureBox> flopPictureBoxes, Label player1ResultLabel, Label player2ResultLabel, List<PictureBox> player1PictureBoxes,
            List<PictureBox> player2PictureBoxes, Label gameResultLabel)
        {
            this.GameDeck = GenerateGameDeck.CreateDeck();
            this.GenerateFlop();
            this.DisplayFlop(flopPictureBoxes);
            this.GenerateRandomHand(this.Player1);
            this.GenerateRandomHand(this.Player2);

            this.BuildHandCombinations(this.Player1);
            this.BuildHandCombinations(this.Player2);

            this.CheckHandCombinations(this.Player1);
            this.CheckHandCombinations(this.Player2);

            this.DisplayPlayerResults(this.Player1.OutputString, this.Player1, player1ResultLabel, player1PictureBoxes);
            this.DisplayPlayerResults(this.Player2.OutputString, this.Player2, player2ResultLabel, player2PictureBoxes);
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

            //foreach (Card card in this.Flop)
            //{
            //    player.Hand.HandCardList.Add(card);
            //}

            //player.Hand.SortedHand = player.Hand.HandCardList.OrderBy(o => o.CardValue).ToList<Card>();

        }

        private void BuildHandCombinations(Player player)
        {
            List<Card> playerFirstHandCombination = new List<Card>();
            List<Card> playerSecondHandCombination = new List<Card>();
            List<Card> playerThirdHandCombination = new List<Card>();

            foreach (Card card in player.Hand.HandCardList)
            {
                playerFirstHandCombination.Add(card);
                playerSecondHandCombination.Add(card);
                playerThirdHandCombination.Add(card);
            }

            playerFirstHandCombination.Add(this.Flop[0]);
            playerFirstHandCombination.Add(this.Flop[1]);
            playerFirstHandCombination.Add(this.Flop[2]);

            List<Card> firstCombinationSorted = playerFirstHandCombination.OrderBy(o => o.CardValue).ToList<Card>();

            player.HandCombinations.Add(new Hand()
            {
                SortedHand = firstCombinationSorted,
                CardValue1 = player.Hand.CardValue1,
                CardValue2 = player.Hand.CardValue2,
                OutputString = player.OutputString
            });

            playerSecondHandCombination.Add(this.Flop[1]);
            playerSecondHandCombination.Add(this.Flop[2]);
            playerSecondHandCombination.Add(this.Flop[3]);

            List<Card> secondCombinationSorted = playerSecondHandCombination.OrderBy(o => o.CardValue).ToList<Card>();

            player.HandCombinations.Add(new Hand()
            {
                SortedHand = secondCombinationSorted,
                CardValue1 = player.Hand.CardValue1,
                CardValue2 = player.Hand.CardValue2,
                OutputString = player.OutputString
            });

            playerThirdHandCombination.Add(this.Flop[2]);
            playerThirdHandCombination.Add(this.Flop[3]);
            playerThirdHandCombination.Add(this.Flop[4]);

            List<Card> thirdCombinationSorted = playerThirdHandCombination.OrderBy(o => o.CardValue).ToList<Card>();

            player.HandCombinations.Add(new Hand()
            {
                SortedHand = thirdCombinationSorted,
                CardValue1 = player.Hand.CardValue1,
                CardValue2 = player.Hand.CardValue2,
                OutputString = player.OutputString
            });
        }

        private void CheckHandCombinations(Player player)
        {
            List<HandResult> handResultChecks = new List<HandResult>();

            foreach (Hand hand in player.HandCombinations)
            {
                this.CheckSortedHand(hand);
                handResultChecks.Add(hand.HandResult);
            }

            var maxHand = player.HandCombinations.OrderBy(o => o.HandResult);

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
                secondValue = "and pair of " + hand.CardValue2.ToString() + "s";
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

            ///player.Hand.OutputString += player.Hand.HandResult.ToString();

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
            if (this.Player1.Hand.HandResult > this.Player2.Hand.HandResult)
            {
                gameResultLabel.Text = "PLAYER 1 WINS!";
                gameResultLabel.Refresh();
                this.Player1.Hand.Outcome = Outcome.Won;
                this.Player2.Hand.Outcome = Outcome.Lost;
            }

            else if (this.Player2.Hand.HandResult > this.Player1.Hand.HandResult)
            {
                gameResultLabel.Text = "PLAYER 2 WINS!";
                gameResultLabel.Refresh();
                this.Player2.Hand.Outcome = Outcome.Won;
                this.Player1.Hand.Outcome = Outcome.Lost;
            }

            else if (this.Player1.Hand.HandResult == this.Player2.Hand.HandResult)
            {
                if (this.Player1.Hand.CardValue1 > this.Player2.Hand.CardValue1)
                {
                    gameResultLabel.Text = "PLAYER 1 WINS!";
                    gameResultLabel.Refresh();
                    this.Player1.Hand.Outcome = Outcome.Won;
                    this.Player2.Hand.Outcome = Outcome.Lost;
                }

                else if (this.Player2.Hand.CardValue1 > this.Player1.Hand.CardValue1)
                {
                    gameResultLabel.Text = "PLAYER 2 WINS!";
                    gameResultLabel.Refresh();
                    this.Player2.Hand.Outcome = Outcome.Won;
                    this.Player1.Hand.Outcome = Outcome.Lost;
                }

                else
                {
                    gameResultLabel.Text = "DRAW...";
                    gameResultLabel.Refresh();

                    List<Card> player1InitialSorted = new List<Card>() { this.Player1.Hand.HandCardList[0], this.Player1.Hand.HandCardList[1] };
                    List<Card> player2InitialSorted = new List<Card>() { this.Player2.Hand.HandCardList[0], this.Player2.Hand.HandCardList[1] };

                    if (player1InitialSorted.Max(o => o.CardValue) > player2InitialSorted.Max(o => o.CardValue))
                    {
                        gameResultLabel.Text = "PLAYER 1 WINS!";
                        gameResultLabel.Refresh();
                        this.Player1.Hand.Outcome = Outcome.Won;
                        this.Player2.Hand.Outcome = Outcome.Lost;
                    }

                    else if (player2InitialSorted.Max(o => o.CardValue) > player1InitialSorted.Max(o => o.CardValue))
                    {
                        gameResultLabel.Text = "PLAYER 2 WINS!";
                        gameResultLabel.Refresh();
                        this.Player2.Hand.Outcome = Outcome.Won;
                        this.Player1.Hand.Outcome = Outcome.Lost;
                    }

                    else
                    {
                        gameResultLabel.Text = "DRAW...";
                        gameResultLabel.Refresh();
                    }

                }
            }

            else
            {
                gameResultLabel.Text = "UNHANDLED CASE...";
                gameResultLabel.Refresh();

            }
        }

        //SEPARATE INTO A CLASS?
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
