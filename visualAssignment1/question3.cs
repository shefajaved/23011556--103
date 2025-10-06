
using System;

namespace visualAssignment1
{
    // Question 2: Simple Calculator using switch-case
    public class question3
    {
        public static void Run()
        {
            Console.WriteLine("\n--- Question 2: Simple Calculator ---");

            // Ask user for two numbers
            Console.Write("Enter first number: ");
            double num1 = double.Parse(Console.ReadLine());

            Console.Write("Enter second number: ");
            double num2 = double.Parse(Console.ReadLine());

            // Show menu for operations
            Console.WriteLine("\nChoose operation:");
            Console.WriteLine("1. Addition (+)");
            Console.WriteLine("2. Subtraction (-)");
            Console.WriteLine("3. Multiplication (*)");
            Console.WriteLine("4. Division (/)");
            Console.Write("Enter your choice (1–4): ");

            int choice = int.Parse(Console.ReadLine());

            double result;

            // Use switch-case for operation selection
            switch (choice)
            {
                case 1:
                    result = num1 + num2;
                    Console.WriteLine($"Result: {num1} + {num2} = {result}");
                    break;

                case 2:
                    result = num1 - num2;
                    Console.WriteLine($"Result: {num1} - {num2} = {result}");
                    break;

                case 3:
                    result = num1 * num2;
                    Console.WriteLine($"Result: {num1} × {num2} = {result}");
                    break;

                case 4:
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        Console.WriteLine($"Result: {num1} ÷ {num2} = {result}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Division by zero is not allowed.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid operation choice!");
                    break;
            }
        }
    }
}