using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public static class GenerateGameDeck
    {
        public static List<Card> CreateDeck()
        {
            List<Card> gameDeck = new List<Card>()
       {
        new Card() { CardValue = Poker.CardValue.Two, Name = "Two Spades", Image = @"CardImages/2S.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.Three, Name = "Three Spades", Image = @"CardImages/3S.png", Suit = Suit.Spades},
        new Card() { CardValue = Poker.CardValue.Four, Name =  "Four Spades", Image = @"CardImages/4S.png", Suit = Suit.Spades},
        new Card() { CardValue = Poker.CardValue.Five, Name = "Five Spades", Image = @"CardImages/5S.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.Six, Name = "Six Spades", Image = @"CardImages/6S.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.Seven, Name = "Seven Spades", Image = @"CardImages/7S.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.Eight, Name = "Eight Spades", Image = @"CardImages/8S.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.Nine, Name = "Nine Spades", Image = @"CardImages/9S.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.Ten, Name = "Ten Spades", Image = @"CardImages/10S.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.Jack, Name = "Jack Spades", Image = @"CardImages/JS.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.Queen, Name = "Queen Spades", Image = @"CardImages/QS.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.King, Name = "King Spades", Image = @"CardImages/KS.png", Suit = Suit.Spades },
        new Card() { CardValue = Poker.CardValue.Ace, Name = "Ace Spades", Image = @"CardImages/AS.png", Suit = Suit.Spades },

        new Card() { CardValue = Poker.CardValue.Two, Name = "Two Diamonds", Image = @"CardImages/2D.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Three, Name = "Three Diamonds", Image = @"CardImages/3D.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Four, Name =  "Four Diamonds", Image = @"CardImages/4D.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Five, Name = "Five Diamonds", Image = @"CardImages/5D.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Six, Name = "Six Diamonds", Image = @"CardImages/6D.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Seven, Name = "Seven Diamonds", Image = @"CardImages/7D.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Eight, Name = "Eight Diamonds", Image = @"CardImages/8D.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Nine, Name = "Nine Diamonds", Image = @"CardImages/9D.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Ten, Name = "Ten Diamonds", Image = @"CardImages/10D.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Jack, Name = "Jack Diamonds", Image = @"CardImages/JD.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Queen, Name = "Queen Diamonds", Image = @"CardImages/QD.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.King, Name = "King Diamonds", Image = @"CardImages/KD.png", Suit = Suit.Diamonds },
        new Card() { CardValue = Poker.CardValue.Ace, Name = "Ace Diamonds", Image = @"CardImages/AD.png", Suit = Suit.Diamonds },

        new Card() {CardValue = Poker.CardValue.Two, Name = "Two Clubs", Image = @"CardImages/2C.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Three, Name = "Three Clubs", Image = @"CardImages/3C.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Four, Name =  "Four Clubs", Image = @"CardImages/4C.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Five, Name = "Five Clubs", Image = @"CardImages/5C.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Six, Name = "Six Clubs", Image = @"CardImages/6C.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Seven, Name = "Seven Clubs", Image = @"CardImages/7C.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Eight, Name = "Eight Clubs", Image = @"CardImages/8C.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Nine, Name = "Nine Clubs", Image= @"CardImages/9C.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Ten, Name = "Ten Clubs", Image = @"CardImages/10C.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Jack, Name = "Jack Clubs", Image = @"CardImages/JC.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Queen, Name = "Queen Clubs", Image = @"CardImages/QC.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.King, Name = "King Clubs", Image = @"CardImages/KC.png", Suit = Suit.Clubs },
        new Card() { CardValue = Poker.CardValue.Ace, Name = "Ace Clubs", Image = @"CardImages/AC.png", Suit = Suit.Clubs },

        new Card() { CardValue = Poker.CardValue.Two, Name = "Two Hearts", Image = @"CardImages/2H.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Three, Name = "Three Hearts", Image = @"CardImages/3H.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Four, Name =  "Four Hearts", Image = @"CardImages/4H.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Five, Name = "Five Hearts", Image = @"CardImages/5H.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Six, Name = "Six Hearts", Image = @"CardImages/6H.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Seven, Name = "Seven Hearts", Image = @"CardImages/7H.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Eight, Name = "Eight Hearts", Image = @"CardImages/8H.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Nine, Name = "Nine Hearts", Image = @"CardImages/9H.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Ten, Name = "Ten Hearts", Image = @"CardImages/10H.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Jack, Name = "Jack Hearts", Image = @"CardImages/JH.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Queen, Name = "Queen Hearts", Image = @"CardImages/QH.png", Suit = Suit.Hearts },
        new Card(){ CardValue = Poker.CardValue.King, Name = "King Hearts", Image = @"CardImages/KH.png", Suit = Suit.Hearts },
        new Card() { CardValue = Poker.CardValue.Ace, Name = "Ace Hearts", Image = @"CardImages/AH.png", Suit = Suit.Hearts }
            };

            return gameDeck;
        }
    }
}
