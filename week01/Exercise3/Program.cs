using System;

class Program
{
    static void Main(string[] args)
    {
        string playAgain = "y";

        while (playAgain.ToLower() == "y")
        {
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 101);

            int guess = 0;
            int guessCount = 0;

            Console.WriteLine("Welcome to the Guess My Number Game!");

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    Console.WriteLine($"It took you {guessCount} guesses.");
                }
            }

            Console.Write("Do you want to play again? (y/n): ");
            playAgain = Console.ReadLine();
            Console.WriteLine();
        }

        Console.WriteLine("Thanks for playing!");
    }
}