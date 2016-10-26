using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deck.Models;
using Xunit;
using System.Reflection;

namespace Deck.Tests
{
    public class DeckTests
    {
        [Fact]
        public void TestShuffle()
        {
            var deck = new DeckOfCards();
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo field = typeof(DeckOfCards).GetField("cards", bindFlags);
            
            //get the cards in their original order
            var unshuffled = (IEnumerable<Card>)field.GetValue(deck);
            
            //shuffle the deck
            deck.Shuffle();

            //get the cards in their new, shuffled order
            var shuffled = (IEnumerable<Card>)field.GetValue(deck);

            var unshuffledArray = unshuffled.ToArray();
            var shuffledArray = shuffled.ToArray();

            //make sure we're dealing with full decks
            Assert.Equal(unshuffledArray.Length, shuffledArray.Length);

            for (int i = shuffledArray.Length - 1; i > 0; i--)
            {
                if (unshuffledArray[i] != shuffledArray[i])
                {
                    //if at any point the cards from the, we've technically shuffled.
                    return;
                }
            }
            //If we get here, it was a perfect match
            throw new Exception();
        }

        [Fact]
        public void TestDeal()
        {
            var deck = new DeckOfCards();
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo field = typeof(DeckOfCards).GetField("cards", bindFlags);

            //get the card count
            var cards = (IEnumerable<Card>)field.GetValue(deck);
            var count = cards.Count();
            
            //deal one more card than we've counted. Last deal attempt should return a null.
            for (int i = count; i >= 0; i--)
            {
                var card = deck.DealOneCard();
                if (i != 0)
                {
                    Assert.NotNull(card);
                }
                else
                {
                    Assert.Null(card);
                }
            }
        }
    }
}
