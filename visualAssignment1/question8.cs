using System;

namespace visualAssignment1
{
    public class question8
    {
        public static void Run()
        {
            int[] numbers = new int[10];

            Console.WriteLine("\nEnter 10 integers:");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write($"Number {i + 1}: ");
                numbers[i] = Convert.ToInt32(Console.ReadLine());
            }

          int max = numbers[0];
            int min = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                    max = numbers[i];
                if (numbers[i] < min)
                    min = numbers[i];
            }

            Console.WriteLine($"\nMaximum number: {max}");
            Console.WriteLine($"Minimum number: {min}\n");
        }
    }
}