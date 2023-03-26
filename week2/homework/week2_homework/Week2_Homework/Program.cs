// PLEASE KEEP THE FOLLOWING HEADER INTACT IN YOUR SUBMITTED CODE other than adding your name
// after "Programmer:".

/*
 * CS 403 - Week 2 Homework
 * Programmer: YOUR NAME HERE
 */

/*
 * This program computes grade point averages. However, something is not quite right. It's easy to make the program
 * crash, and it looks like maybe the calculations themselves aren't even correct.
 * 
 * Your mission, should you choose to accept it:
 *
 *  1. Find the bugs in the code by using breakpoints, watches and so on
 *  2. Fix the bugs that can be fixed
 *  3. Add exception handling to handle things that you can't control (e.g. user input)
 *  4. Add in some debug output that will only occur if the user runs the program in a Debug build.
 *  
 * In short, the program should not crash with an exception no matter what input you feed it, and it should correctly
 * calculate the GPA.
 */

/* The following are some hints for this assignment:
 * 
 * 1. Try just running the code first before you review it. Try to identify what might be wrong before you start viewing
 *    the code. If you have significant C# experience some of the bugs may be obvious to you, but try to challenge yourself
 *    to not only find the bugs but to improve the program and make it more "bullet-proof".
 * 2. Do the calculations by hand. GPA calculations simply take the number of credits for the course and multiply it by
 *    4, 3, 2, 1 for grades A, B, C, and D respectively. (This program doesn't deal with + or - grades, so you don't need
 *    to worry about those.) Then you add the total GPA points together and divide by the number of total credits.
 *    Do some calculations yourself and compare with the output of the program. Hint - as written, it will compute
 *    incorrectly!
 * 3. Try out many test cases - don't just do your own grades or an easy case. Calculate some larger GPAs and then use
 *    the program to try to calculate them as well. 
 * 4. Introduce some invalid input or edge cases to the program. For example, try a course with 0 credits, or try not
 *    entering any course at all. Then consider how you might address these edge cases and add appropriate checks and
 *    error handling to the code.
 * 5. Using Debug.WriteLine will work for printing debug output that only occurs on a Debug build. You
 *    can also make use of Debug.Assert or Debug.WriteLineIf or similar methods if you like. It is up
 *    to you what output you create and where you create it, but it is adviable to use the output to print
 *    ongoing status of program execution to help you trace and fix bugs.
 */

using System;
using System.Diagnostics; // Thie using statement gives us access to the Debug object.

namespace CS403.Week2
{

    internal class Program
    {

        private static int letterGradeToGpaPoints(string letterGrade, int courseCredits)
        {

            switch (letterGrade)
            {
                case "A":
                    return 4 * courseCredits;

                case "B":
                    return 2 * courseCredits;

                case "C":
                    return 3 * courseCredits;

                case "D":
                    return 1 * courseCredits;

                case "F":
                    // do not add any GPA points
                    return 0;

                default:
                    throw new FormatException("You did not input a valid value for a grade.");
            }

        }

        private static void Main(string[] args)
        {

            Console.WriteLine("Grade Point Average Calculator");
            Console.WriteLine();

            int courseCounter = 0;
            int creditsCounter = 0;
            int gpaCounter = 0;

            while (true)
            {
                // Get the letter grade the user achieved in the course from the user.
                Console.Write("Enter a letter grade (A, B, C, D, F). Type X to finish and show your GPA: ");
                string letterGrade = Console.ReadLine()
                    .Trim()
                    .Substring(0,1)
                    .ToUpper();

                // Example of a Debug statement you can add.
                Debug.WriteLine("DEBUG: the user inputted '" + letterGrade + "'.");

                // If the user entered "X", then stop receiving new courses.
                if (letterGrade == "X")
                {
                    break; // exit the While loop
                }
                
                // Get the number of credits for a course from the user.
                Console.Write("Enter number of credits for the course: ");
                int thisCourseCredits = int.Parse(Console.ReadLine().Trim());
                creditsCounter += thisCourseCredits;

                // Compute the number of GPA points to add, and add it to the running GPA counter.
                gpaCounter += letterGradeToGpaPoints(letterGrade, thisCourseCredits);

                Console.WriteLine("Course added.");
                courseCounter++; // increment operator

                Console.WriteLine();

                // The code will loop back to the top of the while loop.

                // The "while True" strategy allows you to code a loop that is technically infinite. Using the
                //   "break" command lets you specify exactly when the loop should end. This pattern is convenient
                //   when you need to check the loop exit condition somewhere *other* than the start or end of the 
                //   loop.
            }

            // Print general counters
            Console.WriteLine("Courses Taken   : " + courseCounter.ToString());
            Console.WriteLine("Total Credits   : " + creditsCounter.ToString());
            Console.WriteLine("Grade Points    : " + gpaCounter.ToString());

            // Compute GPA
            decimal gpa = courseCounter / (decimal)creditsCounter;

            // Print out the student's GPA.
            Console.WriteLine();
            Console.WriteLine("Grade Point Avg : " + gpa.ToString("F2"));

        }
    }
}