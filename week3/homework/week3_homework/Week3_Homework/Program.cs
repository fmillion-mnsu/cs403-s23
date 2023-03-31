// PLEASE KEEP THE FOLLOWING HEADER INTACT IN YOUR SUBMITTED CODE other than adding your name
// after "Programmer:".

/*
 * CS 403 - Week 3 Homework
 * Programmer: YOUR NAME HERE
 */

/*
 * This program is a very simple point-of-sale system - the software that might run on a 
 * cash register at a checkout counter. This is an extremely simple implementation of such
 * a system - it only accepts product codes and generates a total for the sale.
 * 
 * You've been asked to refactor this code to make it more organized and usable, and to add
 * some new functionality to the program:
 * 
 *   - Use classes and/or structs to represent data, rather than just having some arrays 
 *     and variables fluttering around.
 *   - Add the ability to print the list of all products that were sold in the sale - like
 *     a receipt.
 *   - Add the ability to ask the user (the cashier) what type of payment is being used.
 *     If a payment type other than Cash is used, accept a value for the payment number.
 *     (Don't worry about "validating" credit card numbers or anything similar.) This code
 *     includes an "enum" (we'll discuss that in class) to help get you started with this.
 *   - Imagine the store has a loyalty card system, and the only piece of information you need
 *     for that system is the customer's phone number. The customer may choose not to give their
 *     number, or they may not even be a member of the program. (Don't worry about storing a 
 *     list of valid member numbers or anything; just have a strategy for associating a 
 *     member's number with the sale.)
 * 
 * Some tips:
 * 
 *   - Use classes. Think about the data entities in this program, and then think about how you would
 *     logically group them into classes.
 *   - We will be covering a lot more about lists and similar data structures in Week 4, but for this 
 *     exercise, consider using the List<T> type (ref. Friday's lecture on generics). A List<T> has
 *     methods like Add() and Remove() along with other aggregating methods like Count(). 
 *   - Week 4's homework will be built upon this week's homework - there will be no new code given
 *     for Week 4. More information during Week 4's lectures!
 * 
 * We will get started with this homework in class on Friday. 
 */

using System;

namespace CS403.Week3
{
    // This is a construct that was not assigned as part of a presentation, so it will be used
    // here to show you how it's done: the enum.

    // Enums are value types that allow you to represent values in code using a set of keywords.
    // It can make code more readable. 

    // For example, this enum indicates the type of payment a user utilized when paying for
    // their items:
    public enum PaymentType : int
    {
        Cash = 0,
        CreditCard = 1,
        CompanyAccount = 2,
        Other = 255
    }

    // You can then use PaymentType like a type. It is stored as an "int" in memory and can be
    // used as such, but you can assign values to it by using the given identifiers. For example:
    // 
    //     PaymentType paytype = PaymentType.CreditCard;
    //
    // paytype is now actually an int with the value of 1, but using the enum lets you make the code
    // more clear as to what the meaning of the numbers are.

    internal class Program
    {
        public static void Main() 
        {
        
            Console.WriteLine("Welcome to the Point of Sale System.");

            // Holds the total of a sale.
            decimal saleTotal = 0M;
            // Represents the tax rate for taxable items.
            decimal taxRatePercent = 7.5M;
            
            // This is your product database.
            // This is functional, but not scalable - adding more products to the system
            // would be a nightmare over time!
            string[] productCodes = { "11", "12", "21", "22", "23", "31", "32", "41", "42" };
            string[] productNames = { "Bottled Water", "Pepsi", "Lays Potato Chips", "Nacho Cheese Doritos", "Chee-tos", "M&M's", "Reese's Peanut Butter Cups", "2% Milk Gallon", "Skim Milk Gallon" };
            decimal[] productPrices = { 1.75M, 2.00M, 2.49M, 2.29M, 2.39M, 1.49M, 1.59M, 3.19M, 3.19M };
            bool[] productIsTaxed = { false, true, true, true, true, true, true, false, false };

            // This is an infinite loop that we will "break" out of when appropriate.
            while (true) 
            {
                Console.Write("Enter product code, or '0' to end sale: ");

                // ?? is the null-coalescing operator - if the item on the left is null, the
                // value is the item on the right, otherwise the value on the left is used.
                string productCode = Console.ReadLine() ?? "";

                // If 0 was entered, end the sale and print totals.
                if (productCode == "0")
                {
                    // Some types let you pass parameters to the ToString() method that alter the
                    // type of string generated. The "C" specifier for numeric values converts the
                    // value to a currency, e.g. "$1.00".
                    Console.WriteLine("Sale complete. The total is " + saleTotal.ToString("C")); 
                    break;
                }

                // Determine which index in the array the product number entered represents.
                int productArrayPosition = Array.IndexOf(productCodes, productCode);

                // The Array.IndexOf method returns -1 if the item is not found in the array.
                if (productArrayPosition == -1)
                {
                    Console.WriteLine("Invalid product code. Try again.");
                    continue; // jump to the start of the loop
                }

                // Is the product taxed?
                bool thisProductIsTaxed = productIsTaxed[productArrayPosition];
                decimal thisProductTotal;
                if (thisProductIsTaxed)
                {
                    thisProductTotal = productPrices[productArrayPosition] + (productPrices[productArrayPosition] * (taxRatePercent / 100.0M));
                }
                else
                {
                    thisProductTotal = productPrices[productArrayPosition];
                }
                saleTotal += thisProductTotal;

                // This is string interpolation. Using the $ prefix before a string, you can include
                // variables or even short expressions inside of curly brackets in the string, and they
                // will be replaced with the value of that variable or expression. If the value is
                // not a string, ToString() is implicitly called to create a string from the value.
                Console.WriteLine($"Ok, {productNames[productArrayPosition]} added to sale for {thisProductTotal.ToString("C")}");

            }
        }
    }
}