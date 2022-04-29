using Cards;
using System;
using System.Threading;
namespace Blackjack
{

    public class Blackjack
    {

        public Deck drawDeck = new Deck().Shuffle();
        public Deck discardDeck = new Deck().Clear();
        public Deck yourHand = new Deck().Clear();
        public Deck dealersHand = new Deck().Clear();

        public Blackjack()
        {
            string input;
            bool isPlaying = true;
            do
            {
                Round();
                Console.WriteLine("Do you want to play again?");
                bool validInput = false;
                do
                {
                    input = Console.ReadLine();
                    switch (input.ToLower())
                    {
                        case "yes":
                        case "y":
                            validInput = true;
                            break;
                        case "no":
                        case "n":
                            isPlaying = false;
                            validInput = true;
                            break;
                        default:
                            Console.WriteLine("Invalid input. Try again.");
                            break;
                    }
                } while (!validInput);
            } while (isPlaying);
        }

        public void Round()
        {
            if (drawDeck.Count < 4)
            {
                drawDeck.AddRange(discardDeck.Shuffle());
                discardDeck.Clear();
            }
            for (int i = 0; i < 2; i++)
            {
                drawDeck.Deal(yourHand);
                drawDeck.Deal(dealersHand);
            }
            YourTurn();
            if (DetermineValue(yourHand) > 21)
            {
                Console.WriteLine("You went over 21, the dealer wins!");
            }
            else
            {
                DealersTurn();
                Console.WriteLine($"End result\n\nDealer's Hand:\n{dealersHand}\nYour Hand:\n{yourHand}");
                if (DetermineValue(dealersHand) <= 21)
                {
                    if (DetermineValue(dealersHand) > DetermineValue(yourHand))
                    {
                        Console.WriteLine("The dealer won!");
                    }
                    else if (DetermineValue(dealersHand) == DetermineValue(yourHand))
                    {
                        Console.WriteLine("You tied!");
                    }
                    else
                    {
                        Console.WriteLine("You won!");
                    }
                }
                else
                {
                    Console.WriteLine("The dealer went over 21, you win!");
                }
            }
            yourHand.DealRange(discardDeck, yourHand);
            dealersHand.DealRange(discardDeck, dealersHand);
        }

        public void YourTurn()
        {
            bool yourTurn = true;
            Console.Write($"Dealer's Hand:\n{dealersHand}\nYour Hand:\n{yourHand}\n");
            do
            {
                Console.Write("Hit or stay?: ");
                bool validInput;
                do
                {
                    validInput = false;
                    string input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "h":
                        case "hit":
                            if (drawDeck.Count == 0)
                            {
                                Console.WriteLine("Shuffling deck...");
                                drawDeck.AddRange(discardDeck.Shuffle());
                                discardDeck.Clear();
                                Thread.Sleep(1000);
                                Console.WriteLine("Deck has been shuffled.");
                            }
                            drawDeck.Deal(yourHand);
                            validInput = true;
                            yourTurn = DetermineValue(yourHand) < 21;
                            Console.WriteLine($"\nYour Hand:\n{yourHand}");
                            break;
                        case "s":
                        case "stay":
                            validInput = true;
                            yourTurn = false;
                            break;
                        default:
                            Console.WriteLine("Invalid input. Try again.");
                            break;
                    }
                } while (!validInput);
            } while (yourTurn);
        }

        public void DealersTurn()
        {
            if (DetermineValue(yourHand) > 21)
            {
                return;
            }
            while (DetermineValue(dealersHand) < 17)
            {   
                if (DetermineValue(dealersHand) > DetermineValue(yourHand))
                {
                    break;
                }
                if (drawDeck.Count == 0)
                {
                    Console.WriteLine("Shuffling deck...");
                    drawDeck.AddRange(discardDeck.Shuffle());
                    discardDeck.Clear();
                    Thread.Sleep(1000);
                    Console.WriteLine("Deck has been shuffled.");
                }
                drawDeck.Deal(dealersHand);
                Console.WriteLine($"Dealer's Hand:\n{dealersHand}");
                Thread.Sleep(500);
            }
        }

        public int DetermineValue(Deck deck)
        {
            int value = 0;
            int aceCount = 0;
            foreach (Card card in deck)
            {
                switch (card.value)
                {
                    case Value.King:
                    case Value.Queen:
                    case Value.Jack:
                    case Value.Ten:
                        value += 10;
                        break;
                    case Value.Ace:
                        aceCount += 1;
                        break;
                    default:
                        value += (int)card.value + 1;
                        break;
                }
            }
            for (int i = 0; i < aceCount; i++)
            {
                if (value < 11)
                {
                    value += 11;
                }
                else
                {
                    value += 1;
                }
            }
            return value;
        }
    }
}