
using System;

namespace visualAssignment1
{
    public class question9
    {
        public static void Run()
        {
            int[] numbers = new int[10];
            int evenCount = 0, oddCount = 0;

            Console.WriteLine("\nEnter 10 integers:");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write($"Number {i + 1}: ");
                numbers[i] = Convert.ToInt32(Console.ReadLine());
            }

            foreach (int num in numbers)
            {
                if (num % 2 == 0)
                    evenCount++;
                else
                    oddCount++;
            }

            Console.WriteLine($"\nEven numbers: {evenCount}");
            Console.WriteLine($"Odd numbers: {oddCount}\n");
        }
    }
}