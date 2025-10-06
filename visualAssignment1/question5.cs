
using System;

namespace visualAssignment1
{
    // Question 5: Sum of Natural Numbers using for loop
    public class question5
    {
        public static void Run()
        {
            Console.WriteLine("--- Question 4: Sum of Natural Numbers ---");

            Console.Write("Enter a positive number (n): ");
            int n = int.Parse(Console.ReadLine());

            if (n <= 0)
            {
                Console.WriteLine("Please enter a positive integer greater than 0.");
                return;
            }

            int sum = 0;

            // Using for loop to calculate sum
            for (int i = 1; i <= n; i++)
            {
                sum += i;
            }

            Console.WriteLine($"The sum of natural numbers from 1 to {n} is: {sum}");
        }
    }
}