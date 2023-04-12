# Week 5 Resources and Presentation Guides

This week's presentations focus on code documentation, organization, refactoring and libraries.

**Wednesday**:

- [Namespaces, Using statements and Global Usings](#namespaces-using-statements-and-global-usings)
- [Class Libraries](#class-libraries)
- [IntelliSense and Hints](#intellisense-and-hints)

**Friday**:

- [XML Inline Documentation](#xml-inline-documentation)
- [Code Refactoring](#refactoring)
- [Code Analysis](#code-analysis)
- [SPECIAL TOPIC: C# Testing with MSTest](#special-topic-mstest)

## Namespaces, Using statements and Global Usings

C# code is organized into *namesapces*. Many other languages use a very similar paradigm for organizing code into groups of objects. In C#, namespaces typically contain classes and interfaces. 

In C#, similarly to Java, most of the objects provided by the framework are located within the `System` namespace. For example, the `Console.WriteLine()` method we have been using regularly is actually part of the `System` namespace. (`Console` is a *static class* - a class that contains only static members, and thus is not ever directly instantiated.)

You may have noticed that you actually can use `Console.WriteLine` without importing `System` using the `using System;` statement. This is because C# (at least, as of version 10) has a function known as *implicit usings*. This function works in tandem with another newer function known as *global usings*.

Global usings allow you to write a `using` statement in one file that applies to *all* files in the project. For example, if you included the following statement within your `Program.cs` file:

    global using System;

every `.cs` file in your program would behave as if it had `using System;` at the top. You can define as many `global using` statements as you like, and repeating the same `global using` statement in many places should not cause a problem. 

You can go one step further by using the `static` modifier. For example, if you add `global static using System.Console;` to your `Program.cs` file, now *every* file in your program can simply call `WriteLine(...)` to write something to the console.

The implicit usings feature builds on the capability of global usings by automatically generating a global usings file for you. The list of namespaces that are included is based on the project type. Since we have been working with the .NET Console App template, namespaces such as `System` are imported automatically for us. 

### Using your own namesapces

It is a good idea for you to organize your code into your own namespaces. A namespace is simply a "path", with each component separated by periods. `System.Net`, `System.Linq`, `FlintMillion.Programs.MyApp` and `MNSU.CS.Projects.CS403.Week5` are all valid namespaces. When you are simply working with a `Main` method and perhaps a few supporting methods or a class or two, simply having a root-level namespace for your program (such as `Week4Homework`) is usually sufficient. However, as you start to write more complex code, you'll find that organizing your code into namespaces can be very useful.

All of the code (classes, interfaces, etc.) within a `.cs` code file can be wrapped inside a `namespace` block. You have probably already seen this, but it looks like this:

    using System;
    // other using statements

    namespace MyCompany.MyProgram
    {
        public class Program
        {
            public void Main()
            {
                // ...
            }
        }
    }

All of the items within this `.cs` file, which are inside the `namespace` block, will exist within the given namespace. In other `.cs` files, you can provide the same namespace (all of the objects will simply be combined into that namespace from all of the files using it), or you can specify a different namespace. 

It's also important to remember that if you put code in a different namespace, you either need to include that namespace using a `using` directive, or you need to fully qualify the path to a class. (Objects in the same namespace as the file your code is in are implicitly available, but any other namespaces - even sub-namespaces - must be explicitly imported).

    namespace MyCompany.MyProgram.Utils
    {
        public static class UtilityFunctions
        {
            public static void DoSomething() 
            {
                // a method that does something
            }
        }
    }

    namespace MyCompany.MyProgram
    {
        public static class Program
        {
            public static void Main()
            {
                // This is invalid code - even though Utils is a sub-namespace of our namespace MyCompany.MyProgram,
                // we can't use it without explicitly importing the namespace, or by fully qualifying the path
                // to the object.
                UtilityFunctions.DoSomething();

                // This IS valid and will work without a using statement.
                MyCompany.MyProgram.Utils.UtilityFunctions.DoSomething();
            }
        }
    }

You also can always directly reference an object by its full path. Even with no `using` statements at all, you could use `System.Console.WriteLine` to access the `Console.WriteLine` method. `using` statements simply allow you to abbreviate your code to make it more readable - everything in the namespace that you import with `using` is available without specifying the full path to the object. 

Namespaces can become especially important when you start separating out parts of your code into *class libraries* (another topic for today's presentations). All C# libraries place their code objects within a namespace for that library - commonly, it follows a pattern similar to the ones I've described so far, but as long as the pattern is unique enough that it is extremely unlikely that other code will conflict with it, it will work fine.

In Visual Studio, you can explore the entire set of namespaces and their objects with the *Object Explorer*, which is available under the `View` menu. The Object Explorer gives you a friendly tree view interface with which you can explore and navigate all of the namespaces that are available in your project. This includes all of the system namespaces as well as the namespaces for your own code and for any libraries you might have imported into the project. 

### Namespace Caveats

It's possible to have objects in two different namespaces that have an identical name. For example, you might have a `DoSomething` method, on a class called `Utilities`, in both the `MyCompany.MyProgram` namespace as well as the `OtherCompany.Program` namespace. When an ambiguity occurs, the compiler will refuse to compile your code and you will be required to fully qualify the name of the class, method, or other object you are intending to use. This will only happen for objects that are ambiguous - i.e. they share an identical name other than the namespace. Be cautious with this especially if you're using static using statements; sometihng like `using static System.Console` means you can use `WriteLine` with no qualifier, but it could actually make your code *less* readable since many other objects also contain a `WriteLine` method (such as the `Debug` object, streams and more).

While you can come up with your own namespaces, you should be careful to minimize any chance that your namespace might get "overridden" by another code library. For example, simply naming your namespace `Utilities` is probably a poor idea. Instead, something like `MyCompany.Utilities` is better, since `MyCompany` is (ideally) a unique company name and thus the chances of a collision are very low. In the same vain, you should refrain from creating any custom namespaces that start with `System`, as the `System` namespace is reserved for the .NET framework's library objects. While the language will not explicitly *prevent* you from putting your code in, say, `System.Utilities`, this is extremely ill-advised, as the `System` namespace is generally assumed to be consisten and documented.

One other point to keep in mind is that importing a namespace does *not* implicitly import any sub-namespaces. For example:

    namespace MyApp
    {
        public class Program 
        {
            // ...
        }
    }

    namespace MyApp.Utilities
    {
        public class Utils
        {
            // ...
        }
    }

In this example, if you were to use `using MyApp;`, you would *not* get access to `MyApp.Utilities` implicitly. Despite `Utilities` appearing to be under `MyApp`, you still need to *explicitly* import `MyApp.Utilities` if you want the objects from that namespace available in your code directly. (You can, of course, always use the full path, i.e. `MyApp.Utilities.Utils`, even without the extra `using` statement.)

One final point - the `using` keyword has *two* uses in C#. When used at the top of a source code file, it is used to import namespaces into the workspace for that file. However, it is also seen in the context of disposable objects  such as file handles or network sockets (for example: `using (StreamReader sr = File.Open("file.txt")) { ... }`). Make sure not to confuse the two when you're studying material on the `using` statement!

In your presentation, please cover:

- What are C# namespaces? How do they compare to namespaces used in other programming languages, such as Java or Python?
- Explain how you can view the list of objects within namespaces using the Object Browser in Visual Studio.
- Provide an overview of placing your code within a custom namespace and how to access that code from other code files or blocks. Don't worry about moving code to different files or projects - that's a topic for another presentation.
- Compare/contrast how other languages handle namespaces. For example, Java and Python use very similar notation (dot notation) to specify paths to objects within namespaces. Other languages use different separators (such as `::` or `->`) to specify namespace paths.

Sources to get you started (but please do seek out and use other sources as well!):

- [using directive](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive) at Microsoft.
- [Organizing types into namespaces](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/namespaces) at Microsoft.
- [Implicit global using](https://endjin.com/blog/2021/09/dotnet-csharp-10-implicit-global-using-directives)

## Class Libraries

So far, we have written all of our C# code in a single *project* - a unit that compiles to a single assembly. However, like many other languages, C# offers a way to package up some code into a *library* that can be imported into another project. In C#, this is known as a *class library* (because the fundamental code object is a class!). 

We have been using the .NET Console App template for our code thus far. A .NET console app, along with many other types of projects, is designed to be directly executed. A class library, in contrast, is not intended for direct execution. It does not have to contain a static `Main` method. However, it can contain any C# code you desire. 

C# class libraries compile to `.dll` files. The name of the `.dll` is typically the namespace the code exists within. (There's nothing preventing one DLL from putting code in multiple namespaces, but this is uncommon and can lead to confusion - the most prominent example is actually the .NET Framework itself, where some `.dll` files include code for multiple `System` namespaces.) You can then add the `.dll` file to another project and access all of its classes and objects by simply importing (with `using`) the correct namespace in your code. The C# compiler will figure out how to access the appropriate code within your DLL file when it is necessary to do so. 

You can also split your code into a class library without setting up separate projects. Visual Studio implements the idea of a *solution* - a group of projects that work together as one unit. A solution can contain multiple executalbe projects (for example, a command line version as well as a GUI version, or even a mobile app) as well as supporting class libraries. Each of the executable projects can directly access the class library without needing to explicitly import it - you just need to use the appropriate `using` statements.

A related topic to class libraries is Microsoft's *NuGet* package repository. The NuGet repository is similar to `pip` in Python or `maven` in Java. It's a centralized repository of code libraries, nearly all written in C# (but all written for the .NET framework). NuGet packages can be imported into your project, which will download the library's code into your project. Publishing a library to NuGet is outside the scope of our course, but the homework for this week will include adding one library to your project from NuGet.

For your presentation, please cover:

- What is a class library in C#?
- Explain the concept of a solution and how it relates to the many projects contained therein.
- Provide either a demonstration or an overview of how you would go about adding a class library to your C# solution, and then using it from another project in the same solution.
  - We won't worry about distributing the DLL file and loading it into a completely unrelated solution for now.
- Consider how other programming languages implement sharable code libraries. Compare and contrast C#'s strategy with other languages. For example, Python uses the idea of *packages*, where you can use an `__init__.py` file to designate a directory as a package and can then `import` it into your code using `import` statements.
- What is NuGet? How does it compare to other repositories for other languages? 

Sources to get you started (but please do seek out and use other sources as well!):

- [Class Libraries](https://learn.microsoft.com/en-us/dotnet/standard/class-libraries) at Microsoft.
- [Creating a Class Library in C#](https://www.c-sharpcorner.com/UploadFile/61b832/creating-class-library-in-visual-C-Sharp/) at CSharpCorner.
- [What are solutions and projects?](https://learn.microsoft.com/en-us/visualstudio/ide/solutions-and-projects-in-visual-studio?view=vs-2022)

## IntelliSense and Hints

You have probably noticed by now, if you're using Visual Studio (many other C#-focused IDEs and IDE plugins will do this as well), that as you write code, popups will appear with suggestions or lists of available objects. For example, if you type `Console.` and wait a moment, a list will appear showing you all of the methods and properties available on the `Console` object. This feature is known by Microsoft as *IntelliSense* and it has been a key feature of Visual Studio for many years, going back to versions of Visual Studio even prior to the C# language's existence. 

![Screenshot showing the IntelliSense popup after writing Console. Various methods such as WriteLine are listed along with a secondary popup describing the WriteLine method and its parameters.](images/intellisense_1.png)

IntelliSense is a feature of the IDE (in this case Visual Studio), not a language feature on its own. IntelliSense is aware of the programming language's syntax and pops up when you've entered a partial object identifier or similar construct. If you type `Console.` it will pop up all objects available on that object. If you start typing a method call, like `Console.WriteLine(`, it will pop up with the parameters for the method. In the case of an overloaded method, where there are multiple ways you can call it based on the parameters, it will give you arrows you can click (or use the arrow keys) to switch between the different method signatures. 

![Screenshot showing the IntelliSense popup after beginning a method call to Console.WriteLine. The interface indicates that there are 18 overloads - method signatures - and the version acceoting a boolean value is highlighted.](images/intellisense_2.png)

Another IntelliSense feature is *tab completion*. Once you've typed enough of an object's name to disambiguate it from other similarly-named objects, you can simply press the **Tab** key and Visual Studio will fill in the rest of the name for you. Even if there is still ambiguity, IntelliSense will still try to auto-complete using the most commonly used object name. For example, you can type `Cons <tab> .Wr <tab>` and you'll have `Console.WriteLine`. It might take some experimenting to find out just how much disambiguation you have to do, but the IntelliSense popups will help you. With some skill, you can write code at super-speeds using this technique!

Another feature is the Hints feature. Hints are prompts that IntelliSense offers to you, either as ideas for how to fix a known code issue, or as suggestions based on coding practices. There is a large amount of configurability of how the code analyzer works that is beyond the scope of this discussion, but be aware that you can configure Visual Studio to enforce various code styles and quality aspects, especially issues that might create potential bugs. For example, if you are using a nullable type (one ending with a `?`) in a context where a null value is not allowed, the code analyzer can warn you that a null value might produce an exception. (If the value is not null, the nullable type will be automatically cast to the non-nullable type, but a null value could produce unexpected results.) Situations like this are typically marked as warnings, since it does not necessarily prevent your code from working, but you need to be aware of it in case there is any scenario in which the value might be null. (Contrast this with Java, which often *requires* you to implement exception handling, even in situations where you may have near-100% assurance that the exception in question will never occur.)

Hints are available when you see a light-bulb icon or a screwdriver icon in the left column. If you are on the line on which the hint is available, you can press Ctrl+. (Control+period/full-stop) to bring up the hint popup and see the available options. You can also click on the icon itself to bring up the list of options. Many of the options are automatic and IntelliSense can make changes to large sections of code for you automatically. Of course it is always a good idea to review the changes made by IntelliSense, but since IntelliSense is able to understand the code at a syntactic level, it is often quite reliable in making changes to code - even widespread changes. (We'll hear more about this in Friday's discussion on refactoring - imagine changing the name of a class and having that change propagate automatically throughout your *entire* project!)

In your presentation, please cover:

- What is IntelliSense? (Is it a language feature or an IDE feature?)
- What functions can IntelliSense offer to you as a developer?
- Show a couple of examples of IntelliSense in action. You can either do a live demo or some screenshots.
- Pick at least one other IDE other than Visual Studio and investigate how it provides similar features for code analysis and "helper" features. (VS Code, JetBrains, etc. are all valid choices.)

Sources to get you started (but please do seek out and use other sources as well!):

- [C# IntelliSense](https://learn.microsoft.com/en-us/visualstudio/ide/visual-csharp-intellisense?view=vs-2022) at Microsoft.
- [Quick Actions](https://learn.microsoft.com/en-us/visualstudio/ide/quick-actions?view=vs-2022) at Microsoft.
- [Light Bulb Suggestions](https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-light-bulb-suggestions?view=vs-2022)

## XML Inline Documentation

C# offers a way to provide parsable documentation within your code. The documentation may not be as cleanly readable as simple comments, but the fact that the comments can be parsed affords many useful features, including:

* The comments can describe methods, properties, etc. and those descriptions can be made available via IntelliSense
* Automated tools can generate comprehensive reference documentation directly from your comments
* Code quality tools can help you make sure you are writing effective documentation.

C# "standard" comments follow C syntax - double-slashes (`//`) start a comment that extends to the end of the line, and the slash-star pair (`/*` and `*/`) produce a multi-line comment.

C#'s parsable comments use a simplified variant of XML. All lines of XML comments begin with *three* slashes (`///`). A comment can (and almost always does) span multiple lines. A multi-line syntax for XML comments is also available but is not commonly seen - you can generate multi-line XML comment blocks by starting with `/**` (and ending with `*/` - note only one asterisk in the closing delimiter. Inside a multi-line XML comment, a leading asterisk will be ignored if it appears on *all* lines of the comment.)

XML comments are specifically intended to document C# code objects. XML comments can be used to add documentation fields to most C# objects - classes, interfaces, methods, properties, fields, etc. can all have XML comments applied. Whenever an XML comment block appears in your code, it refers to the class, method, property, field, interface, etc. that *immediately* follows it.

Here is a very simple example of a method with a simple XML comment:

    /// <summary>
    /// Returns the number zero.
    /// </summary>
    public int Zero() 
    { 
        return 0; 
    }

    // or

    /**
      * <summary>Returns the number one.</summary>
      */
    public int One()
    {
        return 1;
    }

What makes parsable comments so useful is that once you have written them, they are used to provide IntelliSense hints:

![Image showing the IntelliSense tooltip for the Zero method, showing that the summary is provided in the IntelliSense tooltip](images/zero.png)

For a simple method that returns no value and accepts no parameters, simply including the `<summary>` tag is typically sufficient. However, you can also explicitly provide comments for the parameters of a method and its return value. You can even include examples and remarks that will be rendered by documentation generators and in the Object Browser.

Example:

    /// <summary>
    /// Adds two numbers together.
    /// </summary>
    /// <param name="x">The first number to add</param>
    /// <param name="y">The second number to add</param>
    /// <returns>The sum of the numbers x and y.</returns>
    public static int Add(int x, int y)
    {
        return x + y;
    }

In this case, we add two `<param>` tags with a `name` attribute that indicates which parameter we are referring to, and then we provide a simple text string that describes the parameter. Finally we add a `<returns>` tag that describes the value returned by the method.

This will appear in IntelliSense like this:

![Image showing the IntelliSense tooltip that appears when the `Add()` method is referenced. It shows the description of the method as presented in the `<summary>` tag as well as the help text for the `x` parameter.](images/add_1.png)

![Image showing the IntelliSense tooltip that appears when you hover over the `Add` method. It shows the summary text, the names of the parameters and the text in the `<returns>` tag.](images/add_2.png)

Comments can also be applied to properties and fields:

    /// <summary>
    /// A property that always has the value "Hello World"
    /// </summary>
    public static string Hello
    {
        get
        {
            return "Hello World!";
        }
    }

    /// <summary>
    /// A field that the programmer can assign values to or read values from directly.
    /// </summary>
    public static int TheNumber = 0;

The list of most commonly used XML tags is as follows:

- `<summary>` - Provides a description of the method. 
- `<param name="(name)">` - Provides a description of the relevance of a method's parameter
- `<returns`> - Provides a description of the return value; especially how it might be calculated or its use.
- `<remarks>` - Provides a more lengthy description of the method. Remarks are typically longer than the summary and can include multiple paragraphs.
- `<exception cref="(exception_type)">` - Indicates that the method might throw the given exception, and describes why it might occur.
- `<value>` - Specifically used for properties; describes the meaning of the actual value of the property (as opposed to the summary, which would typically describe the property's overall relevance to the code)
- `<example>` - Provides one or more examples that demonstrate using the method.

There is also the `<paramref>` tag, which you can use in the text area of any other tag, to refer specifically to a parameter. This makes the documentation rich and allows navigation between the different parameters as desired by the programmer.

Comments can contain multiple lines. You can either use the `<para>` and `</para>` tags to separate paragraphs (similar to `<p>` and `</p>` in HTML) or you can use the `<br/>` tag to insert line breaks. Using `<para>` will produce paragraphs with a blank line between them; using `<br/>` allows you to insert arbitrary single line breaks.

You can mark a section of a comment as code using the `<code>` and `</code>` tags. This is particularly useful inside of `<example>` tags.

Here is a good example that uses almost all of the above tags:

    /// <summary>
    /// Adds two numbers together.
    /// </summary>
    /// <remarks>
    /// This is a very redundant method that has no real appreciable use, but it demonstrates the usefulness of XML comments!
    /// </remarks>
    /// <exception cref="OverflowException">Occurs if you try to add numbers that would overflow the size of an int.</exception>
    /// <param name="x">The first number to add</param>
    /// <param name="y">The second number to add</param>
    /// <example>
    /// This example returns the value 3.
    /// <code>Add(1, 2);</code>
    /// </example>
    /// <returns>The sum of the numbers <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static int Add(int x, int y)
    {
        if ((long)x + (long)y > 2147483647L) throw new OverflowException("Integer overflow.");
        return x + y;
    }

This example will appear in the object browser as follows:

![Image showing the Add method in the object browser, including all of the content entered in the comments except for the <example> section.](images/object_browser.png)

One final note: You can specify XML comments for each implementation of an overloaded method (a method with multiple signatures). If you do this, IntelliSense will dynamically show the correct comments based on the method signature the programmer is using (and the programmer can cycle through the available method signatures using the up and down arrow icons in the IntelliSense tooltip).

### Notes on XML

XML is a markup language with its roots in HTML; if you have ever written any HTML, the angle-bracket syntax with closing slash tags will be very familiar to you. Since XML is an HTML-based language, there are certain requirements when it comes to escaping characters. One primary example is if you want to include a less-than or greater-than sign in your comment; you need to use `&lt;` and `&gt;` to do so. Since HTML entities always begin with an ampersand, you also need to "escape" the ampersand itself: `&amp;`. 

Despite the XML comment style using XML-like tags, the comments themselves are not "fully-formed" XML. This is acceptable - littering your code with `DOCTYPE` statements and `xmlns` attributes would be incredibly ugly. Interally, C# collects all your comments and produces a master XML file that *is* fully formed; it is that XML data that is used to drive IntelliSense and also to produce automated documentation.

For your presentation, please cover:

- How to write basic comments in C# (C-style syntax)
- Writing basic XML comments
- At least one example other than the one in this resource guide of writing XML comments for a method
- How IntelliSense shows the XML comments during development and how the Object Browser shows the comments

Sources to get you started (but please do seek out and use other sources as well!):

- [Documentation comments](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments)
- [XML Documentation Recommended Tags](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags#example)

## Refactoring

*Refactoring* is the process of modifying code structure or identifiers to make the code easier to maintain, understand or modify. In particular, it refers to modifying the structure and appearance of code without modifying its function. 

Visual Studio offers many strategies for refactoring. Many options are available, often as light-bulb hints (see the section on [IntelliSense](#intellisense-and-hints)), and others can be directly invoked by you.

Let's start with a very common example. Suppose you have a reason to rename a method - perhaps you've since devised a better naming scheme or the name of the method itself is just confusing. You want to rename the method, but you don't necessarily know of all of the places you have called that method in your code. You *could* use Find and Replace to do this manually, but Visual Studio offers a much more robust solution:

1. Right click the method's name in its signature. For example, `public void MyMethod()` -- right click the text `MyMethod`.
2. Choose "Rename".
3. Simply enter a new name for the method and press Enter.
4. You can optionally check the boxes to have the name changed in comments and strings as well. 

Visual Studio automatically locates all of the references to the method in your code and autonomously updates them to match the new name of your method!

There are many other types of refactoring that reflect different code styles. For example, C#'s properties are a unique language feature; some developers may be used to using a scheme such as `GetValue` and `SetValue` for properties. C# allows you to easily convert between property syntax and method syntax by using refactoring tools.

Refactoring also involves helper routines that can do things like extract an interface from a class (i.e. creating an interface from the methods a class exports, which that class can directly inherit from immediately), converting a class into an abstract class, converting a `foreach` loop to a LINQ query, and so on. There are many refactoring options available in Visual Studio alone, and there are also third-party tools for Visual Studio as well as tools for Visual Studio Code that offer similar refactoring capabilities.

Refactoring is a huge topic and we certainly don't have time to cover every possible scenario you might want to refactor code in, nor can we cover every possible way to refactor. Thus, this discussion is meant to serve as a light introduction to the concept of refactoring that you might find useful in your own C# development journey.

For your presentation, please cover:

- Explain the simplest of refactoring options - renaming an object.
- Choose a few other refactoring tasks from the resources below and provide an example of using them. 
- Why refactor? Name a few examples of situations where refactoring is a good idea (and optionally, if you can find any, where refactoring is a *poor* idea!)

Sources to get you started (but please do seek out and use other sources as well!):

- [Code Refactoring in Visual Studio](https://learn.microsoft.com/en-us/visualstudio/ide/refactoring-in-visual-studio?view=vs-2022) at Microsoft. This page has a listing of many common refactoring operations available in Visual Studio - you can choose a few from this list for your examples.
- [C# Refactoring](https://www.c-sharpcorner.com/UploadFile/2072a9/refactoring-in-C-Sharp/) at CSharpCorner.

## Code Analysis

*Code analysis* is a broad topic that involves automated analysis of source code to check for aspects such as code style, complexity, efficiency, and so on. Like refactoring, it is a very broad topic and we will only have time to scratch the surface of the depths to which code analysis can play a role in development on large source code repositories.

To cause Visual Studio to run code analysis on your code, you can use the menu option Run Code Analysis under the Analyze menu. This will manually examine your code as it exists at that point in time, and potentially produce a list of warnings or recommendations in the Error List window. (Some of these may appear as messages, which may be hidden - click the blue "Messages" button to see them. Messages are typically of relatively low concern, but it is still prudent to look them over for potential code quality issues.)

To configure code analysis for a project, you can choose "For <project>" under the Configure Code Analysis option under the Analyze menu.  You can specify the level of "intensity" the code analysis process uses in critiquing your code. Setting it to "6.0 All" will produce the most aggressive results, while simply choosing "6.0" is a more relaxed setting that only produces notices for more glaring code quality issues.

Here is an example of running code analysis on the class and methods from the [XML Comments](#xml-inline-documentation) section:

![Code Quality results in the Error List, showing two code quality issues: initializing a value to its default value and a field that is visible directly instead of being a property.](images/code_quality.png)

Another key functionality of code analysis is analysis of *code metrics*. Code metrics produce measurable values for various aspects of your code. Among these are:

- **Maintainability index**. An overall calculation based on a formula that tries to predict how easily the code can be maintained in the future. It is based on many of the other parameters in this list.
- **Cyclomatic Complexity**. This metric identifies how many possible code paths exist in the code. More code paths means more potential pathways for errors or bugs, and also means an increased need for tests (if you're writing tests). 
- **Depth of Inheritance**. Determines how many levels deep the deepest class that inherits from another class is. For example, if you have a class hierarchy like this: `Animal -> Canine -> Dog -> GoldenRetriever -> GoldenRetrieverPuppy`, you will have a depth of inheritance of `5`. While inheritance is very useful at organizing and reducing duplication of code, *overuse* of inheritance can lead to increased complexity and more chances for unexpected behavior.
- **Class coupling**. Measures how "coupled" your classes are to each other. Essentially, this is a measure of how reusable your classes are. Classes that are tightly bound to other classes cannot be as easily reused and will increase this value.
- **Lines of code**. The total number of lines of code in the project.
- **Lines of Executable Code**. The total number of lines that actually compile to executable code. This excludes comments, definitions without instantiations, structural elements (e.g. braces), class definitions, etc.

Here's a sample run of code metrics:

![Sample output from Code Metrics. For the code analyzed, Maintainability Index is 92, Cyclomatic Complexity is 6, Depth of Inheritance is 1, Class Coupling is 3, Lines of Source Code is 69, and Lines of Executable Code is 7.](images/code_metrics.png)

Code metrics provides another analytical way to provide an overview of the quality of your code. It's sort of like a "code credit score" - in particular the maintainability index, which can be a value between 0 and 100, with a higher value being better. 

For your presentation, please cover:

- Basic definition and purpose of code analysis and code metrics.
- Some overview of the various code style issues that Visual Studio looks for in code analysis. One of the resources is a complete list of rules that .NET analysis checks for - select a couple of rules that stand out to you and explain why the rule is important.
- An overview of Code Metrics, what is considered "good" for each value, and why a "bad" value is bad to begin with.

Sources to get you started (but please do seek out and use other sources as well!):

- [Overview of .NET source analysis](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview?tabs=net-6) at Microsoft.
- A [complete list](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/) of rules used for .NET code analysis. (You don't have to go over all of these - that would take a long time! Pick a few rules that stand out to you, explain the rule and why following it is good practice.)
- [Code Metrics](https://learn.microsoft.com/en-us/visualstudio/code-quality/code-metrics-values?view=vs-2022) at Microsoft.

## SPECIAL TOPIC: MSTest

We will have a special topic presentation this week on MSTest, the built-in testing framework for C#
