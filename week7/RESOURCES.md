# Week 7 Resources and Presentation Guides

It's time to wrap up our journey into C# and to cover some other general programming topics! With the knowledge you've gained in this course, you're well on your way to not only being a C# developer, but to being a better overall programmer!

The topics in our last week don't follow any particular theme. Many topics stand alone, and a couple of them group together nicely, but the overarching theme for this final week of content is "everything else".

- [C# Delegates](#c-delegates)
- [C# Multicast Delegates](#c-multicast-delegates)
- [Source Control - Basics](#source-control-basics)
- [Source Control - Advanced](#source-control-advanced)

- [Continuous Integration and Deployment (CI/CD)](#continuous-integration-and-deployment)


## C# Delegates

A *delegate* is a special kind of C# type that refers to a method. 

Python and other languages are often said to treat methods (functions) as "first-class" objects - that is, a function itself is a variable, and can be treated as such. Take for example the following Python code:

    def myFunction():
        print("I am a function!")

    def executeSomething(func):
        func()

    executeSomething(myFunction)

Notice something interesting happening here. There are two functions defined. The second function - `executeSomething` - is a little special. Notice how it accepts a parameter (`func`), and then *calls the parameter* as if it were a function. This is because when we define a function in Python, the function actually becomes a *variable* in a sense. (To further prove this, try writing code where you define both a function and a normal variable with the same name. It won't fail; instead, defining one will override the other.) In `executeSomething` we can actually call a passed parameter as a function, and we can pass the function (without parameters) around as a variable. It might take a bit to wrap your head around it, but if you're familiar with JavaScript it also has this construct and it's commonly seen when you define a callback.

C# has a similar capability, but functions aren't simply "variables" in the traditional sense. Being a method is different than being a variable in C#. However, the *delegate* type allows us to indeed treat a method as if it were a variable. We can then pass the method around to different methods as parameters, just like we do in Python.

In C#, a delegate type is a method signature. Recall that a *method signature* simply means a method's return type, parameter list and type of each parameter. So, if we have a method that's defined as `public string ManipulateString(string str)`, the method signature is a method that returns a string and accepts one parameter which is a string. We could define a delegate for this type of method like this:

    public delegate string StringManipulator(string str);

We define this at the *namespace* level typically, since it's a type (like a class) and not a class-level field or object. 

Now, let's define a method that matches the signature of the delegate:

    public static string AddToString(string str)
    {
        return str + " (string)";
    }
    
Now that we have the delegate type and a method that matches it, we can use them:

    string s = "Hello World!";

    // Create an instance of a StringModifier delegate, using the AddToString method.
    // Note that we do not *call* the method (by adding parameters after it), but instead we use
    // the method's name.
    StringModifier sm = AddToString;

    // Now we can call "sm" just like any other method.

    string modifiedString = sm(s);
    
    Console.WriteLine(modifiedString);
    // The program will output "Hello World! (string)"

Just like in Python, delegates gives us a way to treat methods like variables. We can now use the delegate just like any other type, such as passing it into another method:

    static void ModifyAndPrintString(string str, StringModifier delegateMethod)
    {
        Console.WriteLine(delegateMethod(str));
    }

With this syntax, we can pass a method into another method. This is just like the Python we looked at a moment ago, but done in C#.

Since a delegate is just a reference to a method, you can even use lambda syntax to create delegates:

    StringModifier exclaim = x => x + "!!!";

(As with any other lambda function, you can do multiple parameters by putting the parameter section - before the `=>` - in parentheses and including a name for each parameter.)

### How are delegates used?

If you've done any JavaScript programming, you've probably seen *callbacks*. A callback is code that executes after some other task has finished running - for example, you might make a call to a web API service, and when the data from that service returns, you have some code that runs to process that data. This is typically done as a callback. 

Callbacks are one example where a C# program might use delegates. It's quite common to see delegates combined with `async` programming. Just like would be done in a JavaScript-based website frontend, a delegate could be used to process information that has come back from a Web request or similar call by updating information in an application user interface.

In your presentation, please cover:

- What is a C# delegate?
- Provide an example of the syntax for defining, assigning and using a delegate
- Compare C# delegates to another language (e.g. Python first-class functions or JavaScript callbacks)

Sources to get you started (but please do seek out and use other sources as well!):

- [Delegates](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/) in the C# Programming Guide.
- [Delegates](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/delegates) in the C# Language specification.
- [How to use a Delegate](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/how-to-declare-instantiate-and-use-a-delegate) at Microsoft.

## C# Multicast Delegates

In the last presentation, we looked at *delegates* - a strategy for treating C# methods as first-class functions. One feature of delegates that is somewhat unique to C# is that a delegate can actually point to *more than one* method at a time. In this scenario, you can call one delegate and have multiple methods execute at the same time. This is known as a *multicast* delegate.

One scenario where this is useful is if you are designing software that allows *plugins*. A common scenario would be that you would have multiple methods that might need to be called in order - one per plugin. You could simply add a reference to each plugin's method to the delegate, and then when the delegate is called, each plugin's method is called.

One caveat of multicast delegates is that they are not very useful when you are working with delegates that return values. The reason is that only the *last* value will be returned, and there is no in-built way to pass the output of one delegate call into the next. This does limit the usefulness of multicast delegates to a certain degree; they are typically only used for methods that perform *actions* rather than ones that process and return values.

To setup a multicast delegate, you can simply add methods to an existing delegate. For example:

    public static void PrintString(string str) 
    {
        Console.WriteLine(str + " (string)");
    }

    public static void ExclaimString(string str)
    {
        Console.WriteLine(str + "!!!");
    }

    public delegate void ConsolePrintString(string str);

    public static void Main()
    {
        // create the delegate with the first method
        ConsolePrintString delegate = PrintString;

        // add the second method to the delegate, making it a multicast delegate.
        delegate += ExclaimString;

        // run the delegate
        delegate("Hello World!");
        // will print two lines:
        //   Hello World! (string)
        //   Hello World!!!!
    }

You can get direct access to the list of methods referenced by a delegate by using its `InvocationList` property. It contains a fixed-size array of `Delegate` objects (all delegates are subclassed from `Delegate`). You can also directly invoke the delegate itself using the `Invoke` method - this is useful if you are doing a complex scenario involving chaining delegates.

In your presentation, please cover:

- What is a multicast delegate?
- Explain the limitations - most notably that you can't return values other than the final delegate's value
- A very quick description of some of the properties on a delegate object.

Sources to get you started (but please do seek out and use other sources as well!):

- [Multicast delegates](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/how-to-combine-delegates-multicast-delegates) at Microsoft.
- [Multicast Delegates with Examples](https://dotnettutorials.net/lesson/multicast-delegate-csharp/) at DotNetTutorials.

## Source Control - Basics

You've probably seen and used source control already - the most common system for doing so is Git. You've probably even done a clone, a commit, a pull and a push. You might even have had to do a merge. But what's actually going on with source control? And why bother?

Source control is arguably one of the greatest advances in programming, because its design allows for safe storage of code and a clean, accurate history of a project's entire codebase. Git was originally conceived and designed by Linus Torvalds, the man who created the Linux kernel. One of the largest Git repositories out there (at least when it comes to amount of *code*) is the Linux kernel source tree. Its main branch alone contains over 1.1 million commits, and the entire Git repository data contains over 4GB of compressed source code. (That's because it contains the history of all development on the Linux kernel going back to )

The odds that you'll work on a repository that large are pretty small (unless you're a Linux kernel developer!), but Git has become a de-facto industry standard for source code management and control. Its ubiquity has earned it a place within many IDEs, and many hosted Git services are available along with software you can install on your own server to host a Git service of your own. 

Let's do a quick review of the things that make up the core workflow of Git:

- You typically start by *cloning* a Git repository (unless you're making a new one from scratch). This downloads the source code database to your computer and sets up the directory to contain the contents of the latest commit on the default branch.
- If you're planning to work on the code, you'll then typically create a *branch*. A branch is an independent thread of source code progress. When you make a branch, you can assume you have full privileges to put code onto commits on your branch.
- You then make changes to the code.
- When you want to save the changes to your code into the Git database, you make a *commit*. A commit is a snapshot of the source code in its current state. It is identified by its cryptographic hash, and contains a commit *message* (including your name and E-mail address) as well as a `diff` dump comparing the contents of the branch prior to and after your commit. 
  - The `diff` tool is a Unix tool that produces files that list the differences between two sets of files. A `diff` is a somewhat compact representation of only the changes of one or more files. It is able to determine where lines were added, deleted, or changed. A `diff` report file is itself a plain text file, and it is essentially what a Git *commit* consists of.
- When you have made one or more commits, you can *push* those commits up to the original server you cloned your repository from (such as Github). Pushing the commits to the remote server stores them "permanently" on that server. 
- You can make as many branches as you want, and many of your branches may never be *merged* into a higher branch. However, when you have a branch of code that you do want to be taken up by the project, you will first push the commits you want onto a branch, and then you make a *pull request* for your branch. A pull request is just that - a *request* that a project manager *pull* your changes into a higher branch. 
  - Some Git systems refer to pull requests as *merge requests*, which is arguably a more accurate description - you are asking that your branch be *merged* into an upper branch. Merging is the process of creating a commit that represents the changes between *your* branch and *another* branch. Depending on the circumstances, this may be a completely automatic process, or it may result in a *merge conflict* where you need to manually review the source code and determine what the actual results should be.
- Rinse and repeat!

### Merge conflicts

A *merge conflict* occurs when you want to merge your branch of code onto another branch, but there have been commits on *both* branches that make changes to the *same* files (or sections of files). 

If there is one commit on each branch, but each commit changes *different* files, the files will just be "merged" into the new commit. Even if the same file is modified, the algorithm that is used for computing the merge may be able to *automatically merge* the file. However, suppose that both commits changed the same *line* of code - in this case, a merge conflict will occur, since the Git algorithm has no way to know which modification is the "right" one. 

Merge conflicts show up in your source files as blocks that indicate the commits that are conflicting and contains a copy of that section of code from each file. It is then up to you to manually edit the source file and resolve the conflict. Once you have done so, you do a traditional commit to finish the "merge". (Or, you can use `git reset` if you want to abort merging.)

The next presentation will cover a few more advanced scenarios with Git, so consider this presentation a review of what you probably already know about Git. (And if you are unfamiliar with it, this is definitely something worth learning - you will almost certainly see Git used in any programming or development scenario you come across!)

In your presentation, please cover:

- What is source control (in general)?
- What is the `git` source control system?
- Provide a very basic workflow for how to work with a project that uses `git`. This includes cloning (or creating) the project, adding files, and committing the files. If the repository is remote, it also involves pushing the code.
- Take a very brief look at one other source code system, such as Mercurial or CVS. Compare and contrast that system with `git`.

Sources to get you started (but please do seek out and use other sources as well!):

## Source Control - Advanced

Since `git` is the de-facto standard for source control, and it's most likely the source control system you will encounter in the industry, in open source projects and so on, we'll focus on `git` from now on.

The very basics of `git` are not too difficult to understand, and you've probably already used it for these purposes as we've said. The notion of *committing* your code (and then *pushing* those changes to the server) is the most common workflow - by committing your code, you are storing a snapshot of the code at that instant in time. The beauty of this is that you can refer back to that snapshot at any time in the future - even months or years into the future, and even thousands or *millions* of commits later. That commit you made effectively freezes the code in time at that point, and since commits are intended to be immutable, that code will forever exist as long as the repository does. (With some exceptions...read on.)

### Rebasing

The Git *rebase* is a tool that can seem quite confusing at first, because its use case is unique. Essentially, the rebase command lets you "restart" your branch from a different place. 

Let's back up a bit. Remember that in Git, branches are threads of commits. A branch starts at a certain point and diverges on its own from there. You can edit the branch's code and make commits on the branch, but those commits exist independently of any other branch. In essence, you can have as many "versions" of the code as you have branches. You have possibly worked on a project where every developer started by making a branch off of the main branch and working from there. If you each simply worked on your branches, you'd end up with a version of the code for each developer. But of course you ultimately need to bring all this code together on a main branch.

The typical way of doing this is to do a *merge*. A merge essentially compares two branches and generates a commit that will take the changes from both branches and *merge* them together. If branch A has some changes and branch B has some changes, merging branch B into branch A means that branch A will now have its changes *as well* as branch B's changes. This is typically how you bring everyone's work together into one place.

Of course, you have also likely encountered the dreaded *merge conflict*. This is actually somewhat common if you're working on an active project. You do a bunch of work on your branch and you're ready to do a *pull request* to have your work merged into the main branch. However, in the meantime, other people have also done work that has been pulled into the main branch. When you try to merge your code into main, suddenly you get merge conflicts all over the place - because other people also made changes to the same code you made changes to!

A *rebase* is a way to not only help alleviate this problem, but to also keep the history log *linear*. A complex history of branches, merges, more branches and more merges and conflict resolutions can quickly become hard to track. Especially when you're looking to find where a bug was introduced, having a complex commit history can make the task especially daunting. 

Conceptually, a git rebase simply means that you change the *starting point* of your branch, and then work your way back to where you are now in the code. Note that we said that each branch starts at some specific commit - so let's say that your branch started on the main branch at commit 12345. You made some changes and made commit 12346 and 12347. But in the meantime someone else made changes to main that produced commit 12348. When you try to merge, you have merge conflicts with commit 12348.

Rather than dealing with the conflicts, you could instead *rebase* your branch so that its *starting* point is now commit 12348. In effect, it's as if you made the branch *after* commit 12348 was put onto main. Git will then walk through your commits and apply them, one by one, to the code from 12348. You also get the chance to deal with any potential merge conflicts on your own, without "bugging" the project maintainers or embarrassing yourself with a huge merge conflict on the main branch. 

Once you have rebased, you can simply merge with main - and since your commits all are *based* on the current commit of main (and not the older one you initially started with), your merge becomes a simple "fast-forward" merge, and no merge commit even needs to be made - your commits are applied as-is to the main branch! In effect, rebasing moves the effort of merging the code from the main branch to your own branch. 

There are two rebase strategies - the *interactive* strategy and the *automatic* strategy. The automatic strategy simply restarts your branch at the new commit and then walks forward all your commits. The *interactive* model gives you the chance to "rewrite history" as it were. You can cherry-pick *which* of your commits get applied to the new base commit, and even change the commit messages, among other options.

There are a few caveats when it comes to rebasing - the main one being that it can be challenging to rebase on a branch after you have pushed it. This effectively creates a scenario where "history is rewritten". (In essence, that's what rebasing is doing, but as long as it's all in your own machine and not on the server, it's no big deal... but as soon as you ask the *server* to change history, you run into possible problems. Some servers may simply refuse to let you do this at all, others may choke on the push and get confused.)

### Git Large File Storage

The other advanced Git topic we'll cover in this section is large file storage. Git LFS is a plugin for Git that allows you to manage the storage of large files in your repository. If you've ever worked with a Git repository where someone committed a huge file (say, a Zip file, video file, etc.), you will notice that this file will pretty much get stuck in the repository's history, even if you try to delete it - remember Git history is intended to be immutable. Additionally, Git's change tracking algorithms are optimized for text content; large binary files simply waste time trying to analyze humongous files for changes, and even if those changes do occur, they can't be efficiently stored in a text-based format.



- [Git Rebase tutorial](https://www.atlassian.com/git/tutorials/rewriting-history/git-rebase) at Atlassian.

## Continuous Integration and Deployment

One of the great things about computers is that they can automate many tedious tasks that we otherwise would have to do by hand. Automation is all over, and a big part of being an "efficient" programmer as well as computer user is learning how to make the computer do most of the work for you. A joke about IT workers suggests that the best IT workers are the ones who do the least work - because they've automated so much of their workflow that the computers basically run themselves, and they only need to be around for edge cases and emergencies.

While there's plenty of jokes to tell (and arguments to be made with management) about why people who automate things are still good at their jobs and not just "lazy", there's other reasons for it as well. Automating things means there is much less chance for simple user errors. Ever accidentally deleted the wrong file on your computer? Or perhaps typed an entire paper and realized you hit "N" when closing the program instead of "Y" when it asked you to save? Automation can significantly reduce or even eliminate certain kinds of errors like these.

Continuous Integration, or CI, describes the practice of using software to automate the process that must occur after a new piece of code has been written. A CI pipeline or script might contain any number of tasks and steps, but a typical CI pipeline for a program might look like this:

    1. Download the latest code that was just written.
    2. Run code analysis on the code, looking for obvious errors or subtle bugs that can be detected with analysis.
    3. Build the code, checking for build errors and/or warnings.
    4. Run tests on the code.
    5. If steps 2-4 completed successfully, package the code for distribution. This could be zipping the final build, compiling an installer program, or producing a package for deployment to a server.

Continuous deployment is typically viewed as distinct from continuous integration, although it is usually part of the same process, and could often be viewed as a step in a CI pipeline. Continuous deployment refers to "deploying" the code automatically. This might mean pushing the code onto a web server or publishing an installer package to a web server or online app store, and might include steps such as digitally signing the code.

CI/CD systems are often integrated with source code management systems such as Git. Hosted Git services such as Github offer CI/CD services as part of their system, and there are also CI/CD solutions for code you manage and host on your own servers. A CI/CD system is based on common job control theory; usually, you will run an "agent" or "runner" on one or more "build hosts", which are the computers or servers responsible for actually building the code. These agents will retrieve work from the source code management system as it is made available. (Interestingly, you can model it on the same model we used to describe how to do parallel programming - imagine if each build host were a "thread" and each CI/CD pipeline job was a "task"!)

Part of the reason CI/CD is often separated is that a CD host often needs higher privileges than a build host. A build host could run into any number of unusual situations, the worst of which could involve buggy code creating security vulnerabilities. For this reason, a build host's job is typically to produce *artifacts* that are then delivered to the *deployment host*. You usually can get by with far fewer deployment hosts than build hosts (since you may not always deploy your code on each build), and only the deployment hosts contain the necessary credentials to actually publish code to your live server. These hosts require the utmost in security and are typically fully isolated from the build hosts. (In a small setting however, you could run the build host and deployment host on the same system - you just have to accept the risks of the possibility that some rogue code ends up on the build host and "spills over" onto the deployment host, potentially infecting your deployment.)

I have actually been using some CI/CD techniques for parts of the grading for this course! When you submit your code to D2L, I run a script that extracts your code and looks for the solution file and/or `.cs` files. These files are then all pushed into a private Git repository hosted on my own system. Once the code is there for each student, a CI/CD task runs which, among other things, builds your code and tries some test runs, and also does some static analysis and performs some checks to look for things like extremely similar submissions (potential plagiarism). The result that comes out of this for me is a nice report of your submissions and any potential issues identified. Of course, I do manually review your code myself as well, but CI/CD helps with the process of some of the more mundane aspects of assignment grading! (Note that I had to write code that handles all of the above - not just the CI/CD pipelines, but the programs that do things like compare submissions and run typical scenarios!)

One thing to keep in mind is that CI/CD systems are literally just running scripts or sets of commands. Those commands can be anything that can run on its own without the need for direct user intervention. CI/CD can be used beyond normal software development workflows. For example, assuming you had code and/or scripts to do so, you could use CI/CD to:

- Take your book manuscript and automatically typeset it into a PDF file for publication
- Convert LaTeX documents into formatted papers
- Run spell checks, grammar checks, style checks and more on your college paper
- Convert a set of images into various formats for various purposes
- Retrain an AI model every time you update the training data
- Build an operating system image for your Linux project device
- Mixdown an audio project to a final mix and produce multiple file formats of the output
- Send you a text message or an E-mail along with some details any time a new commit is made to Git
- And anything else you can imagine scripting that would occur when a "Build" is triggered!

There is no code or other demos needed for this project, but for your presentation, it would be a good idea to look into at least one CI/CD system to get a few more details. 

In your presentation, please cover:

- What is CI/CD?
- How is CI/CD helpful in the software development process?
- What other interesting ways can you think of that CI/CD could be used for outside of "typical" software development?
- Provide a quick brief description of at least one CI/CD system.

Sources to get you started (but please do seek out and use other sources as well!):
