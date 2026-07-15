using System;

namespace FractionsProject
{
    /// <summary>
    /// Represents a fraction with a numerator and a denominator.
    /// Provides multiple constructors, getters/setters, a decimal
    /// conversion, and a friendly string representation.
    /// </summary>
    class Fraction
    {
        private int _numerator;
        private int _denominator;

        // Default constructor: represents 1/1
        public Fraction()
        {
            _numerator = 1;
            _denominator = 1;
        }

        // Constructor for a whole number, e.g. Fraction(5) -> 5/1
        public Fraction(int wholeNumber)
        {
            _numerator = wholeNumber;
            _denominator = 1;
        }

        // Constructor for a numerator and denominator, e.g. Fraction(3, 4) -> 3/4
        public Fraction(int numerator, int denominator)
        {
            _numerator = numerator;
            _denominator = denominator;
        }

        public int GetNumerator()
        {
            return _numerator;
        }

        public void SetNumerator(int numerator)
        {
            _numerator = numerator;
        }

        public int GetDenominator()
        {
            return _denominator;
        }

        public void SetDenominator(int denominator)
        {
            _denominator = denominator;
        }

        public double GetDecimalValue()
        {
            return (double)_numerator / _denominator;
        }

        public override string ToString()
        {
            return $"{_numerator}/{_denominator}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Fraction f1 = new Fraction();
            Fraction f2 = new Fraction(5);
            Fraction f3 = new Fraction(3, 4);
            Fraction f4 = new Fraction(1, 3);

            Fraction[] fractions = { f1, f2, f3, f4 };

            foreach (Fraction f in fractions)
            {
                Console.WriteLine($"{f} = {f.GetDecimalValue()}");
            }

            // Demonstrate the getters and setters
            f3.SetNumerator(7);
            f3.SetDenominator(8);
            Console.WriteLine($"Updated fraction: {f3} = {f3.GetDecimalValue()}");
        }
    }
}