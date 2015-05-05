using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Card
    {
        //public int Value { get; set; }
        public CardValue CardValue { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Suit Suit { get; set; }
    }
    public enum Suit
    {
        Spades,
        Hearts,
        Clubs,
        Diamonds
    };
    public enum CardValue
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

}
