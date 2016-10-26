using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deck.Models;
using Deck.Services;
using Xunit;
using System.Reflection;

namespace Deck.Tests
{
    public class DeckTests
    {
        [Fact]
        public void TestShuffle()
        {
            var dealer = new Dealer();
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo field = typeof(Dealer).GetField("deck", bindFlags);
            
            //get the cards in their original order
            var unshuffled = (IEnumerable<Card>)field.GetValue(dealer);
            
            //shuffle the deck
            dealer.Shuffle();

            //get the cards in their new, shuffled order
            var shuffled = (IEnumerable<Card>)field.GetValue(dealer);

            var unshuffledArray = unshuffled.ToArray();
            var shuffledArray = shuffled.ToArray();

            //make sure we're dealing with full decks
            Assert.Equal(unshuffledArray.Length, 52);
            Assert.Equal(shuffledArray.Length, 52);

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
            var deck = new Dealer();
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo field = typeof(Dealer).GetField("deck", bindFlags);

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
