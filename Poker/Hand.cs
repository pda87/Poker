using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Hand
    {
        public List<Card> HandCardList { get; set; }
        public List<Card> SortedHand { get; set; }
        public HandResult HandResult { get; set; }
        public CardValue CardValue1 { get; set; }
        public CardValue CardValue2 { get; set; }
        public string OutputString { get; set; }

        public Hand()
        {
            this.HandCardList = new List<Card>();
            this.SortedHand = new List<Card>();
        }

        public void GenerateRandomHand(Game game, Player player)
        {
            Random random = new Random();

            //TAKE 5 RANDOM CARDS FROM THE DECK OF CARDS
            //USING 51 INSTEAD OF 52 FOR THE ARRAY ITERATION
            //int deckCount = 51;

            player.Hand.HandCardList = new List<Card>();

            for (int i = 0; i < 2; i++)
            {
                int randomCard = random.Next(0, game.DeckCount - 1);

                player.Hand.HandCardList.Add(game.GameDeck[randomCard]);
                game.GameDeck.RemoveAt(randomCard);
                game.DeckCount--;
            }
        }

    }

    public enum HandResult
    {
        HighCard,
        Pair,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush
    }
}
