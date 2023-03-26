# Week 2 Presentation Guides

This week's presentations will focus on debugging. C# and Visual Studio have some extensive debugging features. Many other environments have similar features but which may behave differently. 

## Exceptions in C#

Most modern programming languages have the concept of an exception - an object that is created when an error occurs. In C# terms, the exception object is "thrown" by the runtime, and it can be "caught" by your code. Not catching an exception usually results in the program terminating (a "crash").

C#'s exception handling syntax is similar to many other languages - you write code that you want to protect from crashes within a `try` block, and you then provide `catch` blocks for each type of exception you want to catch and handle gracefuly. 

We will be covering classes, inheritance and polymorphism in Week 3, but this is a great place to do a "sneak preview" - exceptions are class instances just like any other object in C#, and you can catch either an explicit exception type (e.g. `DivideByZeroException`) or you can catch a class of exceptions (e.g. `System.Exception`) and any exception whose type is subclassed from that class will also be caught.

For your presentation, please cover:

* How to write code that catches exceptions (try/catch syntax)
* A few common exceptions that you might encounter in C# (for example, `DivideByZeroException`, `IOException`, `IndexOutOfRangeException`, etc.)
* How you can throw an execption in your own code. 
  * As a suggestion, you could demo this by writing a method that throws an exception and then write code in your `Main` method to call that method and catch its exception.

Sources to get you started (but please do seek out and use other sources as well!):

