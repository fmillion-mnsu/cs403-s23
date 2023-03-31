## Homework

This week's homework is more open-ended. Rather than giving you code to expand on or repair, you're going to be taking a very short existing application and adding functionality to it.

From the homework assignment:

> This program is a very simple point-of-sale system - the software that might run on a cash register at a checkout counter. This is an extremely simple implementation of such a system - it only accepts product codes and generates a total for the sale.
>
> You've been asked to refactor this code to make it more organized and usable, and to add some new functionality to the program:
>
> - Use classes and/or structs to represent data, rather than just having some arrays and variables fluttering around.
> - Add code to print a complete "receipt" as the sale ends, rather than just the total. Include all of the items, their price, and the total. 
>   (You do not need to worry about handling the same item appearing more than once - just print it multiple times.)
> - Add the ability to ask the user (the cashier) what type of payment is being used. This should happen right after the user enters 0 to end the sale. If a payment type other than Cash is used, accept a value for the payment number.
> 
>    (Don't worry about "validating" credit card numbers or anything similar.) This code includes an "enum" (we'll discuss that in class) to help get you started with this.
>
> - Imagine the store has a loyalty card system, and the only piece of information you need for that system is the customer's phone number. The customer may choose not to give their number, or they may not even be a member of the rogram. Add code to request and accept a phone number for the user - but the code (and the variable you store it in within your data class) must allow an empty or null value.
> 
>     (Don't worry about storing a list of valid member numbers or anything similar; just have a strategy for associating a member's number with the sale.)
>
> Some tips:
>
> - Think about the data entities in this program, and then think about how you would logically group them into classes.
> - After you have mapped out your classes, write the boilerplate code for the classes.
> - Then, refactor the main program code to use the classes rather than the existing variables.
> - Also consider the additional features that have been requested.
> - We will be covering a lot more about lists and similar data structures in Week 4, but for this exercise, consider using the List<T> type (ref. Friday's lecture on generics). A List<T> has methods like Add() and Remove() along with other aggregating methods like Count(). 
> - Week 4's homework will be built upon this week's homework - there will be no new code given for Week 4. More information during Week 4's lectures!

We will explore this homework on both Wednesday and Friday - make sure you come to class! 

Also note that Week 4's homework will build directly upon Week 3's homework - so ask questions if anything is not making sense!