using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Deck.Models;
using Deck.Services;

namespace Deck
{
    class Program
    {
        private enum Operation
        {
            Shuffle,
            Deal,
            Help,
            Exit
        }

        static void Main(string[] args)
        {
            var usage = "Commands: <shuffle | deal | help | exit>\n...so which is it gonna be?";
            
            //parse args... specifically the first operation
            var nextOp = parseArgs(args);

            //spin up a Dealer!
            var dealer = new Dealer();

            //stay alive 'till otherwise instructed
            while(true)
            {
                //do the specified operation
                switch (nextOp)
                {
                    case Operation.Shuffle:
                        Console.WriteLine("\nShuffling deck...\n");
                        dealer.Shuffle();
                        break;
                    case Operation.Deal:
                        var card = dealer.DealOneCard();
                        dealCard(card);
                        break;
                    case Operation.Exit:
                        exitGracefully();
                        break;
                    case Operation.Help:
                        Console.WriteLine(usage);
                        break;
                    default:
                        Console.WriteLine(usage);
                        break;
                }

                nextOp = getNextOp();
            }
        }


        #region user inputs
        private static Operation getNextOp()
        {
            var input = Console.ReadLine();
            return parseOp(input);
        }

        private static Operation parseOp(string input)
        {
            var ignoreCase = true;
            Operation op;
            var canParse = Enum.TryParse(input, ignoreCase, out op);
            if(!canParse)
            {
                return Operation.Help;
            }
            return op;
        }

        private static Operation parseArgs(string[] args)
        {
            //one op at a time!
            if (args.Length != 1)
            {
                return Operation.Help;
            }

            return parseOp(args[0]);
        }
        #endregion
        
        #region operations
        private static void dealCard(Card card)
        {
            if (card == null)
            {
                Console.WriteLine("\nNo cards left pardner!\n");
                return;
            }

            var cardTemplate = "\n{0} of {1}\n";
            var cardString = string.Format(cardTemplate, card.Value, card.Suit);
            Console.WriteLine(cardString);
        }

        private static void exitGracefully()
        {
            Console.WriteLine("\nThanks for stopping by! Shutting down...");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }
        #endregion
    }
}
