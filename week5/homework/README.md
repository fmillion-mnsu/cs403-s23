## Homework

In Week 5, you will refactor your Point of Sale system code to utilize a class library. You will also add appropriate documentation to the class library. 

Please perform the following tasks:

- **Refactor the code that actually calculates your sales from the user interface.** 
  - To get started, right-click your Solution in Visual Studio and add a Class Library project. 
  - Decide on a namespace for your class library, and make sure your code files start with `namespace ...` (after `using` statements) as appropriate.
  - Create at least one class in your class library. You could also move all of your existing classes for objects such as product and sale over to the new class library.
  - Refactor and rework your code so that the class library does not depend on the user interface at all. For example, if you have methods that are simply printing to the console with `Console.WriteLine`, you will want to refactor that code manually by *creating a method* that returns the value in question.
  - The existing project that you already have should be reworked into a user interface that makes use of the class library but **does not implement any logic of its own** other than handling user input and printing results from the class library (including handling user input errors).
- **Document your new class library using XML comments.** 
  - Each class and method should have, at minimum, a `<summary>` section.

## Some hints

- You should not use `Console.WriteLine` at all in your class library. If you are printing something to the console in the class library, refactor and rewrite the code so that the `Console.WriteLine` stays in the original "user interface" application, but the *processing* occurs in the class library.
- This process may seem daunting at first, but consider that the only thing you really need to separate out is the user interface. Your classes for handling entities and however you handle retrieving product information will be able to largely stay unchanged. You can of course retain any `Debug` statements in the class library if you use them. The point of this exercise is to rework the code so that you separate the project into a "frontend" and a "backend". 
- You aren't being asked to do this for the homework, but this scenario is quite common in a production setting - you will have some code that handles the "business logic" and other code that handles the user interface. Once you've separated the logic from the interface, you could probably start to imagine how the user interface itself could be completely rewritten - say, to be a graphical desktop application, or even a Web-based application - without needing to make any changes at all to the parts of the code that handle business calculations. This is important because it allows **separation of concern** - you now have two pieces of code that each focus on two specific domains of the overall program, and these two parts communicate at "arm's length" with each other (i.e. only through method calls and return values).

We will go over this homework more on Wednesday and Friday, so don't worry right now if this seems complex. We'll go over some examples of how to do the refactoring as well. 

This homework will be due on **Tuesday April 18th at 11:59PM**.

Good luck!