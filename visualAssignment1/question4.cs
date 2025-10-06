
using System;

namespace visualAssignment1
{
    // Question 4: Grade Evaluator
    public class question4
    {
        public static void Run()
        {
            Console.WriteLine("--- Question 3: Grade Evaluator ---");

            Console.Write("Enter your marks (0–100): ");
            int marks = int.Parse(Console.ReadLine());

            // Validate marks
            if (marks < 0 || marks > 100)
            {
                Console.WriteLine("Invalid marks. Please enter a value between 0 and 100.");
                return;
            }

            // Determine grade
            if (marks >= 85)
                Console.WriteLine("Your Grade: A");
            else if (marks >= 70)
                Console.WriteLine("Your Grade: B");
            else if (marks >= 55)
                Console.WriteLine("Your Grade: C");
            else if (marks >= 40)
                Console.WriteLine("Your Grade: D");
            else
                Console.WriteLine("Your Grade: F");
        }
    }
}