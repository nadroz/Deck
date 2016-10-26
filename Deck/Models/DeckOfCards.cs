using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck.Models
{
    public sealed class DeckOfCards
    {
        private readonly Random rand;
        private Queue<Card> cards;

        public DeckOfCards()
        {
            this.cards = populateDeck();
            rand = new Random();
        }

        public void Shuffle()
        {
            var cardsArray = cards.ToArray();
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
            cards = new Queue<Card>(cardsArray);
        }

        public Card DealOneCard()
        {   
            var len = cards.Count();
            
            //if there are no cards left, return null
            if(len == 0)
            {
                return null;
            }

            //deal one off the top
            return cards.Dequeue();
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
