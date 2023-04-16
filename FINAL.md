# CS 403 - Final Exam

The final exam for this course is intended to give you a chance to demonstrate your mastery of core C# concepts as well as an understanding of the programming constructs we have explored and studied. 

> **TL;dr**
>
> * The final project is both a quiz and a programming project. 
> * The **quiz** is on D2L and covers questions from previous quizzes as well as new questions. Any of the content we have covered in the course is fair game. 
>   * The quiz is open-book, open-notes. You may use Internet resources for the quiz. However, you may **not** ask fellow classmates for answers or work together on the quiz, nor may you directly ask anyone for the answer for a question.
> * The **programming project** is open-ended and you will make a **proposal** regarding what you want to develop for your final. You must submit this project proposal on D2L **by Wednesday, April 26th at 11:59 PM**. 
>   * If you do not submit a project proposal **I will assign you a project.**
> * The final code for your project must be submitted to D2L by the final due date of **Thursday, May 4th at 11:59 PM.** *(May the 4th be with you!)* 
>   * I cannot accept late final project submissions without prior arrangements. If you have not communicated with me and have not submitted the final by the due date **you will receive zero points for the final programming project.**
> * Your final program must be **functional**. It must compile and run successfully as submitted. Programs that do not compile or run as submitted will receive substantial loss of points. Your program must also meet the requirements we agree on in your proposal - a program not meeting the requirements we set forth will lose points.
>
> Please keep reading for details.

Since the overarching goal for the course is more about learning *how* programming languages work and less about the *specifics* of a programming language, I encourage you to use reference materials for C# freely - when I grade your final code, I will be looking for an understanding of concepts such as program flow, exception handling, data querying and so on, with less emphasis on "proper C# form". In other words, you won't lose points if you didn't write your code "the right way in C#", as long as your code is *functional*. Focus on understanding the *intent* of your code less than the actual syntax of C# - you can use references for that as you need to. 

(Don't take this mean that you may submit "sloppy" code. Code should still compile cleanly and include exception handling and user validation. By "the right way in C#", I mean that you will not lose points if, for example, you do not use the "accepted" C# method naming conventions or you use a "less accepted" C# construct compared to a "better" one. Your program *must* still be fully functional, and you should take time to make your code robust and efficient.)

The final exam consists of three components:

- A **quiz** taken on D2L. The quiz will contain some questions from previous quizzes as well as new questions. You should study your notes and materials, but the quiz is open-book and open-notes - the only restriction is that **you may not ask others for help on the questions or to give you the answers** - everyone must *independently* take the quiz on their own.
- A **design document** for a simple C# program. [A template is available on D2L at this link](https://mnsu.learn.minnstate.edu/d2l/le/content/6192136/viewContent/60233331/View). The purpose of your program is quite open-ended. You can take some inspiration from some of the homework projects we've done (but you can't use the homework as your final!), or you can try something totally new if you're up for the challenge. See the rest of this document for information on the items your program must include. Your design document must include a full description of the program you intend to write, the functionality it will have and the ways the program will demonstrate your understanding of the programming concepts we discussed in this course.
  - I will open a D2L dropbox for you to submit your proposal for a final project and I will either approve it or suggest additions/changes. The due date on the dropbox will be **Wednesday April 26th** at 11:59 PM. If you have not submitted a project proposal by this time **I will assign a project to you**!
  - I will respond to all project plan submissions within 24 hours. I will either accept your proposal as-is, or I will add or modify the requirements you set forth. In either case, you can begin working on your code whenever you're ready. However, I suggest that you submit your proposal and get approval *before* you start investing a lot of time into writing your code, in case we need to make changes to your project. Submit your plan ASAP!
- The **C# project** for your program. Your code should be functional and run as submitted. 

## Project Requirements

Since the final programming project is intended to give you a chance to demonstrate your mastery of some programming theory and your experience with the C# language, your program must incorporate the following aspects in some way:

* Reading user input and parsing it into different types (such as numeric types). *Don't forget exception handling!*
* Debugging statements (e.g. `Debug.WriteLine`).
* Querying collections of user data. (This could also mean using LINQ to work with collections of in-app data, such as game objects.)
* Separating code that performs calculations and logic from user interface code (with a class library)
* Providing the user multiple functions to perform with the data. (This is loosely defined - most games would qualify by design since the user must make decisions during play.)
* Proper error handling (e.g. data validation) and exception catching. (At a minimum, you must catch invalid input from the user and react appropriately.)
* If your application uses a dataset, such as a product catalog, list of statistics, etc. that data must be stored externally, such as in a CSV file, and must be loaded in via reading the file. You may not put large amounts of data in C# enumerable initializers. (Use common sense with this - a small list of numbers is fine, such as needed values for some game logic, but a list of complex data that you will be querying should be stored separately.)
* Use of classes, interfaces, properties, etc. to logically and cleanly organize objects in code. As appropriate, use abstract classes, subclasses and interface inheritance.
* Use of XML code comments on your methods, classes, etc. 

Your program can interact with users at the console, but if you wish you can explore writing a graphical user interface application (using frameworks such as Windows Forms or WPF). I encourage you to experiment and explore on your own - think about your goals and interests as a programmer and try new things! 

### Some ideas if you can't think of a project

Here are a few ideas that might help you think about what project you might like to write:

* A simple game that can be interacted with via command line. (For example, many card games can be easily adapted to a simple command-line application, and you also have some obvious entities to get started with for your classes, such as `Card`, `Deck`, etc.)
* A gradebook program - calculating student grades based on assignments.
* A program to quiz students on a topic of interest.
* A program to calculate sports statistics and perform simulations based on user-input scenarios.

## Grading Rubric

| Item | Percent |
| ---- | ---- |
| **Functionality** - Program compiles and runs as submitted without any errors. | 20% |
| **Completeness** - Program includes all of the functionality as agreed in the proposal. | 30% |
| **Error handling** - The program does not crash even if provided invalid input or unexpected behavior occurs. | 20% |
| **Organization** - Program appropriately uses classes, interfaces, etc. to organize functionality. Program separates program logic from user interface elements, e.g. by using a class library or at minimum by separating code into different classes. | 15% |
| **Comments** - Program includes appropriate XML comments. | 15% |

## Notes

*You cannot use the scenario of a convenience store checkout program for your final*, since we used that scenario for many of our homework assignments. Be creative - come up with your own ideas! If you are struggling, use some of the listed ideas above to help you brainstorm. If you *really* are unsure of what to do, contact me and I'll help you with some ideas. Again, if you have not submitted any proposal to me and you have not contacted me by 11:59 PM on April 26th **I will assign you a project**!

While you have till April 26th to propose your project, I advise you to start thinking immediately about your final - you will have more time to work on your code the earlier you submit (and I approve) your proposal. 

Your program does not need to be **complex**, but it needs to be **complete**. For example, if you write a game, you don't need to write an artificial intelligence computer player, but you do need to have a way for two players to play the game if it is a two-player game. You don't need to design a database to hold data, but you do need to at a minimum store data in an external file (like what we did in Week 4) rather than hard-coding datasets into C# code.

You must submit your final exam program by **Thursday, May 4th** at **11:59 PM**. I cannot accept late final submissions as I need to submit your grades to the university. 

**If you have extenuating circumstances that may require you to be late on the final, you** ***must*** **talk to me** ***ahead of time*** **so that we can arrange an incomplete if necessary.** If you have not spoken with me ahead of time and have not submitted your final by 11:59 PM on May 4th **you will receive zero points for the final**. This will adversely affect your final grade for the course!

**Good Luck!!**
