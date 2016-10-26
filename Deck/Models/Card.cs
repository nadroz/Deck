using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck.Models
{
    public sealed class Card
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
    }

    public enum CardSuit
    {
        Hearts,
        Diamonds,
        Spades,
        Clubs
    }

    public enum CardValue
    {
        Ace,
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
        King
    }
}
