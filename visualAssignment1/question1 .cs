
using System;

namespace visualAssignment1
{
    // Question 1: Even or Odd Checker
    public class question1
    {
        public static void Run()
        {
            Console.WriteLine("\n--- Question 1: Even or Odd Checker ---");

            // Ask user for input
            Console.Write("Enter a number: ");
            int number = Convert.ToInt32(Console.ReadLine());

            // Check whether number is even or odd
            if (number % 2 == 0)
            {
                Console.WriteLine($"{number} is Even.");
            }
            else
            {
                Console.WriteLine($"{number} is Odd.");
            }
        }
    }
}