using System;

namespace visualAssignment1
{
    // Question 7: Factorial Calculator using while loop
    public class question7
    {
        public static void Run()
        {
            Console.WriteLine("--- Question 6: Factorial Calculator ---");

            Console.Write("Enter a positive integer: ");
            int num = int.Parse(Console.ReadLine());

            if (num < 0)
            {
                Console.WriteLine("Factorial is not defined for negative numbers.");
                return;
            }

            int i = 1;
            long factorial = 1;

            // Using while loop to calculate factorial
            while (i <= num)
            {
                factorial *= i;
                i++;
            }

            Console.WriteLine($"Factorial of {num} = {factorial}");
        }
    }
}
