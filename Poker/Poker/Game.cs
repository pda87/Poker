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

        public static void PlayGame(List<PictureBox> flopPictureBoxes, Label player1ResultLabel, Label player2ResultLabel, List<PictureBox> player1PictureBoxes, List<PictureBox> player2PictureBoxes,
            Label gameResultLabel)
        {
            Game game = new Game();
            game.GameDeck = GenerateGameDeck.CreateDeck();
            game.GenerateFlop();
            game.DisplayFlop(flopPictureBoxes);
            game.GenerateRandomHand(game.Player1);
            game.GenerateRandomHand(game.Player2);
            game.CheckSortedHand(game.Player1, game.Player1.OutputString);
            game.CheckSortedHand(game.Player2, game.Player2.OutputString);
            game.DisplayPlayerResults(game.Player1.OutputString, game.Player1, player1ResultLabel, player1PictureBoxes);
            game.DisplayPlayerResults(game.Player2.OutputString, game.Player2, player2ResultLabel, player2PictureBoxes);
            game.DisplayGameResults(gameResultLabel);
        }

        public void GenerateFlop()
        {
            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                int randomCard = random.Next(0, this.DeckCount - 1);

                this.Flop.Add(this.GameDeck[randomCard]);
                this.GameDeck.RemoveAt(randomCard);
            }

            this.DeckCount -= 2;
        }

        public void DisplayFlop(List<PictureBox> flopPictureBoxes)
        {
            for (int i = 0; i < 3; i++)
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

            foreach (Card card in this.Flop)
            {
                player.Hand.HandCardList.Add(card);
            }
            
            player.Hand.SortedHand = player.Hand.HandCardList.OrderBy(o => o.CardValue).ToList<Card>();

        }

        public void CheckSortedHand(Player player, string outputString)
        {
            //HAND FOR TESTING...
            //this.Player1.Hand.SortedHand.Clear();

            //this.Player1.Hand.SortedHand.Add(new Card() { Suit = Suit.Clubs, Value = 2 });
            //this.Player1.Hand.SortedHand.Add(new Card() { Suit = Suit.Diamonds, Value = 3 });
            //this.Player1.Hand.SortedHand.Add(new Card() { Suit = Suit.Hearts, Value = 4 });
            //this.Player1.Hand.SortedHand.Add(new Card() { Suit = Suit.Diamonds, Value = 9 });
            //this.Player1.Hand.SortedHand.Add(new Card() { Suit = Suit.Clubs, Value = 9 });

            string firstValue = "";
            string secondValue = "";

            //STRAIGHT FLUSH CASE
            if (checkStraightFlushCase(player))
            {
                //YOU HAVE A STRAIGHT FLUSH
                player.Hand.HandResult = HandResult.StraightFlush;
            }

            //FOUR OF A KIND CASE
            else if (checkFourOfAKindCase(player))
            {
                //YOU HAVE FOUR OF A KIND
                player.Hand.HandResult = HandResult.FourOfAKind;
                firstValue = ": " + player.Hand.CardValue1.ToString();
            }

            //FULL HOUSE CASE
            else if (checkFullHouseCase(player))
            {
                //YOU HAVE A FULL HOUSE
                player.Hand.HandResult = HandResult.FullHouse;
            }

            //FLUSH CASE
            else if (checkFlushCase(player))
            {
                //YOU HAVE A FLUSH
                player.Hand.HandResult = HandResult.Flush;
            }

              //STRAIGHT CASE
            else if (checkStraightCase(player))
            {
                //YOU HAVE A STRAIGHT
                player.Hand.HandResult = HandResult.Straight;
            }

              //THREE OF A KIND
            else if (checkThreeOfAKindCase(player))
            {
                //YOU HAVE THREE OF A KIND
                player.Hand.HandResult = HandResult.ThreeOfAKind;
                firstValue = ": " + player.Hand.CardValue1.ToString();

            }

              //TWO PAIRS
            else if (checkIfTwoPairsCase(player))
            {
                //YOU HAVE TWO PAIRS
                player.Hand.HandResult = HandResult.TwoPairs;
                firstValue = ": a pair of " + player.Hand.CardValue1.ToString() + "s";
                secondValue = "and pair of " + player.Hand.CardValue2.ToString() + "s";
            }

              //PAIR
            else if (checkPairCase(player))
            {
                //YOU HAVE A PAIR
                player.Hand.HandResult = HandResult.Pair;
                firstValue = " of " + player.Hand.CardValue1.ToString() + "s";
            }

            //IF NO OTHER HANDS, THE HIGH CARD IS THE CATCH ALL
            else
            {
                //YOU HAVE A HIGH CARD
                player.Hand.HandResult = HandResult.HighCard;
                player.Hand.CardValue1 = player.Hand.SortedHand[4].CardValue;
                firstValue = ": " + player.Hand.SortedHand[4].Name;
            }

            //outputString += player.Hand.HandResult.ToString() + firstValue + " " + secondValue;

            player.OutputString += player.Hand.HandResult.ToString() + firstValue + " " + secondValue;

        }

        private void DisplayPlayerResults(string outputString, Player player, Label playerResultLabel, List<PictureBox> playerPictureBoxes)
        {
            playerResultLabel.Text = "";
            playerResultLabel.Text = player.Name + ": "+ player.OutputString;

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
            }

            else if (this.Player2.Hand.HandResult > this.Player1.Hand.HandResult)
            {
                gameResultLabel.Text = "PLAYER 2 WINS!";
                gameResultLabel.Refresh();
            }

            else if (this.Player1.Hand.HandResult == this.Player2.Hand.HandResult)
            {
                if (this.Player1.Hand.CardValue1 > this.Player2.Hand.CardValue1)
                {
                    gameResultLabel.Text = "PLAYER 1 WINS!";
                    gameResultLabel.Refresh();
                }

                else if (this.Player2.Hand.CardValue1 > this.Player1.Hand.CardValue1)
                {
                    gameResultLabel.Text = "PLAYER 2 WINS!";
                    gameResultLabel.Refresh();
                }
            }

            else
            {
                gameResultLabel.Text = "UNHANDLED CASE...";
                gameResultLabel.Refresh();

            }
        }


        private bool checkStraightFlushCase(Player player)
        {
            bool checkStraightFlush = false;

            List<Suit> suitList = new List<Suit>() { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };

            foreach (Suit suit in suitList)
            {
                if (player.Hand.SortedHand.Count(o => o.Suit.Equals(suit)) == 5
                  && (player.Hand.SortedHand[1].CardValue == player.Hand.SortedHand[0].CardValue + 1)
                  && (player.Hand.SortedHand[2].CardValue == player.Hand.SortedHand[1].CardValue + 1)
                  && (player.Hand.SortedHand[3].CardValue == player.Hand.SortedHand[2].CardValue + 1)
                  && (player.Hand.SortedHand[4].CardValue == player.Hand.SortedHand[3].CardValue + 1))
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

        private bool checkFourOfAKindCase(Player player)
        {
            bool checkFourOfAKind = false;

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            foreach (CardValue cardValue in cardValueList)
            {
                if (player.Hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 4)
                {
                    checkFourOfAKind = true;
                    player.Hand.CardValue1 = cardValue;
                    break;
                }
            }
            return checkFourOfAKind;
        }

        private bool checkFullHouseCase(Player player)
        {

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            bool checkFullHouse = false;

            bool threeOfAKind = false;
            bool twoOfAKind = false;

            foreach (CardValue cardValue in cardValueList)
            {
                if (player.Hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 3)
                {
                    threeOfAKind = true;
                    cardValueList.Remove(cardValue);
                    player.Hand.CardValue1 = cardValue;
                    break;
                }
            }

            if (threeOfAKind)
            {
                foreach (CardValue cardValue in cardValueList)
                {
                    if (player.Hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 2)
                    {
                        twoOfAKind = true;
                        player.Hand.CardValue2 = cardValue;
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

        private bool checkStraightCase(Player player)
        {
            bool checkStraight = false;

            if ((player.Hand.SortedHand[1].CardValue == player.Hand.SortedHand[0].CardValue + 1)
                && (player.Hand.SortedHand[2].CardValue == player.Hand.SortedHand[1].CardValue + 1)
                  && (player.Hand.SortedHand[3].CardValue == player.Hand.SortedHand[2].CardValue + 1)
                  && (player.Hand.SortedHand[4].CardValue == player.Hand.SortedHand[3].CardValue + 1))
            {
                checkStraight = true;
            }
            return checkStraight;
        }

        private bool checkFlushCase(Player player)
        {
            bool checkFlush = false;

            List<Suit> suitList = new List<Suit>() { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };

            foreach (Suit suit in suitList)
            {
                if ((player.Hand.SortedHand.Count(o => o.Suit.Equals(suit)) == 5))
                {
                    checkFlush = true;
                    break;
                }
            }
            return checkFlush;
        }

        private bool checkThreeOfAKindCase(Player player)
        {
            bool checkThreeOfAKind = false;

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            foreach (CardValue cardValue in cardValueList)
            {
                if (player.Hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 3)
                {
                    checkThreeOfAKind = true;
                    player.Hand.CardValue1 = cardValue;
                    break;
                }
            }

            return checkThreeOfAKind;
        }

        private bool checkIfTwoPairsCase(Player player)
        {
            bool checkTwoPairs = false;

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            bool firstPair = false;
            bool secondPair = false;

            foreach (CardValue cardValue in cardValueList)
            {
                if (player.Hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 2)
                {
                    firstPair = true;
                    cardValueList.Remove(cardValue);
                    player.Hand.CardValue1 = cardValue;
                    break;
                }
            }

            foreach (CardValue cardValue in cardValueList)
            {
                if (player.Hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 2)
                {
                    secondPair = true;
                    player.Hand.CardValue2 = cardValue;
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

        private bool checkPairCase(Player player)
        {
            bool checkPair = false;

            List<CardValue> cardValueList = new List<CardValue>() { CardValue.Ace, CardValue.King, CardValue.Queen, CardValue.Jack, CardValue.Ten,
            CardValue.Nine, CardValue.Eight, CardValue.Seven, CardValue.Six, CardValue.Five, CardValue.Four, CardValue.Three, CardValue.Two };

            foreach (CardValue cardValue in cardValueList)
            {
                if (player.Hand.SortedHand.Count(o => o.CardValue.Equals(cardValue)) == 2)
                {
                    checkPair = true;
                    player.Hand.CardValue1 = cardValue;
                    break;
                }
            }
            return checkPair;
        }
    }
}
