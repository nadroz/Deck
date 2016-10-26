using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deck.Models;

namespace Deck.Services
{
    public sealed class Dealer
    {
        private readonly Random rand;
        private Queue<Card> deck;

        public Dealer()
        {
            this.deck = populateDeck();
            rand = new Random();
        }

        public void Shuffle()
        {
            var cardsArray = deck.ToArray();
            var len = cardsArray.Length;

            //if there are one or fewer cards, bounce
            if (len <= 1)
            {
                return;
            }

            //do some shufflin'!
            for (int i = cardsArray.Length - 1; i > 0; i--)
            {
                var swapIndex = rand.Next(i + 1);
                var newVal = cardsArray[swapIndex];
                cardsArray[swapIndex] = cardsArray[i];
                cardsArray[i] = newVal;
            }
            
            //put 'em back where you found 'em
            deck = new Queue<Card>(cardsArray);
        }

        public Card DealOneCard()
        {   
            var len = deck.Count();
            
            //if there are no cards left, return null
            if(len == 0)
            {
                return null;
            }

            //deal one off the top
            return deck.Dequeue();
        }

        private Queue<Card> populateDeck()
        {
            var suitType = typeof(CardSuit);
            var valueType = typeof(CardValue);
            var suits = Enum.GetNames(suitType);
            var values = Enum.GetNames(valueType);

            var cards =
                from suit in suits
                from value in values
                select new Card 
                { 
                    Suit = (CardSuit)Enum.Parse(suitType, suit),
                    Value = (CardValue)Enum.Parse(valueType, value)
                };

            var cardsQueue = new Queue<Card>(cards);
            return cardsQueue;
        }
    }
}
