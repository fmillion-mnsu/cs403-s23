# Week 1 Presentation Guides

This week's presentations will focus on core language constructs in C#. You will be going over some aspect of C# as a language and how it represents common programming paradigms and constructs. 

## Terminology

Programming languages often have their own terminology for referring to common constructs. As just one example, C# refers to what might otherwise be called a "function" as a **method**. 

For your presentation, please cover:

* C# terminology - what is meant by *method*, *assembly*, *the CLR*, *managed code*, and *unsafe code* in the specific context of the C# language.
* What other terms might refer to these constructs in other programming languages? (No need to focus on any one language, examples from multiple other languages are OK.)
* Feel free to include any additional unique terminology that stands out to you or that you feel is important.

Sources to get you started (but please do seek out and use other sources as well!):

* [Terms and Definitions](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/terms-and-definitions) at Microsoft.
* [C# Jargon Buster](https://www.talk-it.biz/tutorials-list/jargon-buster/).

## Core Syntax

C#'s syntax follows some basic rules which are very similar to those followed by other languages commonly referred to as "C-style" languages. While it follows these rules largely, it does have its own unique intricacies. 

For your presentation, please cover:

* The core aspects of C# syntax, such as: how to terminate a statement, how to continue something onto a new line, how to start and end a block of code (e.g. for an `if` statement), how to write an `if` statement and a `for` loop, and so on.
* How to print output to the console and how to read user input from the console (hint: `Console` object)
* Inline vs. traditional syntax (i.e. using a class and Main() method versus simply typing code in a `.cs` file at the high level).
  * I prefer to use the traditional syntax - of defining a class and a Main() method - because it makes your code more explicit. I strongly recommend using this approach. 

Sources to get you started (but please do seek out and use other sources as well!):

* [C# Program Structure and Basic Syntax](https://www.softwaretestinghelp.com/c-sharp/csharp-program-structure-and-syntax/)
* [C# reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/) at Microsoft.
* Advanced reading: [C# Language Specification](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/introduction) at Microsoft.

## Object-Oriented Programming

You have probably encountered object-oriented programming in previous courses or programming experience, and we will be covering it in much more depth during Week 3 when we discuss polymorphism. Thus, your presentation need not go into an extreme level of detail. Consider this a review of the subject for those with experience but who just need a quick refresher before diving into more advanced topics.

For your presentation, please cover:

* What is object-oriented programming?
* What are some of the main features and advantages of OOP?
* How does C# implement OOP (keep it basic - we will be covering polymorphism, abstraction and so on later.)
* A brief comparison to another language of your choice, and at least one way that C#'s OOP model differs

Sources to get you started (but please do seek out and use other sources as well!):

* [Object-Oriented Programming in C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/introduction)
* [OOP in C# for Beginners](https://www.codeguru.com/csharp/object-oriented-programming-oop-c-sharp/) at CodeGuru.

## Value Data Types

C# is a *strongly-typed language*. This means that all variables must be given a data type when defined, and that variable may only ever contain data of that type for its entire lifetime. While you will use a very wide variety of types in C#, there are a few important data types that are central to the language and could be seen as its "base" types. 

For your presentation, please cover:

* The core numeric value data types (e.g. `int`, `uint`...) and `bool` and `char`.
  * Don't worry about struct/enum/etc types - we will be covering those later.
* A very brief, high-level description of value versus reference types
* The concept of a strongly-typed language (which C# is) versus a dynamic-typed language (such as Python or JavaScript).
* You may also discuss other non-numeric non-value data types like `object` and `string` if you need more content to fill out your presentation.

Sources to get you started (but please do seek out and use other sources as well!):

* [C# value types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types) at Microsoft.
* [C# Concepts - Value and Reference Type](https://www.c-sharpcorner.com/UploadFile/ca6c61/concepts-of-C-Sharp-value-type-and-reference-type/) at C# Corner.
* [Stack Overflow question](https://stackoverflow.com/questions/1517582/what-is-the-difference-between-statically-typed-and-dynamically-typed-languages) about static vs. dynamic typed languages
* Advanced reading: [A deep dive: Value and reference types in .NET](https://www.infoworld.com/article/3043992/a-deep-dive-value-and-reference-types-in-net.html) from InfoWorld.