* [Terms and Definitions](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/terms-and-definitions) at Microsoft.
* [C# Jargon Buster](https://www.talk-it.biz/tutorials-list/jargon-buster/).

## Debug Output

When debugging, it's common to want to print some output somewhere to show what is happening in your code. However, printing this output to the screen with `Console.WriteLine` can become messy and cumbersome, not to mention it can be disruptive to the user interface. 

Many programming languages include some form of logging function that you can use to log information about your program's execution. Often this interface allows you to specify different levels of verbosity; for example, you can log a message as a "Debug" message, and it will only be shown if the user requests such messages to be printed. Alternatively, you could redirect debug messages to a file, so they don't appear in the user interface but are stored on disk in a file that could be sent to the developer.

In C#, you can use built-in objects to write debug output. You can also make use of many convenience methods, such as `Debug.WriteIf` which only writes the mesaage if a certain condition is true, or `Debug.Assert` which will pause execution if a condiiton is not met.

For your presentation, please cover:

* Writing debug messages to the Debug output window (and how to view them)
* Other things you can do with Debug (`WriteIf`, `Assert`, and `IndentLevel`/`IndentSize` for example)
* A brief discussion about Trace Listeners and how they can be used to direct Debug output to other places like a file. (The `Trace` object works very much like `Debug`, but lets you extend it to write your messages to many different destinations.)

Sources to get you started (but please do seek out and use other sources as well!):

* [Sending text to the Debug window](https://learn.microsoft.com/en-us/previous-versions/windows/desktop/forefront-2010/ms698739(v=vs.100)) at Microsoft.
* [Debug class](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug?view=net-7.0) at Microsoft.
* [Trace Listeners](https://learn.microsoft.com/en-us/dotnet/framework/debug-trace-profile/how-to-create-and-initialize-trace-listeners) at Microsoft.

## Breakpoints

A breakpoint is a flag you place on a statement that tells the execution environment to stop program execution at that statement. In the case of Visual Studio, when a breakpoint is hit, Visual Studio will pause the program and enter the debugger.

For your presentation, please cover:

* How to set a breakpoint, clear a breakpoint and remove all breakpoints
* How to set a Conditional breakpoint (a breakpoint that only stops execution if a certain condition is met at the time that line of code executes)
* Continuing execution after hitting a breakpoint

Sources to get you started (but please do seek out and use other sources as well!):

* [Use breakpoints in the Visual Studio Debugger](https://learn.microsoft.com/en-us/visualstudio/debugger/using-breakpoints?view=vs-2022) at Microsoft.
* [Breakpoints: the complete tutorial](https://csharp.net-tutorials.com/debugging/breakpoints/)

## Watches and Conditional Breakpoints

A watch is a debugging feature that lets you view the value of a variable in real-time as the program executes. It can be very useful to track down unexpected behavior in a program. 

Visual Studio implements watches as part of the VS debugger. Another presentation for the day will be focusing on setting and managing breakpoints, so you do not need to cover how to set a breakpoint and enter the debugger. However, you should be able to explain how watches work and what they are useful for.

Another related function of the VS debugger is that you can hover the mouse over any variable while in the debugger and the value of that variable will be displayed to you. This works even for complex objects with many properties (more on this next week).

A conditional breakpoint is one that you can set to only cause program execution to stop if a condition is met. For example, you can say "only break if this integer is greater than this value". This can be useful for debugging unpredictable or inconsistent edge cases that do not occur reliably. 

For your presentation, please cover:

* Adding a watch in the VS debugger
* An explanation or demo of how to view a variable's value in the debugger - both as a watch and as a mouse hover-over

Sources to get you started (but please do seek out and use other sources as well!):

* [Setting a watch on variables](https://learn.microsoft.com/en-us/visualstudio/debugger/watch-and-quickwatch-windows?view=vs-2022) at Microsoft.
* [Visual Studio Debugging](https://michaelscodingspot.com/debugging-part2/)
* [Conditional breakpoints in C#](https://www.c-sharpcorner.com/UploadFile/b1df45/conditional-breakpoints-in-C-Sharp/)

## Advanced Debugging

Once you have entered the VS Debugger, there are many things you can do. We've already heard about watches and viewing variable values. In addition, you can navigate a codebase, single-step through your code, and even make certain changes to the code while paused.

For your presentation, please cover:

* Stepping through code - Step In, Step Out, Step Over
* Edit and Continue - changing code while paused in the debugger
* Understanding the call stack
  * Fun fact: A "Stack Overflow" occurs when the call stack "overflows" and no more entries can fit into the call stack. This will usually happen if a method calls itself unconditionally; a loop will occur that will eventually fill up the call stack.

Sources to get you started (but please do seek out and use other sources as well!):

* [Navigate code with the VS Debugger](https://learn.microsoft.com/en-us/visualstudio/debugger/navigating-through-code-with-the-debugger?view=vs-2022&tabs=csharp) at Microsoft.
* [Edit and Continue](https://learn.microsoft.com/en-us/visualstudio/debugger/edit-and-continue?view=vs-2022) at Microsoft.
* [Step through code in C#](https://kodify.net/csharp/visual-studio/step-code-debug/)

## Conditional Compilation

C# partially borrows a feature from C and C++ known as "preprocessor directives". While C# does not have an actual preprocessor like C and C++, it borrows the syntax of certain directives that allow you to change the behavior of the compiler and runtime. 

One significant feature this enables is the ability to exclude certain lines of code based on the build and run configuration of your program. Most commonly, this is used to allow you to have certain code that exists in a Debug build, but does not exist in a Release build.

In fact, the .NET Framework itself makes use of this capability. The entire `Debug` class is essentially excluded if you are not running a Debug build. This means that `Debug.WriteLine` and similar statements simply "don't exist" in a Release build. It's not like an `if` statement that skips over the statements; the statements are literally not included in the source that is compiled for the build.

For your presentation, please cover:

* C# "Preprocessor" directives - especially `#if` and related, as used to exclude code from certain builds
* How to define custom symbols (in project properties -> Build -> General)
* Explain build configurations - by default "debug" and "release", but can also be used to setup custom build workflows for special use cases.

Sources to get you started (but please do seek out and use other sources as well!):

* [C# Processor Directives](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives) at Microsoft.
* [Understanding build configurations](https://learn.microsoft.com/en-us/visualstudio/ide/understanding-build-configurations?view=vs-2022) at Microsoft

## Special Topic: C# with Docker

This week we will have a presentation about how you can build and run your C# code in a Docker container.
