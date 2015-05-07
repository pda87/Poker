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
        public Outcome Outcome { get; set; }

        public Hand()
        {
            this.HandCardList = new List<Card>();
            this.SortedHand = new List<Card>();
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

    public enum Outcome
    {
        Lost,
        Won
    }

}
