using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask the user for their name
        Console.Write("What is your name? ");
        string name = Console.ReadLine();

        // Ask the user for their favorite color
        Console.Write("What is your favorite color? ");
        string color = Console.ReadLine();

        // Display the results
        Console.WriteLine();
        Console.WriteLine($"Hello, {name}!");
        Console.WriteLine($"Your favorite color is {color}.");
    }
}