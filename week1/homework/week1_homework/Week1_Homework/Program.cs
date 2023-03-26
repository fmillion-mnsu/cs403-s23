// PLEASE KEEP THE FOLLOWING HEADER INTACT IN YOUR SUBMITTED CODE other than adding your name
// after "Programmer:".

/*
 * CS 403 - Week 1 Homework
 * Programmer: YOUR NAME HERE
 */

/*
 * This program is a starting point for your Week 1 homework.
 * I have given you the code for parsing and attempting to print a conversion to an int data type.
 * You need to do the same for the following data types:
 * 
 *   - byte
 *   - short
 *   - long
 *   - ushort
 *   - uint
 *   - ulong
 *   - float
 *   - double
 */

// Using statements tell C# that you want access to certain namespaces in this code file.
// They are similar conceptually to Python "import" statements.

// "System" gives us many core functionalities like the Console object.
// It is technically not required for the latest .NET versions, but it does not hurt to
//   include it, and it will help reduce ambiguities.
using System;

// Namespaces help you organize your code objects into logical collections, and also help
//   prevent name collisions across multiple code libraries.

namespace CS403.Week1 
{

    // In C#, it is good practice to have a class for your program.
    // The class should contain a static method, with return type of either void or int,
    //   which will be the main entry point for your program.
    // The "string[] args" parameter is not required, but will be populated if you provide
    //   command line arguments to your program.

    // This is not strictly necessary in the latest C# / .NET versions, but it is still
    //   good practice to do this - it helps keep your code organized and makes it clear
    //   to the compiler and runtime where your entry point code is.
    
    internal class Program
    {
        private static void Main(string[] args)
        {

            // This defines a variable but does not assign a value to it.
            // This variable will be used to indicate success or failure in
            //   parsing values.
            bool success;

            // Read a line of input from the user
            Console.Write("Please enter a number: "); // Write vs WriteLine = Write does not append a newline.
            string userInput = Console.ReadLine() ?? "";
            // The "??" syntax means "if a null value is received, then use the value to the right of ??".
            // Nullable types in C# end with a ?, e.g. "string?", and mean that it is possible for the value
            // to be null. Non-nullable types must have a value. Console.Readline() returns a nullable string,
            // because there are some specific use cases where Console.ReadLine() has nothing to read from, so
            // null would be returned. We simply say "if you get null, instead use the empty string" for this.

            // The Trim() operation strips off spaces, newlines, etc. from the resulting string.
            userInput = userInput.Trim();

            // Try to convert to a signed int
            success = int.TryParse(userInput, out int convertedInt);
            // TryParse functions return a boolean value of "true" if the string was able to be parsed into the
            //   requested format. The "out int" syntax allows a function to return another value other than its
            //   return value - in this case, the actual value that was parsed is put into a new int variable 
            //   called "convertedInt".

            // Check whether the parse was successful and print accordingly.
            if (success)
            {
                // Print success message along with the actual value that was parsed.
                Console.WriteLine("Converting to int succeeded. Value is " + convertedInt.ToString());
            }
            else
            {
                Console.WriteLine("Converting to int FAILED.");
            }

            // Note: Don't ask the user for more input -try to parse one  input as ALL of the 
            //   data types.

            // Try to convert to an unsigned int (uint)
            // ** YOUR CODE HERE **

            // Try to convert to a signed long (long)
            // ** YOUR CODE HERE **

            // ...

        }
    }
}