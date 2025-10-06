using System;

namespace visualAssignment1
{
    public class question10
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

            Console.Write("\nEnter a number to search: ");
            int searchNum = Convert.ToInt32(Console.ReadLine());

            bool found = false;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == searchNum)
                {
                    Console.WriteLine($"\n{searchNum} found at position {i + 1}.");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"\n{searchNum} not found in the array.");
            }
        }
    }
}